using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Processing.Filters
{
    public class OpeningFilter : MatriceFilter
    {
        private readonly ErosionFilter erosionFilter;
        private readonly DilationFilter dilationFilter;

        public OpeningFilter(int size = 3)
            : base(size)
        {
            erosionFilter = new ErosionFilter(Size);
            dilationFilter = new DilationFilter(Size);
        }

        public override Mat ApplyFilter(Mat I)
        {
            return dilationFilter.ApplyFilter(erosionFilter.ApplyFilter(I));
        }

        protected override Vec3b Filter(Vec3b[,] vectorsArr)
        {
            return new Vec3b();
        }
    }
}
