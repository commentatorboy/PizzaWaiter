<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/Global.Master" AutoEventWireup="true" CodeBehind="ShowMenu.aspx.cs" Inherits="WebClient.ShowMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="Wrapper">
        <div class="MenuCard">

            <h1>
                <asp:Literal ID="ltRestaurantTitle" runat="server"></asp:Literal></h1>
            <div class="DashedBorder">
                <ul>
                    <asp:Repeater ID="rptMenu" runat="server">

                        <ItemTemplate>
                            <li>


                                <%-- <asp:Literal ID="ltMenuTitle" runat="server" Text='<%#Eval("Title") %>'></asp:Literal>--%>
                                <span class="MenuTitle">
                                    <asp:Literal ID="ltMenuTitle" runat="server"
                                        Text='<%#FormatMenuTitle((WebClient.PizzaWaiterTestServiceReference.Menu)Eval("Menu")) %>'></asp:Literal>
                                </span>


                                <asp:Repeater ID="rptDish" runat="server" DataSource='<%#GetDishes(Container.DataItem)%>'>

                                    <HeaderTemplate>
                                        <table class="Favorite">
                                    </HeaderTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="addToOrder" CommandName='addToOrder' CommandArgument='<%#Eval("ID") %>' OnCommand="blAddToOrder" runat="server">

                                                    <asp:Literal ID="ltDishName" runat="server" Text='<%#String.Format("{0}.{1} - {2} kr.", Eval("Number"), Eval("Name"),Eval("Price")) %>'></asp:Literal>
                                                    <br />
                                                    <asp:Repeater ID="rptIngredient" runat="server" DataSource='<%#GetIngredients(Container.DataItem)%>'>

                                                        <ItemTemplate>

                                                            <asp:Literal ID="Literal2" runat="server" Text='<%#FormatIngredientName((WebClient.PizzaWaiterTestServiceReference.Ingredient)Eval("Ingredient")) %>'></asp:Literal>

                                                        </ItemTemplate>
                                                        <HeaderTemplate>(</HeaderTemplate>
                                                        <SeparatorTemplate>, </SeparatorTemplate>
                                                        <FooterTemplate>)</FooterTemplate>

                                                    </asp:Repeater>

                                                </asp:LinkButton>

                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ibAddToFavorites" runat="server" ImageUrl="~/Resources/FavoritesButton.png" OnCommand="ibAddToFavorites_Command"
                                                    CommandArgument='<%#Eval("ID") %>' Visible='<%#ShowFavoriteButton(Eval("ID")) %>' />
                                            </td>

                                        </tr>
                                    </ItemTemplate>


                                </asp:Repeater>

                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <div class="Order">
            <h1>Order</h1>
            <div class="DashedBorder">
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
        </div>
        <asp:Literal runat="server" ID="ltConfirmation" />

        <div class="SubmitOrder">
            <h1>Submit Order</h1>
            <div class="DashedBorder">
                <div class="PhoneNr">
                    <p>
                        <asp:Literal ID="ltPhoneNr" runat="server" Text="Phone Number:" />
                    </p>
                    <p>
                        <asp:TextBox ID="txtPhoneNr" runat="server" />
                    </p>
                </div>
                <div class="Address">
                    <p>
                        <asp:Literal ID="ltAddress" runat="server" Text="Address:" />
                    </p>
                    <p>
                        <asp:DropDownList ID="ddlAllAddresses" runat="server" AppendDataBoundItems="true" Visible="false" Enabled="false"></asp:DropDownList>
                        </p>
                    <p>
                        <asp:TextBox ID="txtAddress" runat="server" AutoPostBack="true" OnTextChanged="txtAddress_TextChanged" />
                        
                    </p>
                </div>
                <div class="SubmitOrderButton">

                    <asp:Button ID="btnSubmitOrder" runat="server" Text="Submit Order" OnClick="btnSubmitOrder_Click" />
                    <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
