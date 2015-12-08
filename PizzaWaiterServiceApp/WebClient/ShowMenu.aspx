﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/Global.Master" AutoEventWireup="true" CodeBehind="ShowMenu.aspx.cs" Inherits="WebClient.ShowMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Literal ID="ltTest" runat="server"></asp:Literal>
    <asp:Literal ID="ltRestaurantTitle" runat="server"></asp:Literal>
    <div>
        <ul>
            <asp:Repeater ID="rptMenu" runat="server">
                <HeaderTemplate>
                    <h1>Menu</h1>
                </HeaderTemplate>
                <ItemTemplate>
                    <li>


                        <%-- <asp:Literal ID="ltMenuTitle" runat="server" Text='<%#Eval("Title") %>'></asp:Literal>--%>
                        <asp:Literal ID="ltMenuTitle" runat="server"
                            Text='<%#FormatMenuTitle((WebClient.PizzaWaiterTestServiceReference.Menu)Eval("Menu")) %>'></asp:Literal>

                        <ul>
                            <asp:Repeater ID="rptDish" runat="server" DataSource='<%#GetDishes(Container.DataItem)%>'>

                                <ItemTemplate>
                                    <li>
                                        <asp:LinkButton ID="addToOrder" CommandName='addToOrder' CommandArgument='<%#Eval("ID") %>' OnCommand="blAddToOrder" runat="server">

                                            <asp:Literal ID="ltDishName" runat="server" Text='<%#String.Format("{0}.{1} - {2}", Eval("Number"), Eval("Name"),Eval("Price")) %>'></asp:Literal>

                                            <asp:Repeater ID="rptIngredient" runat="server" DataSource='<%#GetIngredients(Container.DataItem)%>'>

                                                <ItemTemplate>

                                                    <asp:Literal ID="Literal2" runat="server" Text='<%#FormatIngredientName((WebClient.PizzaWaiterTestServiceReference.Ingredient)Eval("Ingredient")) %>'></asp:Literal>

                                                </ItemTemplate>
                                                <HeaderTemplate>(</HeaderTemplate>
                                                <SeparatorTemplate>, </SeparatorTemplate>
                                                <FooterTemplate>)</FooterTemplate>

                                            </asp:Repeater>
                                        </asp:LinkButton>
                                    </li>


                                </ItemTemplate>


                            </asp:Repeater>
                        </ul>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div>
        <h1>Order</h1>
        <ul>
            <asp:Repeater ID="rptOrder" runat="server">
                <ItemTemplate>
                    <li>
                        <asp:LinkButton ID="lbRemoveFromOrder" CommandName='<%#GetDishIngredientsFromPartOrder(Container.DataItem) %>'
                            CommandArgument='<%#GetDishIdFromPartOrder(Container.DataItem) %>' OnCommand="removeDishFromOrder" runat="server">
                            <asp:Literal runat="server" ID="ltPartOrder" Text='<%#FormatPartOrder(Container.DataItem) %>'></asp:Literal>
                        </asp:LinkButton>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                        <asp:Literal runat="server" ID="ltPrice" Text='<%#string.Format("Total: {0}",CalculatePrice()) %>'></asp:Literal>

                </FooterTemplate>
            </asp:Repeater>

        </ul>
    </div>
    <asp:Literal runat="server" ID="ltConfirmation" />

    <div>
        <asp:Literal ID="ltPhoneNr" runat="server" Text="Phone Number:" />
        <asp:TextBox ID="txtPhoneNr" runat="server" />
    </div>
    <div>
        <asp:Literal ID="ltAddress" runat="server" Text="Address" />
        <asp:TextBox ID="txtAddress" runat="server" />
    </div>
    <asp:Button ID="btnSubmitOrder" runat="server" Text="Submit Order" OnClick="btnSubmitOrder_Click" />

</asp:Content>
