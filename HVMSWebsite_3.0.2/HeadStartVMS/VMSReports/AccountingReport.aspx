<%@ Page Language="C#" MasterPageFile="~/VMSReports/ReportMaster.Master" AutoEventWireup="true" CodeBehind="AccountingReport.aspx.cs" Inherits="METAOPTION.Reports.AccountingReport" %>
<asp:Content ID="accReportContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <fieldset class="ForFieldSet">
        <legend class="ForLegend">Accounting Report</legend>
        <table border="0" cellspacing="0" cellpadding="0" width="100%" style="border-collapse: collapse">
            <tr>
                <td colspan="4" align="center" style="color:Red;text-align:left;">             
                    <asp:Literal ID="ltErr" runat="server" EnableViewState="false" />               
                </td>
            </tr>
            <tr>
                <td class="TableBorderB">
                    From Date
                </td>
                <td class="TableBorder">
                    <asp:TextBox ID="txtFromDate" runat="server" 
                        onkeydown="return false;" 
                        CssClass="date" />
                    <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" 
                        ErrorMessage="*"  
                        Display="Dynamic"
                        ControlToValidate="txtFromDate" />  
                    <img id="imgFromDate" 
                        style="vertical-align:middle;" 
                        src="../Images/calender-icon.gif" />                    
                    <ajax:CalendarExtender ID="Calendarextender1" runat="server" 
                        PopupButtonID="imgFromDate"
                        TargetControlID="txtFromDate" />                 
                </td>
                <td class="TableBorderB">
                    To Date
                </td>
                <td class="TableBorder">
                    <asp:TextBox ID="txtToDate" runat="server" 
                        onkeydown="return false;" 
                        CssClass="date" />
                    <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator1" 
                        runat="server" 
                        Display="Dynamic"
                        ErrorMessage="*"
                        ControlToValidate="txtToDate" />
                    <img id="imgEndDate"
                        style="vertical-align:middle;"
                        src="../Images/calender-icon.gif" />                   
                    <ajax:CalendarExtender 
                        ID="txtToDate_CalendarExtender" 
                        runat="server" 
                        PopupButtonID="imgEndDate"
                        TargetControlID="txtToDate" />
                  
                </td>
            </tr>
             <tr>   
                <td colspan="3"class="TableBorderB">
                   Note:<span style="font-weight:normal;">
                        Report will take time to render if the date range is more than one year.
                        </span>
                </td>          
                <td class="TableBorderB" style="text-align:right; padding-right:70px;">
                    <asp:Button ID="btnViewReport" runat="server" 
                        CssClass="btn"
                        Text="View Report" onclick="btnViewReport_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>