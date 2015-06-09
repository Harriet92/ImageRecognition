using System;
using ImageRecognition.Processing;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Analysis
{
    public class LogoSegment
    {
        public Segment W { get; set; }
        public Segment N { get; set; }
        public Mat SliceImage { get; set; }
        public LogoSegment(Segment wseg)
        {
            W = wseg;
        }

        public ImageArea ImageArea
        {
            get
            {
                if (W == null || N == null)
                    return null;
                return new ImageArea(Math.Min(W.ImageArea.LeftUpperX, N.ImageArea.LeftUpperX), Math.Min(W.ImageArea.LeftUpperY, N.ImageArea.LeftUpperY),
                    Math.Max(W.ImageArea.RightBottomX, N.ImageArea.RightBottomX), Math.Max(W.ImageArea.RightBottomY, N.ImageArea.RightBottomY));
            }
        }
    }
}
