using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaarTest
{
    class Program
    {

        static public float [,] GetHaarMatrix(int discr_num)
        {
            int row = 0;
            int n = (int)Math.Log(discr_num, 2);
            float t_step = (float)1 / discr_num;
            float[,] matrix = new float[discr_num, discr_num];
            for(int i = 0; i < discr_num; i++)
            {
                matrix[row, i] = 1;
            }
            row++;
            for (float r = 0; r < n; r++)
            {
                for(float m = 1; m<= (int)Math.Pow(2, r); m++)
                {
                    for(int i = 0; i < discr_num; i++)
                    {
                        float t = t_step * i;
                        matrix[row, i] = (float)HaarFunc(r, m, t);
                    }
                    row++;
                }
            }
            return matrix;

        }

        static private double HaarFunc(float r, float m, float t)
        {
            double m1 = ((m - 1) / Math.Pow(2, r));
            double m2 = ((m - 1f / 2f) / Math.Pow(2, r));
            double m3 = (m / Math.Pow(2, r));
            if ((m1 <= t) && (t < m2))
                return Math.Pow(2, r / 2);
            else if ((m2 <= t) && (t < m3))
                return -Math.Pow(2, r / 2);
            else
                return 0;

        }

        static void Main(string[] args)
        {
            int N = 512;
            float[,] matrix = GetHaarMatrix(N);
            for( int i = 0; i < N; i++)
            {
                for( int z =0; z< N; z++)
                {
                    Console.Write(Convert.ToString(matrix[i, z]));
                    if (z != N - 1)
                        Console.Write(" ");

                }
                Console.WriteLine();
            }
            Console.ReadKey();

        }
    }
}
