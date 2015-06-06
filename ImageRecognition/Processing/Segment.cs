using System;
using ImageRecognition.Analysis;

namespace ImageRecognition.Processing
{
    public class Segment
    {
        public Shapes RecognizedShape { get; set; }
        public int LeftUpperX { get; set; }
        public int LeftUpperY { get; set; }
        public int RightBottomX { get; set; }
        public int RightBottomY { get; set; }
        public int Rows { get { return RightBottomY - LeftUpperY - 1;} }
        public int Cols { get { return RightBottomX - LeftUpperX - 1; } }
        public int[,] Slice { get; private set; }

        public Segment(int leftUpperX, int rightBottomX, int leftUpperY, int rightBottomY)
        {
            LeftUpperX = leftUpperX;
            LeftUpperY = leftUpperY;
            RightBottomX = rightBottomX;
            RightBottomY = rightBottomY;
            RecognizedShape = Shapes.None;
        }

        public void AddPoint(int x, int y)
        {
            LeftUpperX = Math.Min(LeftUpperX, x);
            RightBottomX = Math.Max(RightBottomX, x);
            LeftUpperY = Math.Min(LeftUpperY, y);
            RightBottomY = Math.Max(RightBottomY, y);
        }

        public void SaveMapSlice(int[,] fullMap)
        {
            Slice = new int[Rows,Cols];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    Slice[i, j] = fullMap[j + LeftUpperX, i + LeftUpperY ];
                }
            }
        }

    }
}
