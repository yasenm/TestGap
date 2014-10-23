<%@ Page Title="Manage Account" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="CodeSmells.Web.Account.Manage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Title %>.</h2>

    <div>
        <asp:PlaceHolder runat="server" ID="successMessage" Visible="false" ViewStateMode="Disabled">
            <p class="text-success"><%: this.SuccessMessage %></p>
        </asp:PlaceHolder>
    </div>

    <div class="row">
        <div class="container-fluid">
            <asp:Image CssClass="img-rounded" Width="125" 
                AlternateText="Profile Picture" runat="server" ID="ImgProfileImage"/>
        </div>
        <div class="col-md-12">
            <div class="form-horizontal">
                <h4>Manage your profile information</h4>
                <hr />
                <div class="form-group">
                    <asp:Label Text="Username" runat="server" />
                    <asp:TextBox ID="TbUserName" runat="server" CssClass="form-control" />
                </div>

                <div class="form-group">
                    <asp:Label Text="Email" runat="server" />
                    <asp:TextBox ID="TbEmail" runat="server" CssClass="form-control" />
                </div>
                <br />
                <asp:LinkButton ID="LinkBtnSaveUser" runat="server" CssClass="btn btn-success"
                    Text="Save Changes" OnClick="LinkBtnSaveUser_Click" />
                <br />
            </div>
        </div>
        <div class="col-md-12">
            <div class="form-horizontal">
                <h4>Change your account settings</h4>
                <hr />
                <dl class="dl-horizontal">
                    <dt>Password</dt>
                    <dd>
                        <asp:HyperLink NavigateUrl="/Account/ManagePassword" Text="[Change]" Visible="false" ID="ChangePassword" runat="server" />
                        <asp:HyperLink NavigateUrl="/Account/ManagePassword" Text="[Create]" Visible="false" ID="CreatePassword" runat="server" />
                    </dd>
                </dl>
            </div>
        </div>
        <div class="col-md-12">
            <div class="form-horizontal">
                <h4>Change your profile image</h4>
                <hr />
                <dl class="dl-horizontal">
                    <dt>Image</dt>
                    <dd>
                        <asp:HyperLink NavigateUrl="/Account/ProfileImage" Text="[Change]" Visible="true" ID="ChangeProfileImage" runat="server" />
                    </dd>
                </dl>
            </div>
        </div>
    </div>

</asp:Content>
