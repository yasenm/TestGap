<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreatePost.aspx.cs" Inherits="CodeSmells.Web.Posts.CreatePost" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >
    <h2>Create Post</h2>
    <div class="form-horizontal">
        <div class="form-group">
                <asp:Label runat="server" ID="Label1" Text="Category" CssClass="col-md-2 control-label"></asp:Label>
                <div class="col-md-10">
                    <asp:DropDownList runat="server" ID="CategoryDropDownList" CssClass="form-control" >
                    </asp:DropDownList>
                </div>
        </div>
        <asp:FormView runat="server" ID="CreatePostFormView" 
        ItemType="CodeSmells.Models.Post" 
        DataKeyNames="PostId"
        InsertMethod="AddPost"
        OnItemInserted="AddPostForm_ItemInserted" 
        DefaultMode="Insert">
        <InsertItemTemplate>
            <div class="form-group">
                <asp:Label runat="server" ID="TitleLable" Text="Title" CssClass="col-md-2 control-label"></asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="TitleTextBox" CssClass="form-control" Text="<%#: BindItem.Title %>"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" ID="BodyLabel" Text="Code" CssClass="col-md-2 control-label"></asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" Width="650" Height="300" ID="BodyTextBox" TextMode="MultiLine" CssClass="form-control" Text="<%#: BindItem.Body %>"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <asp:Button runat="server" Text="Insert" CommandName="Insert" CssClass="btn btn-default" />
                    <asp:Button runat="server" Text="Cancel" CausesValidation="false" OnClick="CancelButton_Click" CssClass="btn btn-default" />
                </div>
            </div>
        </InsertItemTemplate>
    </asp:FormView>

    </div>
</asp:Content>
