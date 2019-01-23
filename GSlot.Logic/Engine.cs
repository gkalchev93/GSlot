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
        }
        public void Init()
        {
        }
    }
}
