using System.Collections.Generic;
using System.Linq;
using ImageRecognition.Helpers;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Processing.Filters
{
    public class FillHoles: MatriceFilter
    {
        public FillHoles(int size = 3)
            : base(size) { }

        protected override Vec3b Filter(Vec3b[,] arr)
        {
            var vectors = arr.ToList();
            var max = new Vec3b(vectors.Max(x => x.Item0),
                vectors.Max(x => x.Item1),
                vectors.Max(x => x.Item2));
            if ((arr[0, 1].IsEqual(max) && arr[2, 1].IsEqual(max)) ||
                (arr[1, 0].IsEqual(max) && arr[1, 2].IsEqual(max)))
                return max;
            return arr[1, 1];
        }
    }
}
