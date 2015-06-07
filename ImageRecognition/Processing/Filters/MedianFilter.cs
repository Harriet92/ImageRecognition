using System.Collections.Generic;
using System.Linq;
using ImageRecognition.Helpers;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Processing.Filters
{
    public class MedianFilter: MatriceFilter
    {
        public MedianFilter(int size = 3)
            : base(size) { }

        protected override Vec3b Filter(Vec3b[,] vectorsArr)
        {
            var vectors = vectorsArr.ToList();
            return new Vec3b(vectors.Select(x => x.Item0).Median(),
                vectors.Select(x => x.Item1).Median(),
                vectors.Select(x => x.Item2).Median());
        }
    }
}
