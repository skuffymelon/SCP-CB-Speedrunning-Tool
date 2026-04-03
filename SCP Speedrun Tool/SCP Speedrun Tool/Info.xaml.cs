using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SCP_Speedrun_Tool
{
    /// <summary>
    /// Interaction logic for Info.xaml
    /// </summary>
    public partial class Info : Window
    {
        SettingInit settings;

        Settings set = new Settings();
        MainWindow main = null;

        public Info(MainWindow w)
        {
            InitializeComponent();
            BuhAppear();
            ScpFlicker();
            settings = set.ReadSet();
            SetSettings();
            main = w;

            Closing += CancelClosure;
        }

        private void CancelClosure(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            main.InfoButton(null,null);
        }

        private void BuhAppear()
        {
            if (new Random().Next(100) == 1)
            {
                buh.Visibility = Visibility.Visible;
            }
        }
        public void SetSettings()
        {
            gencopyseed.IsChecked = settings.copygenseeds;
            gengenerate.IsChecked = settings.genseedctrl;
            gencmaynard.IsChecked = settings.copycalccode;
            rankedmode.IsChecked = settings.rankmode;
            rprecache.IsChecked = settings.cacheseeds;
            serverrun.IsChecked = settings.serverrun;
            cheats.IsChecked = settings.cheats;
            if (rankedmode.IsChecked == false)
            {
                rdisable.Opacity = 0.5;
                rprecache.IsEnabled = false;
                rprecache.IsChecked = false;
            }
            gencopyseed.Click += SettingChange;
            gengenerate.Click += SettingChange;
            gencmaynard.Click += SettingChange;
            rankedmode.Click += SettingChange;
            rprecache.Click += SettingChange;
            serverrun.Click += SettingChange;
            cheats.Click += SettingChange;
        }

        private void SettingChange(object sender, RoutedEventArgs e)
        {
            CheckBox ch = sender as CheckBox;
            switch (ch.Name)
            {
                case "gencopyseed":
                    settings.copygenseeds = (bool)ch.IsChecked;
                    break;
                case "gengenerate":
                    settings.genseedctrl = (bool)ch.IsChecked;
                    break;
                case "gencmaynard":
                    settings.copycalccode = (bool)ch.IsChecked;
                    break;
                case "rankedmode":
                    settings.rankmode = (bool)ch.IsChecked;
                    if (ch.IsChecked == true)
                    {
                        rdisable.Opacity = 1;
                        rprecache.IsEnabled = true;
                        rprecache.IsChecked = true;
                        settings.cacheseeds = true;
                    }
                    else
                    {
                        rdisable.Opacity = 0.5;
                        rprecache.IsEnabled = false;
                        rprecache.IsChecked = false;
                        settings.cacheseeds = false;
                    }
                    break;
                case "rprecache":
                    settings.cacheseeds = (bool)ch.IsChecked;
                    break;
                case "serverrun":
                    settings.serverrun = (bool)ch.IsChecked;
                    break;
                case "cheats":
                    settings.cheats = (bool)ch.IsChecked;
                    break;
            }

            set.SaveChanges(settings);
            main.MakeSettings();
        }

        private async void ScpFlicker()
        {
            Random rnd = new Random();

            await Task.Delay(rnd.Next(5000));

            scp.Opacity = 0;
            await Task.Delay(30);
            scp.Opacity = 1;
            await Task.Delay(30);
            scp.Opacity = 0.25;
            await Task.Delay(30);
            scp.Opacity = 0.66;
            await Task.Delay(30);
            scp.Opacity = 0.88;
            await Task.Delay(30);
            scp.Opacity = 1;

            ScpFlicker();
        }

        private void social(object sender, RoutedEventArgs e)
        {
            string name = ((Grid)(((StackPanel)((Button)sender).Parent).Parent)).Children[0].Uid;

            string type = ((Button)sender).Uid;

            SocialCall(name, type);
        }

        public void SocialCall(string name, string type)
        {
            switch (type)
            {
                case "yt":
                    if (name == "sooslick_art")
                    {
                        Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = "chrome.exe", Arguments = $"https://youtube.com/@ya_soos" });
                        break;
                    }
                    Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = "chrome.exe", Arguments = $"https://youtube.com/@{name}" });
                    break;
                case "tw":
                    Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = "chrome.exe", Arguments = $"https://twitch.com/{name}" });
                    break;
                case "git":
                    if (name == "sooslick_art")
                    {
                        Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = "chrome.exe", Arguments = $"https://github.com/sooslick" });
                        break;
                    }
                    Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = "chrome.exe", Arguments = $"https://github.com/{name}" });
                    break;
                case "cus":
                    if (name == "sooslick_art")
                        Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = "chrome.exe", Arguments = $"https://sooslick.art" });
                    break;
                case "sp":
                    if (name == "sooslick_art")
                    {
                        Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = "chrome.exe", Arguments = $"https://www.speedrun.com/users/sooslick" });
                        break;
                    }
                    else if (name == "forxandknives")
                    {
                        Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = "chrome.exe", Arguments = $"https://www.speedrun.com/users/Forx" });
                        break;
                    }
                        Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = "chrome.exe", Arguments = $"https://www.speedrun.com/users/{name}" });
                    break;
                case "d":
                    Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = "chrome.exe", Arguments = $"https://discord.com/invite/gwUWtWcB6Y" });
                    break;
            }
        }

        private void helpbut_Click(object sender, RoutedEventArgs e)
        {
            if (HelpMenu.Visibility == Visibility.Collapsed)
            {
                HelpMenu.Visibility = Visibility.Visible;
                helpbut.Content = "HELP /\\";
            }
            else
            {
                HelpMenu.Visibility = Visibility.Collapsed;
                helpbut.Content = "HELP \\/";
            }
        }
    }
}
