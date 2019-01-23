using GSlot.Logic;
using System;

namespace GSlot.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var presenter = new GamePresenter();
            var interaction = new UserInteraction();
            var game = new Engine(presenter, interaction);
            game.Init();
            Console.Clear();
            game.Run();
        }
    }
}
