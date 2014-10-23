<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CodeSmells.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Welcome to CodeSmells.com</h1>
        <p>If you use this code for your course project do not forget to mention the source! Yes we sue.</p>
    </div>

    <div class="jumbotron">
        <h1>Categories</h1>
        <br />
        <asp:Repeater runat="server" ID="PostsCategories">
            <ItemTemplate>
                <a href="Posts/GetPosts?category=<%#: Eval("Name") %>"><%#: Eval("Name") %></a>
            </ItemTemplate>
        </asp:Repeater>
        <br />
        <br />
        <br />
        <br />
    </div>

    <div class="container">
        <asp:Repeater runat="server" ID="LatestPosts" ItemType="CodeSmells.Models.Post">
            <ItemTemplate>
                <div class="container" style="border: 2px solid white; padding: 20px;">
                    <div class="row">
                        <h1>
                            <a href="Posts/PostDetails?id=<%#: Item.PostId %>">
                                <%#: Item.Title %>
                            </a>
                        </h1>
                    </div>
                    <br />
                    <div class="row">
                        <pre class="prettyprint linenums"><%#: Item.Body %></pre>
                    </div>
                    <div class="row">
                        <h1>
                            <a href="Public/ProfileDetails?id=<%#: Item.AuthorId %>">
                                <%#: Item.Author.UserName %>
                            </a>
                        </h1>
                    </div>
                    
                    <div class="row">
                        <h1>
                            Rating <%#: Item.Rating %>
                        </h1>
                    </div>
                </div>
                <br />
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
