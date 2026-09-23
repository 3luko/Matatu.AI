namespace MatatuCSharp
{
    public class SimulationEvaluator
    {
        public void RunEvaluation(int numberOfGames)
        {
            int heuristicWins = 0;
            int randomWins = 0;
            int ties = 0;

            int totalTurns = 0;

            GameSimulator simulator = new GameSimulator();

            for (int i = 0; i < numberOfGames; i++)
            {
                // Alternate who gets the first turn
                bool heuristicStarts = i % 2 == 0;

                SimulationResult result =
                    simulator.RunGame(heuristicStarts);

                totalTurns += result.TurnCount;

                if (result.Winner == "MatatuAI")
                {
                    heuristicWins++;
                }
                else if (result.Winner == "RandomAgent")
                {
                    randomWins++;
                }
                else
                {
                    ties++;
                }
            }

            double heuristicWinRate =
                (double)heuristicWins / numberOfGames * 100;

            double randomWinRate =
                (double)randomWins / numberOfGames * 100;

            double averageTurns =
                (double)totalTurns / numberOfGames;

            Console.WriteLine("\n===== AI EVALUATION =====");
            Console.WriteLine("Games Played: " + numberOfGames);

            Console.WriteLine(
                "MatatuAI Wins: " +
                heuristicWins +
                " (" +
                heuristicWinRate.ToString("F2") +
                "%)");

            Console.WriteLine(
                "RandomAgent Wins: " +
                randomWins +
                " (" +
                randomWinRate.ToString("F2") +
                "%)");

            Console.WriteLine("Ties: " + ties);

            Console.WriteLine(
                "Average Turns Per Game: " +
                averageTurns.ToString("F2"));
        }
    }
}