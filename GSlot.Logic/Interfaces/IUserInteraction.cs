namespace GSlot.Logic.Interfaces
{
    public interface IUserInteraction
    {
        string GetUserInput();
        PlayerCommand GetPlayerCommand();
        void Exit();
    }
}
