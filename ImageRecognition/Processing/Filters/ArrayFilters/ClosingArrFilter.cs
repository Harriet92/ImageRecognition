using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Processing.Filters
{
    public class ClosingArrFilter : ArrayFilter
    {
        private readonly ErosionArrFilter erosionFilter;
        private readonly DilationArrFilter dilationFilter;

        public ClosingArrFilter(int size = 5)
            : base(size)
        {
            erosionFilter = new ErosionArrFilter(Size);
            dilationFilter = new DilationArrFilter(Size);
        }

        public override int[,] ApplyFilter(int[,] I)
        {
            return erosionFilter.ApplyFilter(dilationFilter.ApplyFilter(I));
        }

        protected override int Filter(int[,] vectorsArr)
        {
            return 0;
        }
    }
}
