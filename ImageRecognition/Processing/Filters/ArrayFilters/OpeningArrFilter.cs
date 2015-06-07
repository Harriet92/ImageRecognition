namespace ImageRecognition.Processing.Filters
{
    public class OpeningArrFilter : ArrayFilter
    {
        private readonly ErosionArrFilter erosionFilter;
        private readonly DilationArrFilter dilationFilter;

        public OpeningArrFilter(int size = 3)
            : base(size)
        {
            erosionFilter = new ErosionArrFilter(Size);
            dilationFilter = new DilationArrFilter(Size);
        }

        public override int[,] ApplyFilter(int[,] I)
        {
            return dilationFilter.ApplyFilter(erosionFilter.ApplyFilter(I));
        }

        protected override int Filter(int[,] vectorsArr)
        {
            return 0;
        }
    }
}
