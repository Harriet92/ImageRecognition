using System.Linq;
using ImageRecognition.Helpers;

namespace ImageRecognition.Processing.Filters
{
    public class MedianArrFilter: ArrayFilter
    {
        public MedianArrFilter(int size = 3)
            : base(size) { }

        protected override int Filter(int[,] vectorsArr)
        {
            var vectors = vectorsArr.ToList();
            vectors.Sort();
            return vectors[5];
        }
    }
}
