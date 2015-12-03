﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/Global.Master" AutoEventWireup="true" CodeBehind="ShowMenu.aspx.cs" Inherits="WebClient.ShowMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Literal ID="ltRestaurantTitle" runat="server"></asp:Literal>
    <ul>
        <asp:Repeater ID="rptMenu" runat="server">
            <HeaderTemplate>Pick restaurant:</HeaderTemplate>
            <ItemTemplate>
                <li>


                    <%-- <asp:Literal ID="ltMenuTitle" runat="server" Text='<%#Eval("Title") %>'></asp:Literal>--%>
                    <asp:Literal ID="ltMenuTitle" runat="server"
                        Text='<%#FormatMenuTitle((WebClient.PizzaWaiterTestServiceReference.Menu)Eval("Menu")) %>'></asp:Literal>

                    <ul>
                        <asp:Repeater ID="rptMenuItems" runat="server" DataSource='<%#GetDishes(Container.DataItem)%>'>

                            <ItemTemplate>
                                <li>
                                    <asp:Literal ID="Literal2" runat="server" Text='<%#Eval("Name") %>'></asp:Literal></li>
                                </li>
                            </ItemTemplate>

                        </asp:Repeater>
                     </ul>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</asp:Content>