using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YMM4_Snipping_Tool {
    internal class MainViewModel {

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainViewModel() {
            ButtonCommand = new ButtonCommand(this);
        }

        public ButtonCommand ButtonCommand { get; set; }
    }
}
