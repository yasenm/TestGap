<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProfileImage.aspx.cs" Inherits="CodeSmells.Web.Account.ProfileImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Profile Picture Settings</h1>


    <asp:FileUpload ID="UploadImage" runat="server" />
    <%--<asp:RegularExpressionValidator ID="uplValidator" runat="server" ControlToValidate="UploadImage" CssClass="text-error"
        ErrorMessage=".JPG, .JPEG , PNG &amp; GIF formats are allowed"
        ValidationExpression="(.+\.([Gg][iI][fF])|.+\.([Jj][pP][Gg])|.+\.([Pp][Nn][Gg])|.+\.([Jj][Pp][Ee][Gg]))" />--%>
    <br />
    <br />
    <asp:Button Text="Upload" CssClass="btn btn-success" OnClick="OnUploadBtn_Click" runat="server" />
    <asp:Button Text="Set Default" CssClass="btn btn-success" OnClick="OnSetDefault_Click" runat="server" />
</asp:Content>
