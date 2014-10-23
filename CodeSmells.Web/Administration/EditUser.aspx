<%@ Page Title="" Language="C#" MasterPageFile="~/Administration/MasterPageAdmins.master" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="CodeSmells.Web.Administration.EditUser" %>

<asp:Content ID="EditUserContent" ContentPlaceHolderID="ContentPlaceHolderAdminArea" runat="server">
    <h2>Edit User</h2>
    <div class="container-fluid">
        <asp:Image CssClass="img-rounded" Width="125"
            AlternateText="Profile Picture" runat="server" ID="ImgProfileImage" />
    </div>
    <div class="form-group">
        <asp:Label Text="Username" runat="server" />
        <asp:TextBox ID="TbUserName" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <asp:Label Text="Email" runat="server" />
        <asp:TextBox ID="TbEmail" runat="server" TextMode="Email" CssClass="form-control" />
    </div>

    <div class="btn-group">
        <asp:Button ID="BtnBanUser" runat="server" Text="Ban" CssClass="btn btn-danger"
            OnClick="BtnBanUser_Click" />

        <asp:Button ID="BtnToggleAdminRole" runat="server" Text="Assign Admin Rights" CssClass="btn btn-default"
            OnClick="BtnToggleAdminRole_Click" />
    </div>
    <br />
    <br />
    <br />
    <asp:LinkButton ID="LinkBtnSaveUser" runat="server" CssClass="btn btn-success"
        Text="Save Changes" OnClick="LinkBtnSaveUser_Click" />
    <br />

</asp:Content>
