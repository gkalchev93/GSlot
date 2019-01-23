using GSlot.Model;
using System.Collections.Generic;

namespace GSlot.Logic.Interfaces
{
    public interface IPresenter
    {
        void ShowMessage(string msg);
        void ShowGrid(List<char[]> grid);
        void ShowPlayerStats(Player player, decimal lastSpinWon);
        void Clear();
    }
}
