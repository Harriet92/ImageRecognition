namespace ImageRecognition.Processing
{
    public class ImageArea
    {
        public ImageArea(int leftUpperX, int leftUpperY, int rightBottomX, int rightBottomY)
        {
            LeftUpperX = leftUpperX;
            LeftUpperY = leftUpperY;
            RightBottomX = rightBottomX;
            RightBottomY = rightBottomY;
        }

        public int LeftUpperX { get; set; }
        public int LeftUpperY { get; set; }
        public int RightBottomX { get; set; }
        public int RightBottomY { get; set; }
        public int Rows { get { return RightBottomY - LeftUpperY + 1; } }
        public int Cols { get { return RightBottomX - LeftUpperX + 1; } }
    }
}