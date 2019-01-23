namespace GSlot.Common
{
    public static class GameConfiguration
    {
        // Symbols and probability configuration
        // If the win coefficient of a symbol is 0, then it is treated as a wildcard
        public const int SymbolCount = 4;
        public static char[] GameSymbols = new char[SymbolCount] { 'A', 'B', 'P', '*' };
        public static decimal[] SymbolCoefficient = new decimal[SymbolCount] { 0.4m, 0.6m, 0.8m, 0 };
        public static int[] SymbolProbability = new int[SymbolCount] { 45, 35, 15, 5 };

        // Basic configurations
        // Minimum symbols in a sequance
        public const int MinSequance = 2;
        public const decimal DefaultBetStep = 0.10m;
        public const int GridRowsCount = 4;
        public const int GridColsCount = 3;
    }
}
