using System.Linq;
using ImageRecognition.Helpers;

namespace ImageRecognition.Processing.Filters
{
    public class FillHolesArrFilter  : ArrayFilter
    {
        public FillHolesArrFilter(int size = 3)
            : base(size) { }

        protected override int Filter(int[,] arr)
        {
            var vectors = arr.ToList();
            var max = vectors.Max(x => x);
            if ((arr[0, 1] == max && arr[2, 1] == max) ||
                (arr[1, 0] == max && arr[1, 2] == max))
                return max;
            return arr[1, 1];
        }
    }
}
