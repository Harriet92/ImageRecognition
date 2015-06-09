using OpenCvSharp.CPlusPlus;

namespace ImageRecognition
{
    class Program
    {
        static void Main()
        {
            Mat src = new Mat("images/model.jpg");
            Recognizer worker = new Recognizer(src);
            worker.PrintAllImages();

            src = new Mat("images/sample2.jpg");
            worker = new Recognizer(src);
            worker.PrintResults();

            src = new Mat("images/easy.jpg");
            worker = new Recognizer(src);
            worker.PrintResults();

            src = new Mat("images/puzzle.jpg");
            worker = new Recognizer(src);
            worker.PrintResults();

            src = new Mat("images/dwa.jpg");
            worker = new Recognizer(src);
            worker.PrintResults();

            src = new Mat("images/plytka.jpg");
            worker = new Recognizer(src);
            worker.PrintResults();

            src = new Mat("images/trzy.jpg");
            worker = new Recognizer(src);
            worker.PrintResults();

            src = new Mat("images/wiele_rowno.jpg");
            worker = new Recognizer(src);
            worker.PrintResults();

            src = new Mat("images/hard_rowno.jpg");
            worker = new Recognizer(src);
            worker.PrintResults();
        }
    }
}