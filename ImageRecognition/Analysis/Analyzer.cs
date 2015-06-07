using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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
            foreach (var seg in Segments)
            {
                Console.WriteLine("Shape recognized! X: {0}, Y: {1}, M1: {2}, M7: {3}, M3: {4}", seg.LeftUpperX, seg.LeftUpperY, Momentums.M1(seg.Slice), Momentums.M7(seg.Slice), Momentums.M3(seg.Slice));
                seg.RecognizedShape = MatchShape(Momentums.M1(seg.Slice), Momentums.M7(seg.Slice), Momentums.M3(seg.Slice));
                if (seg.RecognizedShape != Shapes.None)
                    Console.WriteLine("Shape recognized! X: {0}, Y: {1}, shape: {2}", seg.LeftUpperX, seg.LeftUpperY, seg.RecognizedShape);
            }
        }

        public Mat PrintMatchedSegments(int[,] map)
        {
            Mat result = new Mat(map.GetLength(0), map.GetLength(1), MatType.CV_8UC3, new Scalar(0,0,0));
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

        private Shapes MatchShape(double m1, double m7, double m3)
        {
            if (m1.IsInRange(ProcessingParams.W_M1_min, ProcessingParams.W_M1_max)
                && m7.IsInRange(ProcessingParams.W_M7_min, ProcessingParams.W_M7_max)
                && m3.IsInRange(ProcessingParams.W_M3_min, ProcessingParams.W_M3_max))
                return Shapes.W;
            if (m1.IsInRange(ProcessingParams.N_M1_min, ProcessingParams.N_M1_max)
                && m7.IsInRange(ProcessingParams.N_M7_min, ProcessingParams.N_M7_max)
                & m3.IsInRange(ProcessingParams.N_M3_min, ProcessingParams.N_M3_max))
                return Shapes.N;
            return Shapes.None;
        }


    }
}
