using OpenCvSharp.CPlusPlus;

namespace ImageRecognition
{
    class Program
    {
        static void Main()
        {
            Mat src = new Mat("images/plytka.jpg");
            Recognizer worker = new Recognizer(src);
            worker.PrintAllImages();
        }
    }
}