using ImageRecognition.Helpers;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition
{
    public static class ProcessingParams
    {
        public static int MedianFilterSize { get { return 3; } }
        public static int ContrastFilter { get { return 100; } }
        public static int MinSegmentSize { get { return 500; } }
        public static Vec3b MinRed { get { return new Vec3b(0,0,250);} }
        public static Vec3b MaxRed { get { return new Vec3b(10,10,255); } }
        public static Vec3b MinText { get { return new Vec3b(240,240,240); } }
        public static Vec3b MaxText { get { return ColorVectors.White; } }
    }
}
