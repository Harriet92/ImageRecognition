using System;
using ImageRecognition.Processing;

namespace ImageRecognition.Analysis
{
    public class LogoSegment
    {
        public Segment W { get; set; }
        public Segment N { get; set; }

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
                return new ImageArea(W.ImageArea.LeftUpperX, Math.Min(W.ImageArea.LeftUpperY, N.ImageArea.LeftUpperY),
                    N.ImageArea.RightBottomX, Math.Max(W.ImageArea.RightBottomY, N.ImageArea.RightBottomY));
            }
        }
    }
}
