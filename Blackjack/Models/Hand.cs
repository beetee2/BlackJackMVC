using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blackjack
{
    public class Hand
    {

        public enum outcomeType
        {
            None        = 0,
            Won         = 1,
            Lost        = 2,
            Push        = 3,
            Blackjack   = 4,
            Busted      = 5,
            Reset       = 6
        }

        private static string[] outcomeString = new string[] { "None", "Won", "Lost", "Push", "Blackjack", "Busted", "Reset" };

        private List<Card> cards;

        public Hand()
        {
            this.cards = new List<Card>();
        }

        public List<Card> getCards()
        {
            return this.cards;
        }

        public Hand addCard(Card card)
        {
            this.cards.Add(card);
            return this;
        }

        public Hand addCard(int number)
        {
            for (int i = 0; i < number; i++)
            {
                Card c = new Card();
                this.cards.Add(c);
            }
            return this;
        }

        public bool isEmpty()
        {
            return this.cards.Count == 0;
        }


        public bool hasCards()
        {
            return this.cards.Count > 0;
        }
        
        public int getTotal()
        {
            int total = 0;

            foreach (Card card in this.cards)
            {
                if (card.value == "1")
                {
                    total += 11;
                }
                else
                {
                    total += card.getIntValue();
                }
                
            }

            if (total > 21)
            {
                foreach (Card ace in this.cards.Where((c) => c.value == "1"))
                {
                    total -= 10;
                    
                    if (total <= 21)
                    {
                        break;
                    }
                }
            }

            return total;
        }

        public bool isFirst(Card card)
        {
            return this.cards.IndexOf(card).Equals(0);
        }


        public bool isBlackJack()
        {
            return (this.getTotal() == 21 && this.cards.Count == 2);
        }

        public outcomeType outcome(Hand dealerHand) 
        {
            outcomeType returnValue = outcomeType.None;

            if (this.getTotal() > 21)
                returnValue = outcomeType.Lost;
            else if (this.isBlackJack() && !dealerHand.isBlackJack())
                returnValue = outcomeType.Blackjack;
            else if (dealerHand.getTotal() > 21)
                returnValue = outcomeType.Won;
            else if (this.getTotal() < dealerHand.getTotal())
                returnValue = outcomeType.Lost;
            else if (this.getTotal() > dealerHand.getTotal())
                returnValue = outcomeType.Won;
            else if (this.getTotal() == dealerHand.getTotal())
                returnValue = outcomeType.Push;

            return returnValue;
        }

        public static string getOutcomeString(outcomeType outcome)
        { 
            return outcomeString[(int) outcome];
        }

    }
}
