namespace WADV.Core.Utility {
    /// <summary>
    /// 2D rectangle
    /// </summary>
    public struct Rect2 {
        /// <summary>
        /// X coordinate of top-left point
        /// </summary>
        public float X;

        /// <summary>
        /// Y coordinate of top-left point
        /// </summary>
        public float Y;

        /// <summary>
        /// Width of rectanle
        /// </summary>
        public float Width;

        /// <summary>
        /// Height of rectangle
        /// </summary>
        public float Height;

        /// <summary>
        /// Get a new 2D rectangle
        /// </summary>
        /// <param name="x">X coordinate of top-left point</param>
        /// <param name="y">Y coordinate of top-left point</param>
        /// <param name="width">Width of rectanle</param>
        /// <param name="height">Height of rectangle</param>
        public Rect2(float x, float y, float width, float height) {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Get a new 2D rectangle
        /// </summary>
        /// <param name="x">X coordinate of top-left point</param>
        /// <param name="y">Y coordinate of top-left point</param>
        /// <param name="width">Width of rectanle</param>
        /// <param name="height">Height of rectangle</param>
        public Rect2(double x, double y, double width, double height) {
            X = (float) x;
            Y = (float) y;
            Width = (float) width;
            Height = (float) height;
        }

        /// <summary>
        /// Get a new 2D rectangle
        /// </summary>
        /// <param name="x">X coordinate of top-left point</param>
        /// <param name="y">Y coordinate of top-left point</param>
        /// <param name="width">Width of rectanle</param>
        /// <param name="height">Height of rectangle</param>
        public Rect2(int x, int y, int width, int height) {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Get a new 2D rectangle
        /// </summary>
        /// <param name="topLeft">Top-left point of this rectange</param>
        /// <param name="size">Size of this rectangle</param>
        public Rect2(Point2 topLeft, Vector2 size) {
            X = topLeft.X;
            Y = topLeft.Y;
            Width = size.X;
            Height = size.Y;
        }

        public static Rect2 operator +(Rect2 a, Point2 b) {
            return new Rect2(a.X + b.X, a.Y + b.Y, a.Width, a.Height);
        }

        public static Rect2 operator +(Rect2 a, Vector2 b) {
            return new Rect2(a.X, a.Y, a.Width + b.X, a.Height + b.Y);
        }

        public static Rect2 operator +(Rect2 a, float b) {
            return new Rect2(a.X, a.Y, a.Width + b, a.Height + b);
        }

        public static Rect2 operator +(Rect2 a, double b) {
            return a + (float) b;
        }

        public static Rect2 operator +(Rect2 a, int b) {
            return a + (float) b;
        }

        public static Rect2 operator -(Rect2 a, Point2 b) {
            return new Rect2(a.X - b.X, a.Y - b.Y, a.Width, a.Height);
        }

        public static Rect2 operator -(Rect2 a, Vector2 b) {
            return new Rect2(a.X, a.Y, a.Width - b.X, a.Height - b.Y);
        }

        public static Rect2 operator -(Rect2 a, float b) {
            return new Rect2(a.X, a.Y, a.Width - b, a.Height - b);
        }

        public static Rect2 operator -(Rect2 a, double b) {
            return a - (float) b;
        }

        public static Rect2 operator -(Rect2 a, int b) {
            return a - (float) b;
        }

        public static Rect2 operator *(Rect2 a, Point2 b) {
            return new Rect2(a.X * b.X, a.Y * b.Y, a.Width, a.Height);
        }

        public static Rect2 operator *(Rect2 a, Vector2 b) {
            return new Rect2(a.X, a.Y, a.Width * b.X, a.Height * b.Y);
        }

        public static Rect2 operator *(Rect2 a, float b) {
            return new Rect2(a.X, a.Y, a.Width * b, a.Height * b);
        }

        public static Rect2 operator *(Rect2 a, double b) {
            return a * (float) b;
        }

        public static Rect2 operator *(Rect2 a, int b) {
            return a * (float) b;
        }

        public static Rect2 operator /(Rect2 a, Point2 b) {
            return new Rect2(a.X / b.X, a.Y / b.Y, a.Width, a.Height);
        }

        public static Rect2 operator /(Rect2 a, Vector2 b) {
            return new Rect2(a.X, a.Y, a.Width / b.X, a.Height / b.Y);
        }

        public static Rect2 operator /(Rect2 a, float b) {
            return new Rect2(a.X, a.Y, a.Width / b, a.Height / b);
        }

        public static Rect2 operator /(Rect2 a, double b) {
            return a / (float) b;
        }

        public static Rect2 operator /(Rect2 a, int b) {
            return a / (float) b;
        }

        public static bool operator ==(Rect2 a, Rect2 b) {
            return a.X.Equals(b.X) && a.Y.Equals(b.Y) && a.Width.Equals(b.Width) && a.Height.Equals(b.Height);
        }

        public static bool operator ==(Rect2 a, Point2 b) {
            return a.X.Equals(b.X) && a.Y.Equals(b.Y);
        }

        public static bool operator ==(Rect2 a, Vector2 b) {
            return a.Width.Equals(b.X) && a.Height.Equals(b.Y);
        }

        public static bool operator !=(Rect2 a, Rect2 b) {
            return !(a == b);
        }

        public static bool operator !=(Rect2 a, Point2 b) {
            return !(a == b);
        }

        public static bool operator !=(Rect2 a, Vector2 b) {
            return !(a == b);
        }

        public static bool operator >(Rect2 a, Rect2 b) {
            return a.Width > b.Width && a.Height > b.Height;
        }

        public static bool operator >(Rect2 a, Point2 b) {
            return a.X > b.X && a.Y > b.Y;
        }

        public static bool operator >(Rect2 a, Vector2 b) {
            return a.Width > b.X && a.Height > b.Y;
        }

        public static bool operator <(Rect2 a, Rect2 b) {
            return a.Width < b.Width && a.Height < b.Height;
        }

        public static bool operator <(Rect2 a, Point2 b) {
            return a.X < b.X && a.Y < b.Y;
        }

        public static bool operator <(Rect2 a, Vector2 b) {
            return a.Width < b.X && a.Height < b.Y;
        }

        public static bool operator >=(Rect2 a, Rect2 b) {
            return a.Width >= b.Width && a.Height >= b.Height;
        }

        public static bool operator >=(Rect2 a, Point2 b) {
            return a.X >= b.X && a.Y >= b.Y;
        }

        public static bool operator >=(Rect2 a, Vector2 b) {
            return a.Width >= b.X && a.Height >= b.Y;
        }

        public static bool operator <=(Rect2 a, Rect2 b) {
            return a.Width <= b.Width && a.Height <= b.Height;
        }

        public static bool operator <=(Rect2 a, Point2 b) {
            return a.X <= b.X && a.Y <= b.Y;
        }

        public static bool operator <=(Rect2 a, Vector2 b) {
            return a.Width <= b.X && a.Height <= b.Y;
        }

        public static implicit operator Rect2(Point2 v) {
            return new Rect2(v.X, v.Y, 0, 0);
        }

        public static implicit operator Rect2(Vector2 v) {
            return new Rect2(0, 0, v.X, v.Y);
        }

        /// <summary>
        /// Get or set the point of center
        /// </summary>
        public Point2 Center {
            get { return new Point2(X + Width / 2, Y + Height / 2); }
            set {
                X = value.X - Width / 2;
                Y = value.Y - Height / 2;
            }
        }

        /// <summary>
        /// Get or set the point of top-left point
        /// </summary>
        public Point2 TopLeft {
            get { return new Point2(X, Y); }
            set {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
        /// Get or set the point of top-right point
        /// </summary>
        public Point2 TopRight {
            get { return new Point2(X + Width, Y); }
            set {
                X = value.X - Width;
                Y = value.Y;
            }
        }

        /// <summary>
        /// Get or set the point of bottom-left point
        /// </summary>
        public Point2 BottomLeft {
            get { return new Point2(X, Y + Height); }
            set {
                X = value.X;
                Y = value.Y + Height;
            }
        }

        /// <summary>
        /// Get or set the point of bottom-right point
        /// </summary>
        public Point2 BottomRight {
            get { return new Point2(X + Width, Y + Height); }
            set {
                X = value.X - Width;
                Y = value.Y - Height;
            }
        }

        /// <summary>
        /// Get the size of this rectangle
        /// </summary>
        /// <returns></returns>
        public Vector2 Size() {
            return new Vector2(Width, Height);
        }

        /// <summary>
        /// Indicate that if a point is covered by this
        /// </summary>
        /// <param name="point">Target point</param>
        /// <returns></returns>
        public bool Contains(Point2 point) {
            return point >= TopLeft && point <= BottomRight;
        }

        /// <summary>
        /// Indicate that if a 2D rectangle is covered by this
        /// </summary>
        /// <param name="rect">Target rectangle</param>
        /// <returns></returns>
        public bool Contains(Rect2 rect) {
            return rect.TopLeft <= TopLeft && rect.BottomRight <= BottomRight;
        }

        /// <summary>
        /// Indicate that if a 2D rectangle shares same area with this
        /// </summary>
        /// <param name="target">Target rectangle</param>
        /// <returns></returns>
        public bool IsOverlap(Rect2 target) {
            var distance = Center.DistanceTo(target.Center);
            return (distance <= (Width + target.Width) / 2 && distance <= (Height + target.Height) / 2) ? true : false;
        }

        public bool Equals(Rect2 other) {
            return this == other;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Rect2 && Equals((Rect2) obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Width.GetHashCode();
                hashCode = (hashCode * 397) ^ Height.GetHashCode();
                return hashCode;
            }
        }
    }
}