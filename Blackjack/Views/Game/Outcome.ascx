<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.RenderPartial("Table",ViewData); %>

<% Html.RenderPartial("Actions",ViewData); %>