<%@ Page Language="C#" MasterPageFile="~/VMSReports/ReportMaster.Master" EnableEventValidation="false" 
AutoEventWireup="true" CodeBehind="Account_Payable.aspx.cs" Inherits="METAOPTION.Reports.Account_Payable" %>

<asp:Content ID="cphAccount_Payable" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
   <fieldset class="ForFieldSet">
      <legend class="ForLegend">Account Payable Report</legend>
      <table border="0" cellspacing="0" cellpadding="0" width="100%" style="border-collapse: collapse">
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
          
            <td class="TableBorderB" colspan="2" style="text-align: center">
               <asp:Button ID="btnViewReportAccount" runat="server" Text="View Report" CssClass="btn" OnClick="btnViewReport_Click" />
            </td>
             
         </tr>
      </table>
   </fieldset>
</asp:Content>
