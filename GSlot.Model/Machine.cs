using GSlot.Common;
using System.Collections.Generic;
using System.Linq;

namespace GSlot.Model
{
    public class Machine
    {
        public List<char[]> Grid;
        public List<Symbol> Symbols { get; set; }

        public int TotalRows { get; private set; }
        public int TotalCols { get; private set; }

        public Machine(int rows = GameConfiguration.GridRowsCount, int cols = GameConfiguration.GridColsCount)
        {
            this.TotalRows = rows;
            this.TotalCols = cols;
            this.Grid = new List<char[]>();
            this.Symbols = new List<Symbol>();
        }

        public decimal GetSymbolCoefficient(char c)
        {
            return Symbols.FirstOrDefault(x => x.Character == c).Coefficient;
        }

        public int GetSymbolProbability(char c)
        {
            return Symbols.FirstOrDefault(x => x.Character == c).Probability;
        }

        public bool IsWildCard(char c)
        {
            return this.GetSymbolCoefficient(c) == 0;
        }
    }
}
