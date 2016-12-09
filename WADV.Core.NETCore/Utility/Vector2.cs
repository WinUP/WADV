using System;

namespace WADV.Core.NETCore.Utility
{
    /// <summary>
    /// 2D vector
    /// </summary>
    public struct Vector2
    {
        /// <summary>
        /// X component
        /// </summary>
        public double X;
        /// <summary>
        /// Y component
        /// </summary>
        public double Y;

        /// <summary>
        /// Get a new 2D vector
        /// </summary>
        /// <param name="x">X component</param>
        /// <param name="y">Y component</param>
        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }
        public static Vector2 operator +(Vector2 a, double b)
        {
            return new Vector2(a.X + b, a.Y + b);
        }
        public static Vector2 operator +(Vector2 a, int b)
        {
            return new Vector2(a.X + b, a.Y + b);
        }
        public static Vector2 operator +(Vector2 a, Point2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }
        public static Vector2 operator -(Vector2 a, double b)
        {
            return new Vector2(a.X - b, a.Y - b);
        }
        public static Vector2 operator -(Vector2 a, int b)
        {
            return new Vector2(a.X - b, a.Y - b);
        }
        public static Vector2 operator -(Vector2 a, Point2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }
        public static double operator *(Vector2 a, Vector2 b)
        {
            return a.X * b.X + a.Y * b.Y;
        }
        public static Vector2 operator *(Vector2 a, double b)
        {
            return new Vector2(a.X * b, a.Y * b);
        }
        public static Vector2 operator *(Vector2 a, int b)
        {
            return new Vector2(a.X * b, a.Y * b);
        }
        public static Vector2 operator *(Vector2 a, Point2 b)
        {
            return new Vector2(a.X * b.X, a.Y * b.X);
        }
        public static Vector2 operator /(Vector2 a, double b)
        {
            return new Vector2(a.X / b, a.Y / b);
        }
        public static Vector2 operator /(Vector2 a, int b)
        {
            return new Vector2(a.X / b, a.Y / b);
        }
        public static Vector2 operator /(Vector2 a, Point2 b)
        {
            return new Vector2(a.X / b.X, a.Y / b.X);
        }
        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return a.X.Equals(b.X) && a.Y.Equals(b.Y);
        }
        public static bool operator ==(Vector2 a, double b)
        {
            return Mod(a).Equals(b);
        }
        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return !(a == b);
        }
        public static bool operator !=(Vector2 a, double b)
        {
            return !(a == b);
        }
        public static bool operator >(Vector2 a, Vector2 b)
        {
            return (a.X * a.X + a.Y * a.Y) > (b.X * b.X + b.Y * b.Y);
        }
        public static bool operator <(Vector2 a, Vector2 b)
        {
            return (a.X * a.X + a.Y * a.Y) < (b.X * b.X + b.Y * b.Y);
        }
        public static bool operator >=(Vector2 a, Vector2 b)
        {
            return (a.X * a.X + a.Y * a.Y) >= (b.X * b.X + b.Y * b.Y);
        }
        public static bool operator <=(Vector2 a, Vector2 b)
        {
            return (a.X * a.X + a.Y * a.Y) <= (b.X * b.X + b.Y * b.Y);
        }
        public static implicit operator Vector2(int v)
        {
            return new Vector2(v, v);
        }
        public static implicit operator Vector2(double v)
        {
            return new Vector2(v, v);
        }

        /// <summary>
        /// Get the mod of this vector
        /// </summary>
        /// <returns></returns>
        public double Mod()
        {
            return Math.Sqrt(Mod2());
        }
        /// <summary>
        /// Get the square mod of this vector
        /// </summary>
        /// <returns></returns>
        public double Mod2()
        {
            return X * X + Y * Y;
        }

        /// <summary>
        /// Zero vector
        /// </summary>
        /// <returns></returns>
        public static Vector2 Zero { get; } = new Vector2(0, 0);
        /// <summary>
        /// Unit vector
        /// </summary>
        /// <returns></returns>
        public static Vector2 Unit { get; } = new Vector2(1, 1);

        public bool Equals(Vector2 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector2 && Equals((Vector2)obj);
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