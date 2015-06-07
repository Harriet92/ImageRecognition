using ImageRecognition.Helpers;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition
{
    public static class ProcArgs
    {
        public static int MedianFilterSize { get { return 5; } }
        public static int ContrastFilter { get { return 150; } }
        public static int LightBoost { get { return 40; } }
        public static int MinSegmentSize { get { return 500; } }
        public static Vec3b MinRed { get { return new Vec3b(0,0,160);} }
        public static Vec3b MaxRed { get { return new Vec3b(20,20,255); } }
        public static Vec3b MinGray { get { return new Vec3b(120, 120, 110); } }
        public static Vec3b MaxGray { get { return new Vec3b(145, 142, 126); } }
        public static Vec3b MinText { get { return new Vec3b(200,200,200); } }
        public static Vec3b MaxText { get { return ColorVectors.White; } }
        public static double Cut_M1_min { get { return 0.38; } }
        public static double Cut_M1_max { get { return 0.39; } }
        public static double Cut_M7_min { get { return 0.026; } }
        public static double Cut_M7_max { get { return 0.027; } }

        public static double N_M1_min { get { return 0.2315; } }
        public static double N_M1_max { get { return 0.314; } }
        public static double N_M7_min { get { return 0.01315; } }
        public static double N_M7_max { get { return 0.021; } }
        public static double N_M3_min { get { return 3.8e-06; } }
        public static double N_M3_max { get { return 0.00013; } }
        public static double N_ShapeRatio_min { get { return 1.1; } }
        public static double N_ShapeRatio_max { get { return 1.3; } }

        public static double W_M1_min { get { return 0.272; } }
        public static double W_M1_max { get { return 0.335; } }
        public static double W_M7_min { get { return 0.014; } }
        public static double W_M7_max { get { return 0.025; } }
        public static double W_M3_min { get { return 0.00117; } }
        public static double W_M3_max { get { return 0.0022; } }
        public static double W_ShapeRatio_min { get { return 0.79; } }
        public static double W_ShapeRatio_max { get { return 0.85; } }
    }
}
