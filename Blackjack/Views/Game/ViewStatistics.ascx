<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Blackjack" %>

<table>
    <tr>
        <th>Outcome</th>
        <th>Total</th>
    </tr>


<% foreach (var statistic in ((Statistics)ViewData["statistics"]).getReport()){ %>
        <tr>
            <td><%= statistic.Key %></td>
            <td><%= statistic.Value %></td>
        </tr>
<% } %>

</table>