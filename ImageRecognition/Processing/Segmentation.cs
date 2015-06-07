using System;
using System.Collections.Generic;
using System.Linq;
using ImageRecognition.Helpers;
using OpenCvSharp.CPlusPlus;

namespace ImageRecognition.Processing
{
    public class Segmentation
    {
        public int[,] segMap;
        public List<Segment> segments;
        private Random random = new Random();
        private const int dilationSize = 3;
        public void ConvertToBW(Mat I)
        {
            var map = new int[I.Rows, I.Cols];
            var indexer = MatExt.GetMatIndexer(I);
            for (var i = 0; i < I.Rows; i++)
                for (var j = 0; j < I.Cols; j++)
                    map[i, j] = indexer[i, j].IsInRange(ProcessingParams.MinRed, ProcessingParams.MaxRed) ||
                                indexer[i, j].IsInRange(ProcessingParams.MinText, ProcessingParams.MaxText) ||
                                indexer[i, j].IsInRange(ProcessingParams.MinGray, ProcessingParams.MaxGray)
                        ? 1 : 0;
            segMap = map;
            DilateSegMap();
        }

        public Mat PrintBWMap()
        {
            Mat result = new Mat(segMap.GetLength(0), segMap.GetLength(1), MatType.CV_8UC3);
            var rindexer = MatExt.GetMatIndexer(result);
            for (var i = 0; i < segMap.GetLength(0); i++)
                for (var j = 0; j < segMap.GetLength(1); j++)
                    rindexer[i, j] = segMap[i, j] == 0 ? ColorVectors.Black : ColorVectors.White;
            return result;
        }

        public void GetSegments()
        {
            List<Segment> segs = new List<Segment>();
            var tempSegMap = new int[segMap.GetLength(0), segMap.GetLength(1)];
            Buffer.BlockCopy(segMap, 0, tempSegMap, 0, segMap.Length * sizeof(int));
            for (int x = 2; x < tempSegMap.GetLength(0); x++)
            {
                for (int y = 2; y < tempSegMap.GetLength(1); y++)
                {
                    if (tempSegMap[x, y] == 1)
                    {
                        Stack<Point> pending = new Stack<Point>();
                        pending.Push(new Point(x, y));
                        int count = 0;
                        Segment seg = new Segment(x, x, y, y);
                        while (pending.Count != 0)
                        {
                            count++;
                            Point p = pending.Pop();
                            if (tempSegMap[p.X, p.Y] == 1)
                            {
                                tempSegMap[p.X, p.Y] = 0;
                                seg.AddPoint(p.X, p.Y);
                                if (tempSegMap[p.X - 1, p.Y] == 1)
                                    pending.Push(new Point(p.X - 1, p.Y));
                                if (tempSegMap[p.X + 1, p.Y] == 1)
                                    pending.Push(new Point(p.X + 1, p.Y));
                                if (tempSegMap[p.X, p.Y - 1] == 1)
                                    pending.Push(new Point(p.X, p.Y - 1));
                                if (tempSegMap[p.X, p.Y + 1] == 1)
                                    pending.Push(new Point(p.X, p.Y + 1));
                            }
                        }

                        if (count > ProcessingParams.MinSegmentSize)
                        {
                            segs.Add(seg);
                        }
                    }
                }
            }
            segments = segs;
        }

        public Mat PrintSegments()
        {
            Mat result = new Mat(segMap.GetLength(0), segMap.GetLength(1), MatType.CV_8UC3);
            var rindexer = MatExt.GetMatIndexer(result);
            foreach (var segment in segments)
            {
                Vec3b color = new Vec3b((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));
                for (var i = segment.LeftUpperX; i < segment.RightBottomX; i++)
                    for (var j = segment.LeftUpperY; j < segment.RightBottomY; j++)
                        if (segment.Slice[i - segment.LeftUpperX, j - segment.LeftUpperY] == 1)
                            rindexer[i, j] = color;
            }
            return result;
        }

        private void DilateSegMap()
        {
            var tempSegMap = new int[segMap.GetLength(0), segMap.GetLength(1)];
            for (int i = dilationSize / 2; i < segMap.GetLength(0) - dilationSize / 2; ++i)
                for (int j = dilationSize / 2; j < segMap.GetLength(1) - dilationSize / 2; ++j)
                {
                    var sx = i - dilationSize / 2;
                    var sy = j - dilationSize / 2;
                    var vectors = new List<int>();
                    for (int x = 0; x < dilationSize; x++)
                        for (int y = 0; y < dilationSize; y++)
                            vectors.Add(segMap[sx + x, sy + y]);
                    tempSegMap[i, j] = vectors.Max(x => x);
                }
            segMap = tempSegMap;
        }
    }
}
