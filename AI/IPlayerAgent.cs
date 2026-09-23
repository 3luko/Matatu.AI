namespace MatatuCSharp
{
    public interface IPlayerAgent
    {
        AIAction ChooseAction(GameState gameState);
    }
}