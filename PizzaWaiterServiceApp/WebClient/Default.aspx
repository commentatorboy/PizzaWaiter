<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/Global.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebClient.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul>
        <asp:Repeater ID="rptRestaurants" runat="server">
            <HeaderTemplate>Pick restaurant:</HeaderTemplate>
            <ItemTemplate>
                <li>

                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("ID", "~/ShowMenu.aspx?ID={0}") %>'>
                        <asp:Literal ID="Literal1" runat="server" Text='<%#Eval("Name") %>'></asp:Literal>
                    </asp:HyperLink>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</asp:Content>
