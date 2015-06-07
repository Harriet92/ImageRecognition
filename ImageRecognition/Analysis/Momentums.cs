using System;

namespace ImageRecognition.Analysis
{
    public class Momentums
    {
        private readonly int[,] map;
        private readonly double m00;
        private readonly double ic;
        private readonly double jc;
        public Momentums(int[,] map)
        {
            this.map = map;
            m00 = mpq(0, 0);
            var m10 = mpq(1, 0);
            var m01 = mpq(0, 1);
            ic = m10 / m00;
            jc = m01 / m00;
        }

        public double M1()
        {
            return (Mpq(2, 0) + Mpq(0, 2)) / Math.Pow(m00, 2);
        }

        public double M3()
        {
            double N30 = Npq(3, 0);
            double N12 = Npq(1, 2);
            double N21 = Npq(2, 1);
            double N03 = Npq(0, 3);
            return Math.Pow(N30 - 3f * N12, 2f) + Math.Pow(3f * N21 - N03, 2f);
        }

        public double M7()
        {
            double N20 = Npq(2, 0);
            double N02 = Npq(0, 2);
            double N11 = Npq(1, 1);
            return N20 * N02 - Math.Pow(N11, 2f);
        }

        private double mpq(int p, int q)
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

        private double Mpq(int p, int q)
        {
            double result = 0;
            for (int i = 0; i < map.GetLength(0); ++i)
            {
                for (int j = 0; j < map.GetLength(1); ++j)
                {
                    result += Math.Pow(i - ic, p) * Math.Pow(j - jc, q) * map[i, j];
                }
            }
            return result;
        }

        private double Npq(int p, int q)
        {
            return Mpq(p, q) / Math.Pow(m00, (p + q) / 2f + 1f);
        }
    }
}
