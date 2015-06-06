using System;
using System.Collections.Generic;
using System.Linq;
using ImageRecognition.Helpers;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Processing.Filters
{
    public class SharpeningFilter:MatriceFilter
    {
        private readonly int neighbourMult;
        private readonly int centerMult;
        public SharpeningFilter(int _neighbourMult, int _centerMult, int size = 3)
            : base(size)
        {
            neighbourMult = _neighbourMult;
            centerMult = _centerMult;
        }
        public override Mat ApplyFilter(Mat I)
        {
            Mat result = new Mat(I.Rows, I.Cols, MatType.CV_8UC3);
            var rIndexer = MatExt.GetMatIndexer(result);
            var iIndexer = MatExt.GetMatIndexer(I);
            for (int i = Size / 2; i < I.Rows - Size / 2; ++i)
                for (int j = Size/2; j < I.Cols - Size/2; ++j)
                {
                    var vals = GetValues(iIndexer, i - Size/2, j - Size/2);
                    vals.Remove(iIndexer[i, j]);
                    rIndexer[i, j] = iIndexer[i,j].MuliplyBy(centerMult).Add(Filter(vals));
                }
            return result;
        }

        protected override Vec3b Filter(List<Vec3b> vectors)
        {
            return new Vec3b((byte)vectors.Sum(x => x.Item0 * neighbourMult).Trunc(),
                (byte)vectors.Sum(x => x.Item1 * neighbourMult).Trunc(),
                (byte)vectors.Sum(x => x.Item2 * neighbourMult).Trunc());
        }
    }
}
