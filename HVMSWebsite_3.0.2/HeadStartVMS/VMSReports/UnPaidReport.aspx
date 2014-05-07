<%@ Page Language="C#" AutoEventWireup="true"  
CodeBehind="UnPaidReport.aspx.cs" Inherits="METAOPTION.Reports.UnPaidReport" %>

<asp:Content ID="cphNoPaid" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <fieldset class="ForFieldSet">
      <legend class="ForLegend">Unpaid Report</legend>
      <table border="0" cellspacing="0" cellpadding="0" width="100%" style="border-collapse: collapse">
         <tr>
            <td class="TableBorderB">
               Start Date
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
             End Date
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
                  <ContentTemplate>
                     <asp:DropDownList ID="ddlMake" runat="server" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged"
                        AutoPostBack="True" Width="168px">
                     </asp:DropDownList>
                  </ContentTemplate>
                  <Triggers>
                     <asp:AsyncPostBackTrigger ControlID="ddlYear" />
                  </Triggers>
               </asp:UpdatePanel>
            </td>
         </tr>
         <tr>
            <td class="TableBorderB">
               Model
            </td>
            <td class="TableBorder">
               <asp:UpdatePanel ID="updPnlModel" runat="server" UpdateMode="Conditional">
                  <ContentTemplate>
                     <asp:DropDownList ID="ddlModel" runat="server" Width="168px">
                        <asp:ListItem Selected="True" Value="-1">NIL</asp:ListItem>
                     </asp:DropDownList>
                  </ContentTemplate>
                  <Triggers>
                     <asp:AsyncPostBackTrigger ControlID="ddlMake" />
                     <asp:AsyncPostBackTrigger ControlID="ddlYear" />
                  </Triggers>
               </asp:UpdatePanel>
            </td>
         
         <td class="TableBorderB">
               Car Status
            </td>
            <td class="TableBorder">
              
                     <asp:DropDownList ID="ddlCarStatus" runat="server">
                        <asp:ListItem Value="-1">ALL</asp:ListItem>
                        <asp:ListItem Value="1">Inventory</asp:ListItem>
                        <asp:ListItem Value="2">On-Hand</asp:ListItem>
                     </asp:DropDownList>
                 
            </td>
            </tr>
            <tr>
                
            <td class="TableBorderB">
               Buyer
            </td>
            <td class="TableBorder">
               <asp:UpdatePanel ID="updPnlBuyer" runat="server" UpdateMode="Conditional">
                  <ContentTemplate>
                     <asp:DropDownList ID="ddlBuyers" runat="server" Width="168px">
                     </asp:DropDownList>
                  </ContentTemplate>
                  <Triggers>
                     <asp:AsyncPostBackTrigger ControlID="ddlMake" />
                     <asp:AsyncPostBackTrigger ControlID="ddlYear" />
                  </Triggers>
               </asp:UpdatePanel>
            </td>
              <td class="TableBorderB" colspan="2" style="text-align: center">
                <asp:Button ID="btnViewReportNoPaid"
                     runat="server" 
                     CssClass="btn" 
                     OnClick="btnViewReport_Click" 
                     Text="View Report" />
             </td>
      
            
         </tr>
      </table>
   </fieldset>
</asp:Content>
