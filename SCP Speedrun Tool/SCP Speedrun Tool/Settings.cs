using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

public class SettingInit
{
    public bool copygenseeds { get; set; }
    public bool genseedctrl { get; set; }
    public bool copycalccode { get; set; }
    public bool rankmode { get; set; }
    public bool cacheseeds { get; set; }
    public bool serverrun { get; set; }
    public bool cheats { get; set; }
}

namespace SCP_Speedrun_Tool
{
    class Settings
    {
        public SettingInit ReadSet()
        {
            SettingInit set = JsonSerializer.Deserialize<SettingInit>(File.ReadAllText(@"Assets\settings.json"));
            return set;
        }

        public void SaveChanges(SettingInit settingInit)
        {
            File.WriteAllText(@"Assets\settings.json", JsonSerializer.Serialize<SettingInit>(settingInit));
        }
        
        public void InitializeSettings()
        {
            SettingInit nya = new SettingInit();
            nya.copygenseeds = false;
            nya.genseedctrl = false;
            nya.rankmode = false;
            nya.cacheseeds = false;
            nya.copycalccode = false;
            nya.serverrun = false;
            nya.cheats = false;

            File.WriteAllText(@"Assets\settings.json", JsonSerializer.Serialize<SettingInit>(nya));
        }
    }
}
