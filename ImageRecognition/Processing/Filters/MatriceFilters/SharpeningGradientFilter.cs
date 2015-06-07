using System;
using ImageRecognition.Helpers;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Processing.Filters
{
    public class SharpeningGradientFilter : MatriceFilter
    {
        private readonly bool gradient;

        public SharpeningGradientFilter(bool gradient, int size = 3)
            : base(size)
        {
            this.gradient = gradient;
        }

        protected override Vec3b Filter(Vec3b[,] arr)
        {
            var result = gradient ? GradientAbs(arr) : LaplasianAbs(arr);
            return arr[1,1].Subtract(result.DivideBy(8)).ToByte();
        }
        private Vec3i LaplasianAbs(Vec3b[,] arr)
        {
            return (new Vec3i(0, 0, 0)).Add(arr[2, 1]).Add(arr[0, 1]).Add(arr[1, 2])
                .Subtract(arr[1, 1].MuliplyBy(4));
        }
        private Vec3i GradientAbs(Vec3b[,] arr)
        {
            var dx = (new Vec3i(0, 0, 0)).Add(arr[2, 0]).Add(arr[2, 1].MuliplyBy(2)).Add(arr[2, 2])
                .Subtract(arr[0, 0]).Subtract(arr[0, 1].MuliplyBy(2)).Subtract(arr[0, 2]);
            var dy = (new Vec3i(0, 0, 0)).Add(arr[0, 2]).Add(arr[1, 2].MuliplyBy(2)).Add(arr[2, 2])
                .Subtract(arr[0, 0]).Subtract(arr[1, 0].MuliplyBy(2)).Subtract(arr[2, 0]);
            var result = new Vec3i(Abs(dx.Item0, dy.Item0), Abs(dx.Item1, dy.Item1), Abs(dx.Item2, dy.Item2));
            return result;
        }

        private int Abs(int dx, int dy)
        {
            return (int)Math.Sqrt(dx*dx + dy*dy);
        }
    }
}
