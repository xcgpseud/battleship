using Battleship.Models;
using System;
using System.Collections.Generic;

namespace Battleship.Views
{
    class ConsoleView
    {
        public List<String> Lines { get; }
        public string Output { get; private set; }

        public ConsoleView()
        {
            Lines = new List<string>();
        }

        // Handles the console output, made a view for this since I added a small "GUI"
        public void Respond()
        {
            Console.Clear();
            Lines.ForEach(l =>
            {
                Console.WriteLine(l);
            });
            if (!String.IsNullOrEmpty(Output))
            {
                Console.WriteLine("\n" + Output + "\n");
            }
            Lines.Clear();
            Output = "";
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void DisplayError(string msg)
        {
            Output = msg;
        }

        private void DisplayResult(string msg)
        {
            Output = msg;
        }

        public void AddUserPrompt()
        {
            Lines.Add("Please enter your next move.");
        }

        public void AddHits(List<Point> points)
        {
            string line = "";
            int counter = 1;
            points.ForEach(point =>
            {
                switch (point.State)
                {
                    case Point.StateType.UNDISTURBED:
                        line += "O  ";
                        break;
                    case Point.StateType.MISS:
                        line += "/  ";
                        break;
                    case Point.StateType.HIT:
                        line += "X  ";
                        break;
                }
                if(counter % 10 == 0)
                {
                    if (counter / 10 >= 10)
                    {
                        Lines.Add(counter / 10 + "  " + line); // 1 less space if multiple digits
                    }
                    else
                    {
                        Lines.Add(counter / 10 + "   " + line);
                    }
                    line = "";
                }
                counter++;
            });
        }

        public void AddHitResponse()
        {
            Output += "You hit a ship!\n";
        }

        public void AddMissResponse()
        {
            Output += "You didn't hit anything this time.\n";
        }

        public void AddSinkResponse()
        {
            Output += "You sunk the ship!\n";
        }

        public void AddBoardTop()
        {
            Lines.Add("    A  B  C  D  E  F  G  H  I  J\n");
        }

        public void AddInstructions()
        {
            Lines.Add("\nO = not hit yet. / = hit but no ship was hit. X = hit a ship!\n");
        }

        public void AddShipData(List<Ship> ships)
        {
            ships.ForEach(s =>
            {
                Lines.Add("START: " + X(s.Start) + Y(s.Start) + " -> END: " + X(s.End) + Y(s.End));
            });
        }

        public void AddHitLog(List<Point> hits)
        {
            Lines.Add("\n\nYour hits so far:");
            string line = "";
            hits.ForEach(p =>
            {
                line += X(p.X) + ":" + p.Y + "  ";
            });
            Lines.Add(line);
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
