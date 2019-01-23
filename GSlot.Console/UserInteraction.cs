using GSlot.Logic;
using GSlot.Logic.Interfaces;
using System;

namespace GSlot.ConsoleClient
{
    public class UserInteraction : IUserInteraction
    {
        public void Exit()
        {
            Environment.Exit(1);
        }

        public PlayerCommand GetPlayerCommand()
        {
            var pCmd = PlayerCommand.Nothing;
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Spacebar:
                    pCmd = PlayerCommand.Spin;
                    break;

                case ConsoleKey.A:
                    pCmd = PlayerCommand.IncreaseBet;
                    break;

                case ConsoleKey.Z:
                    pCmd = PlayerCommand.DecreaseBet;
                    break;

                case ConsoleKey.Escape:
                    pCmd = PlayerCommand.Exit;
                    break;
            }

            return pCmd;
        }

        public string GetUserInput()
        {
            return Console.ReadLine();
        }
    }
}
