using System;
using System.Collections.Generic;
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
    /// Interaction logic for CodeOverlay.xaml
    /// </summary>
    public partial class CodeOverlay : Window
    {
        Functions Function = new Functions();

        public SCPCBMenu menu;

        public CodeOverlay()
        {
            InitializeComponent();

            maycode.TextChanged += MaynardConversion;
            maincode.TextChanged += MaintenenceConversion;
            maycode.GotFocus += ClearText;
            maincode.GotFocus += ClearText;

            MouseEnter += GotOverlayFocus;
            MouseLeave += LostOverlayFocus;

            Closing += ClosingCancel;
        }

        private void ClosingCancel(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            menu.CodeOverlayShowHotkey();
        }

        private void GotOverlayFocus(object sender, RoutedEventArgs e)
        {
            main.Opacity = 1;
        }

        private void LostOverlayFocus(object sender, RoutedEventArgs e)
        {
            main.Opacity = 0.3;
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
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

                maycode.Text = scode;
                maycode.Opacity = 1;
                maytool.Opacity = 0;
            }

        }
    }
}
