using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SCP_Speedrun_Tool
{
    /// <summary>
    /// Interaction logic for AchievementOverlay.xaml
    /// </summary>
    public partial class AchievementOverlay : Window
    {
        SCPCB cb;

        List<string> template = ["008", "012", "035", "049", "055", "079", "096", "106", "148", "205", "294", "372", "420-J", "427", "500", "513", "714", "789", "860", "895", "914", "939", "966", "970", "1025", "1048", "1123", "MAYNARD", "HARP", "S-NAV", "OMNI", "CONSOLE", "SHELL SHOCKED", "NO MANS LAND", "1162", "1499", "KETER"];

        List<string> achievementstoget = [ "008", "012", "035", "049", "055", "079", "096", "106", "148", "205", "294", "372", "420-J", "427", "500", "513", "714", "789", "860", "895", "914", "939", "966", "970", "1025", "1048", "1123", "MAYNARD", "HARP", "S-NAV", "OMNI", "CONSOLE", "SHELL SHOCKED", "NO MANS LAND", "1162", "1499", "KETER" ];

        List<Border> cheatBorders = new List<Border>();

        List<TextBlock> RunStatistics = new List<TextBlock>();

        public SCPCBMenu menu;

        public int AchievementCount;

        public bool isCheated = false;

        public bool scrollon = false;

        public bool isRunOver = false;

        public AchievementOverlay(SCPCB cbdata)
        {
            InitializeComponent();

            foreach (TextBlock tb in runstats.Children)
            {
                RunStatistics.Add(tb);
            }

            cb = cbdata;

            CheckForEndOfRun();

            UpdateVals();

            Closing += ClosingCancel;
        }

        private void ClosingCancel(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            menu.OverlayShowHotkey();
        }

        /*
        public async void ScrollText()
        {
            //achtaken.Text = String.Join(" - ", achievementstoget);
            achtaken.Text = "";
            achtaken.Text = String.Join(" - ", achievementstoget);
            await Task.Delay(2000);
            achtaken.Text = "";
            //for (int i = 0; i < achievementstoget.Count; i++)
            //{
            //    
            //}
            await Task.Delay(1000);
            if (!scrollon)
            {
                achtaken.Margin = new Thickness(5, 19, 0, 0);
                ScrollText();
            }
        }
        */
        public async void UpdateVals()
        {
            int acount = 0;

            //achtaken.Text = "";

            for (int i = 0; i < achvindex.Children.Count; i++)
            {
                if (cb.SingleAchievements[i] == 1)
                {
                    achvindex.Children[i].Opacity = 1;
                    acount++;
                }
                else
                    achvindex.Children[i].Opacity = 0.3;
            }

            CheatedRun(isCheated);

            /*
            if (cb.PlayerPosZ < 95 && cb.PlayerPosZ > 94)
            {
                achtaken.Text = "ENTERED/EXITED HCZ";
            }

            if (cb.PlayerPosZ < 48 && cb.PlayerPosZ > 47)
            {
                achtaken.Text = "ENTERED/EXITED EZ";
            }
            */
            achcount.Text = $"ACHIEVEMENTS: {acount}/37";

            AchievementCount = acount;

            await Task.Delay(1000);

            //if (scrollon)
            //{
            //    scrollon = false;
            //    ScrollText();
            //}

            UpdateVals();
        }

        public async void CheckForEndOfRun()
        {
            await Task.Delay(1000);

            if (!isRunOver)
            {
                if (cb.RunFinished == 1)
                {
                    isRunOver = true;

                    achvindex.Visibility = Visibility.Hidden;

                    achievementbar.Visibility = Visibility.Hidden;

                    runstatsgrid.Visibility = Visibility.Visible;

                    char[] sruntimechar = cb.GameTime.ToString().ToCharArray();

                    string ms = "";

                    string sec = "";

                    for (int i = sruntimechar.Length - 3; i < sruntimechar.Length; i++)
                        ms += sruntimechar[i];

                    for (int i = 0; i < sruntimechar.Length - 3; i++)
                        sec += sruntimechar[i];

                    int intsec = int.Parse(sec) % 60;

                    int minute = int.Parse(sec) / 60;

                    sec = intsec.ToString();

                    if (sec.Length == 1)
                        sec = "0" + sec;
                    
                    RunStatistics[0].Text = $"RUN FINISHED IN:\n{minute}:{sec}:{ms}";

                    if (cb.Difficulty == 0x50)
                    {
                        RunStatistics[2].Text = $"DIFFICULTY:\nSAFE";
                        RunStatistics[2].Foreground = Brushes.LightGreen;
                    }
                    else if (cb.Difficulty == 0x8C)
                    {
                        RunStatistics[2].Text = $"DIFFICULTY:\nEUCLID";
                        RunStatistics[2].Foreground = Brushes.Orange;

                    }
                    else if (cb.Difficulty == 0xC8)
                    {
                        RunStatistics[2].Text = $"DIFFICULTY:\nKETER";
                        RunStatistics[2].Foreground = Brushes.Red;
                        AchievementCount++;
                    }
                    else
                    {
                        RunStatistics[2].Text = $"DIFFICULTY:\nCUSTOM";
                        RunStatistics[2].Foreground = Brushes.White;
                    }

                    if (isCheated)
                        RunStatistics[1].Text = $"ACHIEVEMENTS:\n{AchievementCount + 1}/37";
                    else
                        RunStatistics[1].Text = $"ACHIEVEMENTS:\n{AchievementCount + 2}/37";

                    if (isCheated)
                        RunStatistics[3].Height = 14;
                    else
                        RunStatistics[3].Height = 0;
                }
            }
            else
            {
                if (cb.RunStart == 0)
                {
                    isRunOver = false;

                    achvindex.Visibility = Visibility.Visible;

                    achievementbar.Visibility = Visibility.Visible;

                    runstatsgrid.Visibility = Visibility.Hidden;
                }
            }

            CheckForEndOfRun();
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void AddToCheatIndex(object sender, RoutedEventArgs e)
        {
            cheatBorders.Add((Border)sender);
        }

        public void CheatedRun(bool c)
        {
            if (c)
                foreach (Border b in cheatBorders)
                    b.BorderBrush = Brushes.Red;
            else
                foreach (Border b in cheatBorders)
                    b.BorderBrush = Brushes.White;
        }
    }
}
