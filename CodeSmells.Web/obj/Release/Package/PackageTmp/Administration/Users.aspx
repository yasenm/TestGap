<%@ Page Title="" Language="C#" MasterPageFile="~/Administration/MasterPageAdmins.master" AutoEventWireup="true"
    CodeBehind="Users.aspx.cs" Inherits="CodeSmells.Web.Administration.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderAdminArea" runat="server">
    
    <asp:Label Text="Page Size" runat="server" />
    <asp:DropDownList runat="server" ID="DdlPageSize" OnSelectedIndexChanged="ResizePages" AutoPostBack="true">
        <asp:ListItem Text="5" />
        <asp:ListItem Text="10" Selected="true"/>
        <asp:ListItem Text="25" />
        <asp:ListItem Text="50" />
    </asp:DropDownList>

    <asp:TextBox ID="TbSearch" runat="server" CssClass="input-large"></asp:TextBox>
    <asp:Button ID="BtnSearch" runat="server" Text="Search" CssClass="btn" OnClick="OnSearchButton_Click" />

    <asp:GridView CssClass="table table-striped table-bordered" ID="GridViewUsers" runat="server"
        AutoGenerateColumns="False" DataKeyNames="Id"
        PageSize="10" AllowPaging="true" AllowSorting="true"
        ItemType="CodeSmells.Models.User"
        SelectMethod="GridViewUsers_GetData">
        <Columns>
            <asp:ImageField DataImageUrlFormatString="\Uploads\Images\{0}" 
                DataImageUrlField="ProfileImage"
                NullImageUrl="\Uploads\Images\default.jpg">
                <ItemStyle Height="30px" Width="30px" />
            </asp:ImageField>
            <asp:HyperLinkField DataTextField="UserName" HeaderText="Action"
                DataNavigateUrlFields="Id"
                DataNavigateUrlFormatString="EditUser.aspx?userId={0}" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:HyperLinkField Text="Edit" HeaderText="Action"
                DataNavigateUrlFields="Id"
                DataNavigateUrlFormatString="EditUser.aspx?userId={0}" />
        </Columns>
    </asp:GridView>
</asp:Content>
