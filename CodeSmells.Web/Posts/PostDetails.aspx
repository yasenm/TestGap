<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PostDetails.aspx.cs" Inherits="CodeSmells.Web.Posts.PostDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Post Details</h3>
    <asp:DetailsView 
        ID="PostDetailsView" 
        runat="server" 
        ItemType="CodeSmells.Models.Post" 
        DataKeyNames="PostId"
        SelectMethod="GetPostById"
        AutoGenerateRows="false" 
        class="table table-striped table-hover">
        <Fields>
            <asp:BoundField HeaderText="Category" DataField="Category"/>            
            <asp:BoundField HeaderText="Title" DataField="Title"/>
            <asp:BoundField HeaderText="Author" DataField="Author.UserName"/>
            <asp:BoundField HeaderText="Code" DataField="Body"/>
            <asp:BoundField HeaderText="Rating" DataField="Rating"/>
        </Fields>
    </asp:DetailsView>
    <div class="form-horizontal">
        <asp:Button runat="server" ID="UpVoteButton" Text="Up Vote" OnClick="UpVoteButton_Click"/>
        <asp:Button runat="server" ID="DownVoteButton" Text="Down Vote" OnClick="DownVoteButton_Click"/>
    </div>
    <h3>Comments</h3>
    <asp:Repeater runat="server" ID="CommentsRepeater" ItemType="CodeSmells.Models.Comment">
        <ItemTemplate>
            <div class="container">
                <div class="well">
                    <h4><%#: Item.Author.UserName %> says:</h4>
                    <p>
                        <%#: Item.Body %>
                    </p>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <h2>Add Comment</h2>
    <div class="form-horizontal">
        <asp:FormView runat="server" ID="CreateCommentFormView" 
        ItemType="CodeSmells.Models.Comment" 
        DataKeyNames="CommentId"
        InsertMethod="AddComment"
        OnItemInserted="AddCommentForm_ItemInserted" 
        DefaultMode="Insert">
            <InsertItemTemplate>
                <div class="form-group">
                    <div class="col-md-10">
                        <asp:TextBox runat="server" Width="650" Height="300" ID="BodyTextBox" TextMode="MultiLine" CssClass="form-control" Text="<%#: BindItem.Body %>"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-offset-2 col-md-10">
                        <asp:Button runat="server" Text="Add Comment" CommandName="Insert" CssClass="btn btn-default" />
                        <asp:Button runat="server" Text="Cancel" CausesValidation="false" OnClick="CancelButton_Click" CssClass="btn btn-default" />
                    </div>
                </div>
            </InsertItemTemplate>
        </asp:FormView>
    </div>
</asp:Content>
