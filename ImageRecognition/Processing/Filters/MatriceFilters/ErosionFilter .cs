using System.Collections.Generic;
using System.Linq;
using ImageRecognition.Helpers;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Processing.Filters
{
    public class ErosionFilter : MatriceFilter
    {
        public ErosionFilter(int size = 3)
            : base(size) { }

        protected override Vec3b Filter(Vec3b[,] vectorsArr)
        {
            var vectors = vectorsArr.ToList();
            return new Vec3b(vectors.Min(x => x.Item0),
                vectors.Min(x => x.Item1),
                vectors.Min(x => x.Item2));
        }
    }
}
