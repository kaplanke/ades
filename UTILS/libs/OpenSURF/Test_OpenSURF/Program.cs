
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

using OpenSURF;

namespace Test_OpenSURF
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime t0 = DateTime.Now;

            string Path = @"D:\Photosynth\IMAGE_023(2).JPG";
            IplImage pIplImage = IplImage.LoadImage(Path);
            DateTime t1 = DateTime.Now;

            List<Ipoint> aIpoint=null; 

            bool upright=false;
            int octaves = CFastHessian.OCTAVES;
            int intervals = CFastHessian.INTERVALS;
            int init_sample = CFastHessian.INIT_SAMPLE;
            float thres = CFastHessian.THRES;
            int interp_steps = CFastHessian.INTERP_STEPS;

            COpenSURF.surfDetDes(Path,
                                    pIplImage,
                                    out aIpoint,
                                    upright,
                                    octaves,
                                    intervals,
                                    init_sample,
                                    thres,
                                    interp_steps);
            DateTime t2 = DateTime.Now;

            long dt1 = (t1.Ticks - t0.Ticks) / 10000;
            long dt2 = (t2.Ticks - t0.Ticks) / 10000;

            /***
            COpenSURF.Compare_INTFiles(@"D:\Photosynth\IMAGE_023(2).JPG.INT", @"D:\Photosynth\IMAGE_023.JPG.INT");
            int errorcount = COpenSURF.Compare_DETFiles(@"D:\Photosynth\IMAGE_023.JPG.DET", @"D:\Photosynth\IMAGE_023(2).JPG.DET");
            ***/

            COpenSURF.SavePoints(aIpoint,@"D:\Photosynth\IMAGE_023(2).JPG.SURF");

            COpenSURF.PaintOpenSURF(@"D:\Photosynth\IMAGE_023(2).JPG", @"D:\Photosynth\IMAGE_023(2).JPG.SURF", @"D:\Photosynth\IMAGE_023(2).JPG.SURF.JPG");
            COpenSURF.PaintOpenSURF(@"D:\Photosynth\IMAGE_023.JPG", @"D:\Photosynth\IMAGE_023.JPG.SURF", @"D:\Photosynth\IMAGE_023.JPG.SURF.JPG");

            return;

        }
    }
}
