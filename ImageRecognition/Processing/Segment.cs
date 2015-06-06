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
        public int Rows { get { return RightBottomY - LeftUpperY + 1;} }
        public int Cols { get { return RightBottomX - LeftUpperX + 1; } }
        public int[,] Slice { get; private set; }

        public Segment(int leftUpperX, int rightBottomX, int leftUpperY, int rightBottomY)
        {
            LeftUpperX = leftUpperX;
            LeftUpperY = leftUpperY;
            RightBottomX = rightBottomX;
            RightBottomY = rightBottomY;
            RecognizedShape = Shapes.None;
            Slice = new int[1,1];
            Slice[0, 0] = 1;
        }

        public void AddPoint(int x, int y)
        {
            int oldMinX = 0; int oldMinY = 0;
            if (x < LeftUpperX)
            {
                oldMinX = LeftUpperX;
                LeftUpperX = Math.Min(LeftUpperX, x);
            }
            if (y < LeftUpperY)
            {
                oldMinY = LeftUpperY;
                LeftUpperY = Math.Min(LeftUpperY, y);
            }
            RightBottomX = Math.Max(RightBottomX, x);
            RightBottomY = Math.Max(RightBottomY, y);
            var temp = Slice;
            Slice = new int[Cols, Rows];
            int si = (oldMinX - LeftUpperX).Trunc();
            int sj = (oldMinY - LeftUpperY).Trunc(); ;
            
            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    Slice[i + si, j + sj] = temp[i, j];
                }
            }
            //Buffer.BlockCopy(temp, 0, Slice, 0, temp.Length * sizeof(int));
            Slice[x - LeftUpperX, y - LeftUpperY] = 1;
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
