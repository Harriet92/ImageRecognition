using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Helpers
{
    public static class MatExt
    {
        public static MatIndexer<Vec3b> GetMatIndexer(Mat _R)
        {
            MatOfByte3 mat3R = new MatOfByte3(_R);
            var Rindexer = mat3R.GetIndexer();
            return Rindexer;
        }
    }
}
