using System;
using System.Collections.Generic;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Helpers
{
    public static class MatExt
    {
        public static MatIndexer<Vec3b> GetMatIndexer(Mat _R)
        {
            MatOfByte3 mat3R = new MatOfByte3(_R);
            var Rindexer = mat3R.GetIndexer();
            return Rindexer;
        }

        public static List<Vec3b> ToList(this Vec3b[,] arr)
        {
            var result = new List<Vec3b>();
            arr.ForEach((el, x, y) =>
            {
                result.Add(el);
            });
            return result;
        }

        public static void ForEach<T>(this T[,] array, Action<T,int, int> work )
        {
            for (int x = 0; x < array.GetLength(0); x++) 
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    work(array[x, y], x, y);
                }
            }
        }
    }
}
