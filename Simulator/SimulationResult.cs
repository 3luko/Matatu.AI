namespace MatatuCSharp
{
    public class SimulationResult
    {
        public string Winner { get; set; }

        public int TurnCount { get; set; }

        public int HeuristicCardsLeft { get; set; }

        public int RandomCardsLeft { get; set; }
    }
}