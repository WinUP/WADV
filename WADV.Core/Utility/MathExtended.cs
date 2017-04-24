using System;

namespace WADV.Core.Utility {
    public sealed class MathExtended {
        public static float UnitRadian { get; } = (float) (180 / Math.PI);

        public static T Clamp<T>(T value, T min, T max, Func<T, T, int> comparer) {
            if (comparer.Invoke(value, min) < 0) return min;
            return comparer.Invoke(value, max) > 0 ? max : value;
        }

        public static int Clamp(int value, int min, int max) {
            return Clamp(value, min, max, (v1, v2) => v1 - v2);
        }

        public static float Clamp(float value, float min, float max) {
            return Clamp(value, min, max, (v1, v2) => {
                if (v1.Equals(v2)) return 0;
                return v1 - v2 < 0 ? -1 : 1;
            });
        }
    }
}