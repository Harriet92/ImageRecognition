using ImageRecognition.Helpers;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Processing.Filters
{
    public abstract class FunctionFilter
    {
        protected abstract Vec3b Filter(Vec3b vector);
        public virtual Mat ApplyFilter(Mat I)
        {
            Mat result = new Mat(I.Rows, I.Cols, MatType.CV_8UC3);
            var rIndexer = MatExt.GetMatIndexer(result);
            var iIndexer = MatExt.GetMatIndexer(I);
            for (int i = 0; i < I.Rows; ++i)
                for (int j = 0; j < I.Cols; ++j)
                    rIndexer[i, j] = Filter(iIndexer[i,j]);
            return result;
        }
    }
}
