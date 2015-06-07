using ImageRecognition.Helpers;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Processing.Filters
{
    public class ContrastFilter : FunctionFilter
    {
        private readonly int factor;
        public ContrastFilter(int contrastLevel)
        {
            factor = (259 * (contrastLevel + 255)) / (255 * (259 - contrastLevel));
        }
        protected override Vec3b Filter(Vec3b vector)
        {
            return new Vec3b((byte)((factor * (vector.Item0 + ProcArgs.LightBoost - 128) + 128) ).Trunc(),
                (byte)((factor * (vector.Item1 + ProcArgs.LightBoost - 128) + 128)).Trunc(),
                (byte)((factor * (vector.Item2 + ProcArgs.LightBoost - 128) + 128)).Trunc());
        }
    }
}
