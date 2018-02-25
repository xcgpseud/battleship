using Battleship.Controllers;
using System;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            var gc = new GameController();
            gc.StartGame();

            Console.ReadLine();
        }
    }
}
