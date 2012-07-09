<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Blackjack" %>

<div id="actions">
    <% if ((Hand.outcomeType)ViewData["outcome"] == Hand.outcomeType.None) { %>
    
        <%= Ajax.ActionLink("Hit me!!", "Hit", new AjaxOptions() { UpdateTargetId = "table", HttpMethod = "Post", LoadingElementId = "ajax_loader" })%>
        <%= Ajax.ActionLink("Ok I Stand", "Stand", new AjaxOptions() { UpdateTargetId = "table", HttpMethod = "Post", LoadingElementId = "ajax_loader" })%>
        
    <% } %>
    
    <%= Ajax.ActionLink("Start Again", "Reset", new AjaxOptions() { UpdateTargetId = "table", HttpMethod = "Post", LoadingElementId = "ajax_loader" })%>
</div>
