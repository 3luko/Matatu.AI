namespace MatatuCSharp
{
    public enum AIActionType
    {
        PlayCard,
        DrawCard
    }

    public class AIAction
    {
        public AIActionType ActionType { get; set; }

        public int CardIndex { get; set; }

        public string ChosenSuit { get; set; }
    }
}