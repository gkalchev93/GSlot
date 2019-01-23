﻿using GSlot.Common;
using GSlot.Logic.Interfaces;
using GSlot.Model;
using System;
using System.Collections.Generic;

namespace GSlot.ConsoleClient
{
    public class GamePresenter : IPresenter
    {
        public void Clear()
        {
            Console.Clear();
        }

        public void ShowGrid(List<char[]> grid)
        {
            foreach (var row in grid)
            {
                foreach (var ch in row)
                {
                    Console.Write(ch);
                }
                Console.WriteLine();
            }
        }

        public void ShowMessage(string msg)
        {
            Console.WriteLine(msg);
        }

        public void ShowPlayerStats(Player player, decimal lastSpinWon)
        {
            Console.WriteLine($"You won: {Utils.ToCurrency(lastSpinWon)}");
            Console.WriteLine($"Bet: {Utils.ToCurrency(player.BetAmount)}             Balance: {Utils.ToCurrency(player.Balance)}");
        }
    }
}