using GSlot.Common;
using GSlot.Logic.Interfaces;
using GSlot.Model;
using System;
using System.Collections.Generic;

namespace GSlot.Logic
{
    public class Engine : IEngine
    {
        private static Random random = new Random();
        private List<char> symbolBag;
        private decimal lastWon;

        private IUserInteraction UserInteraction;
        private IPresenter Presenter;
        private Player Player { get; set; }
        private Machine Machine { get; set; }

        public Engine(IPresenter presenter, IUserInteraction userInteraction)
        {
            this.Presenter = presenter;
            this.UserInteraction = userInteraction;
            this.Machine = new Machine();
            this.Player = new Player();
            this.symbolBag = new List<char>();
        }

        public void Run()
        {
            Init();

            while (this.Player.Balance > 0)
            {
                try
                {
                    switch (this.UserInteraction.GetPlayerCommand())
                    {
                        case PlayerCommand.Spin:
                            SpinProcess();
                            break;

                        case PlayerCommand.IncreaseBet:
                            this.Player.IncreaseBet();
                            break;

                        case PlayerCommand.DecreaseBet:
                            this.Player.DecreaseBet();
                            break;

                        case PlayerCommand.Exit:
                            UserInteraction.Exit();
                            break;
                    }

                    ShowGameInfo();
                }
                catch (Exception ex)
                {
                    Presenter.ShowMessage(ex.Message);
                }

            }
        }

        private decimal SpinProcess()
        {
            this.Machine.Grid.Clear();
            decimal spinWinCoef = 0;
            lastWon = 0;

            for (int i = 0; i < this.Machine.TotalRows; i++)
            {
                var row = new char[this.Machine.TotalCols];
                for (int j = 0; j < this.Machine.TotalCols; j++)
                {
                    row[j] = this.symbolBag.GetRandomItem();
                }

                this.Machine.Grid.Add(row);
                spinWinCoef += CheckMatchCoef(row);
            }

            this.lastWon = spinWinCoef * this.Player.BetAmount;
            this.Player.Balance += this.lastWon - this.Player.BetAmount;

            return spinWinCoef;
        }

        private decimal CheckMatchCoef(char[] row)
        {
            decimal coef = 0;
            char matchChar = '\0';
            int countSeq = 0;

            for (int i = 0; i < row.Length; i++)
            {
                if (this.Machine.IsWildCard(row[i]))
                {
                    countSeq++;
                    continue;
                }
                else if (matchChar == '\0')
                {
                    matchChar = row[i];
                    countSeq++;
                    continue;
                }

                if (matchChar == row[i])
                {
                    countSeq++;
                }
                else
                {
                    break;
                }
            }

            if (countSeq > GameConfiguration.MinSequance)
            {
                for (int i = 0; i < countSeq; i++)
                {
                    coef += this.Machine.GetSymbolCoefficient(row[i]);
                }
            }

            return coef;
        }

        private void ShowGameInfo()
        {
            Presenter.Clear();
            Presenter.ShowGrid(this.Machine.Grid);
            Presenter.ShowPlayerStats(this.Player, this.lastWon);
        }

        private void LoadSymbolsConfig()
        {
            for (int i = 0; i < GameConfiguration.SymbolCount; i++)
            {
                Machine.Symbols.Add(new Symbol()
                {
                    Character = GameConfiguration.GameSymbols[i],
                    Probability = GameConfiguration.SymbolProbability[i],
                    Coefficient = GameConfiguration.SymbolCoefficient[i]
                });
            }
        }

        public void Init()
        {
            decimal balance = 0;
            this.Player.BetAmount = GameConfiguration.DefaultBetStep;
            this.Presenter.ShowMessage(MessagesConstants.InputBalance);
            while (!Decimal.TryParse(this.UserInteraction.GetUserInput(), out balance))
            {
                this.Presenter.ShowMessage(MessagesConstants.InvalidInput);
            }

            this.Player.Balance = balance;
            LoadSymbolsConfig();

            this.Presenter.Clear();
            this.Presenter.ShowMessage(string.Format(MessagesConstants.GameInfo, string.Join("\n", this.Machine.Symbols)));

            // Shuffle the items in the bag 
            foreach (var s in this.Machine.Symbols)
            {
                symbolBag.Add(s.Character, s.Probability);
            }
            symbolBag.Shuffle();
        }
    }
}
