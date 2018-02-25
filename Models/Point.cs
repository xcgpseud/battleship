using System;

namespace Battleship.Models
{
    class Point
    {
        public int X { get; }
        public int Y { get; }
        public StateType State { get; set; }

        public enum StateType { UNDISTURBED, HIT, MISS };

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static string ParseCoordinate(int i)
        {
            i += 64;
            if(i < 65 || i > 90)
            {
                throw new IndexOutOfRangeException("Coordinate out of range.");
            }
            char character = (char)i;
            return character.ToString();
        }

        public static int ParseCoordinate(string s)
        {
            s = s.ToUpper();
            var ascii = (int)Char.Parse(s);
            if(ascii < 65 || ascii > 90)
            {
                throw new IndexOutOfRangeException("Coordinate out of range.");
            }
            return ascii - 64;
        }

        public static int ParseCoordinate(char c)
        {
            return ParseCoordinate(c.ToString());
        }

        public bool Equals(Point point)
        {
            if(X == point.X && Y == point.Y)
            {
                return true;
            }
            return false;
        }
    }
}
