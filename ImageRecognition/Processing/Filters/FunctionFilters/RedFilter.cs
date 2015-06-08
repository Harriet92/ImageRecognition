using ImageRecognition.Helpers;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Processing.Filters
{
    public class RedFilter : FunctionFilter
    {
        protected override Vec3b Filter(Vec3b vector)
        {
            return vector.IsInRange(ProcArgs.MinRed, ProcArgs.MaxRed) ? vector : ColorVectors.White;
        }
    }
}
