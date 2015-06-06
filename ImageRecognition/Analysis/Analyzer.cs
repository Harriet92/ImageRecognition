using System;
using System.Collections.Generic;
using ImageRecognition.Helpers;
using ImageRecognition.Processing;

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
                seg.RecognizedShape = MatchShape(Momentums.M1(seg.Slice), Momentums.M7(seg.Slice));
                if (seg.RecognizedShape != Shapes.None)
                    Console.WriteLine("Shape recognized! X: {0}, Y: {1}, shape: {2}", seg.LeftUpperX, seg.LeftUpperY, seg.RecognizedShape);
            }
        }

        private Shapes MatchShape(double m1, double m7)
        {
            if (m1.IsInRange(ProcessingParams.W_M1_min, ProcessingParams.W_M1_max)
                && m7.IsInRange(ProcessingParams.W_M7_min, ProcessingParams.W_M7_max))
                return Shapes.W;
            if (m1.IsInRange(ProcessingParams.N_M1_min, ProcessingParams.N_M1_max)
                && m7.IsInRange(ProcessingParams.N_M7_min, ProcessingParams.N_M7_max))
                return Shapes.N;
            return Shapes.None;
        }
    }
}
