using System;
using ImageRecognition.Helpers;

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
        public static int[,] ApplyFilters(int[,] I, params ArrayFilter[] filters)
        {
            var result = new int[I.GetLength(0), I.GetLength(1)];
            Buffer.BlockCopy(I, 0, result, 0, I.Length * sizeof(int));
            foreach (var filter in filters)
            {
                result = filter.ApplyFilter(result);
            }
            return result;
        }
    }
}
