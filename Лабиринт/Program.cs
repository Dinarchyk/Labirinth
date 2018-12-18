using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лабиринт
{
    enum State
    {
        Empty,
        Wall,
        Visited,
        Start,
        Finish
    };

    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point prevPoint { get; set; }
    }

    public class Program
    {

        static void Main()
        {
            var flag = 0;
            var startPoint = new Point { X = 0, Y = 0 };
            var finishPoint = new Point { X = 2, Y = 4 };
            var map = new State[labyrinth[0].Length, labyrinth.Length];
            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                    map[x, y] = labyrinth[y][x] == ' ' ? State.Empty : State.Wall;
            map[finishPoint.X, finishPoint.Y] = State.Finish;

            var queue = new Queue<Point>();
            queue.Enqueue(new Point { X = startPoint.X, Y = startPoint.Y });
            while (queue.Count != 0 && flag != 1)
            {
                var point = queue.Dequeue();
                if (point.X < 0 || point.X >= map.GetLength(0) || point.Y < 0 || point.Y >= map.GetLength(1)) continue;
                if (map[point.X, point.Y] != State.Empty) continue;
                map[point.X, point.Y] = State.Visited;

                for (var dy = -1; dy <= 1; dy++)
                    for (var dx = -1; dx <= 1; dx++)
                        if (dx != 0 && dy != 0) continue;
                        else
                        {
                            if (flag == 1) break;
                            if ((point.X + dx >= 0 && point.X + dx < map.GetLength(0) && point.Y + dy >= 0 && point.Y + dy < map.GetLength(1)))
                                if (map[point.X + dx, point.Y + dy] == State.Finish)
                                {
                                    queue.Enqueue(new Point { X = point.X + dx, Y = point.Y + dy, prevPoint = point });
                                    flag = 1;
                                    break;
                                }
                            queue.Enqueue(new Point { X = point.X + dx, Y = point.Y + dy, prevPoint = point });
                        }
            }
            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                    map[x, y] = labyrinth[y][x] == ' ' ? State.Empty : State.Wall;
            map[finishPoint.X, finishPoint.Y] = State.Finish;
            var point2 = queue.Last();
            while (point2.prevPoint != null)
            {
                point2 = point2.prevPoint;
                map[point2.X, point2.Y] = State.Visited;
            }
            map[point2.X, point2.Y] = State.Start;
            Print(map);
        }

        static string[] labyrinth = new string[]
        {
        " X   X    ",
        " X XXXXX X",
        "      X   ",
        "XXXX XXX X",
        "         X",
        " XXX XXXXX",
        " X        ",
        };

       
        static void Print(State[,] map)
        {

            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            Console.Write("┌");
            for (int x = 1; x < map.GetLength(0) + 1; x++)
                Console.Write("─");
            Console.WriteLine("┐");
            for (int y = 0; y < map.GetLength(1); y++)
            {
                Console.Write("│");
                for (int x = 0; x < map.GetLength(0); x++)
                    switch (map[x, y])
                    {
                        case State.Wall: Grafic(GenerateNewMap(), x+1,y+1); break;
                        case State.Empty: Console.Write(" "); break;
                        case State.Visited: Console.Write("."); break;
                        case State.Finish: Console.Write("F"); break;
                        case State.Start: Console.Write("s"); break;
                    }
                Console.WriteLine("│");
            }
            Console.Write("└");
            for (int x = 1; x < map.GetLength(0) + 1; x++)
                Console.Write("─");
            Console.Write("┘");
        }

        static void Grafic(State[,] map , int x, int y)
        {

            if (map[x + 1, y] == State.Empty && map[x - 1, y] == State.Empty) {
                Console.Write("│");
                return;
            }
            if (map[x, y-1] == State.Empty && map[x, y+1] == State.Empty)
            {
                Console.Write("─");
                return;
            }
            if (map[x + 1, y] == State.Empty && map[x, y - 1] == State.Wall && map[x - 1, y] == State.Wall && map[x, y + 1] == State.Wall)
            {
                Console.Write("┤");
                return;
            }
            if (map[x + 1, y] == State.Empty && map[x, y - 1] == State.Empty && map[x - 1, y] == State.Wall && map[x, y + 1] == State.Wall)
            {
                Console.Write("┐");
                return;
            }
            if (map[x + 1, y] == State.Wall && map[x, y - 1] == State.Wall && map[x - 1, y] == State.Empty && map[x, y + 1] == State.Empty)
            {
                Console.Write("└");
                return;
            }
            if (map[x + 1, y] == State.Wall && map[x, y - 1] == State.Wall && map[x - 1, y] == State.Wall && map[x, y + 1] == State.Empty)
            {
                Console.Write("┴");
                return;
            }
            if (map[x + 1, y] == State.Wall && map[x, y - 1] == State.Empty && map[x - 1, y] == State.Wall && map[x, y + 1] == State.Wall)
            {
                Console.Write("┬");
                return;
            }
            if (map[x + 1, y] == State.Wall && map[x, y - 1] == State.Wall && map[x - 1, y] == State.Empty && map[x, y + 1] == State.Wall)
            {
                Console.Write("├");
                return;
            }
            if (map[x + 1, y] == State.Wall && map[x, y - 1] == State.Wall && map[x - 1, y] == State.Wall && map[x, y + 1] == State.Wall)
            {
                Console.Write("┼");
                return;
            }
            if (map[x + 1, y] == State.Empty && map[x, y - 1] == State.Wall && map[x - 1, y] == State.Wall && map[x, y + 1] == State.Empty)
            {
                Console.Write("┘");
                return;
            }
            if (map[x + 1, y] == State.Wall && map[x, y - 1] == State.Empty && map[x - 1, y] == State.Empty && map[x, y + 1] == State.Wall)
            {
                Console.Write("┌");
                return;
            }
            Console.Write("X");
        }
        static State[,] GenerateNewMap()
        {
            var newMap = new State[labyrinth[0].Length + 2, labyrinth.Length + 2];
            for (int x = 0; x < newMap.GetLength(0); x++)
                for (int y = 0; y < newMap.GetLength(1); y++)
                {
                    if (x == 0 || y == 0 || x == newMap.GetLength(0) - 1 || y == newMap.GetLength(1) - 1)
                    {
                        newMap[x, y] = State.Empty;
                        continue;
                    }
                    newMap[x, y] = labyrinth[y - 1][x - 1] == ' ' ? State.Empty : State.Wall;
                }
            return newMap;
        }
    }
}
