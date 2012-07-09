using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Blackjack.Models;

namespace Blackjack.Controllers
{
    public class GameController : Controller
    {
        #region Session vars
        protected Hand dealerHand
        {
            get
            {
                return Session[SessionKeys.DealerKey] as Hand ?? new Hand();
            }
            set
            {
                Session[SessionKeys.DealerKey] = value;
            }
        }

        protected Hand playerHand
        {
            get
            {
                return Session[SessionKeys.PlayerKey] as Hand ?? new Hand();
            }
            set
            {
                Session[SessionKeys.PlayerKey] = value;
            }
        }

        protected Hand.outcomeType Outcome
        {
            get
            {
                return (Hand.outcomeType)Session[SessionKeys.Outcome];
            }
            set
            {
                Session[SessionKeys.Outcome] = value;
            }
        }

        protected Statistics Statistics
        {
            get
            {
                return Session[SessionKeys.Statistics] as Statistics ?? new Statistics();
            }
            set
            {
                Session[SessionKeys.Statistics] = value;
            }
        }
        #endregion

        
        // GET: /Game/

        public ActionResult Index()
        {
            if (this.dealerHand.isEmpty())
            {
                // initialize session vars only the first time this action is executed
                this.dealerHand = this.dealerHand.addCard(2);
                this.playerHand = this.playerHand.addCard(1);
                this.Outcome = Hand.outcomeType.None;
            }
            
            // pass vars to the view
            ViewData["dealerHand"] = this.dealerHand;
            ViewData["playerHand"] = this.playerHand;
            ViewData["outcome"] = this.Outcome;

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Hit()
        {
            // only hit player if it's an Ajax request
            if (Request.IsAjaxRequest())
            {
                ViewData["dealerHand"] = this.dealerHand;
                // add a card to player's hand
                ViewData["playerHand"] = this.playerHand.addCard(1);

                if (this.playerHand.getTotal() > 21)
                {
                    // busted!!
                    this.Outcome = Hand.outcomeType.Busted;
                    // update statistics
                    this.Statistics = this.Statistics.add(this.Outcome, this.playerHand, this.dealerHand);
                }

                ViewData["outcome"] = this.Outcome;
                return PartialView("Table");
                
            }
           else
            {
                return RedirectToAction("Index");
            } 
            
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Stand()
        {
            // only process stand if it's an Ajax request
            if (Request.IsAjaxRequest())
            {
                // hit dealer until it reaches 17 or more
                while (this.dealerHand.getTotal() < 17)
                {
                    this.dealerHand.addCard(1);
                }

                // at this stage there is an outcome
                this.Outcome = this.playerHand.outcome(this.dealerHand);
                ViewData["outcome"] = this.Outcome;

                // update statistics
                this.Statistics = this.Statistics.add(this.Outcome, this.playerHand, this.dealerHand);

                ViewData["dealerHand"] = this.dealerHand;
                ViewData["playerHand"] = this.playerHand;

                return PartialView("Table");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Reset()
        {
            // only process reset if it's an Ajax request
            if (Request.IsAjaxRequest())
	        {
                // reset hands
                this.dealerHand = new Hand().addCard(2);
                this.playerHand = new Hand().addCard(1);

                ViewData["dealerHand"] = this.dealerHand;
                ViewData["playerHand"] = this.playerHand;

                // only update statistics if the user decided to change cards, not if the game had another outcome
                if (this.Outcome == Hand.outcomeType.None)
                {
                    // update statistics
                    this.Statistics = this.Statistics.add(Hand.outcomeType.Reset, this.playerHand, this.dealerHand);    
                }

                // reset outcome
                this.Outcome = Hand.outcomeType.None;
                ViewData["outcome"] = this.Outcome;

                return PartialView("Table");	 
	        }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ViewStatistics()
        {
            // only show statistics if it's an Ajax request
            if (Request.IsAjaxRequest())
            {
                ViewData["statistics"] = this.Statistics;

                return PartialView("ViewStatistics");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

    }
}
