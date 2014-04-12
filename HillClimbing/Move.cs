using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HillClimbing
{
    class Move
    {
        public static int total = 0, size = 12;
        public Move() { }
        public void moving(int[,] matrix, int count, int type)
        {
            Random rand = new Random();
            List<Point> back = new List<Point>();
            List<Point> list = new List<Point>();
            List<Point> next = new List<Point>();
            int i, j;
            for (int k = 0; k < count; k++)
            {
                Console.WriteLine("");
                Console.WriteLine("Number " + (k+1));
                do
                {
                    i=rand.Next(1, 10); //önceki ve sonraki noktalara bakılabilmesi için 0-11 yerine 1-10 aralığı seçildi
                    j=rand.Next(1, 10);
                    if (matrix[i, j] == 0) 
                    {
                        Console.WriteLine("Selected: (" + i + ", " + j + ")");
                    }
                    else Console.WriteLine("(" + i + ", " + j + ") is captured.");
                    
                }while(matrix[i,j]!=0);
                Point p = new Point(i, j);
                bool find = false;
                while (find==false && p.X >= 0 && p.X < size && p.Y >= 0 && p.Y < size)
                {
                    back = moveSelect(matrix, getBack(p), type);
                    list = moveSelect(matrix, p, type);
                    next = moveSelect(matrix, getNext(p), type);
                    if (list.Count < back.Count)
                    {
                        p = getBack(p);
                        Console.WriteLine("Changed to " + p.X + ", " + p.Y);
                    }
                    else if (list.Count < next.Count)
                    {
                        p = getNext(p);
                        Console.WriteLine("Changed to " + p.X + ", " + p.Y);
                    }
                    else find = true;
                }
                List<Point> finalList = moveSelect(matrix, p, type);
                foreach (Point po in finalList)
                {
                    matrix[po.X, po.Y] = type;
                }
                setTotal(finalList.Count);
                Console.WriteLine("Location: (" + p.X + ", " + p.Y + ")" + "Captured: " + finalList.Count);
                Console.WriteLine("Total captured: " + total);
            }
        }
        public void setTotal(int subTotal) 
        {
            total += subTotal;
        }
        public List<Point> moveSelect(int[,] matrix, Point p, int type) 
        {
            List<Point> list = new List<Point>();
            switch (type)
            {
                case 1: list = bishopMove(matrix, p); break;
                case 2: list = queenMove(matrix, p); break;
                case 3: list = rookMove(matrix, p); break;
                case 4: list = knightMove(matrix, p); break;
                case 5: list = kingMove(matrix, p); break;
            }
            return list;
        }
        public Point getBack(Point p) 
        {
            int x = p.X, y = p.Y;
            if (p.X > 0)
            {
                return new Point(x-1, y);
            }
            else if (p.Y > 0)
            {
                return new Point(size-1, y-1);
            }
            else return p;
        }
        public Point getNext(Point p) 
        {
            int x = p.X, y = p.Y;
            if (p.X < size - 1) //12 kare için 0-11 aralığı kullanılır. (size=12)
            {
                return new Point(x+1, y); 
            }
            else if (p.Y < size - 1)
            {
                return new Point(0, y+1);
            }
            else return p;
        }
        public List<Point> bishopMove(int[,] matrix, Point p)
        {
            int[,] adding = new int[4, 2] { { 1, 1 }, { 1, -1 }, { -1, -1 }, { -1, 1 } };
            int x = p.X, y = p.Y;
            List<Point> list = new List<Point>();
            list.Add(p);
            for (int i = 0; i < 4; i++) 
            {
                while (p.X + adding[i, 0] >= 0 && p.X + adding[i, 0] < size && p.Y + adding[i, 1] >= 0 && p.Y + adding[i, 1] < size && matrix[p.X + adding[i, 0], p.Y + adding[i, 1]] < 1)
                {
                    p.X = p.X + adding[i, 0];
                    p.Y = p.Y + adding[i, 1];
                    list.Add(new Point(p.X, p.Y));
                }
                p.X = x;
                p.Y = y;
            }
            p = new Point(x, y);
            return list;
        }
        public List<Point> queenMove(int[,] matrix, Point p)
        {
            int x = p.X, y = p.Y;
            List<Point> plusList = new List<Point>();
            List<Point> crossList = new List<Point>();
            plusList = rookMove(matrix, p);
            crossList = bishopMove(matrix, p);
            foreach (Point po in crossList) 
            {
                plusList.Add(new Point(po.X, po.Y));
            }
            p = new Point(x, y);
            return plusList;
        }
        public List<Point> rookMove(int[,] matrix, Point p)
        {
            List<Point> list = new List<Point>();
            int x = p.X, y = p.Y;
            for (int i = 0; i < size; i++)
            {
                
                p.Y = i;
                if (matrix[p.X, p.Y] == 0)
                {
                    list.Add(new Point(p.X, p.Y));
                }
            }
            p.Y = y;
            for (int j = 0; j < p.X; j++)
            {
                p.X = j;
                if (matrix[p.X, p.Y] < 1)
                {
                    list.Add(new Point(p.X, p.Y));
                }
            } //kendisini iki kez eklememesi için 2 adet döngü oluşturularak p atlandı
            for (int k = x+1; k < size; k++)
            {
                p.X = k;
                if (matrix[p.X, p.Y] < 1)
                {
                    list.Add(new Point(p.X, p.Y));
                }
            }
            p.X = x;
            p = new Point(x, y);            
            return list;
        }
        public List<Point> kingMove(int[,] matrix, Point p)
        {
            List<Point> list = new List<Point>();
            int x = p.X, y = p.Y;
            for (int i = -1; i < 2; i++) 
            {
                p.X = p.X + i;
                for (int j = -1; j < 2; j++) 
                {                    
                    p.Y = p.Y + j;
                    if (p.X >= 0 && p.X < size && p.Y >= 0 && p.Y < size && matrix[p.X, p.Y] < 1) 
                    {
                        list.Add(new Point(p.X,p.Y));
                    }
                    p.Y = y;
                }
                p.X = x;
            }
            p = new Point(x, y);
            return list;
        }
        public List<Point> knightMove(int[,] matrix, Point p)
        {
            int x = p.X, y = p.Y;
            List<Point> list = new List<Point>();
            int[,] adding = new int[8,2] {{-1, 2},{1, 2},{2, 1},{2, -1},{1, -2},{-1, -2},{-2, -1},{-2, 1}};
            for (int i = 0; i < 8; i++)
            {
                if (p.X + adding[i, 0] >= 0 && p.X + adding[i, 0] < size && p.Y + adding[i, 1] >= 0 && p.Y + adding[i, 1] < size && matrix[p.X + adding[i, 0],p.Y + adding[i, 1]] < 1)
                {
                    list.Add(new Point(p.X+adding[i, 0],p.Y+adding[i, 1]));
                }
            }
            p = new Point(x, y);
            return list;
        }
        public List<Point> getMatrix(int[,] matrix, List<Point> moveList) 
        {
            List<Point> locationList = new List<Point>();
            for (int i = 0; i < moveList.Count; i++) 
            {
                if (matrix[moveList[i].X,moveList[i].Y]!=0) 
                {
                    locationList.Add(new Point(moveList[i].X, moveList[i].Y));
                }
            }
            return locationList;
        }
    }
}
