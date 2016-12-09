namespace WADV.Core.NETCore.Utility
{
    /// <summary>
    /// 2D rectangle
    /// </summary>
    public struct Rect2
    {
        /// <summary>
        /// X coordinate of top-left point
        /// </summary>
        public double X;
        /// <summary>
        /// Y coordinate of top-left point
        /// </summary>
        public double Y;
        /// <summary>
        /// Width of rectanle
        /// </summary>
        public double Width;
        /// <summary>
        /// Height of rectangle
        /// </summary>
        public double Height;

        /// <summary>
        /// Get a new 2D rectangle
        /// </summary>
        /// <param name="x">X coordinate of top-left point</param>
        /// <param name="y">Y coordinate of top-left point</param>
        /// <param name="width">Width of rectanle</param>
        /// <param name="height">Height of rectangle</param>
        public Rect2(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public static Rect2 operator +(Rect2 a, Point2 b)
        {
            return new Rect2(a.X + b.X, a.Y + b.Y, a.Width, a.Height);
        }
        public static Rect2 operator +(Rect2 a, Vector2 b)
        {
            return new Rect2(a.X, a.Y, a.Width + b.X, a.Height + b.Y);
        }
        public static Rect2 operator +(Rect2 a, double b)
        {
            return new Rect2(a.X, a.Y, a.Width + b, a.Height + b);
        }
        public static Rect2 operator +(Rect2 a, int b)
        {
            return new Rect2(a.X, a.Y, a.Width + b, a.Height + b);
        }
        public static Rect2 operator -(Rect2 a, Point2 b)
        {
            return new Rect2(a.X - b.X, a.Y - b.Y, a.Width, a.Height);
        }
        public static Rect2 operator -(Rect2 a, Vector2 b)
        {
            return new Rect2(a.X, a.Y, a.Width - b.X, a.Height - b.Y);
        }
        public static Rect2 operator -(Rect2 a, double b)
        {
            return new Rect2(a.X, a.Y, a.Width - b, a.Height - b);
        }
        public static Rect2 operator -(Rect2 a, int b)
        {
            return new Rect2(a.X, a.Y, a.Width - b, a.Height - b);
        }
        public static Rect2 operator *(Rect2 a, Point2 b)
        {
            return new Rect2(a.X * b.X, a.Y * b.Y, a.Width, a.Height);
        }
        public static Rect2 operator *(Rect2 a, Vector2 b)
        {
            return new Rect2(a.X, a.Y, a.Width * b.X, a.Height * b.Y);
        }
        public static Rect2 operator *(Rect2 a, double b)
        {
            return new Rect2(a.X, a.Y, a.Width * b, a.Height * b);
        }
        public static Rect2 operator *(Rect2 a, int b)
        {
            return new Rect2(a.X, a.Y, a.Width * b, a.Height * b);
        }
        public static Rect2 operator /(Rect2 a, Point2 b)
        {
            return new Rect2(a.X / b.X, a.Y / b.Y, a.Width, a.Height);
        }
        public static Rect2 operator /(Rect2 a, Vector2 b)
        {
            return new Rect2(a.X, a.Y, a.Width / b.X, a.Height / b.Y);
        }
        public static Rect2 operator /(Rect2 a, double b)
        {
            return new Rect2(a.X, a.Y, a.Width / b, a.Height / b);
        }
        public static Rect2 operator /(Rect2 a, int b)
        {
            return new Rect2(a.X, a.Y, a.Width / b, a.Height / b);
        }
        public static bool operator ==(Rect2 a, Rect2 b)
        {
            return a.X.Equals(b.X) && a.Y.Equals(b.Y) && a.Width.Equals(b.Width) && a.Height.Equals(b.Height);
        }
        public static bool operator ==(Rect2 a, Point2 b)
        {
            return a.X.Equals(b.X) && a.Y.Equals(b.Y);
        }
        public static bool operator ==(Rect2 a, Vector2 b)
        {
            return a.Width.Equals(b.X) && a.Height.Equals(b.Y);
        }
        public static bool operator ==(Rect2 a, double b)
        {
            return a.X.Equals(b) && a.Y.Equals(b);
        }
        public static bool operator ==(Rect2 a, int b)
        {
            return a.Width.Equals(b) && a.Height.Equals(b);
        }
        public static bool operator !=(Rect2 a, Rect2 b)
        {
            return !(a == b);
        }
        public static bool operator !=(Rect2 a, Point2 b)
        {
            return !(a == b);
        }
        public static bool operator !=(Rect2 a, Vector2 b)
        {
            return !(a == b);
        }
        public static bool operator !=(Rect2 a, double b)
        {
            return !(a == b);
        }
        public static bool operator !=(Rect2 a, int b)
        {
            return !(a == b);
        }
        public static bool operator >(Rect2 a, Rect2 b)
        {
            return a.Width > b.Width && a.Height > b.Height;
        }
        public static bool operator >(Rect2 a, Point2 b)
        {
            return a.X > b.X && a.Y > b.Y;
        }
        public static bool operator >(Rect2 a, Vector2 b)
        {
            return a.Width > b.X && a.Height > b.Y;
        }
        public static bool operator >(Rect2 a, double b)
        {
            return a.Width > b && a.Height > b;
        }
        public static bool operator >(Rect2 a, int b)
        {
            return a.Width > b && a.Height > b;
        }
        public static bool operator <(Rect2 a, Rect2 b)
        {
            return a.Width < b.Width && a.Height < b.Height;
        }
        public static bool operator <(Rect2 a, Point2 b)
        {
            return a.X < b.X && a.Y < b.Y;
        }
        public static bool operator <(Rect2 a, Vector2 b)
        {
            return a.Width < b.X && a.Height < b.Y;
        }
        public static bool operator <(Rect2 a, double b)
        {
            return a.Width < b && a.Height < b;
        }
        public static bool operator <(Rect2 a, int b)
        {
            return a.Width < b && a.Height < b;
        }
        public static bool operator >=(Rect2 a, Rect2 b)
        {
            return a.Width >= b.Width && a.Height >= b.Height;
        }
        public static bool operator >=(Rect2 a, Point2 b)
        {
            return a.X >= b.X && a.Y >= b.Y;
        }
        public static bool operator >=(Rect2 a, Vector2 b)
        {
            return a.Width >= b.X && a.Height >= b.Y;
        }
        public static bool operator >=(Rect2 a, double b)
        {
            return a.Width >= b && a.Height >= b;
        }
        public static bool operator >=(Rect2 a, int b)
        {
            return a.Width >= b && a.Height >= b;
        }
        public static bool operator <=(Rect2 a, Rect2 b)
        {
            return a.Width <= b.Width && a.Height <= b.Height;
        }
        public static bool operator <=(Rect2 a, Point2 b)
        {
            return a.X <= b.X && a.Y <= b.Y;
        }
        public static bool operator <=(Rect2 a, Vector2 b)
        {
            return a.Width <= b.X && a.Height <= b.Y;
        }
        public static bool operator <=(Rect2 a, double b)
        {
            return a.Width <= b && a.Height <= b;
        }
        public static bool operator <=(Rect2 a, int b)
        {
            return a.Width <= b && a.Height <= b;
        }

        /// <summary>
        /// Get the point of this rectangle's top-left
        /// </summary>
        /// <returns></returns>
        public Point2 Point()
        {
            return new Point2(X, Y);
        }
        /// <summary>
        /// Get the size of this rectangle
        /// </summary>
        /// <returns></returns>
        public Vector2 Vector()
        {
            return new Vector2(Width, Height);
        }

        public bool Equals(Rect2 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Width.Equals(other.Width) && Height.Equals(other.Height);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Rect2 && Equals((Rect2)obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Width.GetHashCode();
                hashCode = (hashCode * 397) ^ Height.GetHashCode();
                return hashCode;
            }
        }
    }
}