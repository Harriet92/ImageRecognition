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
        public static Vec3i Subtract(this Vec3i value, Vec3i value2)
        {
            return new Vec3i(value.Item0 - value2.Item0, value.Item1 - value2.Item1, value.Item2 - value2.Item2);
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

        public static Vec3i[,] Multiply(this Vec3b[,] vecArr, int[,] intArr)
        {
            Vec3i[,] result = new Vec3i[vecArr.GetLength(0), vecArr.GetLength(1)];
            if (vecArr.GetLength(0) != intArr.GetLength(0) ||
                vecArr.GetLength(1) != intArr.GetLength(1))
                return null;
            for (int x = 0; x < vecArr.GetLength(0); x++)
            {
                for (int y = 0; y < vecArr.GetLength(1); y++)
                {
                    result[x, y].Item0 = vecArr[x, y].Item0 * intArr[x, y];
                    result[x, y].Item1 = vecArr[x, y].Item1 * intArr[x, y];
                    result[x, y].Item2 = vecArr[x, y].Item2 * intArr[x, y];
                }
            }
            return result;
        }

        public static Vec3b ToByte(this Vec3i v)
        {
            return new Vec3b((byte) v.Item0.Trunc(), (byte) v.Item1.Trunc(), (byte) v.Item2.Trunc());
        }
    }
}
