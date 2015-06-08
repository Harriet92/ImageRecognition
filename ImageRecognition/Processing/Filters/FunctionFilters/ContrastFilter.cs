using ImageRecognition.Helpers;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Processing.Filters
{
    public class ContrastFilter : FunctionFilter
    {
        private readonly int factor;
        private readonly bool boost;
        public ContrastFilter(int contrastLevel, bool boost = true)
        {
            this.boost = boost;
            factor = (259 * (contrastLevel + 255)) / (255 * (259 - contrastLevel));
        }
        protected override Vec3b Filter(Vec3b vector)
        {
            return new Vec3b((byte)((factor * (vector.Item0 + (boost ? ProcArgs.LightBoost : 0) - 128) + 128) ).Trunc(),
                (byte)((factor * (vector.Item1 + (boost ? ProcArgs.LightBoost : 0) - 128) + 128)).Trunc(),
                (byte)((factor * (vector.Item2 + (boost ? ProcArgs.LightBoost : 0) - 128) + 128)).Trunc());
        }
    }
}
