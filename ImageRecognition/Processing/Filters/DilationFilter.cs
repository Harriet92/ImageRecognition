﻿using System.Collections.Generic;
using System.Linq;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Processing.Filters
{
    public class DilationFilter: MatriceFilter
    {
        public DilationFilter(int size = 3)
            : base(size) { }

        protected override Vec3b Filter(List<Vec3b> vectors)
        {
            return new Vec3b(vectors.Max(x => x.Item0),
                vectors.Max(x => x.Item1),
                vectors.Max(x => x.Item2));
        }
    }
}
