using System;
using WADV.Core.Exception;

namespace WADV.Core.Utility {
    public struct Vector4 {
        /// <summary>
        /// X component
        /// </summary>
        public float X;

        /// <summary>
        /// Y component
        /// </summary>
        public float Y;

        /// <summary>
        /// Z component
        /// </summary>
        public float Z;

        /// <summary>
        /// W component
        /// </summary>
        public float W;

        /// <summary>
        /// Get a new 4D vector
        /// </summary>
        /// <param name="x">X component</param>
        /// <param name="y">Y component</param>
        /// <param name="z">Z component</param>
        /// <param name="w">W component</param>
        public Vector4(float x, float y, float z, float w) {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Get a new 4D vector
        /// </summary>
        /// <param name="x">X component</param>
        /// <param name="y">Y component</param>
        /// <param name="z">Z component</param>
        /// <param name="w">W component</param>
        public Vector4(double x, double y, double z, double w) {
            X = (float) x;
            Y = (float) y;
            Z = (float) z;
            W = (float) w;
        }

        /// <summary>
        /// Get a new 4D vector
        /// </summary>
        /// <param name="x">X component</param>
        /// <param name="y">Y component</param>
        /// <param name="z">Z component</param>
        /// <param name="w">W component</param>
        public Vector4(int x, int y, int z, int w) {
            X = x;
            Y = y;
            Z = z;
            W = w;
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
                    case 3:
                        return W;
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
                    case 3:
                        W = value;
                        break;
                }
            }
        }

        public static Vector4 operator +(Vector4 a, Vector4 b) {
            return new Vector4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }

        public static Vector4 operator +(Vector4 a, Vector3 b) {
            return new Vector4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W);
        }

        public static Vector4 operator +(Vector4 a, Vector2 b) {
            return new Vector4(a.X + b.X, a.Y + b.Y, a.Z, a.W);
        }

        public static Vector4 operator +(Vector4 a, float b) {
            return new Vector4(a.X + b, a.Y + b, a.Z + b, a.W + b);
        }

        public static Vector4 operator +(Vector4 a, double b) {
            return a + (float) b;
        }

        public static Vector4 operator +(Vector4 a, int b) {
            return a + (float) b;
        }

        public static Vector4 operator -(Vector4 a, Vector4 b) {
            return new Vector4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }

        public static Vector4 operator -(Vector4 a, Vector3 b) {
            return new Vector4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W);
        }

        public static Vector4 operator -(Vector4 a, Vector2 b) {
            return new Vector4(a.X - b.X, a.Y - b.Y, a.Z, a.W);
        }

        public static Vector4 operator -(Vector4 a, float b) {
            return new Vector4(a.X - b, a.Y - b, a.Z - b, a.W - b);
        }

        public static Vector4 operator -(Vector4 a, double b) {
            return a - (float) b;
        }

        public static Vector4 operator -(Vector4 a, int b) {
            return a - (float) b;
        }

        public static float operator *(Vector4 a, Vector4 b) {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
        }

        public static Vector4 operator *(Vector4 a, float b) {
            return new Vector4(a.X * b, a.Y * b, a.Z * b, a.W * b);
        }

        public static Vector4 operator *(Vector4 a, double b) {
            return a * (float) b;
        }

        public static Vector4 operator *(Vector4 a, int b) {
            return a * (float) b;
        }

        public static Vector4 operator /(Vector4 a, float b) {
            return new Vector4(a.X / b, a.Y / b, a.Z / b, a.W / b);
        }

        public static Vector4 operator /(Vector4 a, double b) {
            return a / (float) b;
        }

        public static Vector4 operator /(Vector4 a, int b) {
            return a / (float) b;
        }

        public static bool operator ==(Vector4 a, Vector4 b) {
            return a.X.Equals(b.X) && a.Y.Equals(b.Y) && a.Z.Equals(b.Z) && a.W.Equals(b.W);
        }

        public static bool operator ==(Vector4 a, float b) {
            return a.Mod().Equals(b);
        }

        public static bool operator !=(Vector4 a, Vector4 b) {
            return !(a == b);
        }

        public static bool operator !=(Vector4 a, float b) {
            return !(a == b);
        }

        public static bool operator >(Vector4 a, Vector4 b) {
            return a.Mod2() > b.Mod2();
        }

        public static bool operator >(Vector4 a, float b) {
            return a.Mod() > b;
        }

        public static bool operator <(Vector4 a, Vector4 b) {
            return a.Mod2() < b.Mod2();
        }

        public static bool operator <(Vector4 a, float b) {
            return a.Mod() < b;
        }

        public static bool operator >=(Vector4 a, Vector4 b) {
            return a.Mod2() >= b.Mod2();
        }

        public static bool operator >=(Vector4 a, float b) {
            return a.Mod() >= b;
        }

        public static bool operator <=(Vector4 a, Vector4 b) {
            return a.Mod2() <= b.Mod2();
        }

        public static bool operator <=(Vector4 a, float b) {
            return a.Mod() <= b;
        }

        public static implicit operator Vector4(float v) {
            return new Vector4(v, v, v, v);
        }

        public static implicit operator Vector4(double v) {
            return new Vector4(v, v, v, v);
        }

        public static implicit operator Vector4(int v) {
            return new Vector4(v, v, v, v);
        }

        /// <summary>
        /// Get first three components of this vector
        /// </summary>
        /// <returns></returns>
        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, Z);
        }

        /// <summary>
        /// Get first two components of this vector
        /// </summary>
        /// <returns></returns>
        public Vector2 ToVector2() {
            return new Vector2(X, Y);
        }

        /// <summary>
        /// Get the mod of this vector
        /// </summary>
        /// <returns></returns>
        public double Mod() {
            return Math.Sqrt(Mod2());
        }

        /// <summary>
        /// Get the square mod of this vector
        /// </summary>
        /// <returns></returns>
        public double Mod2() {
            return (double) X * X + (double) Y * Y + (double) Z * Z + (double) W * W;
        }

        /// <summary>
        /// Reset this vector
        /// </summary>
        /// <param name="x">New x component</param>
        /// <param name="y">New y component</param>
        /// <param name="z">New z component</param>
        /// <param name="w">New w component</param>
        public void Reset(float x, float y, float z, float w) {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Dot multiple with another vector
        /// </summary>
        /// <param name="b">Target vector</param>
        /// <returns></returns>
        public float Dot(Vector4 b) {
            return this * b;
        }

        /// <summary>
        /// Normalize this vector
        /// </summary>
        public Vector4 Normalized() {
            return this / Mod();
        }

        /// <summary>
        /// Lerp between this vector and another vector
        /// </summary>
        /// <param name="target">Target vector</param>
        /// <param name="scale">scale number (0~1)</param>
        /// <returns></returns>
        public Vector4 LerpTo(Vector4 target, float scale) {
            scale = MathExtended.Clamp(scale, 0, 1);
            return new Vector4(X + (target.X - X) * scale, Y + (target.Y - Y) * scale, Z + (target.Z - Z) * scale,
                W + (target.W - W) * scale);
        }

        /// <summary>
        /// Get reflected vector
        /// </summary>
        /// <param name="normal">Normal vector</param>
        /// <returns></returns>
        public Vector4 ReflectBy(Vector4 normal) {
            return -2 * Dot(normal) * normal + this;
        }

        /// <summary>
        /// Get angle between this vector and another vector
        /// </summary>
        /// <param name="target">Target vector</param>
        /// <returns></returns>
        public float AngleBetween(Vector4 target) {
            return
                (float)
                (Math.Acos(MathExtended.Clamp(Normalized().Dot(target.Normalized()), -1, 1)) * MathExtended.UnitRadian);
        }

        /// <summary>
        /// Project this vector to another vector
        /// </summary>
        /// <param name="normal">Target vector</param>
        /// <returns></returns>
        public Vector4 ProjectTo(Vector4 normal) {
            return normal * Dot(normal) / normal.Dot(normal);
        }

        /// <summary>
        /// Zero vector
        /// </summary>
        /// <returns></returns>
        public static Vector4 Zero { get; } = new Vector4(0, 0, 0, 0);

        /// <summary>
        /// Unit vector
        /// </summary>
        /// <returns></returns>
        public static Vector4 Unit { get; } = new Vector4(1, 1, 1, 1);

        public bool Equals(Vector4 other) {
            return this == other;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector2 && Equals((Vector2) obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                hashCode = (hashCode * 397) ^ W.GetHashCode();
                return hashCode;
            }
        }
    }
}
