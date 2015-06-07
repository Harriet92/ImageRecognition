using ImageRecognition.Helpers;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Processing.Filters
{
    public class SharpeningFilter : MatriceFilter
    {
        private readonly int[,] sharpenigFilter = {
            {1, 1, 1},
            {1, 9, 1},
            {1, 1, 1}};

        public SharpeningFilter(int size = 3)
            : base(size)
        { }

        protected override Vec3b Filter(Vec3b[,] vectorsArr)
        {
            var multiplied = vectorsArr.Multiply(sharpenigFilter);
            var result = multiplied[Size/2, Size/2];
            multiplied.ForEach((elem, x, y) =>
            {
                if (x != Size/2 || y != Size/2)
                    result = result.Subtract(elem);
            });
            return result.ToByte();
        }
    }
}
