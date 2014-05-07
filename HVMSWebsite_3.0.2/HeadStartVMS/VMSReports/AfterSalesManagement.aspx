<%@ Page Language="C#" MasterPageFile="~/VMSReports/ReportMaster.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="AfterSalesManagement.aspx.cs" Inherits="METAOPTION.Reports.AfterSalesManagement" %>

<asp:Content ID="cphAfterSales" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <fieldset class="ForFieldSet">
        <legend class="ForLegend">After Sales Management Report</legend>            
        <asp:UpdatePanel ID="upnlCheck" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <table border="0" cellspacing="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                    <tr>
                        <td colspan="4" align="center"> 
                            <asp:Label ID="lblerrmesg" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorderB">
                            Sold Start Date
                        </td>
                        <td class="TableBorder">
                            <asp:TextBox ID="txtStartDate" runat="server" onkeydown="return false;" CssClass="date" />
                            <asp:Image ID="imgStartDate" runat="server" ImageUrl="~/Images/calender-icon.gif" />                    
                            <ajax:CalendarExtender ID="Calendarextender1" runat="server" PopupButtonID="imgStartDate"
                                TargetControlID="txtStartDate" />
                            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ErrorMessage="*" ControlToValidate="txtStartDate" />                   
                        </td>
                        <td class="TableBorderB">
                            Sold End Date
                        </td>
                        <td class="TableBorder">
                            <asp:TextBox ID="txtEndDate" runat="server" onkeydown="return false;" CssClass="date" />
                            <asp:Image ID="imgEndDate" runat="server" ImageUrl="~/Images/calender-icon.gif" />                   
                            <ajax:CalendarExtender 
                                ID="txtEndDate_CalendarExtender" 
                                runat="server" 
                                PopupButtonID="imgEndDate"
                                TargetControlID="txtEndDate" />
                            <asp:RequiredFieldValidator 
                                ID="RequiredFieldValidator1" 
                                runat="server" 
                                ErrorMessage="*"
                                ControlToValidate="txtEndDate" />
                          
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorderB">
                            Year
                        </td>
                        <td class="TableBorder">
                            <asp:DropDownList ID="ddlYear" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                AutoPostBack="true" Width="168px">
                            </asp:DropDownList>
                        </td>
                        <td class="TableBorderB">
                            Make
                        </td>
                        <td class="TableBorder" nowrap="nowrap">
                            <asp:UpdatePanel ID="updPnlMake" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlYear" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlMake" runat="server" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged"
                                        AutoPostBack="True" Width="168px">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorderB">
                            Model
                        </td>
                        <td class="TableBorder">
                            <asp:UpdatePanel ID="updPnlModel" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlMake" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlYear" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlModel" runat="server" Width="168px">
                                        <asp:ListItem Selected="True" Value="-1">NIL</asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="TableBorderB">
                            Check #
                        </td>
                        <td class="TableBorder">
                            <asp:TextBox ID="txtCheck" runat="server" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorderB">
                            Regular Lane #
                        </td>
                        <td class="TableBorder">
                            <asp:TextBox ID="txtlane" runat="server" MaxLength="10" Height="15px" Width="110px"></asp:TextBox>
                        </td>
                        <td class="TableBorderB">
                            Sale Day
                        </td>
                        <td class="TableBorder">
                            <asp:DropDownList ID="ddlSale" runat="server">
                                <asp:ListItem Value="0">Friday Sale</asp:ListItem>
                                <asp:ListItem Value="1">Exotic Sale</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                        <td colspan="2">
                            <asp:Button ID="btnViewReportAfterSales" runat="server" CssClass="btn" OnClick="btnViewReport_Click"
                                Text="View Report" />
                        </td>
                    </tr>
                </table>          
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlYear" />
                <asp:AsyncPostBackTrigger ControlID="ddlMake" />
                <asp:AsyncPostBackTrigger ControlID="ddlModel" />
            </Triggers>
        </asp:UpdatePanel>
    </fieldset>
</asp:Content>
