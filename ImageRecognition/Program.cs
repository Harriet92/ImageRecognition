// Edge detection by Canny algorithm
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;

class Program
{
    static void Main()
    {
        Mat src = new Mat("Lena.png", LoadMode.GrayScale);
        Mat dst = new Mat();

        Cv2.Canny(src, dst, 50, 200);
        using (new Window("src image", src))
        using (new Window("dst image", dst))
        {
            Cv2.WaitKey();
        }
    }
}