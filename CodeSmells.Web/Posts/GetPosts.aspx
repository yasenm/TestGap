<%@ Page Title="Posts" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GetPosts.aspx.cs" Inherits="CodeSmells.Web.Posts.GetPosts" %>

<asp:Content ID="GetPostsContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="well">
            <div class="table table-striped">
                <asp:GridView runat="server"
                    ID="GetAllPostsGridView"
                    DataKeyNames="PostId"
                    PageSize="10"
                    AllowPaging="true"
                    AllowSorting="true"
                    AutoGenerateColumns="False">

                    <Columns>
                        <asp:TemplateField HeaderText="Title" SortExpression="Title">
                            <ItemTemplate>
                                <asp:HyperLink ID="TextBox1" runat="server" NavigateUrl='<%# "~/Posts/PostDetails.aspx?id=" + Eval("PostId") %>' Text='<%# Eval("Title")%>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ControlStyle-Width="700px" HeaderText="Code">
                            <ItemTemplate>
                                <pre class="prettyprint linenums">
                                 <asp:Label ID="CodeLabel" runat="server" CssClass="prettyprint linenums" Text='<%#: Eval("Body") %>'></asp:Label>
                            </pre>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" SortExpression="DateCreated" />
                        <asp:HyperLinkField DataTextField="Category" HeaderText="Category" SortExpression="Category"
                            DataNavigateUrlFields="Category"
                            DataNavigateUrlFormatString="GetPosts?category={0}" />

                        <asp:HyperLinkField DataTextField="Author" HeaderText="Author" SortExpression="Author"
                            DataNavigateUrlFields="AuthorId"
                            DataNavigateUrlFormatString="~/Public/ProfileDetails?id={0}" />

                        <%--<asp:BoundField DataField="Rating" HeaderText="Rating" />--%>
                    </Columns>
                    <PagerSettings PageButtonCount="4" Mode="NextPrevious" NextPageText="Next" PreviousPageText="Previous" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
