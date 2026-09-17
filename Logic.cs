using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;

namespace MatatuCSharp
{
    public class Logic{


        //method to make the computer choose a suit if they
        //play an Ace value. The computer will check their 
        //own hand and see which card will be suitable 
        //for them to win the game.
        // public static string compSuitChoice(Player computer){
        //     if(!Logic.ace_Value()){
        //         return "No Ace value";
        //     }
        //     string compSuit = "";
        //     //initialize a dictionary where each suit is the key and the value is the amount of cards 
        //     var suitCounts = new Dictionary<Suit, int>{
        //         {Suit.Hearts, 0},
        //         {Suit.Spades, 0},
        //         {Suit.Clubs, 0},
        //         {Suit.Diamonds,0}
        //     };

        //     foreach(Card card in computer.SeeCards){
        //         suitCounts[card.CardSuit]++;
        //     }
        //     //using the Aggregate method to determine which suit has the highest counts
        //     Suit chosenSuit = suitCounts.Aggregate((l, r) => l.Value > r.Value ? l : r).Key; 

        //     if(chosenSuit == Suit.Hearts){
        //         compSuit = "Hearts";
        //     } else if(chosenSuit == Suit.Spades){
        //         compSuit = "Spades";
        //     } else if(chosenSuit == Suit.Clubs){
        //         compSuit = "Clubs";
        //     } else if(chosenSuit == Suit.Diamonds){
        //         compSuit = "Diamonds";
        //     }

        //     return compSuit;
        // }
        //methdo to make the computer pick a card in
        //their deck to play. Returns True if they
        //can play, and plays that card. Returns False 
        //if they can't play and draws a card
        // public static bool computerChoice(Card topCard, Player computer){
        //     bool suit = false;
        //     bool value = false;
        //     int idx = 0;
            
        //     foreach(Card card in computer.SeeCards){
        //         if(card.CardSuit == topCard.CardSuit){
        //             suit = true;
        //         }
        //         if(card.CardValue == topCard.CardValue){
        //             value = true;
        //         }
        //         if(suit || value){
        //             break;
        //         }
        //         idx++;
        //     }
        //     if(suit || value){
        //         idx++;
        //         computer.playCard(idx);
        //         return true;
        //     } else {
        //         computer.drawCard();
        //         return false;
        //     }
        // }

        //method that makes sure the computer plays whatever card
        //that you chose in as the suit after playing an ace
        //it automatically checks it's deck and 

        // public static bool computerChoiceACE(Player computer, String suit){
        //     bool suits = false;
        //     int idx = 0;

        //     if(suit == "h" || suit == "H"){//if the string inputed is an h meaning they want hearts
        //         foreach (Card card in computer.SeeCards)//checks each card in the computers deck
        //         {
        //             if(card.CardSuit == Suit.Hearts){ //if it finds the card it will break loop
        //                 suits = true;
        //                 idx++;
        //                 break;
        //             }
        //             idx++;
        //         }
        //         if(suits){ //it will play that card and return true;
        //             computer.playCard(idx);
        //             return true;
        //         } else { //if suits is false it will just draw a card
        //             computer.drawCard();
        //             return false;
        //         }

        //     } else if(suit == "c" || suit == "C"){ //does the same for if it is a club
        //         foreach (Card card in computer.SeeCards)
        //         {
        //             if(card.CardSuit == Suit.Clubs){
        //                 suits = true;
        //                 idx++;
        //                 break;
        //             }
        //             idx++;
        //         }
        //         if(suits){
        //             computer.playCard(idx);
        //             return true;
        //         } else {
        //             computer.drawCard();
        //             return false;
        //         }

        //     } else if(suit == "s" || suit == "S"){ //if the suit is a spade
        //         foreach (Card card in computer.SeeCards)
        //         {
        //             if(card.CardSuit == Suit.Spades){
        //                 suits = true;
        //                 idx++;
        //                 break;
        //             }
        //             idx++;
        //         }
        //         if(suits){
        //             computer.playCard(idx);
        //             return true;
        //         } else {
        //             computer.drawCard();
        //             return false;
        //         }
                
        //     } else if(suit == "d" || suit == "D"){ //if the suit is a diamond
        //         foreach (Card card in computer.SeeCards)
        //         {
        //             if(card.CardSuit == Suit.Diamonds){
        //                 suits = true;
        //                 idx++;
        //                 break;
        //             }
        //             idx++;
        //         }
        //         if(suits){
        //             computer.playCard(idx);
        //             return true;
        //         } else {
        //             computer.drawCard();
        //             return false;
        //         }

        //     } else {
        //         Console.WriteLine("Invalid suit entered. The computer will play as the Ace's Suit.");
        //         return false;
        //     }
        // }



        //method to check if a card is valid to play
        //returns a bool variable

        public static bool canYouPlay(Card yourCard, Card topcard){
            if(yourCard.CardSuit == topcard.CardSuit || yourCard.CardValue == topcard.CardValue || yourCard.CardValue == Value.Ace){
                return true;
            } else {
                return false;
            }  
        }

        //method to allow the player to play whatever suit 
        //they want and if the computer can't play they
        //will have to play that card.

        public static bool playAce(Card yourCard, string suit){
            if(yourCard.CardValue == Value.Ace){
                return true;
            } else if(suit == "Hearts"){
                if(yourCard.CardSuit == Suit.Hearts){
                    return true;
                } else {
                    return false;
                }
            } else if(suit == "Spades"){
                if(yourCard.CardSuit == Suit.Spades){
                    return true;
                } else {
                    return false;
                }
            } else if(suit == "Clubs"){
                if(yourCard.CardSuit == Suit.Clubs){
                    return true;
                } else {
                    return false;
                }
            } else if(suit == "Diamonds"){
                if(yourCard.CardSuit == Suit.Diamonds){
                    return true;
                } else {
                    return false;
                }
            }
            return false;
        }

        //method to check if the card at the top is an
        //eight or a jack. If it is the player that is next
        //(Computer your user) will be skipped.

        public static bool jack_and_eight(Card topCard){
            if(topCard.CardValue == Value.Eight || topCard.CardValue == Value.Jack){
                return true;
            } else {
                return false;
            }   
        }

        //method that checks if the value placed 
        //at the top of the waste deck is a two
        //then it will automatically make you play 
        //two cards
        public static bool two_Value(Player player){
            if(Player.TopWastedDeck.CardValue == Value.Two){
                player.drawCard();
                player.drawCard();
                return true;
            } else {
                return false;
            }
        }


        //methed that checks if the value at the top
        //of the waste deck is an ace.
        //if it is an ace it will allow any suit to 
        //be placed.

        public static bool ace_Value(){
            if(Player.TopWastedDeck.CardValue == Value.Ace){
                return true;
            }
            return false;
        }

        //method that checks if the value at the top wasted deck is a seven
        //but of the same suit as the card at the top of the deck.

        public static bool seven_Val(Card firstCard){
            if(Player.TopWastedDeck.CardValue == Value.Seven && Player.TopWastedDeck.CardSuit == firstCard.CardSuit){
                return true;
            } else {
                return false;
            }
        }

        public static void results(Player player, Player computer){
            int playerTotal = 0;
            int compTotal = 0;
            foreach(Card card in player.SeeCards){
                if(card.CardSuit == Suit.Spades && card.CardValue == Value.Ace){
                    playerTotal += 30;
                } else if(card.CardValue == Value.Ace){
                    playerTotal += 1;
                } else if(card.CardValue == Value.Two){
                    playerTotal += 2;
                } else if(card.CardValue == Value.Three){
                    playerTotal += 3;
                } else if(card.CardValue == Value.Four){
                    playerTotal += 4;
                } else if(card.CardValue == Value.Five){
                    playerTotal += 5;
                } else if(card.CardValue == Value.Six){
                    playerTotal += 6;
                } else if(card.CardValue == Value.Seven){
                    playerTotal += 7;
                } else if(card.CardValue == Value.Eight){
                    playerTotal += 8;
                } else if(card.CardValue == Value.Nine){
                    playerTotal += 9;
                } else if(card.CardValue == Value.Ten){
                    playerTotal += 10;
                } else if(card.CardValue == Value.Jack){
                    playerTotal += 11;
                } else if(card.CardValue == Value.Queen){
                    playerTotal += 12;
                } else if(card.CardValue == Value.King){
                    playerTotal += 13;
                }
            }

            foreach(Card card in computer.SeeCards){
                if(card.CardSuit == Suit.Spades && card.CardValue == Value.Ace){
                    compTotal += 30;
                } else if(card.CardValue == Value.Ace){
                    compTotal += 1;
                } else if(card.CardValue == Value.Two){
                    compTotal += 2;
                } else if(card.CardValue == Value.Three){
                    compTotal += 3;
                } else if(card.CardValue == Value.Four){
                    compTotal += 4;
                } else if(card.CardValue == Value.Five){
                    compTotal += 5;
                } else if(card.CardValue == Value.Six){
                    compTotal += 6;
                } else if(card.CardValue == Value.Seven){
                    compTotal += 7;
                } else if(card.CardValue == Value.Eight){
                    compTotal += 8;
                } else if(card.CardValue == Value.Nine){
                    compTotal += 9;
                } else if(card.CardValue == Value.Ten){
                    compTotal += 10;
                } else if(card.CardValue == Value.Jack){
                    compTotal += 11;
                } else if(card.CardValue == Value.Queen){
                    compTotal += 12;
                } else if(card.CardValue == Value.King){
                    compTotal += 13;
                } 
            }
            Console.WriteLine("*******************************************");
            Console.WriteLine("Showing Scores:");
            Console.WriteLine("Player\t\t\tComputer");
            Console.WriteLine(playerTotal + "\t\t\t" + compTotal);
            if(playerTotal < compTotal){
                Console.WriteLine("\nCongratulations! You Won!");
            } else if(playerTotal == compTotal){
                Console.WriteLine("\nYou Tied!");
            } else {
                Console.WriteLine("\nSorry! You Lost :(");
            }
        } 
    }
}