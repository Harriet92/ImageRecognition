using System.Drawing.Printing;
using ImageRecognition.Analysis;
using ImageRecognition.Processing;
using ImageRecognition.Processing.Filters;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition
{
    public class Recognizer
    {
        public Mat SourceImage { get; private set; }
        public Mat EnhancedImage { get; private set; }
        public Mat BinaryImage { get; set; }
        public Mat SegmentsImage { get; set; }
        public Mat MatchedLettersImage { get; set; }
        public Mat ResultsImage { get; set; }

        private Segmentation segmentation;
        private Analyzer analyzer;
        private bool imageProcessed;
        public Recognizer(Mat src)
        {
            SourceImage = src;
        }

        public void ProcessImage()
        {
            EnhanceImage();
            PerformSegmentation();
            PerformAnalysis();
            imageProcessed = true;
        }

        private void EnhanceImage()
        {
            EnhancedImage = Filter.ApplyFilters(SourceImage,
                FiltersContainer.Bwfilter,
                FiltersContainer.MedianFilter,
                FiltersContainer.LaplasianSharpeningFilter,
                FiltersContainer.ContrastLightFilter,
                FiltersContainer.SharpeningFilter);
        }

        private void PerformSegmentation()
        {
            segmentation = new Segmentation();
            segmentation.ConvertToBW(EnhancedImage);
            segmentation.segMap = ArrayFilter.ApplyFilters(segmentation.segMap,
                FiltersContainer.ClosingArrFilter,
                FiltersContainer.OpeningArrFilter,
                FiltersContainer.MedianArrFilter);
            segmentation.GetSegments();
            BinaryImage = segmentation.PrintBWMap();
            SegmentsImage = segmentation.PrintSegments();
        }

        private void PerformAnalysis()
        {
            analyzer = new Analyzer(segmentation.segments, SourceImage);
            analyzer.AnalyzeSegments();
            MatchedLettersImage = analyzer.PrintMatchedSegments(segmentation.segMap);
            ResultsImage = analyzer.PrintResults();
        }

        public void PrintResults()
        {
            if(!imageProcessed)
                ProcessImage();
            using (new Window("Source image", SourceImage))
            using (new Window("Recognized elements", ResultsImage))
            {
                Cv2.WaitKey();
            }
        }
        public void PrintAllImages()
        {
            if (!imageProcessed)
                ProcessImage();
            using (new Window("Source image", SourceImage))
            using (new Window("Enhanced image", EnhancedImage))
            using (new Window("Segments", SegmentsImage))
            using (new Window("Matched letters", MatchedLettersImage))
            using (new Window("Binary image", BinaryImage))
            using (new Window("Recognized elements", ResultsImage))
            {
                Cv2.WaitKey();
            }
        }
    }
}
