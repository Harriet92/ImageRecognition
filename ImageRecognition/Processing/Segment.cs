using System;

namespace ImageRecognition.Processing
{
    public class Segment
    {
        public int LeftUpperX { get; set; }
        public int LeftUpperY { get; set; }
        public int RightBottomX { get; set; }
        public int RightBottomY { get; set; }

        public Segment(int leftUpperX, int rightBottomX, int leftUpperY, int rightBottomY)
        {
            LeftUpperX = leftUpperX;
            LeftUpperY = leftUpperY;
            RightBottomX = rightBottomX;
            RightBottomY = rightBottomY;
        }

        public void AddPoint(int x, int y)
        {
            LeftUpperX = Math.Min(LeftUpperX, x);
            RightBottomX = Math.Max(RightBottomX, x);
            LeftUpperY = Math.Min(LeftUpperY, y);
            RightBottomY = Math.Max(RightBottomY, y);
        }
    }
}
