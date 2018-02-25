using Battleship.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.Services
{
    /**
     * This controller handles the generation of ships on the board
     * */
    class ShipService
    {
        private Random rnd = new Random();
        private Board board;

        public ShipService(Board board)
        {
            this.board = board;
        }

        public Ship CreateRandomShip(Ship.Type type)
        {
            return CreateRandomShip((int)type);
        }

        public Ship CreateRandomShip(int shipLength)
        {
            var possibleShips = GeneratePossibleShips(shipLength);
            if (!possibleShips.Any())
            {
                throw new SystemException("There is no more room for an extra ship of this size.");
            }
            int randomShip = rnd.Next(possibleShips.Count());
            return possibleShips.ElementAt(randomShip);
        }

        private List<Ship> GeneratePossibleShips(int shipLength)
        {
            var ships = GenerateHorizontalShips(shipLength);
            ships.AddRange(GenerateVerticalShips(shipLength));
            var validShips = GetNonIntersectingShips(ships);
            return validShips;
        }

        private List<Ship> GetNonIntersectingShips(List<Ship> ships)
        {
            var nonIntersectingShips = new List<Ship>();
            var currentShips = board.Ships;

            // Go through the ships passed to this method and make sure they don't intersect with any on the board
            foreach (Ship ship in ships)
            {
                var intersects = false;
                foreach(Ship currentShip in currentShips)
                {
                    intersects = ShipsIntersect(ship, currentShip);
                    if (intersects)
                    {
                        break;
                    }
                }
                if (!intersects)
                {
                    nonIntersectingShips.Add(ship);
                }
            }
            return nonIntersectingShips;
        }

        private bool ShipsIntersect(Ship ship, Ship otherShip)
        {
            // I am using a foreach for this, to avoid using external libraries / graphical methods
            var points = ship.GetPoints();
            var otherPoints = otherShip.GetPoints();
            foreach(Point point in points)
            {
                foreach (Point otherPoint in otherPoints)
                {
                    if (point.X == otherPoint.X && point.Y == otherPoint.Y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /**
         * Generates all of the possible horizontal ships (even intersecting ones)
         * */
        private List<Ship> GenerateHorizontalShips(int shipLength)
        {
            // Make sure the left-point of the ship is `length` away from the edge at least
            int maxStartX = board.Width - shipLength;
            List<Ship> possibleShips = new List<Ship>();

            // Traverse the entire board and generate as many ships as possible on the board
            for (int x = 1; x <= maxStartX; x++)
            {
                for (int y = 1; y <= board.Height; y++)
                {
                    Point start = new Point(x, y);
                    Point end = new Point(x + shipLength - 1, y);
                    possibleShips.Add(new Ship(start, end));
                }
            }
            return possibleShips;
        }

        /**
         * Generates all of the possible vertical ships (even intersecting ones)
         * */
        private List<Ship> GenerateVerticalShips(int shipLength)
        {
            // Make sure the left-point of the ship is `length` away from the edge at least
            int maxStartY = board.Height - shipLength;
            List<Ship> possibleShips = new List<Ship>();

            // Traverse the entire board and generate as many ships as possible on the board
            for (int x = 1; x <= board.Width; x++)
            {
                for (int y = 1; y <= maxStartY; y++)
                {
                    Point start = new Point(x, y);
                    Point end = new Point(x, y + shipLength - 1);
                    possibleShips.Add(new Ship(start, end));
                }
            }
            return possibleShips;
        }
    }
}
