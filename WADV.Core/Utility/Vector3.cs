using System;
using WADV.Core.Exception;

namespace WADV.Core.Utility {
    public struct Vector3 {
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
        /// Get a new 3D vector
        /// </summary>
        /// <param name="x">X component</param>
        /// <param name="y">Y component</param>
        /// <param name="z">Z component</param>
        public Vector3(float x, float y, float z) {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Get a new 3D vector
        /// </summary>
        /// <param name="x">X component</param>
        /// <param name="y">Y component</param>
        /// <param name="z">Z component</param>
        public Vector3(double x, double y, double z) {
            X = (float) x;
            Y = (float) y;
            Z = (float) z;
        }

        /// <summary>
        /// Get a new 3D vector
        /// </summary>
        /// <param name="x">X component</param>
        /// <param name="y">Y component</param>
        /// <param name="z">Z component</param>
        public Vector3(int x, int y, int z) {
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

        public static Vector3 operator +(Vector3 a, Vector3 b) {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3 operator +(Vector3 a, Vector2 b) {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z);
        }

        public static Vector3 operator +(Vector3 a, float b) {
            return new Vector3(a.X + b, a.Y + b, a.Z + b);
        }

        public static Vector3 operator +(Vector3 a, double b) {
            return a + (float) b;
        }

        public static Vector3 operator +(Vector3 a, int b) {
            return a + (float) b;
        }

        public static Vector3 operator -(Vector3 a, Vector3 b) {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3 operator -(Vector3 a, Vector2 b) {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z);
        }

        public static Vector3 operator -(Vector3 a, float b) {
            return new Vector3(a.X - b, a.Y - b, a.Z - b);
        }

        public static Vector3 operator -(Vector3 a, double b) {
            return a - (float) b;
        }

        public static Vector3 operator -(Vector3 a, int b) {
            return a - (float) b;
        }

        public static Vector3 operator *(Vector3 a, Vector3 b) {
            return new Vector3(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
        }

        public static Vector3 operator *(Vector3 a, Vector2 b) {
            return new Vector3(-a.Z * b.Y, a.Z * b.X, a.X * b.Y - a.Y * b.X);
        }

        public static Vector3 operator *(Vector3 a, float b) {
            return new Vector3(a.X * b, a.Y * b, a.Z * b);
        }

        public static Vector3 operator *(Vector3 a, double b) {
            return a * (float) b;
        }

        public static Vector3 operator *(Vector3 a, int b) {
            return a * (float) b;
        }

        public static Vector3 operator /(Vector3 a, float b) {
            return new Vector3(a.X / b, a.Y / b, a.Z / b);
        }

        public static Vector3 operator /(Vector3 a, double b) {
            return a / (float) b;
        }

        public static Vector3 operator /(Vector3 a, int b) {
            return a / (float)b;
        }

        public static bool operator ==(Vector3 a, Vector3 b) {
            return a.X.Equals(b.X) && a.Y.Equals(b.Y) && a.Z.Equals(b.Z);
        }

        public static bool operator ==(Vector3 a, float b) {
            return a.Mod().Equals(b);
        }

        public static bool operator !=(Vector3 a, Vector3 b) {
            return !(a == b);
        }

        public static bool operator !=(Vector3 a, float b) {
            return !(a == b);
        }

        public static bool operator >(Vector3 a, Vector3 b) {
            return a.Mod2() > b.Mod2();
        }

        public static bool operator >(Vector3 a, float b) {
            return a.Mod() > b;
        }

        public static bool operator <(Vector3 a, Vector3 b) {
            return a.Mod2() < b.Mod2();
        }

        public static bool operator <(Vector3 a, float b) {
            return a.Mod() < b;
        }

        public static bool operator >=(Vector3 a, Vector3 b) {
            return a.Mod2() >= b.Mod2();
        }

        public static bool operator >=(Vector3 a, float b) {
            return a.Mod() >= b;
        }

        public static bool operator <=(Vector3 a, Vector3 b) {
            return a.Mod2() <= b.Mod2();
        }

        public static bool operator <=(Vector3 a, float b) {
            return a.Mod() <= b;
        }

        public static implicit operator Vector3(float v) {
            return new Vector3(v, v, v);
        }

        public static implicit operator Vector3(double v) {
            return new Vector3(v, v, v);
        }

        public static implicit operator Vector3(int v) {
            return new Vector3(v, v, v);
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
            return (double) X * X + (double) Y * Y + (double) Z * Z;
        }

        /// <summary>
        /// Reset this vector
        /// </summary>
        /// <param name="x">New x component</param>
        /// <param name="y">New y component</param>
        /// <param name="z">New z component</param>
        public void Reset(float x, float y, float z) {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Dot multiple with another vector
        /// </summary>
        /// <param name="b">Target vector</param>
        /// <returns></returns>
        public float Dot(Vector3 b) {
            return X * b.X + Y * b.Y + Z * b.Z;
        }

        /// <summary>
        /// Normalize this vector
        /// </summary>
        public Vector3 Normalized() {
            return this / Mod();
        }

        /// <summary>
        /// Lerp between this vector and another vector
        /// </summary>
        /// <param name="target">Target vector</param>
        /// <param name="scale">scale number (0~1)</param>
        /// <returns></returns>
        public Vector3 LerpTo(Vector3 target, float scale) {
            scale = MathExtended.Clamp(scale, 0, 1);
            return new Vector3(X + (target.X - X) * scale, Y + (target.Y - Y) * scale, Z + (target.Z - Z) * scale);
        }

        /// <summary>
        /// Get reflected vector
        /// </summary>
        /// <param name="normal">Normal vector</param>
        /// <returns></returns>
        public Vector3 ReflectBy(Vector3 normal) {
            return -2 * Dot(normal) * normal + this;
        }

        /// <summary>
        /// Get angle between this vector and another vector
        /// </summary>
        /// <param name="target">Target vector</param>
        /// <returns></returns>
        public float AngleBetween(Vector3 target) {
            return
                (float)
                (Math.Acos(MathExtended.Clamp(Normalized().Dot(target.Normalized()), -1, 1)) * MathExtended.UnitRadian);
        }

        /// <summary>
        /// Project this vector to another vector
        /// </summary>
        /// <param name="normal">Target vector</param>
        /// <returns></returns>
        public Vector3 ProjectTo(Vector3 normal) {
            return normal * Dot(normal) / normal.Dot(normal);
        }

        /// <summary>
        /// Zero vector
        /// </summary>
        /// <returns></returns>
        public static Vector3 Zero { get; } = new Vector3(0, 0, 0);

        /// <summary>
        /// Unit vector
        /// </summary>
        /// <returns></returns>
        public static Vector3 Unit { get; } = new Vector3(1, 1, 1);

        /// <summary>
        /// Left direction unit vector
        /// </summary>
        public static Vector3 Left { get; } = new Vector3(-1, 0, 0);

        /// <summary>
        /// Right direction unit vector
        /// </summary>
        public static Vector3 Right { get; } = new Vector3(1, 0, 0);

        /// <summary>
        /// Up direction unit vector
        /// </summary>
        public static Vector3 Up { get; } = new Vector3(0, 1, 0);

        /// <summary>
        /// Down direction unit vector
        /// </summary>
        public static Vector3 Down { get; } = new Vector3(0, -1, 0);

        /// <summary>
        /// Back direction unit vector
        /// </summary>
        public static Vector3 Back { get; } = new Vector3(0, 0, -1);

        /// <summary>
        /// Forward direction unit vector
        /// </summary>
        public static Vector3 Forward { get; } = new Vector3(0, 0, 1);

        public bool Equals(Vector3 other) {
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
                return hashCode;
            }
        }
    }
}