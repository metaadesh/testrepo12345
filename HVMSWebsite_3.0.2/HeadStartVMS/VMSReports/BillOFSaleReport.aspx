<%@ Page Language="C#" MasterPageFile="~/VMSReports/ReportMaster.Master"
AutoEventWireup="true" CodeBehind="BillOFSaleReport.aspx.cs" Inherits="METAOPTION.Reports.BillOFSaleReport" %>

<asp:Content ID="cphBillOfSale" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
   <fieldset class="ForFieldSet">
      <legend class="ForLegend">Bill OF Sale Report</legend>
      <table border="0" cellspacing="0" cellpadding="0" width="100%" style="border-collapse: collapse">
  
           <tr>
            <td class="TableBorderB">
               Inventory Id
            </td>
            <td class="TableBorder">
               <asp:UpdatePanel ID="upnlInventory" runat="server" UpdateMode="Conditional">
                  <ContentTemplate>
                     <asp:DropDownList ID="ddlInventory" runat="server" Width="125px">
                        <asp:ListItem Selected="True" Value="-1">NIL</asp:ListItem>
                     </asp:DropDownList>
                  </ContentTemplate>
                  <Triggers>
                     <asp:AsyncPostBackTrigger ControlID="ddlInventory" />
                  </Triggers>
               </asp:UpdatePanel>
               </td>    
               <td colspan=2>
                <asp:Button ID="btnViewReportBillOfSale"
                     runat="server" 
                     CssClass="btn" 
                     OnClick="btnViewReport_Click" 
                     Text="View Report" />
            </td>
         </tr>
      </table>
   </fieldset>
</asp:Content>
