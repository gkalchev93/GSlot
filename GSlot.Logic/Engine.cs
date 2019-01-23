using GSlot.Common;
using GSlot.Logic.Interfaces;
using GSlot.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSlot.Logic
{
    public class Engine : IEngine
    {
        private static Random random = new Random();
        private List<char> symbolBag;
        private List<Tuple<decimal, decimal>> gameHistory;

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
            this.gameHistory = new List<Tuple<decimal, decimal>>();
        }

        public void Run()
        {
            ShowGameInfo();
            while (this.Player.Balance > 0 && this.Player.Balance >= GameConfiguration.DefaultBetStep)
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

        private void SpinProcess()
        {
            if (this.Player.ValidateBet())
            {
                this.Machine.Grid.Clear();
                decimal spinWinCoef = 0;

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

                var wonAmount = spinWinCoef * this.Player.BetAmount;
                AddGameHistory(wonAmount);
                this.Player.Balance += wonAmount - this.Player.BetAmount;
            }
            else
            {
                throw new Exception(MessagesConstants.NoMoney);
            }
        }

        private void AddGameHistory(decimal wonAmount)
        {
            gameHistory.Insert(0, Tuple.Create(wonAmount, this.Player.BetAmount));
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
            Presenter.ShowPlayerStats(this.Player, GetLastWon());
            Presenter.ShowGameHistory(gameHistory);
        }

        private decimal GetLastWon()
        {
            if (this.gameHistory.Count > 0)
                return this.gameHistory.First().Item1;
            else
                return 0;
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
            LoadSymbolsConfig();
            this.Presenter.ShowMessage(string.Format(MessagesConstants.GameInfo, string.Join("\n", this.Machine.Symbols)));

            decimal balance = 0;
            this.Player.BetAmount = GameConfiguration.DefaultBetStep;
            this.Presenter.ShowMessage(MessagesConstants.InputBalance);
            while (!Decimal.TryParse(this.UserInteraction.GetUserInput(), out balance))
            {
                this.Presenter.ShowMessage(MessagesConstants.InvalidInput);
            }

            this.Player.Balance = balance;

            // Shuffle the items in the bag 
            foreach (var s in this.Machine.Symbols)
            {
                symbolBag.Add(s.Character, s.Probability);
            }

            symbolBag.Shuffle();
        }
    }
}
