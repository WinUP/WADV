using System;

namespace WADV.Core.Utility {
    /// <summary>
    /// 3D point
    /// </summary>
    public struct Point3 {
        /// <summary>
        /// X coordinate
        /// </summary>
        public float X;

        /// <summary>
        /// Y coordinate
        /// </summary>
        public float Y;

        /// <summary>
        /// Z coordinate
        /// </summary>
        public float Z;

        /// <summary>
        /// Get a new 3D point
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        public Point3(double x, double y, double z) {
            X = (float) x;
            Y = (float) y;
            Z = (float) z;
        }

        /// <summary>
        /// Get a new 3D point
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        public Point3(float x, float y, float z) {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Get a new 3D point
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        public Point3(int x, int y, int z) {
            X = x;
            Y = y;
            Z = z;
        }

        public float this[int index] {
            get {
                switch (index) {
                    case 0:
                        return X;
                    case 1:
                        return Y;
                    case 2:
                        return Z;
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
                    case 2:
                        Z = value;
                        break;
                }
            }
        }

        public static Point3 operator +(Point3 a, Point3 b) {
            return new Point3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Point3 operator +(Point3 a, Point2 b) {
            return new Point3(a.X + b.X, a.Y + b.Y, a.Z);
        }

        public static Point3 operator +(Point3 a, double b) {
            return new Point3(a.X + b, a.Y + b, a.Z + b);
        }

        public static Point3 operator +(Point3 a, float b) {
            return new Point3(a.X + b, a.Y + b, a.Z + b);
        }

        public static Point3 operator +(Point3 a, int b) {
            return new Point3(a.X + b, a.Y + b, a.Z + b);
        }

        public static Point3 operator -(Point3 a, Point3 b) {
            return new Point3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Point3 operator -(Point3 a, Point2 b) {
            return new Point3(a.X - b.X, a.Y - b.Y, a.Z);
        }

        public static Point3 operator -(Point3 a, double b) {
            return new Point3(a.X - b, a.Y - b, a.Z - b);
        }

        public static Point3 operator -(Point3 a, float b) {
            return new Point3(a.X - b, a.Y - b, a.Z - b);
        }

        public static Point3 operator -(Point3 a, int b) {
            return new Point3(a.X - b, a.Y - b, a.Z - b);
        }

        public static Point3 operator *(Point3 a, Point3 b) {
            return new Point3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static Point3 operator *(Point3 a, Point2 b) {
            return new Point3(a.X * b.X, a.Y * b.Y, a.Z);
        }

        public static Point3 operator *(Point3 a, double b) {
            return new Point3(a.X * b, a.Y * b, a.Z * b);
        }

        public static Point3 operator *(Point3 a, float b) {
            return new Point3(a.X * b, a.Y * b, a.Z * b);
        }

        public static Point3 operator *(Point3 a, int b) {
            return new Point3(a.X * b, a.Y * b, a.Z * b);
        }

        public static Point3 operator /(Point3 a, Point3 b) {
            return new Point3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }

        public static Point3 operator /(Point3 a, Point2 b) {
            return new Point3(a.X / b.X, a.Y / b.Y, a.Z);
        }

        public static Point3 operator /(Point3 a, double b) {
            return new Point3(a.X / b, a.Y / b, a.Z / b);
        }

        public static Point3 operator /(Point3 a, float b) {
            return new Point3(a.X / b, a.Y / b, a.Z / b);
        }

        public static Point3 operator /(Point3 a, int b) {
            return new Point3(a.X / b, a.Y / b, a.Z / b);
        }

        public static bool operator ==(Point3 a, Point3 b) {
            return a.X.Equals(b.X) && a.Y.Equals(b.Y) && a.Z.Equals(b.Z);
        }

        public static bool operator ==(Point3 a, float b) {
            return a.X.Equals(b) && a.Y.Equals(b) && a.Z.Equals(b);
        }

        public static bool operator !=(Point3 a, Point3 b) {
            return !(a == b);
        }

        public static bool operator !=(Point3 a, float b) {
            return !(a == b);
        }

        public static bool operator >(Point3 a, Point3 b) {
            return a.X > b.X && a.Y > b.Y && a.Z > b.Z;
        }

        public static bool operator >(Point3 a, float b) {
            return a.X > b && a.Y > b && a.Z > b;
        }

        public static bool operator <(Point3 a, Point3 b) {
            return a.X < b.X && a.Y < b.Y && a.Z < b.Z;
        }

        public static bool operator <(Point3 a, float b) {
            return a.X < b && a.Y < b && a.Z < b;
        }

        public static bool operator >=(Point3 a, Point3 b) {
            return a.X >= b.X && a.Y >= b.Y && a.Z >= b.Z;
        }

        public static bool operator >=(Point3 a, float b) {
            return a.X >= b && a.Y >= b && a.Z >= b;
        }

        public static bool operator <=(Point3 a, Point3 b) {
            return a.X <= b.X && a.Y <= b.Y && a.Z <= b.Z;
        }

        public static bool operator <=(Point3 a, float b) {
            return a.X <= b && a.Y <= b && a.Z <= b;
        }

        public static implicit operator Point3(int v) {
            return new Point3(v, v, v);
        }

        public static implicit operator Point3(double v) {
            return new Point3(v, v, v);
        }

        public static implicit operator Point3(float v) {
            return new Point3(v, v, v);
        }

        public static implicit operator Point3(Point2 v) {
            return new Point3(v.X, v.Y, 0);
        }

        public static implicit operator Point3(Rect2 v) {
            return new Point3(v.X, v.Y, 0);
        }

        /// <summary>
        /// Get distance between another point and this
        /// </summary>
        /// <param name="target">Target point</param>
        /// <returns></returns>
        public float DistanceTo(Point3 target) {
            return (float) Math.Sqrt((X - target.X) * (X - target.X) +
                                     (Y - target.Y) * (Y - target.Y) +
                                     (Z - target.Z) * (Z - target.Z));
        }

        /// <summary>
        /// Get first two coordinates of this point
        /// </summary>
        /// <returns></returns>
        public Point2 ToPoint2() {
            return new Point2(X, Y);
        }

        /// <summary>
        /// Indicate another point's X coordinate is same or not with this point
        /// </summary>
        /// <param name="target">Target</param>
        /// <returns></returns>
        public bool EqualX(Point3 target) {
            return X.Equals(target.X);
        }

        /// <summary>
        /// Indicate another point's Y coordinate is same or not with this point
        /// </summary>
        /// <param name="target">Target</param>
        /// <returns></returns>
        public bool EqualY(Point3 target) {
            return Y.Equals(target.Y);
        }

        /// <summary>
        /// Indicate another point's Z coordinate is same or not with this point
        /// </summary>
        /// <param name="target">Target</param>
        /// <returns></returns>
        public bool EqualZ(Point3 target) {
            return Z.Equals(target.Z);
        }

        /// <summary>
        /// Zero point
        /// </summary>
        /// <returns></returns>
        public static Point3 Zero { get; } = new Point3(0, 0, 0);

        public bool Equals(Point3 other) {
            return this == other;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Point3 && Equals((Point3) obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }
    }
}