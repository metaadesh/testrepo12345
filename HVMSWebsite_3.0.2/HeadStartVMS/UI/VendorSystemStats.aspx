<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="VendorSystemStats.aspx.cs"
    Inherits="METAOPTION.UI.VendorSystemStats" %>

<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="server">

<div class="RightPanel">
        <div class="AddHeading">System Stats</div>
        <fieldset class="ForFieldSet">
        <legend class="ForLegend">List of system stats</legend>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td class="TableBorder" style="width: 50%">
                        Number of Expense Vehicle Added  
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblTotalExpAdded" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="TableBorder">
                        Number of Pending Approval
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblPendingApproval" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="TableBorder">
                        Number of Location Capture
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblLocationCapture" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="TableBorder">
                        Number of VINScan Log
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblVinScanLog" runat="server" />
                    </td>
                </tr>
                 <tr>
                    <td class="TableBorder">
                        Number of PreExpense Images
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblPreExpImg" runat="server" />
                    </td>
                </tr>
                 <tr>
                    <td class="TableBorder">
                        Number of Generic Images
                    </td>
                    <td class="TableBorder">
                        <asp:Label ID="lblGenericImg" runat="server" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
</asp:content>
