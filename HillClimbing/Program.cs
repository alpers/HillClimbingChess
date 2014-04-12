using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HillClimbing
{
    class Program
    {
        public static int knight, rook, bishop, queen, king;
        static void Main(string[] args)
        {            
            #region input
            //çapraz tehdit eden taşların ortalara yerleşmesi ihtimali gereği önce fil ve vezir için işlem yapılıyor.
            //satranç tahtasında sol alt (0,0) koordinatları ile tanımlandı
            //if int[][]=0 => null
            //if int[][]=1 => bishop
            //if int[][]=2 => queen
            //if int[][]=3 => rook
            //if int[][]=4 => knight
            //if int[][]=5 => king
            Console.WriteLine("Enter numbers...");
            Console.Write("Bishop:");
            bishop = Convert.ToInt16(Console.ReadLine());
            Console.Write("Queen:");
            queen = Convert.ToInt16(Console.ReadLine());
            Console.Write("Rook:");
            rook = Convert.ToInt16(Console.ReadLine());
            Console.Write("Knight:");
            knight = Convert.ToInt16(Console.ReadLine());
            Console.Write("King:");
            king = Convert.ToInt16(Console.ReadLine());
            #endregion
            Move mv = new Move();
            int size = 12;
            List<Point> list = new List<Point>();
            int[,] matrix = new int[size,size];
            for (int m = 0; m < size; m++) { for (int n = 0; n < size; n++) { matrix[m, n] = 0; } }
            Console.WriteLine("----------");
            Console.WriteLine("BISHOP");
            mv.moving(matrix, bishop, 1);
            Console.WriteLine("----------");
            Console.WriteLine("QUEEN");
            mv.moving(matrix, queen, 2);
            Console.WriteLine("----------");
            Console.WriteLine("ROOK");
            mv.moving(matrix, rook, 3);
            Console.WriteLine("----------");
            Console.WriteLine("KNIGHT");
            mv.moving(matrix, knight, 4);
            Console.WriteLine("----------");
            Console.WriteLine("KING");
            mv.moving(matrix, king, 5);
            for (int m = 0; m < size; m++) 
            {
                Console.WriteLine();
                for (int n = 0; n < size; n++) 
                { 
                    Console.Write(matrix[m, n] + ", "); 
                } 
            }
            Console.ReadKey();        
        }
    }
}
