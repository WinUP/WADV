using WADV.Core.Utility;

namespace WADV.Core.RAL.Resource {
    public abstract class Texture2D {
        private int _width;
        private int _height;

        public string Source { get; }

        public Texture2D(int width, int height) {
            _width = width;
            _height = height;
            RawData = new int[width * height * 4];
            Source = null;
        }

        public Texture2D(string source) {
            Source = source;
        }

        public int[] RawData { get; protected set; }

        public int Width {
            get => _width;
            set {
                Resize(value, Height);
                _width = value;
            }
        }

        public int Height {
            get => _height;
            set {
                Resize(Width, value);
                _height = value;
            }
        }

        protected abstract void LoadContent();

        public abstract void Resize(int width = -1, int height = -1, ResizeMode mode = ResizeMode.Cut);

        public abstract void SetPixel(int offsetX, int offsetY, int width, int height, Color[] pixels);

        public abstract Color[] GetPixel(int offsetX, int offsetY, int width, int height);

        public enum ResizeMode {
            Cut,
            NearestNeighbor,
            Bilinear,
            Bicubic,
            Pixel,
            Supersampling,
            RepeatX,
            RepeatY,
            RepeatBoth
        }
    }
}
