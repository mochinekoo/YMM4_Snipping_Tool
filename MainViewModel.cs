using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using YukkuriMovieMaker.Plugin;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace YMM4_Snipping_Tool {
    internal class MainViewModel : ITimelineToolViewModel {

        public static TimelineToolInfo info_;
        public event PropertyChangedEventHandler? PropertyChanged;

        public MainViewModel() {
            ButtonCommand = new ButtonCommand(this);
        }

        public ButtonCommand ButtonCommand { get; set; }

        public void SetTimelineToolInfo(TimelineToolInfo info) {
            info_ = info;
        }
    }
}
