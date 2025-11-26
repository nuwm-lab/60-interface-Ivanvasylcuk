using System;
using System.Globalization;

namespace Geometry
{
    public interface IPrintable
    {
        void PrintCoefficients();
        void CheckPoint();
    }

    public abstract class LinearInequality
    {
        protected double _b;

        public double B
        {
            get => _b;
            set => _b = value;
        }

        public LinearInequality() { }

        public LinearInequality(double b)
        {
            _b = b;
        }

        ~LinearInequality()
        {
            Console.WriteLine("Викликано деструктор LinearInequality");
        }

        protected double ReadDouble(string message)
        {
            double value;
            Console.Write(message);

            while (!double.TryParse(Console.ReadLine(),
                   NumberStyles.Float, CultureInfo.InvariantCulture, out value))
            {
                Console.Write("Помилка! Введіть коректне число: ");
            }

            return value;
        }

        public abstract void SetCoefficients();
    }

    public class HalfPlane : LinearInequality, IPrintable
    {
        protected double _a1, _a2;

        public HalfPlane() { }

        public HalfPlane(double a1, double a2, double b)
            : base(b)
        {
            _a1 = a1;
            _a2 = a2;
        }

        ~HalfPlane()
        {
            Console.WriteLine("Викликано деструктор HalfPlane");
        }

        public override void SetCoefficients()
        {
            _a1 = ReadDouble("Введіть a1: ");
            _a2 = ReadDouble("Введіть a2: ");
            _b  = ReadDouble("Введіть b: ");
        }

        public void PrintCoefficients()
        {
            Console.WriteLine($"Півплощина: {_a1} * x1 + {_a2} * x2 <= {_b}");
        }

        public void CheckPoint()
        {
            Console.WriteLine("\nВведіть координати точки:");
            double x1 = ReadDouble("x1 = ");
            double x2 = ReadDouble("x2 = ");

            double left = _a1 * x1 + _a2 * x2;

            Console.WriteLine(left <= _b
                ? "Точка належить півплощині."
                : "Точка не належить півплощині.");
        }
    }

    public class HalfSpace : LinearInequality, IPrintable
    {
        private double _a3;
        private double _a1, _a2;

        public HalfSpace() { }

        public HalfSpace(double a1, double a2, double a3, double b)
            : base(b)
        {
            _a1 = a1;
            _a2 = a2;
            _a3 = a3;
        }

        ~HalfSpace()
        {
            Console.WriteLine("Викликано деструктор HalfSpace");
        }

        public override void SetCoefficients()
        {
            _a1 = ReadDouble("Введіть a1: ");
            _a2 = ReadDouble("Введіть a2: ");
            _a3 = ReadDouble("Введіть a3: ");
            _b  = ReadDouble("Введіть b: ");
        }

        public void PrintCoefficients()
        {
            Console.WriteLine($"Півпростір: {_a1} * x1 + {_a2} * x2 + {_a3} * x3 <= {_b}");
        }

        public void CheckPoint()
        {
            Console.WriteLine("\nВведіть координати точки:");
            double x1 = ReadDouble("x1 = ");
            double x2 = ReadDouble("x2 = ");
            double x3 = ReadDouble("x3 = ");

            double left = _a1 * x1 + _a2 * x2 + _a3 * x3;

            Console.WriteLine(left <= _b
                ? "Точка належить півпростору."
                : "Точка не належить півпростору.");
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Оберіть об’єкт:\n1 — Півплощина\n2 — Півпростір");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
            {
                Console.Write("Помилка! Введіть 1 або 2: ");
            }

            IPrintable obj;

            if (choice == 1)
                obj = new HalfPlane();
            else
                obj = new HalfSpace();

            if (obj is LinearInequality li)
                li.SetCoefficients();

            obj.PrintCoefficients();
            obj.CheckPoint();

            Console.WriteLine("\nПрограма завершена.");
        }
    }
}
