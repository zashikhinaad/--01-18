
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    abstract class Function
    {
        public int accuracy;    //Число разбиений интервала
        protected double interval_start;   //Начало интервала
        protected double interval_end;     //Конец интервала
        protected double[] values;         //Значения функции

        protected void FunctionInput()
        {
            Console.WriteLine("Введите число разбиений интервала:");
            accuracy = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите начало интервала:");
            interval_start = double.Parse(Console.ReadLine());

            Console.WriteLine("Введите конец интервала:");
            interval_end = double.Parse(Console.ReadLine());
        }

        public void FillValue()
        {
            double step = Math.Round((interval_end - interval_start) / accuracy, 5);
            if (interval_end - interval_start != 0)
            {
                accuracy += 1;
            }
            values = new double[accuracy];
            double x = interval_start;
            for (int i = 0; i < values.Length; i++)
            {
                x = interval_start + step * i;
                if (interval_end - x < step)
                {
                    x = interval_end;
                }
                values[i] = PolynomValue(x);
            }

        }

        protected abstract double PolynomValue(double x);

        public void Output()
        {
            foreach (double value in values)
            {
                Console.WriteLine("Значение функции: {0}", value);
            }
        }
    }

    class Polynom : Function
    {
        public int n;           //Степень полинома
        public double[] odds;   //Коэффициенты полинома

        public void Input()
        {
            Console.WriteLine("Введите степень полинома:");
            n = int.Parse(Console.ReadLine());
            odds = new double[n + 1];

            Console.WriteLine("Введите коэффициенты полинома:");
            for (int i = 0; i <= n; i++)
            {
                odds[i] = double.Parse(Console.ReadLine());
            }

            FunctionInput();
        }

        protected override double PolynomValue(double x)
        {
            double value = 0;
            for (int i = 0; i <= n; i++)
            {
                value += odds[i] * Math.Pow(x, i);
            }
            return value;
        }


    }

    class FunctionExplorer
    {
        private readonly Func<double, double> _Func;

        public FunctionExplorer(Func<double, double> f)
        {
            _Func = f;
        }

        public double GetMinValue(double x1, double x2, double dx)
        {
            double min = double.PositiveInfinity;
            double x = x1;

            while (x <= x2)
            {
                double y = _Func(x);
                if (y < min)
                {
                    min = y;
                }
                x += dx;
            }

            return min;
        }

        public double GetMinValue(double x1, double x2, double dx, out double xmin)
        {
            double min = double.PositiveInfinity;
            double x = x1;
            xmin = x1;

            while (x <= x2)
            {
                double y = _Func(x);
                if (y < min)
                {
                    min = y;
                    xmin = x;
                }
                x += dx;
            }

            return min;
        }

        public FunctionValue GetMin(double x1, double x2, double dx)
        {
            double min = double.PositiveInfinity;
            double x = x1;
            var xmin = x1;

            while (x <= x2)
            {
                double y = _Func(x);
                if (y < min)
                {
                    min = y;
                }
                x += dx;
            }

            return new FunctionValue(xmin, min);
        }
    }

    struct FunctionValue
    {
        public readonly double x;
        public readonly double y;

        public FunctionValue(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public FunctionValue(double x)
        {
            this.x = x;
        }


    }









    class Program
    {
        static void Main(string[] args)
        {
            const double x0 = -5;
            const double y0 = -7;

            Func<double, double> f = x => (x - x0)*(x - x0) + y0;

            var explorer = new FunctionExplorer(f);
            var min = explorer.GetMinValue(-10, 10, 0.01);
            double x_min;
            //min = explorer.GetMinValue(-10, 10, 0.01, out x_min);

            var min_Value = explorer.GetMin(-10, 10, 0.01);

            Console.WriteLine("min f({0})={1}", min_Value.x, min_Value.y); 


            Console.ReadLine();
        }
    }
}
