<%@ Page Language="C#" MasterPageFile="~/VMSReports/ReportMaster.Master"  EnableEventValidation="false"
AutoEventWireup="true" CodeBehind="ViewAllSoldCars.aspx.cs" Inherits="METAOPTION.Reports.ViewAllSoldCars" %>

<asp:Content ID="cphViewAllSoldCars" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
   <fieldset class="ForFieldSet">
      <legend class="ForLegend">View All Sold Cars</legend>
      <table border="0" cellspacing="0" cellpadding="0" width="100%" style="border-collapse: collapse">
         <tr>
             
            <td class="TableBorderB">
               Sold Start Date
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
               Sold End Date
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
               <asp:UpdatePanel ID="upnlMake" runat="server" UpdateMode="Conditional">
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
               <asp:UpdatePanel ID="upnlModel" runat="server" UpdateMode="Conditional">
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
               Dealer
            </td>
            <td class="TableBorder">
               <asp:UpdatePanel ID="upnlDealer" runat="server" UpdateMode="Conditional">
                  <ContentTemplate>
                     <asp:DropDownList ID="ddlDealer" runat="server" Width="168px">
                     <asp:ListItem Selected="True" Value="-1">ALL</asp:ListItem>
                     </asp:DropDownList>
                  </ContentTemplate>
                  <Triggers>
                     <asp:AsyncPostBackTrigger ControlID="ddlMake" />
                     <asp:AsyncPostBackTrigger ControlID="ddlYear" />
                  </Triggers>
               </asp:UpdatePanel>
            </td>
         </tr>
               <tr>
            <td colspan="2" >
                
                </td>
            <td  colspan="2" >
                <asp:Button ID="btnViewReportSoldCars"
                     runat="server" 
                     CssClass="btn" 
                     OnClick="btnViewReport_Click" 
                     Text="View Report" />
             </td>
           
            
         </tr>
      </table>
   </fieldset>
</asp:Content>
