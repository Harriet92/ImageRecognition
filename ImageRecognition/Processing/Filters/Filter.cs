using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Processing.Filters
{
    public abstract class Filter
    {
        public abstract Mat ApplyFilter(Mat I);

        public static Mat ApplyFilters(Mat I, params Filter[] filters)
        {
            Mat result = I.Clone();
            foreach (var filter in filters)
            {
                result = filter.ApplyFilter(result);
            }
            return result;
        }
    }
}
