namespace MatatuCSharp
{
    public class RandomAgent : IPlayerAgent
    {
        private Player computer;

        public RandomAgent(Player computer)
        {
            this.computer = computer;
        }

        public AIAction ChooseAction(GameState gameState)
        {
            List<int> playableCards = new List<int>();

            // Find every card the AI is legally allowed to play
            for (int i = 0; i < computer.SeeCards.Count; i++)
            {
                Card card = computer.SeeCards[i];

                bool canPlay = gameState.RequiredSuit == ""
                    ? Logic.canYouPlay(card, gameState.TopCard)
                    : Logic.playAce(card, gameState.RequiredSuit);

                if (canPlay)
                {
                    playableCards.Add(i);
                }
            }

            // If there are no legal cards, draw a card
            if (playableCards.Count == 0)
            {
                return new AIAction
                {
                    ActionType = AIActionType.DrawCard,
                    CardIndex = -1
                };
            }

            // Randomly choose one of the legal cards
            int randomIndex = Random.Shared.Next(playableCards.Count);
            int chosenCardIndex = playableCards[randomIndex];

            Card chosenCard = computer.SeeCards[chosenCardIndex];

            // If the random card is an Ace,
            // randomly choose the next suit
            if (chosenCard.CardValue == Value.Ace)
            {
                Suit[] suits = Enum.GetValues<Suit>();

                Suit chosenSuit =
                    suits[Random.Shared.Next(suits.Length)];

                return new AIAction
                {
                    ActionType = AIActionType.PlayCard,
                    CardIndex = chosenCardIndex,
                    ChosenSuit = chosenSuit.ToString()
                };
            }

            return new AIAction
            {
                ActionType = AIActionType.PlayCard,
                CardIndex = chosenCardIndex
            };
        }
    }
}