<%@ Page Language="C#" MasterPageFile="~/VMSReports/ReportMaster.Master"  EnableEventValidation="false"
AutoEventWireup="true" CodeBehind="HereNotHere.aspx.cs" Inherits="METAOPTION.Reports.HereNotHere" %>


<asp:Content ID="cphAfterSales" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <fieldset class="ForFieldSet">
        <legend class="ForLegend">Here/Not Here Report</legend>
        <table border="0" cellspacing="0" cellpadding="0" width="100%" style="border-collapse: collapse">
            
            <tr>
                <td class="TableBorderB">
                    Purchase Start Date
                </td>
                <td class="TableBorder">
                    <asp:TextBox ID="txtStartDate" runat="server" onkeydown="return false;" CssClass="date" />
                    <asp:Image ID="imgStartDate" runat="server" ImageUrl="~/Images/calender-icon.gif" />                    
                    <ajax:CalendarExtender ID="Calendarextender1" runat="server" PopupButtonID="imgStartDate"
                        TargetControlID="txtStartDate" />
                </td>
                <td class="TableBorderB">
                    Purchase End Date
                </td>
                <td class="TableBorder">
                    <asp:TextBox ID="txtEndDate" runat="server" onkeydown="return false;" CssClass="date" />
                    <asp:Image ID="imgEndDate" runat="server" ImageUrl="~/Images/calender-icon.gif" />                   
                    <ajax:CalendarExtender 
                        ID="txtEndDate_CalendarExtender" 
                        runat="server" 
                        PopupButtonID="imgEndDate"
                        TargetControlID="txtEndDate" />
                  
                </td>
            </tr>
                <tr>
            <td class="TableBorderB">
               Not Here Status
            </td>
            <td class="TableBorder">
               <asp:DropDownList ID="ddlNotHereStatus" runat="server"
                  Width="168px">
                  <asp:ListItem Text ="Not Here" Value="2" Selected="True" />
                  <asp:ListItem Text ="Here" Value="1" />
                </asp:DropDownList>
            </td>
            <td class="TableBorderB">
               Price
            </td>
            <td class="TableBorder" nowrap="nowrap">
                      
                     <asp:DropDownList ID="ddlPrice" runat="server" 
                         Width="168px">
                         <asp:ListItem Text ="Hide" Value="1" Selected="True" />
                         <asp:ListItem Text ="Unhide" Value="2" />
                     </asp:DropDownList>
                  
           
                      </td>
         </tr>
            
            <tr>
                <td colspan="2">
                </td>
                <td colspan="2">
                    <asp:Button ID="btnViewReportHereNotHere" runat="server" CssClass="btn" OnClick="btnViewReport_Click"
                        Text="View Report" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>