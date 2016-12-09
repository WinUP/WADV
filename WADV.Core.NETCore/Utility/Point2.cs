namespace WADV.Core.NETCore.Utility
{
    /// <summary>
    /// 2D point
    /// </summary>
    public struct Point2
    {
        /// <summary>
        /// X coordinate
        /// </summary>
        public double X;
        /// <summary>
        /// Y coordinate
        /// </summary>
        public double Y;

        /// <summary>
        /// Get a new 2D point
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Point2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Point2 operator +(Point2 a, Point2 b)
        {
            return new Point2(a.X + b.X, a.Y + b.Y);
        }
        public static Point2 operator +(Point2 a, double b)
        {
            return new Point2(a.X + b, a.Y + b);
        }
        public static Point2 operator +(Point2 a, int b)
        {
            return new Point2(a.X + b, a.Y + b);
        }
        public static Point2 operator +(Point2 a, Vector2 b)
        {
            return new Point2(a.X + b.X, a.Y + b.Y);
        }
        public static Point2 operator -(Point2 a, Point2 b)
        {
            return new Point2(a.X - b.X, a.Y - b.Y);
        }
        public static Point2 operator -(Point2 a, double b)
        {
            return new Point2(a.X - b, a.Y - b);
        }
        public static Point2 operator -(Point2 a, int b)
        {
            return new Point2(a.X - b, a.Y - b);
        }
        public static Point2 operator -(Point2 a, Vector2 b)
        {
            return new Point2(a.X - b.X, a.Y - b.Y);
        }
        public static Point2 operator *(Point2 a, Point2 b)
        {
            return new Point2(a.X * b.X, a.Y * b.Y);
        }
        public static Point2 operator *(Point2 a, double b)
        {
            return new Point2(a.X * b, a.Y * b);
        }
        public static Point2 operator *(Point2 a, int b)
        {
            return new Point2(a.X * b, a.Y * b);
        }
        public static Point2 operator *(Point2 a, Vector2 b)
        {
            return new Point2(a.X * b.X, a.Y * b.Y);
        }
        public static Point2 operator /(Point2 a, Point2 b)
        {
            return new Point2(a.X / b.X, a.Y / b.Y);
        }
        public static Point2 operator /(Point2 a, double b)
        {
            return new Point2(a.X / b, a.Y / b);
        }
        public static Point2 operator /(Point2 a, int b)
        {
            return new Point2(a.X / b, a.Y / b);
        }
        public static Point2 operator /(Point2 a, Vector2 b)
        {
            return new Point2(a.X / b.X, a.Y / b.Y);
        }
        public static bool operator ==(Point2 a, Point2 b)
        {
            return a.X.Equals(b.X) && a.Y.Equals(b.Y);
        }
        public static bool operator !=(Point2 a, Point2 b)
        {
            return !(a == b);
        }
        public static bool operator >(Point2 a, Point2 b)
        {
            return a.X > b.X && a.Y > b.Y;
        }
        public static bool operator <(Point2 a, Point2 b)
        {
            return a.X > b.X && a.Y < b.Y;
        }
        public static bool operator >=(Point2 a, Point2 b)
        {
            return a.X > b.X && a.Y >= b.Y;
        }
        public static bool operator <=(Point2 a, Point2 b)
        {
            return a.X > b.X && a.Y <= b.Y;
        }
        public static implicit operator Point2(int v)
        {
            return new Point2(v, v);
        }
        public static implicit operator Point2(double v)
        {
            return new Point2(v, v);
        }

        /// <summary>
        /// Indicate another point's X coordinate is same or not with this point
        /// </summary>
        /// <param name="target">Target</param>
        /// <returns></returns>
        public bool EqualX(Point2 target)
        {
            return X.Equals(target.X);
        }
        /// <summary>
        /// Indicate another point's X coordinate is same or not with this point
        /// </summary>
        /// <param name="target">Target</param>
        /// <returns></returns>
        public bool EqualY(Point2 target)
        {
            return Y.Equals(target.Y);
        }

        /// <summary>
        /// Zero point
        /// </summary>
        /// <returns></returns>
        public static Point2 Zero { get; } = new Point2(0, 0);

        public bool Equals(Point2 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Point2 && Equals((Point2)obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }
    }
}