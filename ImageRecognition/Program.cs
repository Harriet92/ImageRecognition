using System;
using System.Drawing.Printing;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition
{
    class Program
    {
        private static bool IsBlack(Vec3b vec)
        {
            return vec[0] == 0 && vec[1] == 0 && vec[2] == 0;
        }


        private static bool IsWhite(Vec3b vec)
        {
            return vec[0] == 255 && vec[1] == 255 && vec[2] == 255;
        }


        private static Mat ComputeField(Mat I, ref int sum)
        {
            Mat res = new Mat(I.Rows, I.Cols, MatType.CV_8UC3);

            Mat _I = I;
            Mat _R = res;
            sum = 0;
            MatOfByte3 mat3 = new MatOfByte3(_R);
            var indexer = mat3.GetIndexer();
            Vec3b greenColor = new Vec3b(0, 255, 0);
            Vec3b whiteColor = new Vec3b(255, 255, 255);
            for (int i = 0; i < I.Rows; ++i)
            {
                for (int j = 0; j < I.Cols; ++j)
                {
                    if (IsBlack(_I.Get<Vec3b>(i, j)))
                    {
                        sum++;
                        indexer[i, j] = greenColor;
                    }
                    else
                    {
                        indexer[i, j] = whiteColor;
                    }
                }
            }
            res = _R;
            return res;
        }



        private static Mat ComputeCircuit(Mat I, ref int circuit)
        {
            Mat res = new Mat(I.Rows, I.Cols, MatType.CV_8UC3);
            Mat _I = I;
            Mat _R = res;
            circuit = 0;
            MatOfByte3 mat3R = new MatOfByte3(_R);
            var Rindexer = mat3R.GetIndexer();
            MatOfByte3 mat3I = new MatOfByte3(_I);
            var Iindexer = mat3I.GetIndexer();
            Vec3b redColor = new Vec3b(255, 0, 0);
            Vec3b whiteColor = new Vec3b(255, 255, 255);
            for (int i = 0; i < I.Rows; ++i)
                for (int j = 0; j < I.Cols; ++j)
                {
                    if (IsBlack(Iindexer[i, j]) &&
                        ((i > 0 && IsWhite(Iindexer[i - 1, j])) || (i < I.Rows - 1 && IsWhite(Iindexer[i + 1, j])) ||
                         (j > 0 && IsWhite(Iindexer[i, j - 1])) || (j < I.Cols - 1 && IsWhite(Iindexer[i, j + 1]))))
                    {
                        circuit++;
                        Rindexer[i, j] = redColor;
                    }
                    else
                    {
                        Rindexer[i, j] = whiteColor;
                    }
                }
            res = _R;
            I = _R;
            return res;
        }
        private static void Print(Mat I, string figureName)
        {
            int field, circuit;
            field = circuit = 0;
            Mat res = ComputeField(I, ref field);
            Mat res2 = ComputeCircuit(I, ref circuit);

            Console.WriteLine(figureName);
            Console.WriteLine("Field: " + field);
            Console.WriteLine("Circuit: " + circuit);
        }

        static void Main()
        {
            Mat src = new Mat("elipsa.dib", LoadMode.GrayScale);
            Print(src, "Ellipse");
            int field, circuit;
            field = circuit = 0;
            Mat res = ComputeField(src, ref field);
            Mat res2 = ComputeCircuit(src, ref circuit);
            using (new Window("src image", res))
            using (new Window("dst image", res2))
            {
                Cv2.WaitKey();
            }
        }
    }
}