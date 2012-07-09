using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blackjack
{
    public class Statistics
    {
        private List<StatisticInfo> statisticList = new List<StatisticInfo>();

        public Statistics add(Hand.outcomeType outcome, Hand playerHand, Hand dealerHand)
        {
            this.statisticList.Add(new StatisticInfo(outcome, playerHand, dealerHand));
            return this;
        }

        public Dictionary<string, int> getReport()
        {
            var report = from s in statisticList
                         group s by s.outcome into g
                         orderby g.Key
                         select new { Outcome = g.Key, Total = g.Count() };

            return report.ToDictionary(o => o.Outcome, o=> o.Total);

        }
    }

    public class StatisticInfo 
    {
        public string outcome {get; set;}
        public Hand playerHand {get; set;}
        public Hand dealerHand {get; set;}

        public StatisticInfo(Hand.outcomeType outcome, Hand playerHand, Hand dealerHand)
        {
            this.outcome = Hand.getOutcomeString(outcome);
            this.playerHand = playerHand;
            this.dealerHand = dealerHand;
        }

    }
}
