using System;

namespace MatatuCSharp
{
    public class Program
    {
        static void Main(string[] args)
        {
            bool playerPicked = false;
            bool stop = false;
            bool isEightJack = false;
            bool isAce = false;
            bool gameEnded = false;
            string suit = "";
            string suit2 = "";
            Card firstCard = null;

            Console.WriteLine("Welcome to Matatu!!\n");
            Console.Write("Press (1) to START GAME OR (Enter) to EXIT.");
           
            // User doesn't want to play
            if (Console.ReadLine() != "1")
            {
                Console.WriteLine("Bye!");
                return;
            }

            Console.WriteLine("Shuffling deck...\n");
            Deck myDeck = new Deck();
            myDeck.shuffleDeck();
            Player player = null;
            Player computer = null;
            MatatuAI ai = null;

            // Player picks 4 cards 
            while (!playerPicked)
            {
                Console.WriteLine("Please select (P) to automatically pick 4 cards. Select (S) to end game.");
                string playerPick = Console.ReadLine();
                if (playerPick == "P" || playerPick == "p")
                {
                    playerPicked = true;
                    player = new Player(myDeck);
                    computer = new Player(myDeck);
                    ai = new MatatuAI(computer);
                    Player.showCards(player);
                    firstCard = Player.firstCard(myDeck);
                }
                else if (playerPick == "S" || playerPick == "s")
                {
                    Console.WriteLine("Thanks for playing!");
                    return;
                }
            }

            // Until someone runs out of cards the game will continue
            while (player.cardInHandAmount() > 0 && computer.cardInHandAmount() > 0)
            {
                Console.WriteLine("\n******************************************");

                if (!isEightJack)
                {
                    Console.WriteLine("\nTop of Deck: " + Player.TopWastedDeck);
                    Console.WriteLine("\nPlease PLAY a card (1-" + player.cardInHandAmount() + "), (P) to pick a card, or (S) to STOP");
                    string playerCard = Console.ReadLine();

                    if (playerCard == "S" || playerCard == "s")
                    {
                        Console.WriteLine("Thanks for playing!");
                        stop = true;
                        break;
                    }

                    if (playerCard == "P" || playerCard == "p")
                    {
                        Card yourPick = player.drawCard();
                        Console.WriteLine("You picked a(n) " + yourPick + " from the deck");
                        if (Logic.canYouPlay(yourPick, Player.TopWastedDeck))
                        {
                            Console.WriteLine("This card is playable. Play it? (Y/N)");
                            string drawChoice = Console.ReadLine();
                            if (drawChoice == "y" || drawChoice == "Y")
                            {
                                player.playCard(player.cardInHandAmount());
                                Console.WriteLine("You played: " + Player.TopWastedDeck);
                            }
                        }
                    }
                    else
                    {
                        if (!int.TryParse(playerCard, out int cardToPlay) || cardToPlay < 1 || cardToPlay > player.cardInHandAmount())
                        {
                            Console.WriteLine("Please put a valid card number");
                            continue;
                        }

                        if (Logic.seven_Val(firstCard))
                        {
                            Console.WriteLine("You ended the game!");
                            gameEnded = true;
                            break;
                        }

                        Card selectedCard = player.chooseCard(cardToPlay);
                        if (isAce)
                        {
                            if (!Logic.playAce(selectedCard, suit2))
                            {
                                Console.WriteLine("You can't place that, you must put a " + suit2 + " card");
                                continue;
                            }
                            isAce = false;
                        }
                        else if (!Logic.canYouPlay(selectedCard, Player.TopWastedDeck))
                        {
                            Console.WriteLine("You can't place that card");
                            continue;
                        }

                        player.playCard(cardToPlay);
                        Console.WriteLine("You played a(n) " + Player.TopWastedDeck);
                        if (Logic.ace_Value())
                        {
                            isAce = true;
                            suit = "";
                            while (suit != "h" && suit != "c" && suit != "s" && suit != "d")
                            {
                                Console.WriteLine("What suit would you like? (H), (C), (S), or (D)");
                                suit = Console.ReadLine().ToLower();
                                suit2 = suit == "h" ? "Hearts" : suit == "s" ? "Spades" : suit == "c" ? "Clubs" : suit == "d" ? "Diamonds" : "";
                            }
                        }
                        else if (Logic.jack_and_eight(Player.TopWastedDeck))
                        {
                            Console.WriteLine("The computer has been skipped!");
                            continue;
                        }
                        else if (Logic.two_Value(computer))
                        {
                            Console.WriteLine("The computer drew 2 cards");
                            continue;
                        }
                    }
                }

                if (player.cardInHandAmount() == 0)
                {
                    break;
                }

                isEightJack = false;
                AIAction action = ai.ChooseAction(isAce ? suit2 : "");
                if (action.ActionType == AIActionType.DrawCard)
                {
                    computer.drawCard();
                    Console.WriteLine("\nThe Computer Drew a card. The top of the deck is still: \n" + Player.TopWastedDeck);
                }
                else
                {
                    computer.playCard(action.CardIndex + 1);
                    Console.WriteLine("\nThe Computer played: " + Player.TopWastedDeck);
                    Console.WriteLine("The computer has " + computer.cardInHandAmount() + " left in hand");

                    if (Player.TopWastedDeck.CardValue == Value.Ace)
                    {
                        suit2 = action.ChosenSuit;
                        isAce = true;
                        Console.WriteLine("The Computer chose a " + suit2 + ".");
                        continue;
                    }
                    if (Logic.seven_Val(firstCard))
                    {
                        Console.WriteLine("The Computer ended the game!");
                        gameEnded = true;
                        break;
                    }
                    if (Logic.jack_and_eight(Player.TopWastedDeck))
                    {
                        isEightJack = true;
                        Console.WriteLine("You have been skipped");
                    }
                    else if (Logic.two_Value(player))
                    {
                        isEightJack = true;
                        Console.WriteLine("You must draw 2 cards :(");
                    }
                    isAce = false;
                }
                Player.showCards(player);
            }

            if (gameEnded)
            {
                Logic.results(player, computer);
            }
            else if (!stop)
            {
                Console.WriteLine(player.cardInHandAmount() < 1 ? "You Won! You have no more cards left!  :)" : "The Computer Won! :(");
            }
        }
    }
}
