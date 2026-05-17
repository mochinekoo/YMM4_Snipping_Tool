using YukkuriMovieMaker.Plugin;

namespace YMM4_Snipping_Tool {
    public class PluginMain : IToolPlugin {
        public Type ViewModelType => typeof(MainViewModel);
        public Type ViewType => typeof(MainWindow);
        public string Name => "スクショプラグイン";
    }
}
