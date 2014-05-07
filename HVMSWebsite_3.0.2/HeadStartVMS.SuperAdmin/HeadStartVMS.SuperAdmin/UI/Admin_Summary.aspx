<%@ Page Title="Admin Panel :: Org Summary" Language="C#" MasterPageFile="~/UI/Admin_Master.Master"
    AutoEventWireup="true" CodeBehind="Admin_Summary.aspx.cs" Inherits="METAOPTION.UI.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .OrangeText
        {
            font-size: 12px;
            color: #E68100;
            font-family: Arial, Helvetica, sans-serif;
        }
    </style>
    <div style="min-height: 450px; vertical-align: top;">
        <%--<fieldset class="ForFieldSet">
            <legend class="ForLegend" align="left">Organization Summary</legend>
            color: #CC7200;
        --%>
        <div>
            <table width="100%">
                <tr>
                    <td align="center">
                        <table width="90%" cellspacing="0" id="tblNoSummary" runat="server" visible="false">
                            <tr>
                                <td style="height: 25px;">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="gvEmpty" align="center">
                                    No Summary Found
                                </td>
                            </tr>
                        </table>
                        <table width="90%" cellspacing="0" id="tblSummary" runat="server" visible="true">
                            <tr>
                                <td colspan="2">
                                    <table width="100%">
                                        <tr>
                                            <td align="left" valign="middle">
                                                <asp:Label ID="lblMessage" runat="server" Text="Organization added successfully !"
                                                    CssClass="LeftPanelContentHeading" ForeColor="Green" Visible="false"></asp:Label>
                                            </td>
                                            <td align="right">
                                                <img border="0" src="../images/Summary.jpg" style="margin-left: 8px" alt="" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder" align="left" style="width: 180px;">
                                    <%-- <img border="0" src="../images/Buller-1.gif" style="margin-left: 8px" width="9" height="9"
                                            alt="" />
                                        &nbsp;--%>
                                    <asp:Label ID="Label1" runat="server" Text="Organization Name" CssClass="LeftPanelContentHeading"
                                        Style="margin-left: 15px;"></asp:Label>
                                </td>
                                <td class="TableBorder" align="left">
                                    <asp:Label ID="lblOrgName" runat="server" Text="" class="OrangeText"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder" align="left">
                                    <asp:Label ID="Label2" runat="server" Text="Organization Code" CssClass="LeftPanelContentHeading"
                                        Style="margin-left: 15px;"></asp:Label>
                                </td>
                                <td class="TableBorder" align="left">
                                    <asp:Label ID="lblOrgCode" runat="server" Text="" class="OrangeText"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder" align="left">
                                    <asp:Label ID="Label3" runat="server" Text="Organization Website" CssClass="LeftPanelContentHeading"
                                        Style="margin-left: 15px;"></asp:Label>
                                </td>
                                <td class="TableBorder" align="left">
                                    <asp:Label ID="lblOrgWebsite" runat="server" Text="" class="OrangeText"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder" align="left">
                                    <asp:Label ID="Label4" runat="server" Text="Organization Address" CssClass="LeftPanelContentHeading"
                                        Style="margin-left: 15px;"></asp:Label>
                                </td>
                                <td class="TableBorder" align="left">
                                    <asp:Label ID="lblOrgAddress" runat="server" Text="" class="OrangeText"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder" align="left">
                                    <asp:Label ID="Label5" runat="server" Text="Organization Phone" CssClass="LeftPanelContentHeading"
                                        Style="margin-left: 15px;"></asp:Label>
                                </td>
                                <td class="TableBorder" align="left">
                                    <asp:Label ID="lblOrgPhone" runat="server" Text="" class="OrangeText"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder" align="left">
                                    <asp:Label ID="Label6" runat="server" Text="Organization Fax" CssClass="LeftPanelContentHeading"
                                        Style="margin-left: 15px;"></asp:Label>
                                </td>
                                <td class="TableBorder" align="left">
                                    <asp:Label ID="lblOrgFax" runat="server" Text="" class="OrangeText"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder" align="left">
                                    <asp:Label ID="Label7" runat="server" Text="Organization Email" CssClass="LeftPanelContentHeading"
                                        Style="margin-left: 15px;"></asp:Label>
                                </td>
                                <td class="TableBorder" align="left">
                                    <asp:Label ID="lblOrgEmail" runat="server" Text="" class="OrangeText"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder" align="left">
                                    <asp:Label ID="Label8" runat="server" Text="System Name" CssClass="LeftPanelContentHeading"
                                        Style="margin-left: 15px;"></asp:Label>
                                </td>
                                <td class="TableBorder" align="left">
                                    <asp:Label ID="lblSystemName" runat="server" Text="" class="OrangeText"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder" align="left">
                                    <asp:Label ID="Label9" runat="server" Text="Admin Name" CssClass="LeftPanelContentHeading"
                                        Style="margin-left: 15px;"></asp:Label>
                                </td>
                                <td class="TableBorder" align="left">
                                    <asp:Label ID="lblAdminName" runat="server" Text="" class="OrangeText"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder" align="left">
                                    <asp:Label ID="Label11" runat="server" Text="Login User Name" CssClass="LeftPanelContentHeading"
                                        Style="margin-left: 15px;"></asp:Label>
                                </td>
                                <td class="TableBorder" align="left">
                                    <asp:Label ID="lblLoginUserName" runat="server" Text="" class="OrangeText"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder" align="left">
                                    <asp:Label ID="Label13" runat="server" Text="Login Password" CssClass="LeftPanelContentHeading"
                                        Style="margin-left: 15px;"></asp:Label>
                                </td>
                                <td class="TableBorder" align="left">
                                    <asp:Label ID="lblLoginPassword" runat="server" Text="" class="OrangeText"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder" align="left">
                                    <asp:Label ID="Label15" runat="server" Text="User's Default Groups" CssClass="LeftPanelContentHeading"
                                        Style="margin-left: 15px;"></asp:Label>
                                </td>
                                <td class="TableBorder" align="left">
                                    <div style="text-align: left; width: 100%;">
                                        <asp:Label ID="lblUserDefaultGroups" runat="server" Text="" class="OrangeText"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
        </div>
        <%--  </fieldset>--%>
    </div>
</asp:Content>
