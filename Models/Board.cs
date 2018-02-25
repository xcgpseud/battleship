using System.Collections.Generic;
using System.Linq;

namespace Battleship.Models
{
    class Board
    {
        public int Width { get; }
        public int Height { get; }

        public List<Ship> Ships { get; }
        public List<Point> Hits { get; }

        public Board(int width, int height)
        {
            Width = width;
            Height = height;
            Ships = new List<Ship>();
            Hits = new List<Point>();
        }

        public void AddShip(Ship ship)
        {
            Ships.Add(ship);
        }

        // Is there a ship on this point?
        public Ship GetShip(Point point)
        {
            foreach(Ship ship in Ships)
            {
                foreach(Point p in ship.GetPoints())
                {
                    if(p.Equals(point))
                    {
                        return ship;
                    }
                }
            }
            return null;
        }

        // Did the user just sink a ship?
        public bool SunkShip(Ship ship)
        {
            if(Hits.Count == 0)
            {
                return false;
            }
            bool sunk = true;
            // Get all points for this ship
            // Go through each point
            // Does hits contain this point
            
            foreach(Point point in ship.GetPoints())
            {
                bool pointHit = Hits.Any(h => h.Equals(point));
                if (!pointHit)
                {
                    sunk = false;
                }
            }
            return sunk;
        }

        public bool AlreadyHit(Point point)
        {
            foreach (Point hit in Hits)
            {
                if (hit.Equals(point))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
