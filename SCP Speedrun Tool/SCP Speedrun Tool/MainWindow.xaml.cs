using SCP_Speedrun_Tool.GlobalHotkeys;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shell;

namespace SCP_Speedrun_Tool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Functions Function = new Functions();

        Settings sett = new Settings();

        SCPCBMenu menu;

        public char[] vanillagenerator = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890@`!$^*".ToCharArray();

        string[] seeds = null;
        
        string lastseed = "";

        bool reset = false;

        Info infowindow = null;

        Process javaw = new Process();
        public int procid = 0;

        public bool mapopen = false;

        public bool menuopen = false;

        public MapPreview map = null;

        public bool loaded = false;

        bool infoopen = false;

        SettingInit set = new SettingInit();

        string maindir = Directory.GetCurrentDirectory();

        public MainWindow()
        {
            InitializeComponent();

            GlobalHotkey hotkeydetailedmap = new GlobalHotkey(ModifierKeys.Alt, Key.V, PasteSeed);

            HotkeysManager.AddHotkey(hotkeydetailedmap);

            Loaded += ChirsmasTerr;
            Loaded += MapInit;
            maycode.TextChanged += MaynardConversion;
            maincode.TextChanged += MaintenenceConversion;
            maycode.GotFocus += ClearText;
            maincode.GotFocus += ClearText;
            seed.TextChanged += SeedTC;
            rankseed.TextChanged += RankTC;
            Closed += AppShutdown;
        }

        public void MakeSettings()
        {
            set = sett.ReadSet();

            if (set.rankmode)
            {
                rankedseed.Visibility = Visibility.Visible;
            }
            else
            {
                rankedseed.Visibility = Visibility.Hidden;
            }

            if (set.genseedctrl)
            {
                seed.PreviewKeyDown += LoadEnter;
            }
            else
            {
                seed.PreviewKeyDown -= LoadEnter;
            }

            if (set.cacheseeds)
            {
                LoadRankedArray();
            }
            else
            {
                DeleteRankedArray();
            }

            if (set.serverrun)
            {
                StartJavaServer();
            }
            else
            {
                if (procid != 0)
                    KillJava();
            }

            if (set.cheats)
                menu.CheatsEnabled = true;
            else
                menu.CheatsEnabled = false;
        }

        private void MapInit(object sender, RoutedEventArgs e)
        {
            map = new MapPreview(this);
            infowindow = new Info(this);
            menu = new SCPCBMenu();

            menu.UpdateButton(scpmenuopen);

            menu.main = this;

            MakeSettings();

            HotkeysManager.SetupSystemHook();

            //map.Show();
            //map.Hide();
        }

        private void ChirsmasTerr(object sender, RoutedEventArgs e)
        {
            if (new Random().Next(100) == 2)
            {
                chirmasterr.Opacity = 0.3;
            }

            // Bird Day Gift 2 April 2026
            // V Mp  q nm      with an  y
        }

        private void StartJavaServer()
        {
            if (set.serverrun)
            {
                Directory.SetCurrentDirectory(@".\SCPCBMap");
                javaw = new Process() { StartInfo = new ProcessStartInfo() { FileName = @"scpcbmapServer.jar", UseShellExecute = true} };
                javaw.Start();
                procid = javaw.Id;
                Directory.SetCurrentDirectory(maindir);
            }
        }
        
        private void AppShutdown(object? sender, EventArgs e)
        {
            if (set.serverrun)
            {
                KillJava();
            }
            HotkeysManager.ShutdownSystemHook();
            Application.Current.Shutdown();
        }

        public void KillJava()
        {
            //javaw.Kill();
            new Process() { StartInfo = new ProcessStartInfo() { FileName = "powershell.exe", UseShellExecute = true, Arguments = "taskkill /im javaw.exe /f", WindowStyle = ProcessWindowStyle.Hidden } }.Start();
            procid = 0; 
        }

        private void RankTC(object sender, TextChangedEventArgs e)
        {
            if (rankseed.Text.Length < 1)
            {
                ranktool.Opacity = 0.3;
                rankedbut.Content = "GENERATE";
                rankedbut.BorderBrush = new SolidColorBrush { Color = Color.FromRgb(0, 255, 74) };
                rankseed.IsEnabled = false;
            }
        }

        private void SeedTC(object sender, TextChangedEventArgs e)
        {
            if (seed.Text.Length < 1)
            {
                seed.Opacity = 0.5;
                seedtool.Opacity = 0.4;
            }
            else
            {
                seed.Opacity = 1;
                seedtool.Opacity = 0;
            }
        }

        private void ClearText(object sender, RoutedEventArgs e)
        {
            maycode.Opacity = 0.5;
            maincode.Opacity = 0.5;
            maycode.Text = "";
            maincode.Text = "";
            maytool.Opacity = 0.4;
            maintool.Opacity = 0.4;
        }

        private void MaynardConversion(object sender, TextChangedEventArgs e)
        {
            if (maincode.IsFocused)
                return;

            if (maycode.Text.Length > 0)
            {
                maycode.Opacity = 1;
                maytool.Opacity = 0;
            }
            else
            {
                maycode.Opacity = 0.5;
                maytool.Opacity = 0.4;
                maincode.Text = "";
                maincode.Opacity = 0.5;
                maintool.Opacity = 0.4;
            }

            if (maycode.Text.Length == 4)
            {
                int code = new int();

                bool numbercheck = int.TryParse(maycode.Text, out code);
                if (!numbercheck)
                    return;

                string scode = Function.FunctionP(code).ToString();

                if (set.copycalccode)
                {
                    Clipboard.SetText(scode);
                }

                maincode.Text = scode;
                maincode.Opacity = 1;
                maintool.Opacity = 0;
            }

        }

        private void MaintenenceConversion(object sender, TextChangedEventArgs e)
        {
            if (maycode.IsFocused)
                return;

            if (maincode.Text.Length > 0)
            {
                maincode.Opacity = 1;
                maintool.Opacity = 0;
            }
            else
            {
                maytool.Opacity = 0.4;
                maincode.Opacity = 0.5;
                maycode.Text = "";
                maintool.Opacity = 0.4;
                maycode.Opacity = 0.5;
            }

            if (maincode.Text.Length == 4)
            {
                int code = new int();

                bool numbercheck = int.TryParse(maincode.Text, out code);
                if (!numbercheck)
                    return;

                string scode = Function.FunctionM(code).ToString();

                if (set.copycalccode)
                {
                    Clipboard.SetText(scode);
                }

                maycode.Text = scode;
                maycode.Opacity = 1;
                maytool.Opacity = 0;
            }

        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Minimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void CloseProgram(object sender, RoutedEventArgs e)
        {
            if (set.serverrun)
            {
                KillJava();
            }
            Application.Current.Shutdown();
        }

        private void SeedLoad(object sender, RoutedEventArgs e)
        {
            if (!mapopen)
            {
                mapopen = true;

                loaded = false;

                MapJSON mapjs = new MapJSON();

                bool intseed = false;

                Int64 speedrunseed = new Random().Next(2147483647);

                string vanillaseed = "";

                if (seed.Text.Length == 0)
                {
                    int sorv = new Random().Next(2);
                    if (sorv == 0)
                    {
                        intseed = true;
                    }
                    else if (sorv == 1)
                    {
                        int vanillalength = new Random().Next(15) + 1;
                        intseed = false;
                        for (int i = 0; i < vanillalength; i++)
                        {
                            vanillaseed += vanillagenerator[new Random().Next(vanillagenerator.Length)];
                        }
                    }
                }
                else if (seed.Text.Length <= 10)
                {
                    intseed = Int64.TryParse(seed.Text, out speedrunseed);
                    if (intseed)
                    {
                        if (speedrunseed > 2147483647)
                        {
                            speedrunseed = 2147483647;
                        }
                        else if (speedrunseed < 1)
                        {
                            speedrunseed = 1;
                        }
                        seed.Text = speedrunseed.ToString();
                    }
                    else
                    {
                        vanillaseed = seed.Text;
                    }
                }
                else if (seed.Text.Length > 10)
                {
                    intseed = Int64.TryParse(seed.Text, out speedrunseed);
                    if (intseed)
                    {
                        speedrunseed = 2147483647;
                        seed.Text = speedrunseed.ToString();
                    }
                    else
                        vanillaseed = seed.Text;
                }

                if (vanillaseed.Length > 15)
                {
                    vanillaseed = vanillaseed.Substring(0, 15);
                }

                vanillaseed = vanillaseed.Replace(" ", "%20"); //.Replace("%", "%25").Replace("#", "%23")



                if (intseed)
                {
                    if (set.copygenseeds)
                    {
                        Clipboard.SetText(speedrunseed.ToString());
                    }
                    mapjs.LoadJSON((int)speedrunseed, null, this);
                }
                else
                {
                    if (set.copygenseeds)
                    {
                        Clipboard.SetText(vanillaseed);
                    }
                    mapjs.LoadJSON(null, vanillaseed, this);
                }
                mapjs = null;
                map.Show();
                map.StartGeneration();
            }
            else
            {
                if (loaded)
                {
                    mapopen = false;
                    //GC.Collect();
                    map.Cleanse(null, null);
                    SeedLoad(sender, e);
                    //GC.Collect();
                }
            }
        }

        private void LoadEnter(object sender, KeyEventArgs e)
        {
            //MessageBox.Show((Keyboard.Modifiers == ModifierKeys.Control).ToString());
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.V)
            {
                //if (map != null)
                //    map.Close();
                //mapopen = false;
                seed.Text = Clipboard.GetText();
                SeedLoad(null, null);
                seed.Text = "";
            }

            if (e.Key == Key.Enter)
            {
                SeedLoad(null, null);
            }
        }

        public void PasteSeed()
        {
            seed.Text = Clipboard.GetText();
            SeedLoad(null, null);
            seed.Text = "";
        }

        private void SendRanked(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = "chrome.exe", Arguments = $"https://docs.google.com/forms/d/e/1FAIpQLSeYnGlprajrB3pESpkPTjXxKvDG2TT74uaCaIDDtCana0XZkA/viewform" });
        }

        public void LoadRankedArray()
        {
            StreamReader reader = new StreamReader(File.OpenRead(@"Assets\SEEDS.csv"));

            seeds = new string[361365];

            var seed = reader.ReadToEnd();
            seeds = seed.Split('\n');

            reader.Close();
            reader = null;
            GC.Collect();
        }

        public void DeleteRankedArray()
        {
            seeds = null;

            GC.Collect();
        }

        private void RankedGenerate(object sender, RoutedEventArgs e)
        {
            if (set.cacheseeds == false)
            {
                LoadRankedArray();
            }

            if (rankseed.Text == "")
            {
                rankpreview.IsEnabled = false;
                lastseed = "";

                int rand = new Random().Next(361685);

                rankseed.Text = seeds[rand + 1].Replace("\r", "");
                lastseed = rankseed.Text;
                Clipboard.SetText(rankseed.Text);
                rankseed.IsEnabled = true;
                ranktool.Opacity = 0;
                ((Button)sender).Content = "RESET";
                ((Button)sender).BorderBrush = Brushes.Red;
                if (set.cacheseeds == false)
                {
                    DeleteRankedArray();
                }
            }
            else
            {
                reset = true;
                rankpreview.IsEnabled = true;
                rankseed.Text = "";
                rankseed.IsEnabled = false;
                ranktool.Opacity = 0.3;
                ((Button)sender).Content = "GENERATE";
                ((Button)sender).BorderBrush = new SolidColorBrush { Color = Color.FromRgb(0, 255, 74) };
            }
        }

        public void InfoButton(object sender, RoutedEventArgs e)
        {
            WindowChrome chrome = WindowChrome.GetWindowChrome(this);
            if (!infoopen)
            {
                infoopen = true;

                infowindow.Top = Top;
                infowindow.Left = Left;
                infowindow.Left += Width;

                infowindow.Show();

                LocationChanged += MoveWin;

                infobutton.Opacity = 0.5;

                chrome.CornerRadius = new CornerRadius(16, 0, 0, 16);
            }
            else
            {
                infoopen = false;

                chrome.CornerRadius = new CornerRadius(16, 16, 16, 16);

                infowindow.Hide();

                infobutton.Opacity = 1;

                //infowindow = null;

                LocationChanged -= MoveWin;
            }
        }

        private void MoveWin(object? sender, EventArgs e)
        {
            infowindow.Top = Top;
            infowindow.Left = Left + Width;
        }

        private void LoadLastRanked(object sender, RoutedEventArgs e)
        {
            seed.Text = lastseed;
            SeedLoad(null, null);
            rankpreview.IsEnabled = false;
            lastseed = "";
            seed.Text = "";
        }

        private void suggestionslink(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = "chrome.exe", Arguments = $"https://forms.gle/aLYJcp7RpgPr4G1w9" });
        }

        private void UpdateProcesses(object sender, RoutedEventArgs e)
        {
            if (!menuopen)
            {
                menu.Show();
                menuopen = true;
            }
            else
            {
                menu.Hide();
                menuopen = false;
            }
        }

        private void QuiqChange(object sender, RoutedEventArgs e)
        {
            string name = ((Button)sender).Uid;

            switch (name)
            {
                case "ach":
                    menu.OverlayShowHotkey();
                    break;
                case "code":
                    menu.CodeOverlayShowHotkey();
                    break;
            }
        }
    }
}