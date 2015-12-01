<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/Global.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebClient.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <ul>
        <asp:Repeater ID="rptPath" runat="server">
        <ItemTemplate>
<%--        <li><asp:Literal ID="Literal1" runat="server" Text='<%#Eval("Name") %>'></asp:Literal></li>--%>
        <%--<li><asp:Literal ID="Literal2" runat="server" Text='<%#Eval("Name","Name: {0}") %>'></asp:Literal></li>--%>
<%--            <li><asp:Literal ID="Literal3" runat="server" Text='<%#Format((ElectricCarsWebClient.ElectricCarsUserServiceReference.Location)(Eval("Location")), (bool)Eval("IsStation")) %>'></asp:Literal></li>--%>
        </ItemTemplate>
        </asp:Repeater>
    </ul>
</asp:Content>
