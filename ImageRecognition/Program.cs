using ImageRecognition.Analysis;
using ImageRecognition.Processing;
using ImageRecognition.Processing.Filters;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition
{
    class Program
    {
        static void Main()
        {
            Mat src = new Mat("images/wpuz.jpg");
            MedianFilter fltr = new MedianFilter { Size = ProcessingParams.MedianFilterSize };
            ContrastFilter contrast = new ContrastFilter(ProcessingParams.ContrastFilter);
            SharpeningFilter sharp = new SharpeningFilter();
            DilationFilter dilate = new DilationFilter(3);
            Segmentation seg = new Segmentation();
            //Mat res1 = sharp.ApplyFilter(contrast.ApplyFilter(fltr.ApplyFilter(src)));
            Mat res2 = contrast.ApplyFilter(fltr.ApplyFilter(src));
            seg.ConvertToBW(res2);
            seg.GetSegments();
            Analyzer analyzer = new Analyzer(seg.segments);
            analyzer.AnalyzeSegments();
            using (new Window("source", src))
            using (new Window("processed", sharp.ApplyFilter(src)))
            {
                Cv2.WaitKey();
            }
        }
    }
}