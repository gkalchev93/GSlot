using GSlot.Logic;

namespace GSlot.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var presenter = new GamePresenter();
            var interaction = new UserInteraction();
            var game = new Engine(presenter, interaction);
            game.Run();
        }
    }
}
