using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageRecognition.Helpers;
using ImageRecognition.Processing;
using ImageRecognition.Processing.Filters;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Analysis
{
    public class Analyzer
    {
        public List<Segment> Segments { get; set; }
        public List<LogoSegment> LogoSegments { get; private set; }
        public List<LogoSegment> LogoSegmentsWithoutRed { get; private set; }
        private Mat source;
        public Analyzer(List<Segment> segs, Mat src)
        {
            Segments = segs;
            source = src;
        }

        public void AnalyzeSegments()
        {
            FindWN();
            AssembleLogos();
            AnalyzeRedCut();
        }

        public Mat PrintResults()
        {
            var space = 5;
            if (LogoSegments.Count == 0)
                return new Mat("images/noresults.jpg");
            Mat result = new Mat(LogoSegments.Sum(x => x.ImageArea.Cols) + (LogoSegments.Count - 1) * space, LogoSegments.Max(x => x.ImageArea.Rows), MatType.CV_8UC3, new Scalar(255, 255, 255));
            int row = 0;
            var rIndexer = MatExt.GetMatIndexer(result);
            var iIndexer = MatExt.GetMatIndexer(source);
            foreach (var logoSegment in LogoSegments)
            {
                for (int i = 0; i < logoSegment.ImageArea.Rows; ++i)
                    for (int j = 0; j < logoSegment.ImageArea.Cols; ++j)
                        rIndexer[row + j, i] = iIndexer[logoSegment.ImageArea.LeftUpperX + j, logoSegment.ImageArea.LeftUpperY + i];
                row += logoSegment.ImageArea.Cols - 1 + space;
            }
            return result;
        }

        private void AnalyzeRedCut()
        {
            List<LogoSegment> toBeRemoved = new List<LogoSegment>();
            foreach (var logoSegment in LogoSegments)
            {
                var redSlice = FiltersContainer.RedFilter.ApplyFilter(logoSegment.SliceImage);
                if (!GetRedPixelsRatio(redSlice).IsInRange(ProcArgs.Cut_min, ProcArgs.Cut_max))
                {
                    Console.WriteLine("Removing: " + GetRedPixelsRatio(redSlice));
                    toBeRemoved.Add(logoSegment);
                }
            }
            toBeRemoved.ForEach(x => LogoSegments.Remove(x));
            LogoSegmentsWithoutRed = toBeRemoved;
        }

        private double GetRedPixelsRatio(Mat I)
        {
            var reds = CountRedPixels(I);
            return reds / (double)(I.Rows * I.Cols) * 100;
        }

        private long CountRedPixels(Mat I)
        {
            var iIndexer = MatExt.GetMatIndexer(I);
            long counter = 0;
            for (int i = 0; i < I.Rows; ++i)
                for (int j = 0; j < I.Cols; ++j)
                    if (iIndexer[i, j].IsInRange(ProcArgs.MinRed, ProcArgs.MaxRed))
                        counter++;
            return counter;
        }

        private void AssembleLogos()
        {
            var ws = Segments.FindAll(x => x.RecognizedShape == Shapes.W);
            var ns = Segments.FindAll(x => x.RecognizedShape == Shapes.N);
            var logos = new List<LogoSegment>();
            ws.ForEach(x => logos.Add(new LogoSegment(x)));
            foreach (var logo in logos)
            {
                Segment matching = ns.FirstOrDefault(segment => WNCompatible(logo.W, segment));
                if (matching == null)
                    continue;
                logo.N = matching;
                ns.Remove(matching);
            }
            logos.RemoveAll(x => x.W == null || x.N == null);
            LogoSegments = logos;
            CutSlices();
        }

        private void CutSlices()
        {
            var iIndexer = MatExt.GetMatIndexer(source);
            foreach (var logoSegment in LogoSegments)
            {
                logoSegment.SliceImage = new Mat(logoSegment.ImageArea.Cols, logoSegment.ImageArea.Rows, MatType.CV_8UC3, new Scalar(255, 255, 255));
                var sIndexer = MatExt.GetMatIndexer(logoSegment.SliceImage);
                for (int i = 0; i < logoSegment.ImageArea.Rows; ++i)
                    for (int j = 0; j < logoSegment.ImageArea.Cols; ++j)
                        sIndexer[j, i] = iIndexer[logoSegment.ImageArea.LeftUpperX + j, logoSegment.ImageArea.LeftUpperY + i];
            }
        }

        private bool WNCompatible(Segment w, Segment n)
        {
            if (w.ImageArea.LeftUpperY > n.ImageArea.LeftUpperY)
                return false;

            var shapeRatio = ((n.ImageArea.RightBottomX - w.ImageArea.LeftUpperX) / (double)(n.ImageArea.RightBottomY - w.ImageArea.LeftUpperY));
            Console.WriteLine("Shape ratio: " + shapeRatio);
            return shapeRatio.IsInRange(ProcArgs.Logo_ShapeRatio_min, ProcArgs.Logo_ShapeRatio_max);
        }

        private void FindWN()
        {
            Parallel.ForEach(Segments,
                (seg) =>
                {
                    Momentums moms = new Momentums(seg.Slice);
                    Console.WriteLine("Shape recognized! X: {0}, Y: {1}, M1: {2}, M7: {3}, M3: {4}, Shape: {5}", seg.ImageArea.LeftUpperX, seg.ImageArea.LeftUpperY, moms.M1(), moms.M7(), moms.M3(), Features.ShapeRatio(seg));
                    seg.RecognizedShape = MatchShape(moms.M1(), moms.M7(), moms.M3(), Features.ShapeRatio(seg));
                });
            Segments.RemoveAll(x => x.RecognizedShape == Shapes.None);
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
                for (int i = 0; i < segment.ImageArea.Rows; i++)
                {
                    for (int j = 0; j < segment.ImageArea.Cols; j++)
                    {
                        if (map[j + segment.ImageArea.LeftUpperX, i + segment.ImageArea.LeftUpperY] == 1)
                            rindexer[j + segment.ImageArea.LeftUpperX, i + segment.ImageArea.LeftUpperY] = color;
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
