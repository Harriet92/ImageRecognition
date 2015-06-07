using ImageRecognition.Processing;

namespace ImageRecognition.Analysis
{
    public static class Features
    {
        public static double ShapeRatio(Segment seg)
        {
            return seg.Cols/(double) seg.Rows;
        }
    }
}
