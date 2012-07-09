<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Playing Blackjack
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 id="title">Playing Blackjack</h2>
      
    <img id="ajax_loader" src="/Content/ajax-loader.gif" alt="Ajax loader" style="display:none" />
    
    <div id="table">  
        <% Html.RenderPartial("Table",ViewData); %>
    </div>

    <div id="statistics">
    </div>
    
    <%= Ajax.ActionLink("View/Refresh Statistics", "ViewStatistics", new AjaxOptions(){ HttpMethod = "Post", UpdateTargetId = "statistics", LoadingElementId = "ajax_loader" }) %>
    
</asp:Content>
