using System;
using System.Runtime.ExceptionServices;

namespace MatatuCSharp
{
    public class Player{
        private List<Card> cardsInHand;
        private static List<Card> deckOfCards;

        private static List<Card> wastedDeck;
        
        public Player(Deck myDeck){
            deckOfCards = myDeck.Cards;
            wastedDeck = new List<Card>();
            cardsInHand = new List<Card>();
            for(int i = 0; i < 5; i++){
                drawCard();
            }
        }
        //method to play a card into the waste pile

        //substracts the number from the parameter by 1 to match index rules

        //card in hand amount is zero it Or if user enter number greater than the card
        //amount or a number less than 1 it will throw InvalidOperatinException


        public void playCard(int card){ 
            int cardNum = card - 1; 
            if(cardsInHand.Count == 0){ 
                throw new InvalidOperationException("You have no cards in hand");
            } else if (card > cardsInHand.Count || card < 1){
                throw new InvalidOperationException("That card isn't available");
            }else { //removes card from hand and add card to waste list of cards
                Card myCard = cardsInHand[cardNum];
                wastedDeck.Add(myCard);
                cardsInHand.RemoveAt(cardNum);
            }
        }
        public Card drawCard(){ //method that draws a card from the deck
            if(deckOfCards.Count == 0){
                throw new InvalidOperationException("No cards left in deck!");
            } else {
                Card drawnCard = deckOfCards[0];
                deckOfCards.RemoveAt(0);
                cardsInHand.Add(drawnCard);
                return drawnCard;
            }
        }

        public int cardInHandAmount(){ //method to see amount of cards in hand
            return cardsInHand.Count;
        }

        public List<Card> SeeCards{ //method to see the cards in hand
            get{
                return cardsInHand;
            }
        }

        //static method to show a players cards
        //and also displays which button to select
        //to play that card

        public static void showCards(Player player){
            int count = 1;
            Console.WriteLine("\nShowing Hand: ");
            foreach (Card card in player.SeeCards)
            {
                Console.WriteLine("(" + count + ") " + card);
                count++;  
            }
        }

        public Card chooseCard(int card){
            card -= 1;
            Card myCard = cardsInHand[card];
            return myCard;
        }
        //static method to see the top of the waste deck. 
        //both the player and the computer can interact with it

        public static Card TopWastedDeck{ //method to see the top of the waste deck
            get{
                if(wastedDeck.Count == 0){
                    throw new InvalidOperationException("No cards in wasted deck! Please play a card");
                }
                return wastedDeck[wastedDeck.Count - 1];
            }
        }

        //places the first card into the waste deck
        //makes sure that the first card of the deck
        //doesn't have a value of 7 (7 of whatever card
        //is on the top of the deck will end the game).

        public static Card firstCard(Deck myDeck){
            int idx = 0;
            Card firstCard = null;
            foreach(Card card in deckOfCards){
                if(card.CardValue != Value.Seven){
                    firstCard = deckOfCards[idx];
                    break;
                }
                idx++;
            }
            deckOfCards.RemoveAt(idx);
            wastedDeck.Add(firstCard);
            return firstCard;
        }
    }
}