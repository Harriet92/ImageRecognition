using System.Collections.Generic;
using ImageRecognition.Helpers;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Processing.Filters
{
    public abstract class MatriceFilter
    {
        protected MatriceFilter(int size)
        {
            Size = size;
        }
        public int Size { get; set; }
        protected abstract Vec3b Filter(List<Vec3b> vectors);
        public virtual Mat ApplyFilter(Mat I)
        {
            Mat result = new Mat(I.Rows, I.Cols, MatType.CV_8UC3, new Scalar(0,0,0));
            var rIndexer = MatExt.GetMatIndexer(result);
            var iIndexer = MatExt.GetMatIndexer(I);
            for (int i = Size/2; i < I.Rows - Size/2; ++i)
                for (int j = Size/2; j < I.Cols - Size/2; ++j)
                    rIndexer[i, j] = Filter(GetValues(iIndexer, i - Size/2, j - Size/2));
            return result;
        }

        protected List<Vec3b> GetValues(MatIndexer<Vec3b> mat, int sx, int sy)
        {
            var vectors = new List<Vec3b>();
            for (int x = 0; x < Size; x++)
                for (int y = 0; y < Size; y++)
                    vectors.Add(mat[sx + x,sy + y]);
            return vectors;
        }
    }
}
