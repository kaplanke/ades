// Accord Math Library
// Accord.NET framework
// http://www.crsouza.com
//
// Copyright © César Souza, 2009
// cesarsouza@gmail.com
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Accord.Math.Decompositions;


namespace Accord.Math
{
    public static class Matrix
    {
        public static double[,] Multiply(this double[,] a, double[,] b)
        {
            double[,] r = new double[a.GetLength(0), b.GetLength(1)];

            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < b.GetLength(1); j++)
                    for (int k = 0; k < a.GetLength(1); k++)
                        r[i, j] += a[i, k] * b[k, j];

            return r;
        }

        public static double[,] Multiply(this double[,] a, double x)
        {
            double[,] r = new double[a.GetLength(0), a.GetLength(1)];

            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                        r[i, j] += a[i, j] * x;

            return r;
        }

        public static double[,] Sum(this double[,] a, double x)
        {
            double[,] r = new double[a.GetLength(0), a.GetLength(1)];

            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                    r[i, j] += a[i, j] + x;

            return r;
        }

        public static double[,] Sum(this double[,] a, double[,] b)
        {
            double[,] r = new double[a.GetLength(0), a.GetLength(1)];

            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                    r[i, j] += a[i, j] + b[i,j];

            return r;
        }

        /// <summary>Returns a sub matrix extracted from the current matrix.</summary>
        /// <param name="startRow">Start row index</param>
        /// <param name="endRow">End row index</param>
        /// <param name="startColumn">Start column index</param>
        /// <param name="endColumn">End column index</param>
        public static double[,] Submatrix(this double[,] data, int startRow, int endRow, int startColumn, int endColumn)
        {
            int rows = data.GetLength(0);
            int cols = data.GetLength(1);

            if ((startRow > endRow) || (startColumn > endColumn) || (startRow < 0) ||
                (startRow >= rows) || (endRow < 0) || (endRow >= rows) ||
                (startColumn < 0) || (startColumn >= cols) || (endColumn < 0) ||
                (endColumn >= cols)) 
            {
                throw new ArgumentException("Argument out of range.");
            }

            double[,] X = new double[endRow - startRow + 1, endColumn - startColumn + 1];
            for (int i = startRow; i <= endRow; i++)
            {
                for (int j = startColumn; j <= endColumn; j++)
                {
                    X[i - startRow,j - startColumn] = data[i,j];
                }
            }

            return X;
        }

        /// <summary>Returns a sub matrix extracted from the current matrix.</summary>
        /// <param name="rowIndexes">Array of row indices</param>
        /// <param name="columnIndexes">Array of column indices</param>
        public static double[,] Submatrix(this double[,] data, int[] rowIndexes, int[] columnIndexes)
        {
            double[,] X = new double[rowIndexes.Length, columnIndexes.Length];

            for (int i = 0; i < rowIndexes.Length; i++)
            {
                for (int j = 0; j < columnIndexes.Length; j++)
                {
                    if ((rowIndexes[i] < 0) || (rowIndexes[i] >= data.GetLength(0)) ||
                        (columnIndexes[j] < 0) || (columnIndexes[j] >= data.GetLength(1)))
                    {
                        throw new ArgumentException("Argument out of range.");
                    }

                    X[i,j] = data[rowIndexes[i], columnIndexes[j]];
                }
            }

            return X;
        }

        /// <summary>Returns a sub matrix extracted from the current matrix.</summary>
        /// <param name="i0">Starttial row index</param>
        /// <param name="i1">End row index</param>
        /// <param name="c">Array of row indices</param>
        public static double[,] Submatrix(this double[,] data, int i0, int i1, int[] c)
        {
            if ((i0 > i1) || (i0 < 0) || (i0 >= data.GetLength(0))
                || (i1 < 0) || (i1 >= data.GetLength(0)))
            {
                throw new ArgumentException("Argument out of range.");
            }

            double[,] X = new double[i1 - i0 + 1, c.Length];

            for (int i = i0; i <= i1; i++)
            {
                for (int j = 0; j < c.Length; j++)
                {
                    if ((c[j] < 0) || (c[j] >= data.GetLength(1)))
                    {
                        throw new ArgumentException("Argument out of range.");
                    }

                    X[i - i0,j] = data[i,c[j]];
                }
            }

            return X;
        }

        /// <summary>Returns a sub matrix extracted from the current matrix.</summary>
        /// <param name="r">Array of row indices</param>
        /// <param name="j0">Start column index</param>
        /// <param name="j1">End column index</param>
        public static double[,] Submatrix(this double[,] data, int[] r, int j0, int j1)
        {
            if ((j0 > j1) || (j0 < 0) || (j0 >= data.GetLength(1)) || (j1 < 0)
                || (j1 >= data.GetLength(1)))
            {
                throw new ArgumentException("Argument out of range.");
            }

            double[,] X = new double[r.Length, j1 - j0 + 1];

            for (int i = 0; i < r.Length; i++)
            {
                for (int j = j0; j <= j1; j++)
                {
                    if ((r[i] < 0) || (r[i] >= data.GetLength(0)))
                    {
                        throw new ArgumentException("Argument out of range.");
                    }

                    X[i,j - j0] = data[r[i],j];
                }
            }

            return X;
        }

        public static bool IsSquare(this double[,] matrix)
        {
            return matrix.GetLength(0) == matrix.GetLength(1);
        }

        public static bool IsSymmetric(this double[,] matrix)
        {
            if (matrix.IsSquare())
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j <= i; j++)
                    {
                        if (matrix[i, j] != matrix[j, i])
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>Inverse of the matrix if matrix is square, pseudoinverse otherwise.</summary>
        public static double[,] Inverse(this double[,] m)
        {
            return m.Solve(Matrix.Diagonal(m.GetLength(0), 1.0));
        }

        /// <summary>Returns the LHS solution vetor if the matrix is square or the least squares solution otherwise.</summary>
        public static double[,] Solve(this double[,] m, double[,] rightSide)
        {
            return (m.GetLength(0) == m.GetLength(1)) ?
                new LuDecomposition(m).Solve(rightSide) :
                new QrDecomposition(m).Solve(rightSide);
        }

        /// <summary>Returns a square diagonal matrix of the given size.</summary>
        public static double[,] Diagonal(int size, double value)
        {
            double[,] m = new double[size, size];

            for (int i = 0; i < size; i++)
                m[i, i] = value;

            return m;
        }

        public static double[] GetColumn(this double[,] m, int index)
        {
            double[] column = new double[m.GetLength(0)];

            for (int i = 0; i < column.Length; i++)
            {
                column[i] = m[i, index];
            }
            return column;
        }

        public static double[] GetRow(this double[,] m, int index)
        {
            double[] row = new double[m.GetLength(1)];

            for (int i = 0; i < row.Length; i++)
            {
                row[i] = m[index, i];
            }
            return row;
        }

        public static double[,] Transpose(this double[,] m)
        {
            double[,] t = new double[m.GetLength(1), m.GetLength(0)];
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    t[j, i] = m[i, j];
                }
            }
            return t;
        }

        /// <summary>
        ///   In linear algebra, the trace of an n-by-n square matrix A is defined to be the sum of the elements on the main diagonal (the diagonal from the upper left to the lower right) of A
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static double Trace(this double[,] m)
        {
            double trace = 0.0;
            for (int i = 0; i < m.GetLength(0); i++)
            {
                trace += m[i, i];
            }
            return trace;
        }
    }
}
