using Battleship.Models;
using Battleship.Services;
using Battleship.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.Controllers
{
    class GameController
    {
        private Board board;
        private ShipService sc;
        private ConsoleView view;

        private string input = "";

        public void StartGame()
        {
            board = new Board(GameConfig.BoardWidth, GameConfig.BoardHeight);
            sc = new ShipService(board);
            view = new ConsoleView();

            // Place 3 ships on the board
            board.AddShip(sc.CreateRandomShip(Ship.Type.BATTLESHIP));
            board.AddShip(sc.CreateRandomShip(Ship.Type.DESTROYER));
            board.AddShip(sc.CreateRandomShip(Ship.Type.DESTROYER));

            ExecuteTurn(true);
        }

        /**
         * This is the method that controls most of the application
         * It handles all of user input / output and input direction
         * */
        private void ExecuteTurn(bool first = false)
        {
            view.Clear();

            // This will display information about the "enemy" ships, so you can analyse the inner workings a bit easier
            if (GameConfig.DebugMode)
            {
                view.AddShipData(board.Ships);
            }

            // Adding a basic GUI, not dynamic but for the sake of display
            view.AddInstructions();
            view.AddBoardTop();

            var pointsData = new List<Point>();
            for(int yDisplay = 1; yDisplay <= 10; yDisplay++)
            {
                /*
                Console.Write(yDisplay);
                var padding = "  ";
                Console.Write(yDisplay < 10 ? "    " : "   ");
                */
                for(int xDisplay = 1; xDisplay <= 10; xDisplay++)
                {
                    var pointAdd = new Point(xDisplay, yDisplay);
                    if (board.AlreadyHit(pointAdd))
                    {
                        if (board.GetShip(pointAdd) != null)
                        {
                            pointAdd.State = Point.StateType.HIT;
                        }
                        else
                        {
                            pointAdd.State = Point.StateType.MISS;
                        }
                    }
                    else
                    {
                        pointAdd.State = Point.StateType.UNDISTURBED;
                    }
                    pointsData.Add(pointAdd);
                }
            }
            
            view.AddHits(pointsData);

            if (GameConfig.DebugMode)
            {
                view.AddHitLog(board.Hits);
            }
            
            view.Respond();
            if (first)
            {
                ExecuteTurn();
            }
            input = Console.ReadLine();

            // Validate length
            if(input.Length < 2 || input.Length > 3)
            {
                view.DisplayError("You must enter a letter followed by a number, e.g. A5");
                ExecuteTurn();
            }

            var xChars = input.Substring(0, 1);
            var yChars = input.Substring(1);

            // Validate that first char is a letter
            if (!xChars.All(Char.IsLetter))
            {
                view.DisplayError("You must enter a letter followed by a number, e.g. A5");
                ExecuteTurn();
            }
            // Validate that following chars are numbers
            yChars.ToList().ForEach(c => 
            {
                if (!Char.IsNumber(c))
                {
                    view.DisplayError("You must enter a letter followed by a number, e.g. A5");
                    ExecuteTurn();
                }
            });

            var x = Point.ParseCoordinate(xChars);
            var y = int.Parse(yChars);

            if(x > GameConfig.BoardWidth || y > GameConfig.BoardHeight)
            {
                view.DisplayError("Your square isn't on the board! You can't go above " +
                    (Ship.Type)GameConfig.BoardWidth + " or " + GameConfig.BoardHeight);
                ExecuteTurn();
            }

            var point = new Point(x, y);

            // First see if this point has already been hit
            if(board.AlreadyHit(point))
            {
                view.DisplayError("You have already hit that point!");
                ExecuteTurn();
            }

            // Now set that point to hit 
            board.Hits.Add(point);

            // Is there a ship at these coords?
            var ship = board.GetShip(point);
            if(ship != null)
            {
                view.AddHitResponse();
                ship.Hits++;
                if (ship.IsSunk())
                {
                    view.AddSinkResponse();
                }
                ExecuteTurn();
            }
            view.AddMissResponse();
            ExecuteTurn();
        }

        // Always use these methods to get the X / Y of a ship, as it formats it for the board correctly
        private string X(Point point)
        {
            return Point.ParseCoordinate(point.X);
        }

        private string X(int x)
        {
            return Point.ParseCoordinate(x);
        }

        private int Y(Point point)
        {
            return point.Y;
        }
    }
}
