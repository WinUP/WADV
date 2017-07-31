using System;
using WADV.Core.Exception;

// This file's code is from net47/wpf/src/Shared/MS/internal/MatrixUtil.cs

namespace WADV.Core.Utility {
    /// <summary>
    /// 2D transform matrix
    /// </summary>
    public struct Matrix2 {
        private float _m11, _m12, _m21, _m22, _m31, _m32;

        public Matrix2(float m11, float m12, float m21, float m22, float m31, float m32) {
            _m11 = m11;
            _m12 = m12;
            _m21 = m21;
            _m22 = m22;
            _m31 = m31;
            _m32 = m32;
            Type = MatrixType.Regular;
        }

        /// <summary>
        /// [0, 0] position of matrix
        /// </summary>
        public float M11 {
            get => _m11;
            set {
                _m11 = value;
                if (Type == MatrixType.Identity)
                    Type = MatrixType.Scaling;
                else if (Type != MatrixType.Regular)
                    Type |= MatrixType.Scaling;
            }
        }

        /// <summary>
        /// [0, 1] position of matrix
        /// </summary>
        public float M12 {
            get => _m12;
            set {
                _m12 = value;
                Type = MatrixType.Regular;
            }
        }

        /// <summary>
        /// [1, 0] position of matrix
        /// </summary>
        public float M21 {
            get => _m21;
            set {
                _m21 = value;
                Type = MatrixType.Regular;
            }
        }

        /// <summary>
        /// [1, 1] position of matrix
        /// </summary>
        public float M22 {
            get => _m22;
            set {
                _m22 = value;
                if (Type == MatrixType.Identity)
                    Type = MatrixType.Scaling;
                else if (Type != MatrixType.Regular)
                    Type |= MatrixType.Scaling;
            }
        }

        /// <summary>
        /// [2, 0] position of matrix
        /// </summary>
        public float M31 {
            get => _m31;
            set {
                _m31 = value;
                if (Type == MatrixType.Identity)
                    Type = MatrixType.Translation;
                else if (Type != MatrixType.Regular)
                    Type |= MatrixType.Translation;
            }
        }

        /// <summary>
        /// [2, 1] position of matrix
        /// </summary>
        public float M32 {
            get => _m32;
            set {
                _m32 = value;
                if (Type == MatrixType.Identity)
                    Type = MatrixType.Translation;
                else if (Type != MatrixType.Regular)
                    Type |= MatrixType.Translation;
            }
        }

        /// <summary>
        /// Matrix type
        /// </summary>
        public MatrixType Type { get; private set; }

        /// <summary>
        /// Indicate if this matrix has inverted matrix
        /// </summary>
        public bool HasInvert => !Determinant.Equals(0);

        /// <summary>
        /// Determinant of this matrix
        /// </summary>
        public float Determinant {
            get {
                switch (Type) {
                    case MatrixType.Identity:
                    case MatrixType.Translation:
                        return 1;
                    case MatrixType.Scaling:
                    case MatrixType.ScalingAndTranslation:
                        return M11 * M22;
                    default:
                        return M11 * M22 - M12 * M21;
                }
            }
        }

        /// <summary>
        /// Get this matrix's inverted matrix
        /// </summary>
        public Matrix2 InvertedMatrix {
            get {
                if (!HasInvert)
                    throw GameException.New(ExceptionType.TryInverstUninvertibleMatrix)
                        .Value("Matrix", ToString())
                        .At("Utility.Matrix2")
                        .How("Try inverst uninvertible matrix")
                        .Save();
                var determinate = Determinant;
                switch (Type) {
                    case MatrixType.Identity:
                        return this;
                    case MatrixType.Scaling:
                        return new Matrix2(1 / M11, M12, M21, 1 / M22, M31, M32);
                    case MatrixType.Translation:
                        return new Matrix2(M11, M12, M21, M22, -M31, -M32);
                    case MatrixType.ScalingAndTranslation:
                        return new Matrix2(1 / M11, M12, M21, 1 / M22, -M31 * M11, -M32 * M22);
                    default:
                        determinate = 1 / determinate;
                        return new Matrix2(M22 * determinate, -M12 * determinate,
                            -M21 * determinate, M11 * determinate,
                            (M21 * M32 - M31 * M22) * determinate, (M31 * M12 - M11 * M32) * determinate);
                }
            }
        }

        /// <summary>
        /// Return a new matrix which is the result of append a matrix to this matrix
        /// </summary>
        /// <param name="matrix">Target matrix</param>
        /// <returns></returns>
        public Matrix2 Append(Matrix2 matrix) {
            return this * matrix;
        }

        /// <summary>
        /// Return a new matrix which is the result of prepend a matrix to this matrix
        /// </summary>
        /// <param name="matrix">Target matrix</param>
        /// <returns></returns>
        public Matrix2 Prepend(Matrix2 matrix) {
            return matrix * this;
        }

        /// <summary>
        /// Return a new matrix which is the result of rotate an angle of this matrix
        /// </summary>
        /// <param name="angle">Angle</param>
        /// <param name="centerX">Rotate center X component</param>
        /// <param name="centerY">Rotate center Y component</param>
        /// <returns></returns>
        public Matrix2 Rotate(float angle, float centerX = 0, float centerY = 0) {
            return this * GenerateRotateMatrix(angle, centerX, centerY);
        }

        /// <summary>
        /// Return a new matrix which is the result of append this matrix to a rotate matrix
        /// </summary>
        /// <param name="angle">Angle</param>
        /// <param name="centerX">Rotate center X component</param>
        /// <param name="centerY">Rotate center Y component</param>
        /// <returns></returns>
        public Matrix2 RotatePrepend(float angle, float centerX = 0, float centerY = 0) {
            return GenerateRotateMatrix(angle, centerX, centerY) * this;
        }

        /// <summary>
        /// Return a new matrix which is the result of scale a rate of this matrix
        /// </summary>
        /// <param name="scaleX">Scale rate of X component</param>
        /// <param name="scaleY">Scale rate of Y component</param>
        /// <param name="centerX">Scale center X component</param>
        /// <param name="centerY">Scale center Y conponent</param>
        /// <returns></returns>
        public Matrix2 Scale(float scaleX, float scaleY, float centerX = 0, float centerY = 0) {
            return this * GenerateScaleMatrix(scaleX, scaleY, centerX, centerY);
        }

        /// <summary>
        /// Return a new matrix which is the result of append this matrix to a scale matrix
        /// </summary>
        /// <param name="scaleX">Scale rate of X component</param>
        /// <param name="scaleY">Scale rate of Y component</param>
        /// <param name="centerX">Scale center X component</param>
        /// <param name="centerY">Scale center Y conponent</param>
        /// <returns></returns>
        public Matrix2 ScalePrepend(float scaleX, float scaleY, float centerX = 0, float centerY = 0) {
            return GenerateScaleMatrix(scaleX, scaleY, centerX, centerY) * this;
        }

        /// <summary>
        /// Return a new matrix which is the result of skew a rate of this matrix
        /// </summary>
        /// <param name="skewX">Skew rate of X component</param>
        /// <param name="skewY">Skew rate of Y component</param>
        /// <returns></returns>
        public Matrix2 Skew(float skewX, float skewY) {
            return this * GenerateSkewMatrix(skewX, skewY);
        }

        /// <summary>
        /// Return a new matrix which is the result of append this matrix to a skew matrix
        /// </summary>
        /// <param name="skewX">Skew rate of X component</param>
        /// <param name="skewY">Skew rate of Y component</param>
        /// <returns></returns>
        public Matrix2 SkewPrepend(float skewX, float skewY) {
            return GenerateSkewMatrix(skewX, skewY) * this;
        }

        /// <summary>
        /// Return a new matrix which is the result of translate a distance of this matrix
        /// </summary>
        /// <param name="transateX">Translate distance of X component</param>
        /// <param name="translateY">Translate distance of Y component</param>
        /// <returns></returns>
        public Matrix2 Translate(float transateX, float translateY) {
            return this * GenerateTranslateMatrix(transateX, translateY);
        }

        /// <summary>
        /// Return a new matrix which is the result of append this matrix to a translate matrix
        /// </summary>
        /// <param name="transateX">Translate distance of X component</param>
        /// <param name="translateY">Translate distance of Y component</param>
        /// <returns></returns>
        public Matrix2 TranslatePrepand(float transateX, float translateY) {
            return GenerateTranslateMatrix(transateX, translateY) * this;
        }

        public static Matrix2 operator +(Matrix2 a, Matrix2 b) {
            return new Matrix2(a.M11 + b.M11, a.M12 + b.M12, a.M21 + b.M21, a.M22 + b.M22, a.M31 + b.M31, a.M32 + b.M32);
        }

        public static Matrix2 operator +(Matrix2 a, Vector2 b) {
            return new Matrix2(a.M11, a.M12, a.M21, a.M22, a.M31 + b.X, a.M32 + b.Y);
        }

        public static Matrix2 operator -(Matrix2 a, Matrix2 b) {
            return new Matrix2(a.M11 - b.M11, a.M12 - b.M12, a.M21 - b.M21, a.M22 - b.M22, a.M31 - b.M31, a.M32 - b.M32);
        }

        public static Matrix2 operator -(Matrix2 a, Vector2 b) {
            return new Matrix2(a.M11, a.M12, a.M21, a.M22, a.M31 - b.X, a.M32 - b.Y);
        }

        public static Matrix2 operator *(Matrix2 a, Matrix2 b) {
            if (a.Type == MatrixType.Identity || b.Type == MatrixType.Identity)
                return a;
            if (b.Type == MatrixType.Translation)
                return new Matrix2(a.M11, a.M12, a.M21, a.M22, a.M31 + b.M31, a.M32 + b.M32);
            if (a.Type == MatrixType.Translation)
                return new Matrix2(b.M11, b.M12, b.M21, b.M22, a.M31 * b.M11 + a.M32 * b.M21 + b.M31,
                    a.M31 * b.M12 + a.M32 * b.M22 + b.M32);
            var resultType = ((int) a.Type << 4) | (int) b.Type;
            switch (resultType) {
                case 0B00100010: // Scale * Scale
                    return new Matrix2(a.M11 * b.M11, a.M12, a.M21, a.M22 * b.M22, a.M31, a.M32);
                case 0B00100011: // Scale * (Scale | Translate)
                    return new Matrix2(a.M11 * b.M11, a.M12, a.M21, a.M22 * b.M22, a.M31 * b.M31, a.M32 * b.M32);
                case 0B00110010: // (Scale | Translate) * Scale
                    return new Matrix2(a.M11 * b.M11, a.M12, a.M21, a.M22 * b.M22, a.M31 * b.M11, a.M32 * b.M22);
                case 0B00110011: // (Scale | Translate) * (Scale | Translate)
                    return new Matrix2(a.M11 * b.M11, a.M12, a.M21, a.M22 * b.M22, b.M11 * a.M31 + b.M31,
                        b.M22 * a.M32 + b.M32);
                default: // Others
                    return new Matrix2(
                        a.M11 * b.M11 + a.M12 * b.M21, a.M11 * b.M12 + a.M12 * b.M22,
                        a.M21 * b.M11 + a.M22 * b.M21, a.M21 * b.M12 + a.M22 * b.M22,
                        a.M31 * b.M11 + a.M32 * b.M21 + b.M31, a.M31 * b.M12 + a.M32 * b.M22 + b.M32);
            }
        }

        public static Point2 operator *(Matrix2 a, Point2 b) {
            switch (a.Type) {
                case MatrixType.Identity:
                    return b;
                case MatrixType.Translation:
                    return new Point2(b.X + a.M31, b.Y + a.M32);
                case MatrixType.Scaling:
                    return new Point2(b.X * a.M11, b.Y * a.M22);
                case MatrixType.ScalingAndTranslation:
                    return new Point2(b.X * a.M11 + a.M31, b.Y * a.M22 + a.M32);
                default:
                    return new Point2(b.X * a.M11 + b.Y * a.M21 + a.M31, b.Y * a.M22 + b.X * a.M12 + a.M32);
            }
        }

        public static Vector2 operator *(Matrix2 a, Vector2 b) {
            switch (a.Type)
            {
                case MatrixType.Identity:
                case MatrixType.Translation:
                    return b;
                case MatrixType.Scaling:
                case MatrixType.ScalingAndTranslation:
                    return new Vector2(b.X * a.M11, b.Y * a.M22);
                default:
                    return new Vector2(b.X * a.M11 + b.Y * a.M21, b.Y * a.M22 + b.X * a.M12);
            }
        }

        public static Rect2 operator *(Matrix2 a, Rect2 b) {
            if (a.Type == MatrixType.Identity)
                return b;
            var rect = new Rect2(b.X, b.Y, b.Width, b.Height);
            if ((a.Type & MatrixType.Scaling) != 0) {
                rect.X *= a.M11;
                rect.Y *= a.M22;
                rect.Width *= a.M11;
                rect.Height *= a.M22;
                if (rect.Width < 0.0f) {
                    rect.X += rect.Width;
                    rect.Width = -rect.Width;
                }
                if (rect.Height < 0.0) {
                    rect.Y += rect.Height;
                    rect.Height = -rect.Height;
                }
            }
            if ((a.Type & MatrixType.Translation) != 0) {
                rect.X += a.M31;
                rect.Y += a.M32;
            }
            if (a.Type != MatrixType.Regular) return rect;
            var point0 = a * rect.TopLeft;
            var point1 = a * rect.TopRight;
            var point2 = a * rect.BottomRight;
            var point3 = a * rect.BottomLeft;
            rect.X = Math.Min(Math.Min(point0.X, point1.X), Math.Min(point2.X, point3.X));
            rect.Y = Math.Min(Math.Min(point0.Y, point1.Y), Math.Min(point2.Y, point3.Y));
            rect.Width = Math.Max(Math.Max(point0.X, point1.X), Math.Max(point2.X, point3.X)) - rect.X;
            rect.Height = Math.Max(Math.Max(point0.Y, point1.Y), Math.Max(point2.Y, point3.Y)) - rect.Y;
            return rect;
        }

        public static Matrix2 operator /(Matrix2 a, Matrix2 b) {
            return a * b.InvertedMatrix;
        }

        /// <summary>
        /// Instance of identity matrix
        /// </summary>
        public static Matrix2 IdentityMatrix { get; } = new Matrix2(1, 0, 0, 1, 0, 0);

        public static Matrix2 GenerateRotateMatrix(float angle, float centerX, float centerY) {
            angle %= 360.0f;
            var sin = (float) Math.Sin(angle);
            var cos = (float)Math.Cos(angle);
            var dx = (centerX * (1.0f - cos)) + (centerY * sin);
            var dy = (centerY * (1.0f - cos)) - (centerX * sin);
            return new Matrix2(cos, sin, -sin, cos, dx, dy);
        }

        public static Matrix2 GenerateScaleMatrix(float scaleX, float scaleY, float centerX, float centerY) {
            return new Matrix2(scaleX, 0, 0, scaleY, centerX - scaleX * centerX, centerY - scaleY * centerY);
        }

        public static Matrix2 GenerateSkewMatrix(float skewX, float skewY) {
            return new Matrix2(1, (float) Math.Tan(skewY), (float)Math.Tan(skewX), 1, 0, 0);
        }

        public static Matrix2 GenerateTranslateMatrix(float transateX, float translateY) {
            return new Matrix2(1, 0, 0, 1, transateX, translateY);
        }

        public override string ToString() {
            return $"Matrix2 [{M11}, {M12}, {M21}, {M22}, {M31}, {M32}]";
        }

        private void DetactMatrixType() {
            if (!(_m21.Equals(0) && _m12.Equals(0)))
                return;
            if(!(_m11.Equals(1) && _m22.Equals(1)))
                Type = MatrixType.Scaling;
            if (!(M31.Equals(0) && M32.Equals(0)))
                Type |= MatrixType.Translation;
            if ((Type & MatrixType.ScalingAndTranslation) == 0) {
                Type = MatrixType.Identity;
            }
        }

        [Flags]
        public enum MatrixType {
            Identity = 0,
            Translation = 1,
            Scaling = 2,
            ScalingAndTranslation = 3,
            Regular = 4
        }
    }
}
