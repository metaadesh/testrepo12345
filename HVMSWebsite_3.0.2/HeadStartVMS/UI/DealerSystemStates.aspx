<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DealerSystemStates.aspx.cs" 
 MasterPageFile="~/UI/DealerMasterPage.Master"
Inherits="METAOPTION.UI.DealerSystemStates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="RightPanel">
        <div class="AddHeading">
            System Stats</div>
        <fieldset class="ForFieldSet">
            <legend class="ForLegend">List of system stats</legend>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr id="trUnpaidInventory" runat="server">
                    <td class="TableBorder">
                        <asp:Label ID="lblUnpaidTotal1" runat="server" />
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblUnpaidTotal11" runat="server" />
                        &nbsp;
                        <img border="0" src="../images/Pop-Up.gif" width="13" height="13" alt="" />
                    </td>
                </tr>
                <tr>
                    <td class="TableBorder" style="vertical-align: top;">
                        <asp:Label ID="lblUnpaidCount1" runat="server"></asp:Label>
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblUnpaidCount11" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TableBorder">
                        Vehicles in Regular Lane
                    </td>
                    <td class="TableBorder">
                        <asp:DataList ID="dListRegular" runat="server" RepeatColumns="8" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <span style="padding: 2px 10px 2px 0px; color: #535152; font-size: 12px;">
                                    <%# Eval("Lane") %>:<%# Eval("InventoryCount") %></span>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
                <tr>
                    <td class="TableBorder">
                        Vehicles in Exotic Lane
                    </td>
                    <td class="TableBorder">
                        <asp:DataList ID="dListExotic" runat="server" RepeatColumns="7" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <span style="padding: 2px 10px 2px 0px; color: #535152; font-size: 12px;">
                                    <%# Eval("Lane") %>:<%# Eval("InventoryCount") %></span>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
                <tr id="trInventoryDollers" runat="server">
                    <td class="TableBorder" style="width: 50%">
                        Total Inventory Dollars
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblTotalInventoryDollars" runat="server" />
                    </td>
                </tr>
                <tr id="trTotalVehiclesintheSystem" runat="server">
                    <td class="TableBorder">
                        Number of Vehicles in the System
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblTotalVehiclesintheSystem" runat="server" />
                    </td>
                </tr>
                <tr id="trAvgPerVehicle" runat="server">
                    <td class="TableBorder">
                        Average $ per Vehicle
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblAvgPerVehicle" runat="server" />
                    </td>
                </tr>
                <tr id="trNumberInventoryVehicles" runat="server">
                    <td class="TableBorder">
                        Number of Inventory Vehicles in the System
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblNumberInventoryVehicles" runat="server" />
                    </td>
                </tr>
                <tr id="trAveragePerInventoryVehicle" runat="server">
                    <td class="TableBorder">
                        Average $ per Inventory Vehicle
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblAveragePerInventoryVehicle" runat="server" />
                    </td>
                </tr>
                <tr id="trNumber_OnHand_Vehicles" runat="server">
                    <td class="TableBorder">
                        Number of On-Hand Vehicles in the System
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblNumber_OnHand_Vehicles" runat="server" />
                    </td>
                </tr>
                <tr id="trAverage_OnHand_Vehicles" runat="server">
                    <td class="TableBorder">
                        Average $ per On-Hand Vehicle
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblAverage_OnHand_Vehicles" runat="server" />
                    </td>
                </tr>
                <tr id="trNumber_Archived_Vehicles" runat="server">
                    <td class="TableBorder">
                        Number of Archived Vehicles in the System
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblNumber_Archived_Vehicles" runat="server" />
                    </td>
                </tr>
                <tr id="trAverage_Archived_Vehicles" runat="server">
                    <td class="TableBorder">
                        Average $ per Archived Vehicle
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblAverage_Archived_Vehicles" runat="server" />
                    </td>
                </tr>
                <tr id="trVehicle_Added_WTD" runat="server">
                    <td class="TableBorder">
                        Vehicles Added this week
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblVehicle_Added_WTD" runat="server" />
                    </td>
                </tr>
                <tr id="trVehicles_Added_MTD" runat="server">
                    <td class="TableBorder">
                        Vehicles Added MTD (Month to date)
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblVehicles_Added_MTD" runat="server" />
                    </td>
                </tr>
                <tr id="trVehicles_Added_YTD" runat="server">
                    <td class="TableBorder">
                        Vehicles Added YTD (Year to Date)
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblVehicles_Added_YTD" runat="server" />
                    </td>
                </tr>
                <tr id="trCarsNotPaidYet" runat="server">
                    <td class="TableBorder">
                        Number of Cars not paid yet
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblCarsNotPaidYet" runat="server" />
                    </td>
                </tr>
                <tr id="trAmountNotPaidYet" runat="server">
                    <td class="TableBorder">
                        Amount of Cars not paid yet
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblAmountNotPaidYet" runat="server" />
                    </td>
                </tr>
                <tr id="trNumber_OpenExpenses" runat="server">
                    <td class="TableBorder">
                        Number of Open Expenses
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblNumber_OpenExpenses" runat="server" />
                    </td>
                </tr>
                <tr id="trAmount_OpenExpenses" runat="server">
                    <td class="TableBorder">
                        Amount of Open Expenses
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblAmout_OpenExpenses" runat="server" />
                    </td>
                </tr>
                <tr id="trNumber_OpenCommissions" runat="server">
                    <td class="TableBorder">
                        Number of Open Commissions
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblNumber_OpenCommissions" runat="server" />
                    </td>
                </tr>
                <tr id="trAmount_OpenCommissions" runat="server">
                    <td class="TableBorder">
                        Amount of Open Commissions
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblAmount_OpenCommissions" runat="server" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
</asp:Content>
