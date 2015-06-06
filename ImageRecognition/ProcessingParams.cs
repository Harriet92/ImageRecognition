using ImageRecognition.Helpers;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition
{
    public static class ProcessingParams
    {
        public static int MedianFilterSize { get { return 3; } }
        public static int ContrastFilter { get { return 120; } }
        public static int MinSegmentSize { get { return 200; } }
        public static Vec3b MinRed { get { return new Vec3b(0,0,160);} }
        public static Vec3b MaxRed { get { return new Vec3b(20,20,255); } }
        public static Vec3b MinGray { get { return new Vec3b(120, 120, 110); } }
        public static Vec3b MaxGray { get { return new Vec3b(145, 142, 126); } }
        public static Vec3b MinText { get { return new Vec3b(220,220,220); } }
        public static Vec3b MaxText { get { return ColorVectors.White; } }
        public static double Cut_M1_min { get { return 0.38; } }
        public static double Cut_M1_max { get { return 0.39; } }
        public static double Cut_M7_min { get { return 0.026; } }
        public static double Cut_M7_max { get { return 0.027; } }
        public static double N_M1_min { get { return 0.27; } }
        public static double N_M1_max { get { return 0.29; } }
        public static double N_M7_min { get { return 0.018; } }
        public static double N_M7_max { get { return 0.021; } }
        public static double W_M1_min { get { return 0.31; } }
        public static double W_M1_max { get { return 0.312; } }
        public static double W_M7_min { get { return 0.022; } }
        public static double W_M7_max { get { return 0.023; } }
    }
}
