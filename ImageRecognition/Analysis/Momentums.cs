using System;

namespace ImageRecognition.Analysis
{
    public static class Momentums
    {
        public static double mpq(int[,] map, int p, int q)
        {
            double result = 0;
            for (int i = 0; i < map.GetLength(0); ++i)
            {
                for (int j = 0; j < map.GetLength(1); ++j)
                {
                    result += Math.Pow(i, p) * Math.Pow(j, q) * map[i, j];
                }
            }
            return result;
        }

        public static double Mpq(int[,] map, int p, int q)
        {
            double result = 0;
            double m00 = mpq(map, 0, 0);
            double m10 = mpq(map, 1, 0);
            double m01 = mpq(map, 0, 1);
            double ic = m10 / m00;
            double jc = m01 / m00;

            for (int i = 0; i < map.GetLength(0); ++i)
            {
                for (int j = 0; j < map.GetLength(1); ++j)
                {
                    result += Math.Pow(i - ic, p) * Math.Pow(j - jc, q) * map[i, j];
                }
            }
            return result;
        }

        public static double Npq(int[,] map, int p, int q)
        {
            return Mpq(map, p, q) / Math.Pow(mpq(map, 0, 0), (p + q) / 2f + 1f);
        }

        public static double M1(int[,] map)
        {
            return (Mpq(map, 2, 0) + Mpq(map, 0, 2)) / Math.Pow(mpq(map, 0, 0), 2);
        }

        public static double M3(int[,] map)
        {
            double N30 = Npq(map, 3, 0);
            double N12 = Npq(map, 1, 2);
            double N21 = Npq(map, 2, 1);
            double N03 = Npq(map, 0, 3);
            return Math.Pow(N30 - 3f * N12, 2f) + Math.Pow(3f * N21 - N03, 2f);
        }

        public static double M7(int[,] map)
        {
            double N20 = Npq(map, 2, 0);
            double N02 = Npq(map, 0, 2);
            double N11 = Npq(map, 1, 1);
            return N20 * N02 - Math.Pow(N11, 2f);
        }
    }
}
