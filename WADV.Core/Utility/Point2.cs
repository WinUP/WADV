using System;

namespace WADV.Core.Utility {
    /// <summary>
    /// 2D point
    /// </summary>
    public struct Point2 {
        /// <summary>
        /// X coordinate
        /// </summary>
        public float X;

        /// <summary>
        /// Y coordinate
        /// </summary>
        public float Y;

        /// <summary>
        /// Get a new 2D point
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Point2(double x, double y) {
            X = (float) x;
            Y = (float) y;
        }

        /// <summary>
        /// Get a new 2D point
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Point2(float x, float y) {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Get a new 2D point
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Point2(int x, int y) {
            X = x;
            Y = y;
        }

        public float this[int index] {
            get {
                switch (index) {
                    case 0:
                        return X;
                    case 1:
                        return Y;
                    default:
                        return 0;
                }
            }
            set {
                switch (index) {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                }
            }
        }

        public static Point2 operator +(Point2 a, Point2 b) {
            return new Point2(a.X + b.X, a.Y + b.Y);
        }

        public static Point2 operator +(Point2 a, float b) {
            return new Point2(a.X + b, a.Y + b);
        }

        public static Point2 operator +(Point2 a, double b) {
            return a + (float) b;
        }

        public static Point2 operator +(Point2 a, int b) {
            return a + (float) b;
        }

        public static Point2 operator -(Point2 a, Point2 b) {
            return new Point2(a.X - b.X, a.Y - b.Y);
        }

        public static Point2 operator -(Point2 a, float b) {
            return new Point2(a.X - b, a.Y - b);
        }

        public static Point2 operator -(Point2 a, double b) {
            return a - (float) b;
        }

        public static Point2 operator -(Point2 a, int b) {
            return a - (float) b;
        }

        public static Point2 operator *(Point2 a, Point2 b) {
            return new Point2(a.X * b.X, a.Y * b.Y);
        }

        public static Point2 operator *(Point2 a, float b) {
            return new Point2(a.X * b, a.Y * b);
        }

        public static Point2 operator *(Point2 a, double b) {
            return a * (float) b;
        }

        public static Point2 operator *(Point2 a, int b) {
            return a * (float) b;
        }

        public static Point2 operator /(Point2 a, Point2 b) {
            return new Point2(a.X / b.X, a.Y / b.Y);
        }

        public static Point2 operator /(Point2 a, float b) {
            return new Point2(a.X / b, a.Y / b);
        }

        public static Point2 operator /(Point2 a, double b) {
            return a / (float) b;
        }

        public static Point2 operator /(Point2 a, int b) {
            return a / (float) b;
        }

        public static bool operator ==(Point2 a, Point2 b) {
            return a.X.Equals(b.X) && a.Y.Equals(b.Y);
        }

        public static bool operator ==(Point2 a, float b) {
            return a.X.Equals(b) && a.Y.Equals(b);
        }

        public static bool operator !=(Point2 a, Point2 b) {
            return !(a == b);
        }

        public static bool operator !=(Point2 a, float b) {
            return !(a == b);
        }

        public static bool operator >(Point2 a, Point2 b) {
            return a.X > b.X && a.Y > b.Y;
        }

        public static bool operator >(Point2 a, float b) {
            return a.X > b && a.Y > b;
        }

        public static bool operator <(Point2 a, Point2 b) {
            return a.X < b.X && a.Y < b.Y;
        }

        public static bool operator <(Point2 a, float b) {
            return a.X < b && a.Y < b;
        }

        public static bool operator >=(Point2 a, Point2 b) {
            return a.X >= b.X && a.Y >= b.Y;
        }

        public static bool operator >=(Point2 a, float b) {
            return a.X >= b && a.Y >= b;
        }

        public static bool operator <=(Point2 a, Point2 b) {
            return a.X <= b.X && a.Y <= b.Y;
        }

        public static bool operator <=(Point2 a, float b) {
            return a.X <= b && a.Y <= b;
        }

        public static implicit operator Point2(int v) {
            return new Point2(v, v);
        }

        public static implicit operator Point2(double v) {
            return new Point2(v, v);
        }

        public static implicit operator Point2(float v) {
            return new Point2(v, v);
        }

        public static implicit operator Point2(Point3 v) {
            return new Point2(v.X, v.Y);
        }

        public static implicit operator Point2(Rect2 v) {
            return new Point2(v.X, v.Y);
        }

        /// <summary>
        /// Get distance between another point and this
        /// </summary>
        /// <param name="target">Target point</param>
        /// <returns></returns>
        public float DistanceTo(Point2 target) {
            return (float) Math.Sqrt((X - target.X) * (X - target.X) + (Y - target.Y) * (Y - target.Y));
        }

        /// <summary>
        /// Indicate another point's X coordinate is same or not with this point
        /// </summary>
        /// <param name="target">Target</param>
        /// <returns></returns>
        public bool EqualX(Point2 target) {
            return X.Equals(target.X);
        }

        /// <summary>
        /// Indicate another point's Y coordinate is same or not with this point
        /// </summary>
        /// <param name="target">Target</param>
        /// <returns></returns>
        public bool EqualY(Point2 target) {
            return Y.Equals(target.Y);
        }

        /// <summary>
        /// Zero point
        /// </summary>
        /// <returns></returns>
        public static Point2 Zero { get; } = new Point2(0, 0);

        public bool Equals(Point2 other) {
            return this == other;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Point2 && Equals((Point2) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }
    }
}