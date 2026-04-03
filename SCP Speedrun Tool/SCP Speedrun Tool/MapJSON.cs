using System.IO;
using System.Net.Http;
using System.Text.Json;

namespace SCP_Speedrun_Tool
{
    public class mapobject
    {
        public string seedString { get; set; }
        public int seedValue { get; set; }
        public int state106 { get; set; }
        public int angle { get; set; }
        public string loadingScreen { get; set; }
        public List<rooms> rooms { get; set; }
    }

    public class rooms
    {
        public string name { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int angle { get; set; }
        public int dh { get; set; }
        public int dv { get; set; }
        public string en { get; set; }
        public string ek { get; set; }
        public string info { get; set; }
        public string overlaps { get; set; }
    }

    internal class MapJSON
    {
        public async void LoadJSON(int? seed, string seedstr, MainWindow win)
        {
            bool serverrunning = win.procid != 0;

            HttpClient hc = new HttpClient();
            Task<string> raw;
            if (seedstr == null && serverrunning)
                raw = hc.GetStringAsync($"http://localhost:1499/map?seed={seed}");
            else if (seed == null && serverrunning)
                raw = hc.GetStringAsync($"http://localhost:1499/map?prompt={seedstr}");
            else if (seedstr == null && !serverrunning)
                raw = hc.GetStringAsync($"https://sooslick.art/scpcbmap/map.php?seed={seed}");
            else
                raw = hc.GetStringAsync($"https://sooslick.art/scpcbmap/map.php?prompt={seedstr}");
            

            //raw = hc.GetStringAsync($"http://localhost:1499/map?seed={seed}"); 
            //raw = hc.GetStringAsync($"http://localhost:1499/map?prompt={seedstr}");
            //string webData = Encoding.UTF8.GetString(raw.Result);

            string webData = raw.Result;

            /*
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] raw = new byte[0];
            if (seedstr == null)
            {
                raw = wc.DownloadData($"https://sooslick.art/scpcbmap/map.php?seed={seed}");
            }
            else
                raw = wc.DownloadData($"https://sooslick.art/scpcbmap/map.php?prompt={seedstr}");
            */
            //string webData = Encoding.UTF8.GetString(raw);

            File.WriteAllText(@"Assets\serial.json", JsonSerializer.Serialize(JsonSerializer.Deserialize<mapobject>(webData)));

            //map = null;
            //serialized = null;
            //obj = null;
            //hc = null;
            //raw = null;
            //webData = null;
            //GC.Collect();
        }
    }
}
