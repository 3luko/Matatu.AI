namespace MatatuCSharp
{
    public class GameState
    {
        public Deck Deck { get; set; }

        public List<Card> WastePile { get; set; }

        public Player Human { get; set; }

        public Player Computer { get; set; }
    }
}