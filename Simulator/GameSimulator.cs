namespace MatatuCSharp
{
    public class GameSimulator
    {
        public SimulationResult RunGame(bool heuristicStarts)
        {
            // Create and shuffle a new deck
            Deck deck = new Deck();
            deck.shuffleDeck();

            // Create the two AI-controlled players
            Player heuristicPlayer = new Player(deck);
            Player randomPlayer = new Player(deck);

            // Put the first card onto the waste pile
            Card firstCard = Player.firstCard(deck);

            // Give each player its AI strategy
            IPlayerAgent heuristicAgent = new MatatuAI(heuristicPlayer);
            IPlayerAgent randomAgent = new RandomAgent(randomPlayer);

            // Keep track of whose turn it is
            bool heuristicTurn = heuristicStarts;

            // Needed when an Ace changes the required suit
            string requiredSuit = "";

            // limit to prevent infinite game
            int turnCount = 0;
            int maxTurns = 500;

            while (
                heuristicPlayer.cardInHandAmount() > 0 &&
                randomPlayer.cardInHandAmount() > 0 &&
                turnCount < maxTurns)
            {
                turnCount++;

                Player currentPlayer =
                    heuristicTurn ? heuristicPlayer : randomPlayer;

                Player opponent =
                    heuristicTurn ? randomPlayer : heuristicPlayer;

                IPlayerAgent currentAgent =
                    heuristicTurn ? heuristicAgent : randomAgent;

                GameState gameState = new GameState
                {
                    TopCard = Player.TopWastedDeck,
                    RequiredSuit = requiredSuit
                };

                // Action of the current agent
                AIAction action =
                    currentAgent.ChooseAction(gameState);

                if (action.ActionType == AIActionType.DrawCard)
                {
                    // Avoid drawing if the deck is empty
                    if (deck.Cards.Count == 0)
                    {
                        break;
                    }

                    currentPlayer.drawCard();
                }
                else
                {
                    currentPlayer.playCard(action.CardIndex + 1);

                    Card playedCard = Player.TopWastedDeck;

                    // Handle Ace suit selection
                    if (playedCard.CardValue == Value.Ace)
                    {
                        requiredSuit = action.ChosenSuit;
                    }
                    else
                    {
                        requiredSuit = "";
                    }

                    // Seven can end the game
                    if (Logic.seven_Val(firstCard))
                    {
                        break;
                    }

                    // Jack or Eight skips opponent
                    if (Logic.jack_and_eight(playedCard))
                    {
                        // Do not switch turns.
                        continue;
                    }

                    // Two makes opponent draw two and lose their turn
                    if (playedCard.CardValue == Value.Two)
                    {
                        if (deck.Cards.Count >= 2)
                        {
                            opponent.drawCard();
                            opponent.drawCard();
                        }

                        // Do not switch turns.
                        continue;
                    }
                }

                // Change to the other AI
                heuristicTurn = !heuristicTurn;
            }

            string winner;

            if (heuristicPlayer.cardInHandAmount() <
                randomPlayer.cardInHandAmount())
            {
                winner = "MatatuAI";
            }
            else if (randomPlayer.cardInHandAmount() <
                     heuristicPlayer.cardInHandAmount())
            {
                winner = "RandomAgent";
            }
            else
            {
                winner = "Tie";
            }

            return new SimulationResult
            {
                Winner = winner,
                TurnCount = turnCount,
                HeuristicCardsLeft = heuristicPlayer.cardInHandAmount(),
                RandomCardsLeft = randomPlayer.cardInHandAmount()
            };
        }
    }
}