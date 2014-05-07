<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/VMSReports/ReportMaster.Master"  EnableEventValidation="false"
CodeBehind="ExpenseReport.aspx.cs" Inherits="METAOPTION.Reports.ExpenseReport" %>

<asp:Content ID="cphExpense" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
   <fieldset class="ForFieldSet">
      <legend class="ForLegend">Expense Report</legend>
      <table border="0" cellspacing="0" cellpadding="0" width="100%" style="border-collapse: collapse">
         <tr>             
            <td class="TableBorderB">
              Expense Start Date
            </td>
            <td class="TableBorder">
               <asp:TextBox ID="txtStartDate" runat="server" onkeydown="return false;" CssClass="date" />
               <asp:Image ID="imgStartDate" runat="server" ImageUrl="~/Images/calender-icon.gif" />
               <ajax:TextBoxWatermarkExtender runat="server" ID="txtStartDate_TextBoxWatermarkExtender"
                  WatermarkText="Select Date" TargetControlID="txtStartDate" />
               <ajax:CalendarExtender ID="Calendarextender1" runat="server" PopupButtonID="imgStartDate"
                  TargetControlID="txtStartDate" />
                  
               <asp:RequiredFieldValidator 
                  ID="rfvStartDate" 
                  runat="server" 
                  ErrorMessage="*"
                  ControlToValidate="txtStartDate" />
                  
            </td>
            <td class="TableBorderB">
              Expense End Date
            </td>
            <td class="TableBorder">
               <asp:TextBox ID="txtEndDate" runat="server" onkeydown="return false;" CssClass="date" />
               <asp:Image ID="imgEndDate" runat="server" ImageUrl="~/Images/calender-icon.gif" />
               <ajax:TextBoxWatermarkExtender runat="server" ID="txtEndDate_TextBoxWatermarkExtender"
                  WatermarkText="Select Date" TargetControlID="txtEndDate" />
               <ajax:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" PopupButtonID="imgEndDate"
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
               Entity Type
            </td>
            <td class="TableBorder">
               <asp:DropDownList ID="ddlEntityType" runat="server" 
                  AutoPostBack="true" Width="168px" OnSelectedIndexChanged="ddlEntityType_SelectedIndexChanged">
               </asp:DropDownList>
            </td>
            <td class="TableBorderB">
               Entity Name
            </td>
            <td class="TableBorder">
                <asp:UpdatePanel ID="updEntityName" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                       <asp:DropDownList ID="ddlEntityName" runat="server" 
                          AutoPostBack="true" Width="168px">
                       </asp:DropDownList>
                    </ContentTemplate>
                  <Triggers>
                     <asp:AsyncPostBackTrigger ControlID="ddlEntityType" />
                  </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>            
        <tr>
            <td class="TableBorderB">
                Expense Type
            </td>
            <td class="TableBorder">
                <asp:DropDownList ID="ddlExpenseType" runat="server" 
                    AutoPostBack="true" Width="168px">
                </asp:DropDownList>
            </td>
            <td class="TableBorderB">
                Paid Status
            </td>
            <td class="TableBorder">
                <asp:UpdatePanel ID="upPnlChkPaidStatus" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlChkPaidStatus" runat="server">
                        <asp:ListItem Selected="true" Value="-1">NIL</asp:ListItem>
                        <asp:ListItem Value="1">Paid</asp:ListItem>
                        <asp:ListItem Value="0">Un-Paid</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="TableBorderB" colspan="4" style="text-align: center">
                <asp:Button ID="btnViewReportExpense" runat="server" Text="View Report" CssClass="btn" OnClick="btnViewReport_Click" />
            </td>        
        </tr>
      </table>
   </fieldset>
</asp:Content>