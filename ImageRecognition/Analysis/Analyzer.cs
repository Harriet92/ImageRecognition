using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Threading.Tasks;
using ImageRecognition.Helpers;
using ImageRecognition.Processing;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Analysis
{
    public class Analyzer
    {
        public List<Segment> Segments { get; set; }
        public Analyzer(List<Segment> segs)
        {
            Segments = segs;
        }

        public void AnalyzeSegments()
        {
            Parallel.ForEach(Segments,
                (seg) =>
                {
                    Momentums moms = new Momentums(seg.Slice);
                    Console.WriteLine("Shape recognized! X: {0}, Y: {1}, M1: {2}, M7: {3}, M3: {4}, Shape: {5}", seg.LeftUpperX, seg.LeftUpperY, moms.M1(), moms.M7(), moms.M3(), Features.ShapeRatio(seg));
                    seg.RecognizedShape = MatchShape(moms.M1(), moms.M7(), moms.M3(), Features.ShapeRatio(seg));
                    if (seg.RecognizedShape != Shapes.None)
                        Console.WriteLine("Shape recognized! X: {0}, Y: {1}, shape: {2}", seg.LeftUpperX, seg.LeftUpperY,
                            seg.RecognizedShape);
                });
        }

        public Mat PrintMatchedSegments(int[,] map)
        {
            Mat result = new Mat(map.GetLength(0), map.GetLength(1), MatType.CV_8UC3, new Scalar(0, 0, 0));
            var rindexer = MatExt.GetMatIndexer(result);
            foreach (var segment in Segments)
            {
                var color = ColorVectors.Black;
                if (segment.RecognizedShape == Shapes.N)
                    color = ColorVectors.Red;
                else if (segment.RecognizedShape == Shapes.W)
                    color = ColorVectors.Blue;
                for (int i = 0; i < segment.Rows; i++)
                {
                    for (int j = 0; j < segment.Cols; j++)
                    {
                        if (map[j + segment.LeftUpperX, i + segment.LeftUpperY] == 1)
                            rindexer[j + segment.LeftUpperX, i + segment.LeftUpperY] = color;
                    }
                }
            }
            return result;
        }

        private Shapes MatchShape(double m1, double m7, double m3, double shapeRatio)
        {
            if (IsW(m1, m7, m3, shapeRatio))
                return Shapes.W;
            if (IsN(m1, m7, m3, shapeRatio))
                return Shapes.N;
            return Shapes.None;
        }

        private bool IsW(double m1, double m7, double m3, double shapeRatio)
        {
            return m1.IsInRange(ProcArgs.W_M1_min, ProcArgs.W_M1_max)
                   && m7.IsInRange(ProcArgs.W_M7_min, ProcArgs.W_M7_max)
                   && m3.IsInRange(ProcArgs.W_M3_min, ProcArgs.W_M3_max)
                   && shapeRatio.IsInRange(ProcArgs.W_ShapeRatio_min, ProcArgs.W_ShapeRatio_max);
        }
        private bool IsN(double m1, double m7, double m3, double shapeRatio)
        {
            return m1.IsInRange(ProcArgs.N_M1_min, ProcArgs.N_M1_max)
                && m7.IsInRange(ProcArgs.N_M7_min, ProcArgs.N_M7_max)
                & m3.IsInRange(ProcArgs.N_M3_min, ProcArgs.N_M3_max)
                   && shapeRatio.IsInRange(ProcArgs.N_ShapeRatio_min, ProcArgs.N_ShapeRatio_max);
        }

    }
}
