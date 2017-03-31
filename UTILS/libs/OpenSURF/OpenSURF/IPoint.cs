using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSURF
{

    public class Ipoint
    {
        //! Coordinates of the detected interest point
        public float x, y;

        //! Detected scale
        public float scale;

        //! Orientation measured anti-clockwise from +ve x-axis
        public float orientation;

        //! Sign of laplacian for fast matching purposes
        public int laplacian;

        //! Vector of descriptor components
        public float[] descriptor;

        //! Placeholds for point motion (can be used for frame to frame motion analysis)
        public float dx, dy;

        public float responseVal;

        public Ipoint()
        {
            descriptor=new float[64];
        }

        public Ipoint(Ipoint pIPoint)
        {
            x = pIPoint.x;
            y = pIPoint.y;
            scale = pIPoint.scale;
            orientation = pIPoint.orientation;
            laplacian = pIPoint.laplacian;
            descriptor = pIPoint.descriptor.Clone() as float[];
            dx = pIPoint.x;
            dy = pIPoint.x;
        }

        public override string ToString()
        {
            return "Ipoint x=" + x + " y=" + y + " scale=" + scale + " orientation=" + orientation + " laplacian=" + laplacian + " responseVal=" + responseVal;
        }

    }

}
