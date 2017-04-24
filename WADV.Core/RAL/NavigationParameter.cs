namespace WADV.Core.RAL {
    public sealed class NavigationParameter {
        public Scene PreviousScene { get; }
        public Scene NextScene { get; }
        public bool Canceled { get; set; }
        public object ExtraData { get; set; }

        public NavigationParameter(Scene previous, Scene next) {
            PreviousScene = previous;
            NextScene = next;
            Canceled = false;
            ExtraData = null;
        }
    }
}