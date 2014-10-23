<%@ Page Title="" Language="C#" MasterPageFile="~/Public/MasterPageProfile.master"
    AutoEventWireup="true"
    CodeBehind="ProfileDetails.aspx.cs"
    Inherits="CodeSmells.Web.Public.ProfileDetails" %>

<asp:Content ID="ProfileDetails" ContentPlaceHolderID="ContentPlaceHolderProfileArea" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-lg-4">
                <asp:Image CssClass="img-rounded" Width="125"
                    AlternateText="Profile Picture"
                    runat="server"
                    ID="AvatarContainer" />
                <div class="row">
                    <h1><%#: this.CurrentUser.Email %></h1>
                </div>
            </div>
            <br />
            <div class="col-lg-4">
                <h3>Rank <strong><%#: "User rank here..." %></strong></h3>
                <h3>Posts <strong><%#: this.GetUserPosts().Count() %></strong></h3>
                <h3>Member since <strong><%#: "Some date here..." %></strong></h3>
                <h3>Overal user ratings <strong><%#: this.GetUserRatings %></strong></h3>
            </div>
            <br />
        </div>
    </div>
    <asp:GridView CssClass="table table-striped table-bordered" ID="GridViewUserPosts" runat="server"
        AutoGenerateColumns="False" DataKeyNames="PostId"
        PageSize="10" AllowPaging="true" AllowSorting="true"
        ItemType="CodeSmells.Models.Post"
        SelectMethod="GetUserPosts">
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="PostId" DataNavigateUrlFormatString="~/Posts/PostDetails?id={0}" Text="Go to..." />
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
            <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
            <asp:BoundField DataField="Rating" HeaderText="Rating" SortExpression="Rating" />
            <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" SortExpression="DateCreated" />
        </Columns>
    </asp:GridView>

    <%--<div class="form-group">
                <asp:Label Text="Username" runat="server" />
            </div>

            <div class="form-group">
                <asp:Label Text="Email" runat="server" />
            </div>--%>
</asp:Content>
