using System.Collections.Generic;
using ImageRecognition.Helpers;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Processing.Filters
{
    public abstract class ArrayFilter
    {
        protected ArrayFilter(int size)
        {
            Size = size;
        }
        public int Size { get; set; }
        protected abstract int Filter(int[,] vectors);
        public virtual int[,] ApplyFilter(int[,] I)
        {
            int[,] result = new int[I.GetLength(0), I.GetLength(1)];
            for (int i = Size/2; i < I.GetLength(0) - Size/2; ++i)
                for (int j = Size/2; j < I.GetLength(1) - Size/2; ++j)
                    result[i, j] = Filter(GetValues(I, i - Size/2, j - Size/2));
            return result;
        }

        protected int[,] GetValues(int[,] mat, int sx, int sy)
        {
            var vectors = new int[Size,Size];
            vectors.ForEach((elem, x, y) =>
            {
                vectors[x,y] = mat[sx + x,sy + y];
            });
            return vectors;
        }
    }
}
