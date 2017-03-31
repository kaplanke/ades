
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

using OpenSURF;

namespace Test_OpenSURF_Winform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bnLoad_Click(object sender, EventArgs e)
        {
            lbImages.Items.Clear();

            string[] apath = Directory.GetFiles(tbDirectory.Text,"*.BMP");
            if (apath == null) return;

            foreach (string path in apath)
            {
                lbImages.Items.Add(path);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            nudOctaves.Value = (int)CFastHessian.OCTAVES;
            nudIntervals.Value = (int)CFastHessian.INTERVALS;
            nudInit_Sample.Value = (int)CFastHessian.INIT_SAMPLE;
            nudThreshold.Value = (decimal)CFastHessian.THRES*1000;
            nudinterp_steps.Value = (int)CFastHessian.INTERP_STEPS;

            nudInit_Sample.Value = (int)10;
        }

        private void bnReflect_Click(object sender, EventArgs e)
        {
            updateSURFImage();
        }

        Bitmap m_pcurrentImage = null;

        private void update_currentImage()
        {
            string currentpath = null;
            try
            {
                if (m_pcurrentImage != null)
                {
                    m_pcurrentImage.Dispose();
                    m_pcurrentImage=null;
                }

                currentpath = lbImages.SelectedItem as string;
                if (currentpath == null) return;

                m_pcurrentImage = new Bitmap(currentpath);

            }
            catch
            {
            }
            finally
            {
                string log = "SURF Image: " + currentpath;
                if (m_pcurrentImage != null)
                {
                    log += " (W=" + m_pcurrentImage.Width + " H=" + m_pcurrentImage.Height + ")";
                }
                gbSURFImage.Text = log;
                pnSURFImage.BackgroundImage = m_pcurrentImage;
            }
        }

        private void updateSURFImage()
        {
            IplImage pIplImage = null;
            List<Ipoint> aIpoint = null;
            DateTime t0 = DateTime.Now;
            long dt = 0;
            try
            {
                if (m_pcurrentImage == null) return;

                pIplImage = IplImage.LoadImage(m_pcurrentImage);
                if (pIplImage == null) return;

                dt = (DateTime.Now.Ticks - t0.Ticks) / 10000;
                log("updateSURFImage: LoadImage(+) DT(ms)="+dt);

                bool upright = cbUpright.Checked;
                int octaves = (int)nudOctaves.Value;
                int intervals = (int)nudIntervals.Value;
                int init_sample = (int)nudInit_Sample.Value;
                float thres = (float)nudThreshold.Value / 1000f;
                int interp_steps = (int)nudinterp_steps.Value;

                log("updateSURFImage: surfDetDes(-) upright=" + upright + " octaves=" + octaves + " intervals=" + intervals + " init_sample=" + init_sample + " thres=" + thres + " interp_steps" + interp_steps);

                COpenSURF.surfDetDes(null,
                                        pIplImage,
                                        out aIpoint,
                                        upright,
                                        octaves,
                                        intervals,
                                        init_sample,
                                        thres,
                                        interp_steps);

                dt = (DateTime.Now.Ticks - t0.Ticks) / 10000;

                log("updateSURFImage: surfDetDes(+) DT(ms)=" + dt + " aIpoint=" + (aIpoint!=null ? aIpoint.Count.ToString():"NULL"));

                pnSURFImage.BackgroundImage = paintSURFPoints(m_pcurrentImage, aIpoint);

            }
            catch (Exception E)
            {
                log("updateSURFImage Exception=" + E.Message+" "+E.StackTrace);
            }
            finally
            {
            }
        }

        private Bitmap paintSURFPoints(Bitmap pBitmap, List<Ipoint> aIpoint)
        {
            Bitmap vret = null;

            if (pBitmap == null) return vret;

            vret = new Bitmap(pBitmap.Width, pBitmap.Height, PixelFormat.Format24bppRgb);

            Graphics pgd = null;
            Pen ppenred = null;
            Pen ppenblue = null;
            try
            {
                pgd = Graphics.FromImage(vret);

                pgd.DrawImage(pBitmap, new Rectangle(0, 0, pBitmap.Width, pBitmap.Height));

                if (aIpoint == null) return vret;

                ppenred = new Pen(Color.Red);
                ppenblue = new Pen(Color.Blue);

                foreach (Ipoint pIpoint in aIpoint)
                {
                    if (pIpoint == null) continue;

                    int xd = (int)pIpoint.x;
                    int yd = (int)pIpoint.y;
                    float scale = pIpoint.scale;
                    float orientation = pIpoint.orientation;
                    float radius = ((9.0f / 1.2f) * scale) / 3.0f;

                    Pen ppen = (pIpoint.laplacian > 0 ? ppenred : ppenblue);

                    pgd.DrawEllipse(ppen, xd - radius, yd - radius, 2 * radius, 2 * radius);

                    double dx = radius * Math.Cos(orientation);
                    double dy = radius * Math.Sin(orientation);
                    pgd.DrawLine(ppen, new Point(xd, yd), new Point((int)(xd+dx),(int)(yd+dy)));

                }

            }
            finally
            {
                if (ppenred != null) ppenred.Dispose();
                if (ppenblue != null) ppenblue.Dispose();
                if (pgd != null) pgd.Dispose();
            }

            return vret;
        }

        private void lbImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            update_currentImage();
            updateSURFImage();
        }

        private void log(string Text)
        {
            lbLog.Items.Add(Text);
        }

        private void clearlog()
        {
            lbLog.Items.Clear();
        }

        private void bnClearLog_Click(object sender, EventArgs e)
        {
            clearlog();
        }

        private void cbUpright_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAutoReflect.Checked)
            {
                updateSURFImage();
            }
        }

        private void nudOctaves_ValueChanged(object sender, EventArgs e)
        {
            if (cbAutoReflect.Checked)
            {
                updateSURFImage();
            }
        }

        private void nudIntervals_ValueChanged(object sender, EventArgs e)
        {
            if (cbAutoReflect.Checked)
            {
                updateSURFImage();
            }
        }

        private void nudInit_Sample_ValueChanged(object sender, EventArgs e)
        {
            if (cbAutoReflect.Checked)
            {
                updateSURFImage();
            }
        }

        private void nudThreshold_ValueChanged(object sender, EventArgs e)
        {
            if (cbAutoReflect.Checked)
            {
                updateSURFImage();
            }
        }

        private void nudinterp_steps_ValueChanged(object sender, EventArgs e)
        {
            if (cbAutoReflect.Checked)
            {
                updateSURFImage();
            }
        }

    }
}
