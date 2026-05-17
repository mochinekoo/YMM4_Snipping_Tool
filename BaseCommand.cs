using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace YMM4_Snipping_Tool {
    internal class BaseCommand : ICommand {

        private readonly Action action;
        public event EventHandler? CanExecuteChanged;

        public BaseCommand(Action action) {
            this.action = action;
        }

        public bool CanExecute(object? parameter) {
            return true;
        }

        public void Execute(object? parameter) {
            action();
        }
    }
}
