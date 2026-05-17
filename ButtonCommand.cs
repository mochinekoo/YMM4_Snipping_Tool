using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace YMM4_Snipping_Tool {
    internal class ButtonCommand {

        private readonly MainViewModel mainViewModel_;
        public ICommand ScreenshotCommand { get; private set; }
        public static ScreenshotWindow? ScreenshotWindow { get; private set; }

        public ButtonCommand(MainViewModel mainViewModel) {
            this.mainViewModel_ = mainViewModel;
            ScreenshotCommand = new BaseCommand(RunScreenshotCommand);
        }

        private void RunScreenshotCommand() {
            if (ScreenshotWindow == null) {
                ScreenshotWindow = new ScreenshotWindow();
            }
            ScreenshotWindow.Show();
        }
    }
}
