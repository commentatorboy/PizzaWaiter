<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/Global.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="WebClient.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Profile</h1>
    <p>
        <asp:Label ID="lblRank" runat="server" Text="Rank"></asp:Label></p>
    <div id="divFavorites">
        <h2>Favorites</h2>
        <asp:CheckBoxList ID="cblFavorites" runat="server"></asp:CheckBoxList>
        <%-- <asp:Repeater ID="Repeater1" runat="server"></asp:Repeater> --%>
        <asp:Button ID="btnOrderFavorites" runat="server" Text="Order" />
        <p><asp:Literal ID="ltOrderFavoritesErrorMessage" runat="server"></asp:Literal></p> 
        <h2>Discount card</h2>
        <asp:Image ID="imgDiscountCard" runat="server" Width =" 300" Height="150" />
        <p>Prepaid card: 
            <asp:Literal ID="ltPrepaidAmount" runat="server"></asp:Literal></p>
    </div>
    <div id="divInfo">
        <h2>User information</h2>
        <p>Phone: 
            <asp:Literal ID="ltPhone" runat="server"></asp:Literal>
            <asp:TextBox ID="tbPhone" runat="server" Visible="False"></asp:TextBox>
            <asp:Button ID="btnEditPhone" runat="server" Text="Change" />
            <asp:Button ID="btnSavePhone" runat="server" Text="Save" Visible="False" />
        </p>
        
        <asp:Repeater ID="rptAddresses" runat="server">
            <ItemTemplate>
                <asp:Literal ID="ltUserAddress" runat="server" Text='<%#Eval("UserAddress") %>'>'></asp:Literal>
                <asp:ImageButton ID="ibtnDeleteAddress" runat="server" ImageUrl="~/Resources/button_delete_01.png" Width="24" Height="24" />
            </ItemTemplate>
            <SeparatorTemplate><br /></SeparatorTemplate>
        </asp:Repeater>
        <br />
        <asp:TextBox ID="tbAddress" runat="server" Visible="False"></asp:TextBox>
        <asp:Button ID="btnAddAddress" runat="server" Text="New" />
        <asp:Button ID="btnCancelAddAdddress" runat="server" Text="Cancel" Visible="False" />
        <asp:Button ID="btnSaveAddress" runat="server" Text="Save" Visible="False" />
    </div>
</asp:Content>
