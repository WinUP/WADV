namespace WADV.Core.Utility {
    /// <summary>
    /// 3D rectangle
    /// </summary>
    public struct Rect3 {
        /// <summary>
        /// X coordinate of top-left point
        /// </summary>
        public float X;

        /// <summary>
        /// Y coordinate of top-left point
        /// </summary>
        public float Y;

        /// <summary>
        /// Z coordinate of top-left point
        /// </summary>
        public float Z;

        /// <summary>
        /// Width of rectanle
        /// </summary>
        public float Width;

        /// <summary>
        /// Height of rectangle
        /// </summary>
        public float Height;

        /// <summary>
        /// Depth of rectangle
        /// </summary>
        public float Depth;

        /// <summary>
        /// Get a new 3D rectangle
        /// </summary>
        /// <param name="x">X coordinate of top-left point</param>
        /// <param name="y">Y coordinate of top-left point</param>
        /// <param name="z">Z coordinate of top-left point</param>
        /// <param name="width">Width of rectanle</param>
        /// <param name="height">Height of rectangle</param>
        /// <param name="depth">Depth of rectangle</param>
        public Rect3(float x, float y, float z, float width, float height, float depth) {
            X = x;
            Y = y;
            Z = z;
            Width = width;
            Height = height;
            Depth = depth;
        }

        /// <summary>
        /// Get a new 3D rectangle
        /// </summary>
        /// <param name="x">X coordinate of top-left point</param>
        /// <param name="y">Y coordinate of top-left point</param>
        /// <param name="z">Z coordinate of top-left point</param>
        /// <param name="width">Width of rectanle</param>
        /// <param name="height">Height of rectangle</param>
        /// <param name="depth">Depth of rectangle</param>
        public Rect3(double x, double y, double z, double width, double height, double depth) {
            X = (float) x;
            Y = (float) y;
            Z = (float) z;
            Width = (float) width;
            Height = (float) height;
            Depth = (float) depth;
        }

        /// <summary>
        /// Get a new 3D rectangle
        /// </summary>
        /// <param name="x">X coordinate of top-left point</param>
        /// <param name="y">Y coordinate of top-left point</param>
        /// <param name="z">Z coordinate of top-left point</param>
        /// <param name="width">Width of rectanle</param>
        /// <param name="height">Height of rectangle</param>
        /// <param name="depth">Depth of rectangle</param>
        public Rect3(int x, int y, int z, int width, int height, int depth) {
            X = x;
            Y = y;
            Z = z;
            Width = width;
            Height = height;
            Depth = depth;
        }

        public static Rect3 operator +(Rect3 a, Point3 b) {
            return new Rect3(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.Width, a.Height, a.Depth);
        }

        public static Rect3 operator +(Rect3 a, Point2 b) {
            return new Rect3(a.X + b.X, a.Y + b.Y, a.Z, a.Width, a.Height, a.Depth);
        }

        public static Rect3 operator +(Rect3 a, Vector3 b) {
            return new Rect3(a.X, a.Y, a.Z, a.Width + b.X, a.Height + b.Y, a.Depth + b.Z);
        }

        public static Rect3 operator +(Rect3 a, Vector2 b) {
            return new Rect3(a.X, a.Y, a.Z, a.Width + b.X, a.Height + b.Y, a.Depth);
        }

        public static Rect3 operator +(Rect3 a, float b) {
            return new Rect3(a.X, a.Y, a.Z, a.Width + b, a.Height + b, a.Depth + b);
        }

        public static Rect3 operator +(Rect3 a, double b) {
            return a + (float) b;
        }

        public static Rect3 operator +(Rect3 a, int b) {
            return a + (float) b;
        }

        public static Rect3 operator -(Rect3 a, Point3 b) {
            return new Rect3(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.Width, a.Height, a.Depth);
        }

        public static Rect3 operator -(Rect3 a, Point2 b) {
            return new Rect3(a.X - b.X, a.Y - b.Y, a.Z, a.Width, a.Height, a.Depth);
        }

        public static Rect3 operator -(Rect3 a, Vector3 b) {
            return new Rect3(a.X, a.Y, a.Z, a.Width - b.X, a.Height - b.Y, a.Depth - b.Z);
        }

        public static Rect3 operator -(Rect3 a, Vector2 b) {
            return new Rect3(a.X, a.Y, a.Z, a.Width - b.X, a.Height - b.Y, a.Depth);
        }

        public static Rect3 operator -(Rect3 a, float b) {
            return new Rect3(a.X, a.Y, a.Z, a.Width - b, a.Height - b, a.Depth - b);
        }

        public static Rect3 operator -(Rect3 a, double b) {
            return a - (float) b;
        }

        public static Rect3 operator -(Rect3 a, int b) {
            return a - (float) b;
        }

        public static Rect3 operator *(Rect3 a, Point3 b) {
            return new Rect3(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.Width, a.Height, a.Depth);
        }

        public static Rect3 operator *(Rect3 a, Point2 b) {
            return new Rect3(a.X * b.X, a.Y * b.Y, a.Z, a.Width, a.Height, a.Depth);
        }

        public static Rect3 operator *(Rect3 a, Vector3 b) {
            return new Rect3(a.X, a.Y, a.Z, a.Width * b.X, a.Height * b.Y, a.Depth * b.Z);
        }

        public static Rect3 operator *(Rect3 a, Vector2 b) {
            return new Rect3(a.X, a.Y, a.Z, a.Width * b.X, a.Height * b.Y, a.Depth);
        }

        public static Rect3 operator *(Rect3 a, float b) {
            return new Rect3(a.X, a.Y, a.Z, a.Width * b, a.Height * b, a.Depth * b);
        }

        public static Rect3 operator *(Rect3 a, double b) {
            return a * (float) b;
        }

        public static Rect3 operator *(Rect3 a, int b) {
            return a * (float) b;
        }

        public static Rect3 operator /(Rect3 a, Point3 b) {
            return new Rect3(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.Width, a.Height, a.Depth);
        }

        public static Rect3 operator /(Rect3 a, Point2 b) {
            return new Rect3(a.X / b.X, a.Y / b.Y, a.Z, a.Width, a.Height, a.Depth);
        }

        public static Rect3 operator /(Rect3 a, Vector3 b) {
            return new Rect3(a.X, a.Y, a.Z, a.Width / b.X, a.Height / b.Y, a.Depth / b.Z);
        }

        public static Rect3 operator /(Rect3 a, Vector2 b) {
            return new Rect3(a.X, a.Y, a.Z, a.Width / b.X, a.Height / b.Y, a.Depth);
        }

        public static Rect3 operator /(Rect3 a, float b) {
            return new Rect3(a.X, a.Y, a.Z, a.Width / b, a.Height / b, a.Depth / b);
        }

        public static Rect3 operator /(Rect3 a, double b) {
            return a / (float) b;
        }

        public static Rect3 operator /(Rect3 a, int b) {
            return a / (float) b;
        }

        public static bool operator ==(Rect3 a, Rect3 b) {
            return a.X.Equals(b.X) && a.Y.Equals(b.Y) && a.Z.Equals(b.Z)
                   && a.Width.Equals(b.Width) && a.Height.Equals(b.Height) && a.Depth.Equals(b.Depth);
        }

        public static bool operator ==(Rect3 a, Rect2 b) {
            return a.X.Equals(b.X) && a.Y.Equals(b.Y) && a.Width.Equals(b.Width) && a.Height.Equals(b.Height);
        }

        public static bool operator ==(Rect3 a, Point3 b) {
            return a.X.Equals(b.X) && a.Y.Equals(b.Y) && a.Z.Equals(b.Z);
        }

        public static bool operator ==(Rect3 a, Point2 b) {
            return a.X.Equals(b.X) && a.Y.Equals(b.Y);
        }

        public static bool operator ==(Rect3 a, Vector3 b) {
            return a.Width.Equals(b.X) && a.Height.Equals(b.Y) && a.Depth.Equals(b.Z);
        }

        public static bool operator ==(Rect3 a, Vector2 b) {
            return a.Width.Equals(b.X) && a.Height.Equals(b.Y);
        }

        public static bool operator !=(Rect3 a, Rect3 b) {
            return !(a == b);
        }

        public static bool operator !=(Rect3 a, Rect2 b) {
            return !(a == b);
        }

        public static bool operator !=(Rect3 a, Point3 b) {
            return !(a == b);
        }

        public static bool operator !=(Rect3 a, Point2 b) {
            return !(a == b);
        }

        public static bool operator !=(Rect3 a, Vector3 b) {
            return !(a == b);
        }

        public static bool operator !=(Rect3 a, Vector2 b) {
            return !(a == b);
        }

        public static bool operator >(Rect3 a, Rect3 b) {
            return a.Width > b.Width && a.Height > b.Height && a.Depth > b.Depth;
        }

        public static bool operator >(Rect3 a, Rect2 b) {
            return a.Width > b.Width && a.Height > b.Height;
        }

        public static bool operator >(Rect3 a, Point3 b) {
            return a.X > b.X && a.Y > b.Y && a.Z > b.Z;
        }

        public static bool operator >(Rect3 a, Point2 b) {
            return a.X > b.X && a.Y > b.Y;
        }

        public static bool operator >(Rect3 a, Vector3 b) {
            return a.Width > b.X && a.Height > b.Y && a.Depth > b.Z;
        }

        public static bool operator >(Rect3 a, Vector2 b) {
            return a.Width > b.X && a.Height > b.Y;
        }

        public static bool operator <(Rect3 a, Rect3 b) {
            return a.Width < b.Width && a.Height < b.Height && a.Depth < b.Depth;
        }

        public static bool operator <(Rect3 a, Rect2 b) {
            return a.Width < b.Width && a.Height < b.Height;
        }

        public static bool operator <(Rect3 a, Point3 b) {
            return a.X < b.X && a.Y < b.Y && a.Z < b.Z;
        }

        public static bool operator <(Rect3 a, Point2 b) {
            return a.X < b.X && a.Y < b.Y;
        }

        public static bool operator <(Rect3 a, Vector3 b) {
            return a.Width < b.X && a.Height < b.Y && a.Depth < b.Y;
        }

        public static bool operator <(Rect3 a, Vector2 b) {
            return a.Width < b.X && a.Height < b.Y;
        }

        public static bool operator >=(Rect3 a, Rect3 b) {
            return a.Width >= b.Width && a.Height >= b.Height && a.Depth >= b.Depth;
        }

        public static bool operator >=(Rect3 a, Rect2 b) {
            return a.Width >= b.Width && a.Height >= b.Height;
        }

        public static bool operator >=(Rect3 a, Point3 b) {
            return a.X >= b.X && a.Y >= b.Y && a.Z >= b.Z;
        }

        public static bool operator >=(Rect3 a, Point2 b) {
            return a.X >= b.X && a.Y >= b.Y;
        }

        public static bool operator >=(Rect3 a, Vector3 b) {
            return a.Width >= b.X && a.Height >= b.Y && a.Depth >= b.Z;
        }

        public static bool operator >=(Rect3 a, Vector2 b) {
            return a.Width >= b.X && a.Height >= b.Y;
        }

        public static bool operator <=(Rect3 a, Rect3 b) {
            return a.Width <= b.Width && a.Height <= b.Height && a.Depth <= b.Depth;
        }

        public static bool operator <=(Rect3 a, Rect2 b) {
            return a.Width <= b.Width && a.Height <= b.Height;
        }

        public static bool operator <=(Rect3 a, Point3 b) {
            return a.X <= b.X && a.Y <= b.Y && a.Z <= b.Z;
        }

        public static bool operator <=(Rect3 a, Point2 b) {
            return a.X <= b.X && a.Y <= b.Y;
        }

        public static bool operator <=(Rect3 a, Vector3 b) {
            return a.Width <= b.X && a.Height <= b.Y && a.Depth <= b.Z;
        }

        public static bool operator <=(Rect3 a, Vector2 b) {
            return a.Width <= b.X && a.Height <= b.Y;
        }

        public static implicit operator Rect3(Point3 v) {
            return new Rect3(v.X, v.Y, v.Z, 0, 0, 0);
        }

        public static implicit operator Rect3(Point2 v) {
            return new Rect3(v.X, v.Y, 0, 0, 0, 0);
        }

        public static implicit operator Rect3(Vector3 v) {
            return new Rect3(0, 0, 0, v.X, v.Y, v.Z);
        }

        public static implicit operator Rect3(Vector2 v) {
            return new Rect3(0, 0, 0, v.X, v.Y, 0);
        }

        public static implicit operator Rect3(Rect2 v) {
            return new Rect3(v.X, v.Y, 0, v.Width, v.Height, 0);
        }

        /// <summary>
        /// Get or set the point of center
        /// </summary>
        public Point3 Center {
            get { return new Point3(X + Width / 2, Y + Height / 2, Z + Depth / 2); }
            set {
                X = value.X - Width / 2;
                Y = value.Y - Height / 2;
                Z = value.Z - Depth / 2;
            }
        }

        /// <summary>
        /// Get the point of this rectangle's inner top-left
        /// </summary>
        /// <returns></returns>
        public Point3 InnerTopLeft {
            get { return new Point3(X, Y, Z); }
            set {
                X = value.X;
                Y = value.Y;
                Z = value.Z;
            }
        }

        /// <summary>
        /// Get the point of this rectangle's inner top-right
        /// </summary>
        /// <returns></returns>
        public Point3 InnerTopRight {
            get { return new Point3(X + Width, Y, Z); }
            set {
                X = value.X - Width;
                Y = value.Y;
                Z = value.Z;
            }
        }

        /// <summary>
        /// Get the point of this rectangle's inner bottom-left
        /// </summary>
        /// <returns></returns>
        public Point3 InnerBottomLeft {
            get { return new Point3(X, Y + Height, Z); }
            set {
                X = value.X;
                Y = value.Y - Height;
                Z = value.Z;
            }
        }

        /// <summary>
        /// Get the point of this rectangle's inner bottom-right
        /// </summary>
        /// <returns></returns>
        public Point3 InnerBottomRight {
            get { return new Point3(X + Width, Y + Height, Z); }
            set {
                X = value.X - Width;
                Y = value.Y - Height;
                Z = value.Z;
            }
        }

        /// <summary>
        /// Get the point of this rectangle's outer top-left
        /// </summary>
        /// <returns></returns>
        public Point3 OuterTopLeft {
            get { return new Point3(X, Y, Z + Depth); }
            set {
                X = value.X;
                Y = value.Y;
                Z = value.Z - Depth;
            }
        }

        /// <summary>
        /// Get the point of this rectangle's outer top-right
        /// </summary>
        /// <returns></returns>
        public Point3 OuterTopRight {
            get { return new Point3(X + Width, Y, Z + Depth); }
            set {
                X = value.X - Width;
                Y = value.Y;
                Z = value.Z - Depth;
            }
        }

        /// <summary>
        /// Get the point of this rectangle's outer bottom-left
        /// </summary>
        /// <returns></returns>
        public Point3 OuterBottomLeft {
            get { return new Point3(X, Y + Height, Z + Depth); }
            set {
                X = value.X;
                Y = value.Y - Height;
                Z = value.Z - Depth;
            }
        }

        /// <summary>
        /// Get the point of this rectangle's outer bottom-right
        /// </summary>
        /// <returns></returns>
        public Point3 OuterBottomRight {
            get { return new Point3(X + Width, Y + Height, Z + Depth); }
            set {
                X = value.X - Width;
                Y = value.Y - Height;
                Z = value.Z - Depth;
            }
        }

        /// <summary>
        /// Get 2D rectangle based on this rectangle
        /// </summary>
        /// <returns></returns>
        public Rect2 ToRect2() {
            return new Rect2(X, Y, Width, Height);
        }

        /// <summary>
        /// Get the size of this rectangle
        /// </summary>
        /// <returns></returns>
        public Vector3 Size() {
            return new Vector3(Width, Height, Depth);
        }

        /// <summary>
        /// Indicate that if a point is covered by this
        /// </summary>
        /// <param name="point">Target point</param>
        /// <returns></returns>
        public bool Contains(Point2 point) {
            return point >= InnerTopLeft.ToPoint2() && point <= InnerBottomRight.ToPoint2();
        }

        /// <summary>
        /// Indicate that if a 2D rectangle is covered by this
        /// </summary>
        /// <param name="rect">Target rectangle</param>
        /// <returns></returns>
        public bool Contains(Rect2 rect) {
            return rect.TopLeft <= InnerTopLeft.ToPoint2() && rect.BottomRight <= InnerBottomRight.ToPoint2();
        }

        /// <summary>
        /// Indicate that if a 3D rectangle is covered by this
        /// </summary>
        /// <param name="rect">Target rectangle</param>
        /// <returns></returns>
        public bool Contains(Rect3 rect) {
            return rect.InnerTopLeft <= InnerTopLeft && rect.OuterBottomLeft <= InnerBottomRight;
        }

        /// <summary>
        /// Indicate that if a 2D rectangle shares same area with this
        /// </summary>
        /// <param name="target">Target rectangle</param>
        /// <returns></returns>
        public bool IsOverlap(Rect2 target) {
            var distance = Center.ToPoint2().DistanceTo(target.Center);
            return (distance <= (Width + target.Width) / 2 && distance <= (Height + target.Height) / 2) ? true : false;
        }

        /// <summary>
        /// Indicate that if a 3D rectangle shares same area with this
        /// </summary>
        /// <param name="target">Target rectangle</param>
        /// <returns></returns>
        public bool IsOverlap(Rect3 target) {
            var distance = Center.DistanceTo(target.Center);
            return (distance <= (Width + target.Width) / 2 &&
                    distance <= (Height + target.Height) / 2 &&
                    distance <= (Depth + target.Depth) / 2)
                ? true
                : false;
        }

        public bool Equals(Rect3 other) {
            return this == other;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Rect3 && Equals((Rect3) obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                hashCode = (hashCode * 397) ^ Width.GetHashCode();
                hashCode = (hashCode * 397) ^ Height.GetHashCode();
                hashCode = (hashCode * 397) ^ Depth.GetHashCode();
                return hashCode;
            }
        }
    }
}