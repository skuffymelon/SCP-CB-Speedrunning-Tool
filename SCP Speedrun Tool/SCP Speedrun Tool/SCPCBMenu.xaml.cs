using SCP_Speedrun_Tool.GlobalHotkeys;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SCP_Speedrun_Tool
{
    /// <summary>
    /// Interaction logic for SCPCBMenu.xaml
    /// </summary>
    public partial class SCPCBMenu : Window
    {
        public int UpdateDelta = 2000;

        public bool IsSCPActive = false;

        public bool isCheated = false;

        public bool CheatsEnabled = false;

        public int Command = 0;

        public MainWindow main;

        bool IsActive1 = false;

        bool IsActive2 = false;

        List<Border> cheatBorders = new List<Border>();

        SCPCB SCPCB = new SCPCB();

        AchievementOverlay ao;

        CodeOverlay co;

        public SCPCBMenu()
        {
            InitializeComponent();

            ao = new AchievementOverlay(SCPCB);

            ao.menu = this;

            co = new CodeOverlay();

            co.menu = this;

            Closing += CancelClosure;

            CheckIfActive();

            GlobalHotkey hotkeyoverlay = new GlobalHotkey(ModifierKeys.Alt, Key.Q, OverlayShowHotkey);

            GlobalHotkey hotkeyshowmap = new GlobalHotkey(ModifierKeys.Alt, Key.Z, ShowMapHotkey);

            GlobalHotkey hotkeyomni = new GlobalHotkey(ModifierKeys.Alt, Key.G, OmniHotkey);

            GlobalHotkey hotkeycodeoverlay = new GlobalHotkey(ModifierKeys.Alt, Key.T, CodeOverlayShowHotkey);

            GlobalHotkey hotkeye = new GlobalHotkey(ModifierKeys.Alt, Key.E, ECommand);

            GlobalHotkey hotkey1 = new GlobalHotkey(ModifierKeys.Alt, Key.D1, IHotkey1);

            GlobalHotkey hotkey2 = new GlobalHotkey(ModifierKeys.Alt, Key.D2, IHotkey2);

            GlobalHotkey hotkey3 = new GlobalHotkey(ModifierKeys.Alt, Key.D3, IHotkey3);

            GlobalHotkey hotkey4 = new GlobalHotkey(ModifierKeys.Alt, Key.D4, IHotkey4);

            HotkeysManager.AddHotkey(hotkeyoverlay);

            HotkeysManager.AddHotkey(hotkeycodeoverlay);

            HotkeysManager.AddHotkey(hotkeyshowmap);

            HotkeysManager.AddHotkey(hotkeyomni);

            HotkeysManager.AddHotkey(hotkeye);

            HotkeysManager.AddHotkey(hotkey1); HotkeysManager.AddHotkey(hotkey2); HotkeysManager.AddHotkey(hotkey3); HotkeysManager.AddHotkey(hotkey4);
        }

        private void CancelClosure(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            main.menuopen = false;
            Hide();
        }

        private void IHotkey2()
        {
            if (IsSCPActive && CheatsEnabled)
            {
                VAMemory adr = new VAMemory(SCPCB.procName);

                if (adr.ReadInt32((IntPtr)SCPCB.Cheats1 + 0x204) == 0)
                    adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0x204, 1);
                else
                    adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0x204, 0);
            }
        }

        private void IHotkey3()
        {
            if (IsSCPActive && CheatsEnabled)
            {
                VAMemory adr = new VAMemory(SCPCB.procName);

                if (adr.ReadInt32((IntPtr)SCPCB.Cheats1 + 0x7C4) == 0)
                    adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0x7C4, 1);
                else
                    adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0x7C4, 0);
            }
        }

        private void IHotkey4()
        {
            if (IsSCPActive && CheatsEnabled)
            {
                VAMemory adr = new VAMemory(SCPCB.procName);

                if (adr.ReadInt32((IntPtr)SCPCB.Cheats1 + 0xE78) == 0)
                    adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0xE78, 1);
                else
                    adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0xE78, 0);
            }
        }

        private void IHotkey1()
        {
            if (IsSCPActive && CheatsEnabled)
            {
                VAMemory adr = new VAMemory(SCPCB.procName);

                if (adr.ReadInt32((IntPtr)SCPCB.Cheats1 + 0x200) == 0)
                    adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0x200, 1);
                else
                    adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0x200, 0);
            }
        }

        private void OmniHotkey()
        {
            if(IsSCPActive && CheatsEnabled)
            {
                VAMemory adr = new VAMemory(SCPCB.procName);

                int adrchange = 0;

                if (om.IsChecked == true)
                { adrchange = 1; }
                else
                { adrchange = 0; }


                if (adrchange == 1)
                {
                    adrchange = 0;
                    om.IsChecked = false;
                }
                else if (adrchange == 0)
                {
                    adrchange = 1;
                    om.IsChecked = true;
                }
                adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0x6AC, adrchange);
            }
        }

        private void ShowMapHotkey()
        {
            if (IsSCPActive && CheatsEnabled)
            {
                VAMemory adr = new VAMemory(SCPCB.procName);

                int adrchange = 0;

                if (sm.IsChecked == true)
                    { adrchange = 1; }
                else
                    { adrchange = 0; }


                if (adrchange == 1)
                {
                    adrchange = 0;
                    sm.IsChecked = false;
                }
                else if (adrchange == 0)
                {
                    adrchange = 1;
                    sm.IsChecked = true;
                }
                    adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0x604, adrchange);
            }
        }

        private void ECommand()
        {
            if (IsSCPActive && CheatsEnabled)
            {
                VAMemory adr = new VAMemory(SCPCB.procName);

                int adrchange = adr.ReadInt32((IntPtr)SCPCB.Cheats1 + 0x200);

                if (adrchange == 0)
                {
                    adrchange = 1;
                    adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0x200, adrchange);
                    adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0x204, adrchange);
                    adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0x7c4, adrchange);
                    adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0xE78, adrchange);
                    adr.WriteFloat((IntPtr)SCPCB.Cheats1 + 0xA34, 1000f);
                }
                else
                {
                    adrchange = 0;
                    adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0x200, adrchange);
                    adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0x204, adrchange);
                    adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0x7c4, adrchange);
                    adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0xE78, adrchange);
                    adr.WriteFloat((IntPtr)SCPCB.Cheats1 + 0xA34, 6f);
                }
            }
        }

        private async void CheckIfActive()
        {
            SCPCB.UpdateValues();

            if (SCPCB.procrunning.Length > 0)
            {
                scpcbactive.Text = "SCP CB IS: ON.";
                workspace.IsHitTestVisible = true;
                scpcbactive.Foreground = Brushes.LightGreen;
                workspace.Opacity = 1;
                //UpdateDelta = 200; // user input
                if (!IsSCPActive)
                {
                    SCPCB.Initialize();
                    UpdateVals();
                }
                IsSCPActive = true;
            }
            else
            {
                ao.Hide();
                co.Hide();
                IsSCPActive = false;
                workspace.Opacity = 0.2;
                workspace.IsHitTestVisible = false;
                scpcbactive.Text = "SCP CB IS: OFF.";
                scpcbactive.Foreground = Brushes.Red;
                IsActive1 = false;
                isCheated = false;
                CheatedRun();
            }

            await Task.Delay(2000);

            CheckIfActive();
        }

        public async void UpdateButton(Button but)
        {
            if (IsSCPActive)
            {
                but.BorderBrush = Brushes.LightGreen;
            }
            else
            {
                but.BorderBrush = Brushes.Red;
            }

            await Task.Delay(1000);

            UpdateButton(but);
        }

        public async void UpdateVals()
        {
            SCPCB.UpdateValues();

            for (int i = 0; i < achvindex.Children.Count; i++)
            {
                if (SCPCB.SingleAchievements[i] == 1)
                    achvindex.Children[i].Opacity = 1;
                else
                    achvindex.Children[i].Opacity = 0.3;
            }
            // todo for better code, put the checkboxes in an array and make a foreach loop 
            if (SCPCB.InfStamina == 1 & !isCheated)
                { infs.IsChecked = true; isCheated = true; IsActive1 = false; }
            else if (SCPCB.InfStamina == 1)
                infs.IsChecked = true;
            else
                infs.IsChecked = false;

            if (SCPCB.Godmode == 1 & !isCheated)
                { gm.IsChecked = true; isCheated = true; IsActive1 = false; }
            else if (SCPCB.Godmode == 1)
                gm.IsChecked = true;
            else
                gm.IsChecked = false;

            if (SCPCB.Noclip == 1 & !isCheated)
                { nc.IsChecked = true; isCheated = true; IsActive1 = false; }
            else if (SCPCB.Noclip == 1)
                nc.IsChecked = true;
            else
                nc.IsChecked = false;

            if (SCPCB.NoTarget == 1 & !isCheated)
                { nt.IsChecked = true; isCheated = true; IsActive1 = false; }
            else if (SCPCB.NoTarget == 1)
                nt.IsChecked = true;
            else
                nt.IsChecked = false;

            if (SCPCB.ShowMap == 1 & !isCheated)
                { sm.IsChecked = true; isCheated = true; IsActive1 = false; }
            else if (SCPCB.ShowMap == 1)
                sm.IsChecked = true;
            else
                sm.IsChecked = false;

            if (SCPCB.Omni == 1 & !isCheated)
                { om.IsChecked = true; isCheated = true; IsActive1 = false; }
            else if (SCPCB.Omni == 1)
                om.IsChecked = true;
            else
                om.IsChecked = false;

            if (SCPCB.Command == 1 & !isCheated)
            {
                isCheated = true;
                IsActive1 = false;
            }

            if (SCPCB.RunStart == 0)
            {
                UpdateDelta = 2000;
                isCheated = false;
                IsActive1 = false;
            }   
            else
                UpdateDelta = 200;

            CheatedRun();

            if ((SCPCB.PlayerPosX < 400 && SCPCB.PlayerPosY < 400 && SCPCB.PlayerPosZ < 400) && (SCPCB.PlayerPosX > -100 && SCPCB.PlayerPosY > -100 && SCPCB.PlayerPosZ > -100)) // 
            {
                x.Text = $"X: {SCPCB.PlayerPosX}";
                y.Text = $"Y: {SCPCB.PlayerPosY}";
                z.Text = $"Z: {SCPCB.PlayerPosZ}";
            }
            else
            {
                x.Text = "X: [REDACTED]";
                y.Text = "Y: [REDACTED]";
                z.Text = "Z: [REDACTED]";
            }

            bl.Text = $"BLOODLOSS: {SCPCB.Bloodloss}";
            ij.Text = $"INJURIES: {SCPCB.Injuries}";

            await Task.Delay(UpdateDelta);

            //debug.Text = SCPCB.InfStamina.ToString() + " " + SCPCB.Godmode.ToString() + " " + SCPCB.Noclip.ToString() + " " + SCPCB.DebugText + " " + UpdateDelta.ToString();

            if (IsSCPActive)
            {
                UpdateVals();
            }
        }

        public async void CheatedRun()
        {
            if (!IsActive1)
            {
                if (isCheated)
                {
                    VAMemory adr = new VAMemory(SCPCB.procName);

                    foreach (Border b in cheatBorders)
                        b.BorderBrush = Brushes.Red;

                    if (!IsActive2)
                    {
                        adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0x60, 1);
                        IsActive2 = true;
                    }
                }
                else
                {
                    foreach (Border b in cheatBorders)
                        b.BorderBrush = Brushes.White;

                    foreach (Border b in achvindex.Children)
                        b.BorderBrush = Brushes.White;

                    foreach (Border b in ao.achvindex.Children)
                        b.BorderBrush = Brushes.White;

                    IsActive2 = false;
                }       
            }

            IsActive1 = true;

            ao.isCheated = isCheated;
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseProgram(object sender, RoutedEventArgs e)
        {
            Hide();
            main.menuopen = false;
        }

        private void Minimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ClickCheckBox(object sender, RoutedEventArgs e)
        {
            if (IsSCPActive && CheatsEnabled)
            {
                string name = ((CheckBox)sender).Name;

                VAMemory adr = new VAMemory(SCPCB.procName);

                int adrchange = 0;

                if (((CheckBox)sender).IsChecked == true)
                    adrchange = 1;
                else
                    adrchange = 0;


                switch (name)
                {
                    case "gm":
                        adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0x200, adrchange);
                        break;
                    case "nc":
                        adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0x204, adrchange);
                        break;
                    case "infs":
                        adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0x7c4, adrchange);
                        break;
                    case "nt":
                        adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0xE78, adrchange);
                        break;
                    case "sm":
                        adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0x604, adrchange);
                        break;
                    case "om":
                        adr.WriteInt32((IntPtr)SCPCB.Cheats1 + 0x6AC, adrchange);
                        break;
                }
            }
        }

        public void OverlayShowHotkey()
        {
            if (IsSCPActive)
            {
                Button bt = overlaybut;

                if (bt.Opacity == 1)
                {
                    ao.Show();
                    bt.Opacity = 0.3;
                }
                else if (bt.Opacity == 0.3)
                {
                    ao.Hide();
                    bt.Opacity = 1;
                }
            }
        }

        public void CodeOverlayShowHotkey()
        {
            if (IsSCPActive)
            {
                Button bt = codeoverlaybut;

                if (bt.Opacity == 1)
                {
                    co.Show();
                    bt.Opacity = 0.3;
                }
                else if (bt.Opacity == 0.3)
                {
                    co.Hide();
                    bt.Opacity = 1;
                }
            }
        }

        private void OverlayShow(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            string text = ((Button)sender).Content.ToString();

            if (text == "OVERLAY")
            {
                if (bt.Opacity == 1)
                {
                    ao.Show();
                    bt.Opacity = 0.3;
                }
                else if (bt.Opacity == 0.3)
                {
                    ao.Hide();
                    bt.Opacity = 1;
                }
            }
            else
            {
                if (bt.Opacity == 1)
                {
                    co.Show();
                    bt.Opacity = 0.3;
                    ao.scrollon = false;
                }
                else if (bt.Opacity == 0.3)
                {
                    co.Hide();
                    bt.Opacity = 1;
                }
            }
        }

        private void EnterValue(object sender, KeyEventArgs e)
        {
            if (IsSCPActive && CheatsEnabled)
            {
                VAMemory adr = new VAMemory(SCPCB.procName);

                float f;

                if (float.TryParse(((TextBox)sender).Text, out f))
                {
                    if (e.Key == Key.Enter)
                    {
                        adr.WriteFloat((IntPtr)SCPCB.Cheats1 + 0xA34, f);
                    }
                }
                isCheated = true;
                IsActive1 = false;
            }
        }

        private void ClickButton(object sender, RoutedEventArgs e)
        {
            if (IsSCPActive && CheatsEnabled)
            {
                string name = ((Button)sender).Content.ToString();

                VAMemory adr = new VAMemory(SCPCB.procName);

                switch (name)
                {
                    case "KILL":
                        adr.WriteFloat((IntPtr)SCPCB.Cheats1 + 0x40, 100);
                        break;
                    case "HEAL":
                        adr.WriteFloat((IntPtr)SCPCB.Cheats1 + 0x40, 0);
                        adr.WriteFloat((IntPtr)SCPCB.Cheats1 + 0x3C, 0);
                        break;
                    case "REVIVE":
                        adr.WriteFloat((IntPtr)SCPCB.Cheats1 - 0x58, 0);
                        adr.WriteFloat((IntPtr)SCPCB.Cheats1 + 0x40, 0);
                        adr.WriteFloat((IntPtr)SCPCB.Cheats1 + 0x3C, 0);
                        break;
                }
                isCheated = true;
                IsActive1 = false;
            }
        }

        private void GiveAchv(object sender, MouseButtonEventArgs e)
        {
            if (IsSCPActive && CheatsEnabled)
            {
                string name = ((Border)sender).Uid;

                VAMemory adr = new VAMemory(SCPCB.procName);

                if (adr.ReadInt32((IntPtr)SCPCB.Achievements + (0x4 * int.Parse(name))) == 0)
                    adr.WriteInt32((IntPtr)SCPCB.Achievements + (0x4 * int.Parse(name)), 1);
                else
                    adr.WriteInt32((IntPtr)SCPCB.Achievements + (0x4 * int.Parse(name)), 0);

                ((Border)sender).BorderBrush = Brushes.Red;

                ((Border)ao.achvindex.Children[int.Parse(name)]).BorderBrush = Brushes.Red;

                isCheated = true;
                IsActive1 = false;
            }
        }

        private void AddToCheatIndex(object sender, RoutedEventArgs e)
        {
            cheatBorders.Add((Border)sender);
        }
    }
}
