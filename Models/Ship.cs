using System.Collections.Generic;

namespace Battleship.Models
{
    class Ship
    {
        public enum Orientation { VERTICAL, HORIZONTAL };
        public enum Type { BATTLESHIP = 5, DESTROYER = 4 };

        // Storing coorinates for the first and last point of each ship
        public Point Start { get; }
        public Point End { get; }
        public int Hits { get; set; }

        public Ship(Point start, Point end)
        {
            Start = start;
            End = end;
            Hits = 0;
        }

        public bool IsSunk()
        {
            return Hits == GetPoints().Count;
        }

        public List<Point> GetPoints()
        {
            var points = new List<Point>();
            if(GetOrientation() == Orientation.HORIZONTAL)
            {
                var y = Start.Y;
                for(int x = Start.X; x <= End.X; x++)
                {
                    points.Add(new Point(x, y));
                }
                return points;
            }
            else
            {
                var x = Start.X;
                for(int y = Start.Y; y <= End.Y; y++)
                {
                    points.Add(new Point(x, y));
                }
                return points;
            }
        }

        public Orientation GetOrientation()
        {
            if(Start.X < End.X)
            {
                return Orientation.HORIZONTAL;
            }
            else
            {
                return Orientation.VERTICAL;
            }
        }
    }
}
