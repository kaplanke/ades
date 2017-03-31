// Accord Math Library
// Accord.NET framework
// http://www.crsouza.com
//
// Copyright © César Souza, 2009
// cesarsouza@gmail.com
//

using System;
using System.Collections.Generic;

using AForge;
using AForge.Math;


namespace Accord.Math
{
	
	public static class Tools
	{
		public static int NextPowerOf2(int x) {
			--x;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return ++x;
		}
		
		public static int PreviousPowerOf2(int x) {
			return NextPowerOf2(x + 1) / 2;
		}
		
		public static int[] Abs(this int[] x)
		{
			int[] r = new int[x.Length];
			for (int i = 0; i < x.Length; i++)
				r[i] = System.Math.Abs(x[i]);
			return r;
		}
		
		public static double[] Abs(this double[] x)
		{
			double[] r = new double[x.Length];
			for (int i = 0; i < x.Length; i++)
				r[i] = System.Math.Abs(x[i]);
			return r;
		}
		
		public static Complex[] Abs(this Complex[] x)
		{
			Complex[] r = new Complex[x.Length];
			for (int i = 0; i < x.Length; i++)
				r[i] = new Complex(x[i].Magnitude,0);
			return r;
		}

        
		public static int Sum(this int[] x)
		{
			int r = 0;
			for (int i = 0; i < x.Length; i++)
				r += x[i];
			return r;
		}

        public static double Sum(this double[] x)
        {
            double r = 0.0;
            for (int i = 0; i < x.Length; i++)
                r += x[i];
            return r;
        }

        public static double Max(this double[] x)
        {
            double max = x[0];
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] > max)
                    max = x[i];
            }
            return max;
        }
		
		public static Complex Sum(this Complex[] x)
		{
			Complex r = Complex.Zero;
			for (int i = 0; i < x.Length; i++)
				r += x[i];
			return r;
		}

        public static double[] Sqrt(this double[] x)
        {
            double[] r = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
                r[i] = System.Math.Sqrt(x[i]);
            return r;
        }

		public static Complex[] Multiply(this Complex[] a, Complex[] b)
		{
			Complex[] r = new Complex[a.Length];
			for (int i = 0; i < a.Length; i++)
			{
				r[i] = Complex.Multiply(a[i], b[i]);
			}
			return r;
		}


        /// <summary>
        ///   Hypotenuse calculus without overflow/underflow
        /// </summary>
        /// <param name="a">first value</param>
        /// <param name="b">second value</param>
        /// <returns>The hypotenuse Sqrt(a^2 + b^2)</returns>
        public static double Hypotenuse(double a, double b)
        {
            double r = 0.0;

            if (System.Math.Abs(a) > System.Math.Abs(b))
            {
                r = b / a;
                r = System.Math.Abs(a) * System.Math.Sqrt(1 + r * r);
            }
            else if (b != 0)
            {
                r = a / b;
                r = System.Math.Abs(b) * System.Math.Sqrt(1 + r * r);
            }

            return r;
        }

        public static DoubleRange GetRange(this double[] array)
        {
            double min = array[0];
            double max = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (min > array[i])
                    min = array[i];
                if (max < array[i])
                    max = array[i];
            }
            return new DoubleRange(min, max);
        }

        public static IntRange GetRange(this int[] array)
        {
            int min = array[0];
            int max = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (min > array[i])
                    min = array[i];
                if (max < array[i])
                    max = array[i];
            }
            return new IntRange(min, max);
        }

        public static DoubleRange GetRange(this Complex[] array)
        {
            double min = array[0].SquaredMagnitude;
            double max = array[0].SquaredMagnitude;

            for (int i = 1; i < array.Length; i++)
            {
                double sqMagnitude = array[i].SquaredMagnitude;
                if (min > sqMagnitude)
                    min = sqMagnitude;
                if (max < sqMagnitude)
                    max = sqMagnitude;
            }

            return new DoubleRange(System.Math.Sqrt(min),
               System.Math.Sqrt(max));
        }

        public static double[] Magnitude(this Complex[] c)
        {
            double[] magnitudes = new double[c.Length];
            for (int i = 0; i < c.Length; i++)
                magnitudes[i] = c[i].Magnitude;

            return magnitudes;
        }

        public static double[] Phase(this Complex[] c)
        {
            double[] phases = new double[c.Length];
            for (int i = 0; i < c.Length; i++)
                phases[i] = c[i].Phase;

            return phases;
        }

        public static double[] Re(this Complex[] c)
        {
            double[] re = new double[c.Length];
            for (int i = 0; i < c.Length; i++)
                re[i] = c[i].Re;

            return re;
        }

        public static double[] Im(this Complex[] c)
        {
            double[] im = new double[c.Length];
            for (int i = 0; i < c.Length; i++)
                im[i] = c[i].Im;

            return im;
        }

        public static double[,] ToArray(this Complex[] c)
        {
            double[,] arr = new double[c.Length, 2];
            for (int i = 0; i < c.GetLength(0); i++)
            {
                arr[i, 0] = c[i].Re;
                arr[i, 1] = c[i].Im;
            }

            return arr;
        }

        /// <summary>
        ///   Converts the value x (which is measured in the scale
        ///   'from') to another value measured in the scale 'to'.
        /// </summary>
        public static int ScaleConvert(this IntRange from, IntRange to, int x)
        {
            return (to.Length) * (x - from.Min) / from.Length + to.Min;
        }

        /// <summary>
        ///   Converts the value x (which is measured in the scale
        ///   'from') to another value measured in the scale 'to'.
        /// </summary>
        public static double ScaleConvert(this DoubleRange from, DoubleRange to, double x)
        {
            return (to.Length) * (x - from.Min) / from.Length + to.Min;
        }

        public static double Factorial(int nValue)
        {
            double factorial = 1.0;

            for (int i = 1; i <= nValue; i++)
            {
                factorial *= (double)i;
            }

            return factorial;
        }
	}
}