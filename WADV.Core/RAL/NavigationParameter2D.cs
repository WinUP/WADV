namespace WADV.Core.RAL {
    public sealed class NavigationParameter2D {
        public Sprite2D PreviousScene { get; }
        public Sprite2D NextScene { get; }
        public bool Canceled { get; set; }
        public object ExtraData { get; set; }
        
        public NavigationParameter2D(Sprite2D previous, Sprite2D next) {
            PreviousScene = previous;
            NextScene = next;
            Canceled = false;
            ExtraData = null;
        }
    }
}