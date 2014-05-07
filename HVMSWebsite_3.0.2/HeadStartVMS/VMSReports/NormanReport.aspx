<%@ Page Language="C#"  MasterPageFile="~/VMSReports/ReportMaster.Master"  AutoEventWireup="true" CodeBehind="NormanReport.aspx.cs" Inherits="METAOPTION.Reports.NormanReport" %>

<asp:Content ID="accReportContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <fieldset class="ForFieldSet">
        <legend class="ForLegend">Norman Report</legend>
        <table border="0" cellspacing="0" cellpadding="0" width="100%" style="border-collapse: collapse">
            <tr>
                <td colspan="3" align="center" style="color:Red; text-align:left">             
                    <asp:Literal ID="ltErr" runat="server" EnableViewState="false" />               
                </td>
            </tr>
            <tr>
                <td class="TableBorderB">
                    Sale Date
                </td>
                <td class="TableBorder">
                    <asp:TextBox ID="txtDate" runat="server" 
                        onkeydown="return false;" 
                        CssClass="date" />
                    <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" 
                        ErrorMessage="*"  
                        Display="Dynamic"
                        ControlToValidate="txtDate" />  
                    <img id="imgDate" 
                        style="vertical-align:middle;" 
                        src="../Images/calender-icon.gif" />                    
                    <ajax:CalendarExtender ID="Calendarextender1" runat="server" 
                        PopupButtonID="imgDate"
                        TargetControlID="txtDate" />                 
                </td>                               
                <td  class="TableBorderB" style="text-align:right; padding-right:70px;">
                    <asp:Button ID="btnViewReport" runat="server" 
                        CssClass="btn"
                        Text="View Report" onclick="btnViewReport_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>