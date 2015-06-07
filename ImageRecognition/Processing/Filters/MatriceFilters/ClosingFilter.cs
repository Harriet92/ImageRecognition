using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Processing.Filters
{
    public class ClosingFilter : MatriceFilter
    {
        private readonly ErosionFilter erosionFilter;
        private readonly DilationFilter dilationFilter;

        public ClosingFilter(int size = 5)
            : base(size)
        {
            erosionFilter = new ErosionFilter(Size);
            dilationFilter = new DilationFilter(Size);
        }

        public override Mat ApplyFilter(Mat I)
        {
            return erosionFilter.ApplyFilter(dilationFilter.ApplyFilter(I));
        }

        protected override Vec3b Filter(Vec3b[,] vectorsArr)
        {
            return new Vec3b();
        }
    }
}
