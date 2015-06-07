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
            Mat src = new Mat("images/trzy.jpg");
            MedianFilter fltr = new MedianFilter { Size = ProcArgs.MedianFilterSize };
            MedianArrFilter medianArrFilter = new MedianArrFilter();
            ContrastFilter contrast = new ContrastFilter(ProcArgs.ContrastFilter);
            SharpeningFilter sharp = new SharpeningFilter();
            ClosingArrFilter closingFilter = new ClosingArrFilter();
            ErosionFilter erosionFilter = new ErosionFilter();
            FillHoles holes = new FillHoles();
            OpeningArrFilter openingFilter = new OpeningArrFilter();
            SharpeningGradientFilter sharpGrad = new SharpeningGradientFilter(true);
            SharpeningGradientFilter sharpLap = new SharpeningGradientFilter(false);
            DilationFilter dilate = new DilationFilter(3);
            BWFilter bwfilter = new BWFilter();
            //Segmentation seg = new Segmentation();
            Segmentation seg2 = new Segmentation();
            //Mat res1 = //erosionFilter.ApplyFilter(

            //    //closingFilter.ApplyFilter(
            //    //openingFilter.ApplyFilter(
            //    sharpGrad.ApplyFilter(
            //    contrast.ApplyFilter(
            //    sharp.ApplyFilter(
            //    fltr.ApplyFilter(
            //    bwfilter.ApplyFilter(
            //    src)))));
            Mat res2 = //erosionFilter.ApplyFilter(
                //closingFilter.ApplyFilter(
                //closingFilter.ApplyFilter(
                //holes.ApplyFilter(
               sharp.ApplyFilter(
                contrast.ApplyFilter(
                sharpLap.ApplyFilter(
                //
                fltr.ApplyFilter(
                bwfilter.ApplyFilter(
                src)))));
           // Mat res2 = sharpGrad.ApplyFilter(sharp.ApplyFilter(fltr.ApplyFilter(src)));
            //seg.ConvertToBW(res1);
            seg2.ConvertToBW(res2);
            //seg.segMap = closingFilter.ApplyFilter(seg.segMap);
            //seg.segMap = openingFilter.ApplyFilter(seg.segMap);
            seg2.segMap = closingFilter.ApplyFilter(seg2.segMap);
            seg2.segMap = openingFilter.ApplyFilter(seg2.segMap);
            seg2.segMap = medianArrFilter.ApplyFilter(seg2.segMap); 
            //seg2.segMap = holes.ApplyFilter(seg2.segMap);
            seg2.GetSegments();
            Analyzer analyzer = new Analyzer(seg2.segments);
            analyzer.AnalyzeSegments();
            using (new Window("res1", res2))
            using (new Window("res2", seg2.PrintBWMap()))
            using (new Window("bw res1", seg2.PrintSegments()))
            using (new Window("bw res2", analyzer.PrintMatchedSegments(seg2.segMap)))
            {
                Cv2.WaitKey();
            }
        }
    }
}