using System;
using WADV.Core.Exception;

namespace WADV.Core.Utility {
    /// <summary>
    /// 2D vector
    /// </summary>
    public struct Vector2 {
        /// <summary>
        /// X component
        /// </summary>
        public float X;

        /// <summary>
        /// Y component
        /// </summary>
        public float Y;

        /// <summary>
        /// Get a new 2D vector
        /// </summary>
        /// <param name="x">X component</param>
        /// <param name="y">Y component</param>
        public Vector2(float x, float y) {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Get a new 2D vector
        /// </summary>
        /// <param name="x">X component</param>
        /// <param name="y">Y component</param>
        public Vector2(double x, double y) {
            X = (float) x;
            Y = (float) y;
        }

        /// <summary>
        /// Get a new 2D vector
        /// </summary>
        /// <param name="x">X component</param>
        /// <param name="y">Y component</param>
        public Vector2(int x, int y) {
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

        public static Vector2 operator +(Vector2 a, Vector2 b) {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2 operator +(Vector2 a, float b) {
            return new Vector2(a.X + b, a.Y + b);
        }

        public static Vector2 operator +(Vector2 a, double b) {
            return a + (float) b;
        }

        public static Vector2 operator +(Vector2 a, int b) {
            return a + (float) b;
        }

        public static Vector2 operator -(Vector2 a, Vector2 b) {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2 operator -(Vector2 a, float b) {
            return new Vector2(a.X - b, a.Y - b);
        }

        public static Vector2 operator -(Vector2 a, double b) {
            return a - (float) b;
        }

        public static Vector2 operator -(Vector2 a, int b) {
            return a - (float) b;
        }

        public static Vector3 operator *(Vector2 a, Vector2 b) {
            return new Vector3(0, 0, a.X * b.Y - a.Y * b.X);
        }

        public static Vector2 operator *(Vector2 a, float b) {
            return new Vector2(a.X * b, a.Y * b);
        }

        public static Vector2 operator *(Vector2 a, double b) {
            return a * (float) b;
        }

        public static Vector2 operator *(Vector2 a, int b) {
            return a * (float) b;
        }

        public static Vector2 operator /(Vector2 a, float b) {
            return new Vector2(a.X / b, a.Y / b);
        }

        public static Vector2 operator /(Vector2 a, double b) {
            return a / (float) b;
        }

        public static Vector2 operator /(Vector2 a, int b) {
            return a / (float) b;
        }

        public static bool operator ==(Vector2 a, Vector2 b) {
            return a.X.Equals(b.X) && a.Y.Equals(b.Y);
        }

        public static bool operator ==(Vector2 a, float b) {
            return a.Mod().Equals(b);
        }

        public static bool operator !=(Vector2 a, Vector2 b) {
            return !(a == b);
        }

        public static bool operator !=(Vector2 a, float b) {
            return !(a == b);
        }

        public static bool operator >(Vector2 a, Vector2 b) {
            return a.Mod2() > b.Mod2();
        }

        public static bool operator >(Vector2 a, float b) {
            return a.Mod() > b;
        }

        public static bool operator <(Vector2 a, Vector2 b) {
            return a.Mod2() < b.Mod2();
        }

        public static bool operator <(Vector2 a, float b) {
            return a.Mod() < b;
        }

        public static bool operator >=(Vector2 a, Vector2 b) {
            return a.Mod2() >= b.Mod2();
        }

        public static bool operator >=(Vector2 a, float b) {
            return a.Mod() >= b;
        }

        public static bool operator <=(Vector2 a, Vector2 b) {
            return a.Mod2() <= b.Mod2();
        }

        public static bool operator <=(Vector2 a, float b) {
            return a.Mod2() <= b;
        }

        public static implicit operator Vector2(int v) {
            return new Vector2(v, v);
        }

        public static implicit operator Vector2(double v) {
            return new Vector2(v, v);
        }

        public static implicit operator Vector2(float v) {
            return new Vector2(v, v);
        }

        public static implicit operator Vector2(Vector3 v) {
            return new Vector2(v.X, v.Y);
        }

        public static implicit operator Vector2(Rect2 v) {
            return new Vector2(v.Width, v.Height);
        }

        /// <summary>
        /// Get the mod of this vector
        /// </summary>
        /// <returns></returns>
        public float Mod() {
            return (float) Math.Sqrt(Mod2());
        }

        /// <summary>
        /// Get the square mod of this vector
        /// </summary>
        /// <returns></returns>
        public float Mod2() {
            return X * X + Y * Y;
        }

        /// <summary>
        /// Reset this vector
        /// </summary>
        /// <param name="x">New x component</param>
        /// <param name="y">New y component</param>
        public void Reset(float x, float y) {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Dot multiple with another vector
        /// </summary>
        /// <param name="b">Target vector</param>
        /// <returns></returns>
        public float Dot(Vector2 b) {
            return X * b.X + Y * b.Y;
        }

        /// <summary>
        /// Normalize this vector
        /// </summary>
        public Vector2 Normalized() {
            return this / Mod();
        }

        /// <summary>
        /// Lerp between this vector and another vector
        /// </summary>
        /// <param name="target">Target vector</param>
        /// <param name="scale">scale number (0~1)</param>
        /// <returns></returns>
        public Vector2 LerpTo(Vector2 target, float scale) {
            scale = MathExtended.Clamp(scale, 0, 1);
            return new Vector2(X + (target.X - X) * scale, Y + (target.Y - Y) * scale);
        }

        /// <summary>
        /// Get reflected vector
        /// </summary>
        /// <param name="normal">Normal vector</param>
        /// <returns></returns>
        public Vector2 ReflectBy(Vector2 normal) {
            return -2 * Dot(normal) * normal + this;
        }

        /// <summary>
        /// Get angle between this vector and another vector
        /// </summary>
        /// <param name="target">Target vector</param>
        /// <returns></returns>
        public float AngleBetween(Vector2 target) {
            return
                (float)
                (Math.Acos(MathExtended.Clamp(Normalized().Dot(target.Normalized()), -1, 1)) * MathExtended.UnitRadian);
        }

        /// <summary>
        /// Project this vector to another vector
        /// </summary>
        /// <param name="normal">Target vector</param>
        /// <returns></returns>
        public Vector2 ProjectTo(Vector2 normal)
        {
            return normal * Dot(normal) / normal.Dot(normal);
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

        /// <summary>
        /// Left direction unit vector
        /// </summary>
        public static Vector2 Left { get; } = new Vector2(-1, 0);

        /// <summary>
        /// Right direction unit vector
        /// </summary>
        public static Vector2 Right { get; } = new Vector2(1, 0);

        /// <summary>
        /// Up direction unit vector
        /// </summary>
        public static Vector2 Up { get; } = new Vector2(0, 1);

        /// <summary>
        /// Down direction unit vector
        /// </summary>
        public static Vector2 Down { get; } = new Vector2(0, -1);

        public bool Equals(Vector2 other) {
            return this == other;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector2 && Equals((Vector2) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }
    }
}