<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Blackjack" %>

<p>
    <% foreach (Card card in ((Hand)ViewData["dealerHand"]).getCards()) {%>
        <% if (((Hand)ViewData["dealerHand"]).isFirst(card) && ((Hand.outcomeType)ViewData["outcome"] == Hand.outcomeType.Busted || (Hand.outcomeType)ViewData["outcome"] == Hand.outcomeType.None))
           { %>
            <img src="/Content/cards/<%= card.getImageName(true) %>" alt="No value here, cheater!!"/>
        <% }else{%>
            <img src="/Content/cards/<%= card.getImageName(false) %>" alt="<%= card.getAltName() %>"/>
        <% }%>       
    <% }%>
</p>

<p id="dealerTotal">
    <%= ((Hand.outcomeType)ViewData["outcome"] == Hand.outcomeType.Busted || (Hand.outcomeType)ViewData["outcome"] == Hand.outcomeType.None) ? "" : "Dealer total: " + ((Hand)ViewData["dealerHand"]).getTotal().ToString() %>
</p>



<p>
    <% foreach (Card card in ((Hand)ViewData["playerHand"]).getCards()) {%>
            <img src="/Content/cards/<%= card.getImageName(false) %>" alt="<%= card.getAltName() %>"/>       
    <% }%>
</p>


<% if (((Hand)ViewData["playerHand"]).hasCards()){ %>

    <p id="playerTotal">
        Your total: <%= ((Blackjack.Hand)ViewData["playerHand"]).getTotal() %>        
    </p>
    
<% } %>

<%= (Hand.outcomeType)ViewData["outcome"] != Hand.outcomeType.None ? Hand.getOutcomeString((Hand.outcomeType)ViewData["outcome"]) : ""  %>
    
<% Html.RenderPartial("Actions",ViewData); %>