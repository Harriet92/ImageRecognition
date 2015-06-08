using ImageRecognition.Helpers;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Processing.Filters
{
    public abstract class MatriceFilter:Filter
    {
        protected MatriceFilter(int size)
        {
            Size = size;
        }
        public int Size { get; set; }
        protected abstract Vec3b Filter(Vec3b[,] vectors);
        public override Mat ApplyFilter(Mat I)
        {
            Mat result = new Mat(I.Rows, I.Cols, MatType.CV_8UC3, new Scalar(0,0,0));
            var rIndexer = MatExt.GetMatIndexer(result);
            var iIndexer = MatExt.GetMatIndexer(I);
            for (int i = Size/2; i < I.Rows - Size/2; ++i)
                for (int j = Size/2; j < I.Cols - Size/2; ++j)
                    rIndexer[i, j] = Filter(GetValues(iIndexer, i - Size/2, j - Size/2));
            return result;
        }

        protected Vec3b[,] GetValues(MatIndexer<Vec3b> mat, int sx, int sy)
        {
            var vectors = new Vec3b[Size,Size];
            vectors.ForEach((elem, x, y) =>
            {
                vectors[x,y] = mat[sx + x,sy + y];
            });
            return vectors;
        }
    }
}
