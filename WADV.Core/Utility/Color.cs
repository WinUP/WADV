namespace WADV.Core.Utility {
    public struct Color {
        /// <summary>
        /// Red value
        /// </summary>
        public int Red;

        /// <summary>
        /// Green value
        /// </summary>
        public int Green;

        /// <summary>
        /// Blue value
        /// </summary>
        public int Blue;

        /// <summary>
        /// Alpha value
        /// </summary>
        public int Alpha;

        /// <summary>
        /// Get a Color
        /// </summary>
        /// <param name="red">Red value</param>
        /// <param name="green">Green value</param>
        /// <param name="blue">Blue value</param>
        /// <param name="alpha">Alpha value</param>
        public Color(int red, int green, int blue, int alpha = 255) {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        public int this[int index] {
            get {
                switch (index) {
                    case 0:
                        return Red;
                    case 1:
                        return Green;
                    case 2:
                        return Blue;
                    case 3:
                        return Alpha;
                    default:
                        return 0;
                }
            }
            set {
                switch (index) {
                    case 0:
                        Red = value;
                        break;
                    case 1:
                        Green = value;
                        break;
                    case 2:
                        Blue = value;
                        break;
                    case 3:
                        Alpha = value;
                        break;
                }
            }
        }

        public static Color operator +(Color a, Color b) {
            return new Color(
                MathExtended.Clamp(a.Red + b.Red, 0, 255),
                MathExtended.Clamp(a.Green + b.Green, 0, 255),
                MathExtended.Clamp(a.Blue + b.Blue, 0, 255),
                MathExtended.Clamp(a.Alpha + b.Alpha, 0, 255));
        }

        public static Color operator +(Color a, int b) {
            return new Color(
                MathExtended.Clamp(a.Red + b, 0, 255),
                MathExtended.Clamp(a.Green + b, 0, 255),
                MathExtended.Clamp(a.Blue + b, 0, 255),
                MathExtended.Clamp(a.Alpha + b, 0, 255));
        }

        public static Color operator -(Color a, Color b) {
            return new Color(
                MathExtended.Clamp(a.Red - b.Red, 0, 255),
                MathExtended.Clamp(a.Green - b.Green, 0, 255),
                MathExtended.Clamp(a.Blue - b.Blue, 0, 255),
                MathExtended.Clamp(a.Alpha - b.Alpha, 0, 255));
        }

        public static Color operator -(Color a, int b) {
            return new Color(
                MathExtended.Clamp(a.Red - b, 0, 255),
                MathExtended.Clamp(a.Green - b, 0, 255),
                MathExtended.Clamp(a.Blue - b, 0, 255),
                MathExtended.Clamp(a.Alpha - b, 0, 255));
        }

        public static Color operator *(Color a, Color b) {
            return new Color(
                MathExtended.Clamp(a.Red * b.Red, 0, 255),
                MathExtended.Clamp(a.Green * b.Green, 0, 255),
                MathExtended.Clamp(a.Blue * b.Blue, 0, 255),
                MathExtended.Clamp(a.Alpha * b.Alpha, 0, 255));
        }

        public static Color operator *(Color a, int b) {
            return new Color(
                MathExtended.Clamp(a.Red * b, 0, 255),
                MathExtended.Clamp(a.Green * b, 0, 255),
                MathExtended.Clamp(a.Blue * b, 0, 255),
                MathExtended.Clamp(a.Alpha * b, 0, 255));
        }

        public static Color operator /(Color a, Color b) {
            return new Color(
                MathExtended.Clamp(a.Red / b.Red, 0, 255),
                MathExtended.Clamp(a.Green / b.Green, 0, 255),
                MathExtended.Clamp(a.Blue / b.Blue, 0, 255),
                MathExtended.Clamp(a.Alpha / b.Alpha, 0, 255));
        }

        public static Color operator /(Color a, int b) {
            return new Color(
                MathExtended.Clamp(a.Red / b, 0, 255),
                MathExtended.Clamp(a.Green / b, 0, 255),
                MathExtended.Clamp(a.Blue / b, 0, 255),
                MathExtended.Clamp(a.Alpha / b, 0, 255));
        }

        public static bool operator ==(Color a, Color b) {
            return a.Red == b.Red && a.Green == b.Green && a.Blue == b.Blue && a.Alpha == b.Alpha;
        }

        public static bool operator !=(Color a, Color b) {
            return !(a == b);
        }

        public static implicit operator Color(int v) {
            return new Color(v, v, v, v);
        }

        /// <summary>
        /// Get first three components of this color
        /// </summary>
        /// <returns></returns>
        public Vector3 ToVector3() {
            return new Vector3(Red, Green, Blue);
        }

        /// <summary>
        /// Get first two components of this color
        /// </summary>
        /// <returns></returns>
        public Vector4 ToVector4() {
            return new Vector4(Red, Green, Blue, Alpha);
        }

        /// <summary>
        /// Reset this color
        /// </summary>
        /// <param name="red">Red value</param>
        /// <param name="green">Green value</param>
        /// <param name="blue">Blue value</param>
        /// <param name="alpha">Alpha value</param>
        public void Reset(int red, int green, int blue, int alpha = 255) {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        /// <summary>
        /// Black transparent color (0, 0, 0, 0)
        /// </summary>
        /// <returns></returns>
        public static Color BlackTransparentColor { get; } = new Color(0, 0, 0, 0);

        /// <summary>
        /// Black color (0, 0, 0, 255)
        /// </summary>
        /// <returns></returns>
        public static Color BlackColor { get; } = new Color(0, 0, 0, 255);

        /// <summary>
        /// White transparent color (255, 255, 255, 0)
        /// </summary>
        /// <returns></returns>
        public static Color WhiteTransparentColor { get; } = new Color(255, 255, 255, 0);

        /// <summary>
        /// White color (255, 255, 255, 255)
        /// </summary>
        /// <returns></returns>
        public static Color WhiteColor { get; } = new Color(255, 255, 255, 255);

        /// <summary>
        /// Red color (255, 0, 0, 255)
        /// </summary>
        /// <returns></returns>
        public static Color RedColor { get; } = new Color(255, 0, 0, 255);

        /// <summary>
        /// Green color (0, 255, 0, 255)
        /// </summary>
        /// <returns></returns>
        public static Color GreenColor { get; } = new Color(0, 255, 0, 255);

        /// <summary>
        /// Blue color (0, 0, 255, 255)
        /// </summary>
        /// <returns></returns>
        public static Color BlueColor { get; } = new Color(0, 0, 255, 255);

        public bool Equals(Color other) {
            return Red == other.Red && Green == other.Green && Blue == other.Blue && Alpha == other.Alpha;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Color && Equals((Color) obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = Red;
                hashCode = (hashCode * 397) ^ Green;
                hashCode = (hashCode * 397) ^ Blue;
                hashCode = (hashCode * 397) ^ Alpha;
                return hashCode;
            }
        }
    }
}
