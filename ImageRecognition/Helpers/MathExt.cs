using System.Collections.Generic;
using System.Linq;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Helpers
{
    public static class MathExt
    {
        public static T Median<T>(this IEnumerable<T> collection)
        {
            if (!collection.Any())
                return default(T);
            if (collection.Count() == 1)
                return collection.ElementAt(0);
            collection = collection.OrderBy(x => x);
            return collection.ElementAt(collection.Count() / 2);
        }

        public static int Trunc(this int val)
        {
            if (val < 0) return 0;
            if (val > 255) return 255;
            return val;
        }

        public static Vec3b MuliplyBy(this Vec3b value, int multiplier)
        {
            return new Vec3b((byte)(value.Item0 * multiplier).Trunc(), (byte)(value.Item1 * multiplier).Trunc(), (byte)(value.Item2 * multiplier).Trunc());
        }

        public static Vec3b Add(this Vec3b value, Vec3b value2)
        {
            return new Vec3b((byte)(value.Item0 + value2.Item0).Trunc(), (byte)(value.Item1 + value2.Item1).Trunc(), (byte)(value.Item2 + value2.Item2).Trunc());
        }
        public static bool IsInRange(this Vec3b value, Vec3b min, Vec3b max)
        {
            return value.Item0 >= min.Item0 && value.Item1 >= min.Item1 && value.Item2 >= min.Item2 &&
                   value.Item0 <= max.Item0 && value.Item1 <= max.Item1 && value.Item2 <= max.Item2;
        }
        public static bool IsInRange(this double value, double min, double max)
        {
            return value >= min && value <= max;
        }
    }
}
