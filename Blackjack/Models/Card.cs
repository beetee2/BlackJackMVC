using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace Blackjack
{
    public class Card
    {
        private string[] suits = {"club", "diamond", "heart", "spade"};
        private List<string> letters = new List<string> { "j", "q", "k"};
        private List<string> numbers =  Enumerable.Range(1, 10).ToList().ConvertAll<string>(i => i.ToString());

        public string suit { get; set; }
        public string value { get; set; }

        public Card()
        {
            // ugly way to help initialize random constructor with differnt seed
            System.Threading.Thread.Sleep(1);

            Random rnd = new Random(DateTime.Now.Millisecond);
            List<string> values = numbers.Union(letters).ToList<string>();
            
            this.suit = suits[rnd.Next(suits.Length - 1)];
            this.value = values[rnd.Next(values.Count - 1)];

        }

        public string getImageName(bool back)
        {
            if (back)
            {
                return "b1fv.gif";
            }
            return string.Format("{0}{1}.gif", this.suit.Substring(0,1), this.value.ToString());
        }

        public string getAltName()
        {
            return string.Format("{0} of {1}s", this.getIntValue(), this.suit);
        }

        public int getIntValue()
        {
            try
            {
                return int.Parse(this.value);
            }
            catch (System.FormatException)
            {

                return 10;
            }
        }
    }
}
