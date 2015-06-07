using System;
using ImageRecognition.Analysis;
using ImageRecognition.Helpers;

namespace ImageRecognition.Processing
{
    public class Segment
    {
        public Shapes RecognizedShape { get; set; }
        public int LeftUpperX { get; set; }
        public int LeftUpperY { get; set; }
        public int RightBottomX { get; set; }
        public int RightBottomY { get; set; }
        public int Rows { get { return RightBottomY - LeftUpperY + 1; } }
        public int Cols { get { return RightBottomX - LeftUpperX + 1; } }
        public int[,] Slice { get; private set; }

        public Segment(int leftUpperX, int rightBottomX, int leftUpperY, int rightBottomY)
        {
            LeftUpperX = leftUpperX;
            LeftUpperY = leftUpperY;
            RightBottomX = rightBottomX;
            RightBottomY = rightBottomY;
            RecognizedShape = Shapes.None;
            Slice = new int[1, 1];
            Slice[0, 0] = 1;
        }

        public void AddPoint(int x, int y)
        {
            int oldMinX;
            int oldMinY;
            if (ChangeSliceSize(out oldMinX, x, y, out oldMinY))
                ExpandSliceArea(oldMinX, oldMinY);
            Slice[x - LeftUpperX, y - LeftUpperY] = 1;
        }

        private void ExpandSliceArea(int oldMinX, int oldMinY)
        {
            int si = (oldMinX - LeftUpperX).Trunc();
            int sj = (oldMinY - LeftUpperY).Trunc();
            var temp = Slice;
            Slice = new int[Cols, Rows];
            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    Slice[i + si, j + sj] = temp[i, j];
                }
            }
        }

        private bool ChangeSliceSize(out int oldMinX, int x, int y, out int oldMinY)
        {
            oldMinX = 0;
            oldMinY = 0;
            bool sizeChanged = false;
            if (x < LeftUpperX)
            {
                oldMinX = LeftUpperX;
                LeftUpperX = Math.Min(LeftUpperX, x);
                sizeChanged = true;
            }
            else if (x > RightBottomX)
            {
                RightBottomX = Math.Max(RightBottomX, x);
                sizeChanged = true;
            }
            if (y < LeftUpperY)
            {
                oldMinY = LeftUpperY;
                LeftUpperY = Math.Min(LeftUpperY, y);
                sizeChanged = true;
            }
            else if (y > RightBottomY)
            {
                RightBottomY = Math.Max(RightBottomY, y);
                sizeChanged = true;
            }
            return sizeChanged;
        }
    }
}
