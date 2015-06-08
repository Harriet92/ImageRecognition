using System;
using ImageRecognition.Analysis;
using ImageRecognition.Helpers;

namespace ImageRecognition.Processing
{
    public class Segment
    {
        private readonly ImageArea _imageArea;
        public Shapes RecognizedShape { get; set; }
        public int[,] Slice { get; private set; }

        public ImageArea ImageArea
        {
            get { return _imageArea; }
        }

        public Segment(int leftUpperX, int rightBottomX, int leftUpperY, int rightBottomY)
        {
            _imageArea = new ImageArea(leftUpperX, leftUpperY, rightBottomX, rightBottomY);
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
            Slice[x - ImageArea.LeftUpperX, y - ImageArea.LeftUpperY] = 1;
        }

        private void ExpandSliceArea(int oldMinX, int oldMinY)
        {
            int si = (oldMinX - ImageArea.LeftUpperX).Trunc();
            int sj = (oldMinY - ImageArea.LeftUpperY).Trunc();
            var temp = Slice;
            Slice = new int[ImageArea.Cols, ImageArea.Rows];
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
            if (x < ImageArea.LeftUpperX)
            {
                oldMinX = ImageArea.LeftUpperX;
                ImageArea.LeftUpperX = Math.Min(ImageArea.LeftUpperX, x);
                sizeChanged = true;
            }
            else if (x > ImageArea.RightBottomX)
            {
                ImageArea.RightBottomX = Math.Max(ImageArea.RightBottomX, x);
                sizeChanged = true;
            }
            if (y < ImageArea.LeftUpperY)
            {
                oldMinY = ImageArea.LeftUpperY;
                ImageArea.LeftUpperY = Math.Min(ImageArea.LeftUpperY, y);
                sizeChanged = true;
            }
            else if (y > ImageArea.RightBottomY)
            {
                ImageArea.RightBottomY = Math.Max(ImageArea.RightBottomY, y);
                sizeChanged = true;
            }
            return sizeChanged;
        }
    }
}
