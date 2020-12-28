using System;

namespace lr_5
{
    class Program
    {
        static void Main(string[] args)
        {
            double e_4 = Math.Pow(10, -4);
            double e_8 = Math.Pow(10, -8);

            double[] x0 = { 0, 0 };
            //double[] x0 = { 4, -1 };

            GradientDescent(x0, e_4);
            //GradientDescent(x0, e_8);
        }

        static void GradientDescent(double[] x0, double e)
        {
            int n = 2;
            int step = 0;
            double h0 = 10;
            int fanc = 0;
            double[] x = new double[n];
            double[] g = GetGrad(x0[0], x0[1]);
            double[] x_x0 = new double[n];
            if (GetNorm(g) > e)
            {
                do
                {
                    Array.Copy(x0, x, 2);
                    double h = FindH(n, x0, g, h0, e);
                    for (int i = 0; i < n; i++)
                    {
                        x0[i] = x[i] - h * g[i];
                    }
                    g = GetGrad(x0[0], x0[1]);
                    for (int i = 0; i < n; i++)
                    {
                        x_x0[i] = x[i] - x0[i];
                    }
                    fanc++;
                    //Console.WriteLine($"step {step}, x*={x0[0]}, {x0[1]}, f*={GetFunction(x0[0], x0[1])}");
                    step++;
                }
                while (!(GetNorm(x_x0) < e || GetNorm(g) < e));
            }
            Console.WriteLine($"fanc on osn {fanc}");
            Console.WriteLine($"min={GetFunction(x0[0], x0[1])} on x*={x0[0]};{x0[1]} with {step} steps");
        }

        private static double FindH(int n, double[] x0, double[] g, double h0, double e)
        {
            double h = 0;
            double f1 = GetFunction(x0[0], x0[1]);
            double f2 = 0;
            double[] x1 = new double[n];
            double[] x2 = new double[n];

            int fanc = 0;

            do
            {
                h0 = h0 / 2;
                for (int i = 0; i < n; i++)
                {
                    x2[i] = x0[i] - h0 * g[i];
                }
                f2 = GetFunction(x2[0], x2[1]);
                fanc++;
            }
            while (!(f1 > f2 || h0 < e));
            if (h0 > e)
            {
                do
                {
                    Array.Copy(x2, x1, n);
                    f1 = f2;
                    h = h + h0;
                    for (int i = 0; i < n; i++)
                    {
                        x2[i] = x1[i] - h * g[i];
                    }
                    f2 = GetFunction(x2[0],x2[1]);
                    fanc++;
                }
                while (!(f1 < f2));
                double ha = h - 2 * h0; // 
                double hb = h;
                double q = e / 3;
                do
                {
                    double h1 = (ha + hb - q) / 2;
                    double h2 = (ha + hb + q) / 2;
                    for (int i = 0; i < n; i++)
                    {
                        x1[i] = x0[i] - h1 * g[i];
                        x2[i] = x0[i] - h2 * g[i];
                    }
                    f1 = GetFunction(x1[0], x1[1]);
                    f2 = GetFunction(x2[0], x2[1]);
                    if (f1 <= f2)
                    {
                        hb = h2;
                    }
                    else
                    {
                        ha = h1;
                    }
                    fanc++;
                    fanc++;
                }
                while (!(hb - ha < e));
                h = (ha + hb) / 2;
            }
            Console.WriteLine($"fanc {fanc}");
            return h;
        }

        static double[] GetGrad(double x1, double x2)
        {
            double[] ans = { GetGradX1(x1, x2), GetGradX2(x1, x2) };
            return ans;
        }

        static double GetFunction(double x1, double x2)
        {
            double f_x = 100 * Math.Pow((x2 - x1 * x1), 2) + Math.Pow(1 - x1, 2);
            //double f_x = x1 * x1 - x1 * x2 + 8 * x2 * x2 - 2 * x1 + x2;
            return f_x;
        }

        static double GetGradX1(double x1, double x2)
        {
            return -400 * x1 * (x2 - x1 * x1) + 2 * x1 - 2;
            //return -x2 + 2 * x1 - 2;
        }

        static double GetGradX2(double x1, double x2)
        {
            return 200 * x2 - 200 * x1 * x1;
           // return 16 * x2 - x1 + 1;
        }

        static double GetNorm(double[] arr)
        {
            double sq = 0;
            for (int i = 0; i < 2; i++)
            {
                sq = sq + (arr[i] * arr[i]);
            }
            return Math.Sqrt(sq);
        }

        static void PrintOutput(double[] xZir, double FZir, int k)
        {
            Console.WriteLine($"x*=({xZir[0]};{xZir[1]}), f*={FZir} on k={k} step");
        }
    }
}
