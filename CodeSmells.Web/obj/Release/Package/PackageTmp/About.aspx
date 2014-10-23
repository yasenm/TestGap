<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="About.aspx.cs" Inherits="CodeSmells.Web.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Title %>.</h2>
    <h4>This application lets you share the worst bits of code you've ever encountered.</h4>
    
</asp:Content>