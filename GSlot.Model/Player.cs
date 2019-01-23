using GSlot.Common;

namespace GSlot.Model
{
    public class Player
    {
        public decimal Balance { get; set; }
        public decimal BetAmount { get; set; }

        public Player()
        { }

        public void IncreaseBet(decimal value = GameConfiguration.DefaultBetStep)
        {
            var newBet = this.BetAmount + value;

            if (newBet <= this.Balance)
                this.BetAmount = newBet;
        }

        public void DecreaseBet(decimal value = GameConfiguration.DefaultBetStep)
        {
            var newBet = this.BetAmount - value;

            if (newBet > 0)
                this.BetAmount = newBet;
        }

        public bool ValidateBet()
        {
            return this.BetAmount <= this.Balance;
        }
    }
}
