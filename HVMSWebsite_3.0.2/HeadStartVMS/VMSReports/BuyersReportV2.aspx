<%@ Page Language="C#" EnableEventValidation="false"
AutoEventWireup="true" CodeBehind="BuyersReportV2.aspx.cs" Inherits="METAOPTION.Reports.BuyersReportV2" %>


<asp:Content ID="cphBuyersReport" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <fieldset class="ForFieldSet">
      <legend class="ForLegend">Buyer's Report V2.0</legend>
      <table border="0" cellspacing="0" cellpadding="0" width="100%" style="border-collapse: collapse">
        <tr>
            <td colspan="4" align="center"> 
               <asp:Label ID="lblerrmesg" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
             </td>
        </tr>
        <tr>            
            <td colspan="4" align="center"> 
                <asp:LinkButton ID="lblLinkButton" runat="server" Text="Click Here To Continue" 
                   CssClass="lnktxt" Visible="false" onclick="lblLinkButton_Click"></asp:LinkButton>
            </td>
        </tr>
            
        <tr>
            <td class="TableBorderB" style="width:100px">
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
            <td class="TableBorderB" style="width:100px">
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
               <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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
            <td class="TableBorderB" style="width:100px">
               Model
            </td>
            <td class="TableBorder">
               <asp:UpdatePanel ID="updPnlMake" runat="server" UpdateMode="Conditional">
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
               Buyer
            </td>
            <td class="TableBorder">
               <asp:UpdatePanel ID="updPnlModel" runat="server" UpdateMode="Conditional">
                  <ContentTemplate>
                     <asp:DropDownList ID="ddlBuyers" runat="server" Width="168px" 
                          AutoPostBack="true" onselectedindexchanged="ddlBuyers_SelectedIndexChanged" />                     
                  </ContentTemplate>
                  <Triggers>
                     <asp:AsyncPostBackTrigger ControlID="ddlMake" />
                     <asp:AsyncPostBackTrigger ControlID="ddlYear" />
                  </Triggers>
               </asp:UpdatePanel>
            </td>
            
         </tr>
         <tr>
             <td class="TableBorderB" style="width:100px; vertical-align:top">
               Purchased From
            </td>   
             <td class="TableBorder" colspan="3" >
                 <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                  <ContentTemplate>
                        <div style="max-height:350px; overflow:auto">
                            <asp:CheckBoxList ID="chklstDealer" runat="server" RepeatDirection="Vertical" />
                        </div>                  
                  </ContentTemplate>
                  <Triggers>
                     <asp:AsyncPostBackTrigger ControlID="ddlBuyers" />
                  </Triggers>
               </asp:UpdatePanel>
             </td>
           
            
         </tr>
         <tr>
            <td class="TableBorderB" style="width:100px">
                Sold Status
            </td>
            <td class="TableBorder">
                <asp:DropDownList ID="ddlSoldStatus" runat="server">
                    <asp:ListItem Value="-1">ALL</asp:ListItem>
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="2">Sold not paid</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td colspan="2" class="TableBorder">
                <asp:Button ID="btnViewReportBuyer"
                     runat="server" 
                     CssClass="btn" 
                     OnClick="btnViewReport_Click" 
                     Text="View Report" />            
            </td>
         </tr>
        </table>
   </fieldset>
</asp:Content>
