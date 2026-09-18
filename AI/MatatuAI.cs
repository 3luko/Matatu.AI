namespace MatatuCSharp
{
    public class MatatuAI
    {
        private Player computer;

        public MatatuAI(Player computer)
        {
            this.computer = computer;
        }

        public AIAction ChooseAction(string requiredSuit = "")
        {
            int bestCardIndex = FindBestCard(requiredSuit);

            if (bestCardIndex == -1)
            {
                return new AIAction
                {
                    ActionType = AIActionType.DrawCard,
                    CardIndex = -1
                };
            }


            if (computer.SeeCards[bestCardIndex].CardValue == Value.Ace)
            {
                return new AIAction
                {
                    ActionType = AIActionType.PlayCard,
                    CardIndex = bestCardIndex,
                    ChosenSuit = ChosenSuit().ToString()
                };
            }
            return new AIAction
            {
  
                ActionType = AIActionType.PlayCard,
                CardIndex = bestCardIndex
            };
        }

        private Suit ChosenSuit()
        {
            // Hashmap for the suit counts in the deck
            Dictionary<Suit, int> suitCounts = Enum.GetValues<Suit>()
                .ToDictionary(suit => suit, _ => 0);

            // Looping through the deck and counting the number of occurrances
            foreach (Card card in computer.SeeCards)
            {
                if (card.CardValue != Value.Ace)
                {
                    suitCounts[card.CardSuit]++;                    
                }
                
            }

            // Gettig the highest count
            int highestCount = suitCounts.Values.Max();
            List<Suit> mostCommonSuits = suitCounts
                .Where(pair => pair.Value == highestCount)
                .Select(pair => pair.Key)
                .ToList();

            if (mostCommonSuits.Count == suitCounts.Count)
            {
                return mostCommonSuits[Random.Shared.Next(mostCommonSuits.Count)];
            }

            return mostCommonSuits[0];
        }

        private int EvaluateCard(Card card)
        {
            int score = 0;

            // Getting rid of a card is always good
            score += 10;

            // Special cards
            switch (card.CardValue)
            {
                case Value.Two:
                    score += 5;
                    break;

                case Value.Eight:
                    score += 5;
                    break;

                case Value.Jack:
                    score += 5;
                    break;

                case Value.Ace:
                    score += 4;
                    break;

                case Value.Seven:
                    score += 10;
                    break;
            }

            return score;
        }

        private int FindBestCard(string requiredSuit)
        {
            int bestIndex = -1;
            int bestScore = int.MinValue;

            Card topCard = Player.TopWastedDeck;

            // Looping through each of the cards
            for (int i = 0; i < computer.SeeCards.Count; i++)
            {
                Card card = computer.SeeCards[i];

                // If the current card index you can play than we'll
                // evaluate that card
                bool canPlay = requiredSuit == ""
                    ? Logic.canYouPlay(card, topCard)
                    : Logic.playAce(card, requiredSuit);

                if (canPlay)
                {
                    int score = EvaluateCard(card);

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestIndex = i;
                    }
                }
            }

            return bestIndex;
        }

    }
}