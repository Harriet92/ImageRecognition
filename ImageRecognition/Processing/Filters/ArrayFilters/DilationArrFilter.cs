using System.Linq;
using ImageRecognition.Helpers;

namespace ImageRecognition.Processing.Filters
{
    public class DilationArrFilter: ArrayFilter
    {
        public DilationArrFilter(int size = 3)
            : base(size) { }

        protected override int Filter(int[,] vectorsArr)
        {
            var vectors = vectorsArr.ToList();
            return vectors.Max(x => x);
        }
    }
}
