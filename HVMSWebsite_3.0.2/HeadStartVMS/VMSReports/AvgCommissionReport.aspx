<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="AvgCommissionReport.aspx.cs" Inherits="METAOPTION.VMSReports.AvgCommissionReport" %>
<asp:Content ID="cphAccount_Payable" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <fieldset class="ForFieldSet">
      <legend class="ForLegend">Average Commission Report</legend>
      <table border="0" cellspacing="0" cellpadding="0" width="100%" style="border-collapse: collapse">
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
            <td class="TableBorderB">
               Buyer
            </td>
            <td class="TableBorder">
                 <asp:DropDownList ID="ddlBuyers" runat="server" Width="168px">
                    <asp:ListItem Selected="True" Value="-1">NIL</asp:ListItem>
                 </asp:DropDownList>
            </td>          
            <td class="TableBorderB" colspan="2" style="text-align: center">
               <asp:Button ID="btnViewReportAccount" runat="server" Text="View Report" CssClass="btn" OnClick="btnViewReport_Click" />
            </td>
             
         </tr>
      </table>
   </fieldset>
</asp:Content>