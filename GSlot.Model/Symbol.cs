namespace GSlot.Model
{
    public class Symbol
    {
        public char Character { get; set; }
        public decimal Coefficient { get; set; }
        public int Probability { get; set; }

        public override string ToString()
        {
            return $"{Character} - {Coefficient} - {Probability}";
        }

        public bool IsWildCard()
        {
            return Coefficient == 0;
        }
    }
}
