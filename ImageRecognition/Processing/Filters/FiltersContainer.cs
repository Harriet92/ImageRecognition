namespace ImageRecognition.Processing.Filters
{
    public static class FiltersContainer
    {
        public static MedianFilter MedianFilter = new MedianFilter { Size = ProcArgs.MedianFilterSize };
        public static MedianArrFilter MedianArrFilter = new MedianArrFilter();
        public static ContrastFilter ContrastLightFilter = new ContrastFilter(ProcArgs.ContrastFilter);
        public static ContrastFilter ContrastFilter = new ContrastFilter(ProcArgs.ContrastFilter, false);
        public static SharpeningFilter SharpeningFilter = new SharpeningFilter();
        public static ClosingArrFilter ClosingArrFilter = new ClosingArrFilter();
        public static ErosionFilter ErosionFilter = new ErosionFilter();
        public static OpeningArrFilter OpeningArrFilter = new OpeningArrFilter();
        public static SharpeningGradientFilter GradientSharpeningFilter = new SharpeningGradientFilter(true);
        public static SharpeningGradientFilter LaplasianSharpeningFilter = new SharpeningGradientFilter(false);
        public static DilationFilter DilationFilter = new DilationFilter();
        public static BWFilter Bwfilter = new BWFilter();
        public static RedFilter RedFilter = new RedFilter();
    }
}
