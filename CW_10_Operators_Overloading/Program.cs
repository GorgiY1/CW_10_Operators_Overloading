using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CW_10_Operators_Overloading
{
    class Point2D
    {
        public int x { get; set; }
        public int y { get; set; }

        //unary operator: - ++ --
        public static Point2D operator -(Point2D p)
        {
            return new Point2D { x = -p.x, y = -p.y };
        }

        public static Point2D operator ++(Point2D p)
        {
            p.x++;
            p.y++;
            return p;
        }

        public static Point2D operator --(Point2D p)
        {
            p.x--;
            p.y--;
            return p;
        }

        //compare operatins !=  ==  >  <  >=  <=
        public override bool Equals(object obj)
        {
            return this.ToString() == obj.ToString();
        }
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public static bool operator ==(Point2D p1, Point2D p2)
        {
            //ReferenceEquals(p1, p2);
            if (p1 is null || p2 is null)
            {
                throw new Exception();
            }
            return p1.Equals(p2);
        }

        public static bool operator !=(Point2D p1, Point2D p2)
        {
            return !(p1 == p2);
        }

        public static bool operator >(Point2D p1, Point2D p2)
        {
            return (p1.x * p1.x + p1.y * p1.y) > (p2.x * p2.x + p2.y * p2.y);
        }
        public static bool operator <(Point2D p1, Point2D p2)
        {
            return !(p1 > p2);
        }

        public static bool operator >=(Point2D p1, Point2D p2)
        {
            return !(p1 < p2);
        }
        public static bool operator <=(Point2D p1, Point2D p2)
        {
            return !(p1 > p2);
        }

        //operations overload true and false
        public static bool operator true(Point2D p)
        {
            return p.x != 0 || p.y != 0;
        }
        public static bool operator false(Point2D p)
        {
            return p.x == 0 && p.y == 0;
        }
        public override string ToString()
        {
            return $"X = {x} Y = {y}";
        }
    }

    class Vector
    {
        public int x { get; set; }
        public int y { get; set; }

        public Vector()
        {

        }
        public Vector(Point2D p1, Point2D p2)
        {
            x = p2.x - p1.x;
            y = p2.y - p1.y;
        }

        //binary operations + - 

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector { x = v1.x + v2.y, y = v1.y + v2.y };
        }
        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector { x = v1.x - v2.y, y = v1.y - v2.y };
        }
        public static Vector operator *(Vector v, int n)
        {
            v.x *= n;
            v.y *= n;
            return v;
        }

        public override string ToString()
        {
            return $"X = {x} Y = {y}";
        }
    }
    //indexer

    class Student
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public override string ToString()
        {
            return $"{LastName},{FirstName}";

        }
    }

    class Group
    {
        private Student[] _students;
        public Group()
        {
            _students = new Student[]
            {
                new Student{LastName = "Robertson",FirstName = "Nick" },
                new Student{LastName = "Day",FirstName = "Jeremy" },
                new Student{LastName = "Wilson",FirstName = "Robert" }
            };
        }

        public Student this [int index]
        {
            get 
            { 
                return _students[index]; 
            }
            set
            {
                _students[index] = value;
            }
        }
    }

    //multi indexer
    public class Laptop
    {
        public string Vendor { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return $"{Vendor} {Price}";
        }
    }

    enum Vendors { Samsung, Asus, LG };

    public class MyClass
    {
        public int Number { get; set; }
    }

    public class Shop
    {
        private Laptop[] _laptopArr;

        public Shop(int size)
        {
            _laptopArr = new Laptop[size];
        }
        public int Length
        {
            get { return _laptopArr.Length; }
        }

        public Laptop this[MyClass index]
        {
            get
            {
                if (index.Number >= 0 && index.Number < _laptopArr.Length)
                {
                    return _laptopArr[index.Number];
                }
                throw new IndexOutOfRangeException();
            }
            set { /* set the specified index to value here */ }
        }

        public Laptop this[int index]
        {
            get
            {
                if (index >= 0 && index < _laptopArr.Length)
                {
                    return _laptopArr[index];
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (index >= 0 && index < _laptopArr.Length)
                {
                    _laptopArr[index] = value;
                }
            }
        }

        public Laptop this[string name]
        {
            get
            {
                if (Enum.IsDefined(typeof(Vendors), name))
                {
                    return _laptopArr[(int)Enum.Parse(typeof(Vendors), name)];
                }
                else
                {
                    return new Laptop();
                }
            }
            set
            {
                if (Enum.IsDefined(typeof(Vendors), name))
                {
                    _laptopArr[(int)Enum.Parse(typeof(Vendors), name)] = value;
                }
            }
        }

        private int FindByPrice(double price)
        {
            for (int i = 0; i < _laptopArr.Length; i++)
            {
                if (_laptopArr[i].Price == price)
                {
                    return i;
                }
            }
            return -1;
        }

        public Laptop this[double price]
        {
            get
            {
                int index = FindByPrice(price);
                if (index >= 0)
                {
                    return this[index];
                }
                throw new Exception("Недопустимая стоимость.");
            }
            set
            {
                int index = FindByPrice(price);
                if (index >= 0)
                {
                    this[index] = value;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //indexer

            Group group = new Group();
            Console.WriteLine(group[0]);

            Shop shop = new Shop(3);
            shop[0] = new Laptop { Vendor = "Samsung", Price = 5200 };
            shop[1] = new Laptop { Vendor = "Asus", Price = 4700 };
            shop[2] = new Laptop { Vendor = "LG", Price = 4300 };

            MyClass myClass = new MyClass { Number = 1 };
            Console.WriteLine(shop[myClass]);

            try
            {
                for (int i = 0; i < shop.Length; i++)
                {
                    Console.WriteLine(shop[i]);
                }
                Console.WriteLine();

                Console.WriteLine($"Производитель Asus: {shop["Asus"]}.");

                Console.WriteLine($"Производитель HP: {shop["HP"]}.");

                shop["HP"] = new Laptop(); // игнорирование

                Console.WriteLine($"Стоимость 4300: {shop[4300.0]}.");

                // недопустимая стоимость
                Console.WriteLine($"Стоимость 10500: {shop[10500.0]}.");

                shop[10500.0] = new Laptop(); // игнорирование
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            //Point2D point1 = new Point2D { X = 12, Y = -34 };

            //Point2D point2 = -point1;
            //Console.WriteLine(point1);

            //Console.WriteLine(point1++);
            //Console.WriteLine(++point1);

            //point2--;
            //Console.WriteLine(point2);

            //Point2D point1 = new Point2D { x = -2, y = 2 };
            //Point2D point2 = new Point2D { x = 3, y = 5 };

            //Vector vector1 = new Vector(point1, point2);
            //Console.WriteLine(vector1);

            //Vector vector2 = new Vector { x = 4,y=6};
            //Console.WriteLine(vector2);
            //Console.WriteLine(vector1+vector2);
            //Console.WriteLine(vector1-vector2);
            //Console.WriteLine(vector1*2);

            //double n = 6.7;
            //object obj = n; // pack 'n' to reference type

            //double n1 = (double)obj; // unboxing from reference type
            //Console.WriteLine(n1);

            //int n1 = 34;
            //int n2 = 34;
            //int n3 = 56;
            //Console.WriteLine(object.Equals(n1, n2)); // true
            //Console.WriteLine(object.Equals(n1, n3)); // false

            //Console.WriteLine(object.ReferenceEquals(n1, n2)); // false

            //Point2D p = new Point2D { x = 3, y = 7 };
            //Point2D p1 = p;

            //Console.WriteLine(object.ReferenceEquals(p, p1)); // true

            //string str1 , str2;
            //str1 = str2 = "Helli";

            //Console.WriteLine(object.Equals(str1, str2)); //true
            //Console.WriteLine(object.ReferenceEquals(str1, str2)); //True

            //string str11 = "Helli"; string str22 = "Helli";

            //Console.WriteLine(object.Equals(str1, str2)); //true
            //Console.WriteLine(object.ReferenceEquals(str11, str22)); //True ????
            // object
            //try
            //{
            //    Point2D point1 = new Point2D { x = -2, y = 2 }; // null
            //    Point2D point2 = new Point2D { x = 3, y = 5 }; // null

            //    Console.WriteLine(point1 == point2);
            //    Console.WriteLine(point1 != point2);
            //    Console.WriteLine(point1 < point2);
            //    Console.WriteLine(point1 > point2);
            //    Console.WriteLine(point1 >= point2);
            //    Console.WriteLine(point1 <= point2);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            //Point2D point1 = new Point2D { x = -2, y = 2 };

            //if (point1)
            //{
            //    Console.WriteLine("True");
            //}
            //Point2D point2 = new Point2D { x = 0, y = 0 };
            //if (point2)
            //{

            //}
            //else
            //{
            //    Console.WriteLine("False");
            //}



            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
        }
    }

    //using static System.Console;
    //namespace SimpleProject
    //{
    //    abstract class Figure { public abstract void Draw(); }
    //    abstract class Quadrangle : Figure { }
    //    class Rectangle : Quadrangle
    //    {
    //        public int Width { get; set; }
    //        public int Height { get; set; }
    //        public static implicit operator Rectangle(Square s) { return new Rectangle { Width = s.Length * 2, Height = s.Length }; }
    //        public override void Draw() { for (int i = 0; i < Height; i++, WriteLine()) { for (int j = 0; j < Width; j++) { Write("*"); } } WriteLine(); }
    //        public override string ToString()
    //        {
    //            return$"Rectangle: Width = {Width}, Height = {Height}";
    //        }
    //    }
    //    class Square : Quadrangle
    //    {
    //        public int Length { get; set; }
    //        public static explicit operator Square(Rectangle rect) { return new Square { Length = rect.Height }; }
    //        public static explicit operator int(Square s) { return s.Length; }
    //        public static implicit operator Square(int number) { return new Square { Length = number }; }
    //        public override void Draw() { for (int i = 0; i < Length; i++, WriteLine()) { for (int j = 0; j < Length; j++) { Write("*"); } } WriteLine(); }
    //        public override string ToString()
    //        {
    //            return$"Square: Length = {Length}";
    //        }
    //    }
    //    class Program
    //    {
    //        static void Main(string[] args)
    //        {
    //            Rectangle rectangle = new Rectangle { Width = 5, Height = 10 };
    //            Square square = new Square { Length = 7 };
    //            Rectangle rectSquare = square;
    //            WriteLine($"Неявное преобразование квадрата ({square}) к прямоугольнику.  n{rectSquare}\n");
    //            //rectSquare.Draw();Square squareRect = (Square)rectangle;  WriteLine($"Явное преобразование прямоугольника ({rectangle}) к квадрату.\n{squareRect}\n");//squareRect.Draw();            WriteLine("Введите целое число.");int number = int.Parse(ReadLine());Square squareInt = number;            WriteLine($"Неявное преобразование целого                       ({number}) к квадрату.\                      n{squareInt}\n");//squareInt.Draw();            number = (int)square;            WriteLine($"Явное преобразование квадрата ({square}) к целому.\n{number}");        }    }}
    //            ReadKey();

    //        }
    //    }
    //}
}

