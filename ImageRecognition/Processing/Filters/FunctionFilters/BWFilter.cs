using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Processing.Filters
{
    public class BWFilter : FunctionFilter
    {
        protected override Vec3b Filter(Vec3b vector)
        {
            byte val = (byte)((vector.Item0 + vector.Item1 + vector.Item2)/3);
            return new Vec3b(val, val, val);
        }
    }
}
