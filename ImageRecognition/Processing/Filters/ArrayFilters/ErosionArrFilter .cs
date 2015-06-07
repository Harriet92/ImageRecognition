using System.Collections.Generic;
using System.Linq;
using ImageRecognition.Helpers;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Processing.Filters
{
    public class ErosionArrFilter  : ArrayFilter
    {
        public ErosionArrFilter(int size = 3)
            : base(size) { }

        protected override int Filter(int[,] vectorsArr)
        {
            var vectors = vectorsArr.ToList();
            return vectors.Min(x => x);
        }
    }
}
