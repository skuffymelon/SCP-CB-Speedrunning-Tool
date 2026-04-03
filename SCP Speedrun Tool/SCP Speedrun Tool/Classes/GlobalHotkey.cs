using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SCP_Speedrun_Tool.GlobalHotkeys
{
    public class GlobalHotkey
    {
        public ModifierKeys Modifier { get; set; }
        public Key Key { get; set; }
        public Action Callback { get; set; }
        public bool CanExecute { get; set; }

        public GlobalHotkey(ModifierKeys modifier, Key key, Action callbackMethod, bool canExecute = true)
        {
            this.Modifier = modifier;
            this.Key = key;
            this.Callback = callbackMethod;
            this.CanExecute = canExecute;
        }
    }
}
