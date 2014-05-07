<%@ Page Language="C#" MasterPageFile="~/VMSReports/ReportMaster.Master"  AutoEventWireup="true"  
CodeBehind="InventoryOnHandReport.aspx.cs" Inherits="METAOPTION.Reports.InventoryOnHandReport" %>

<asp:Content ID="cphInventoryOnHand" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <fieldset class="ForFieldSet">
      <legend class="ForLegend">Inventory On-Hand Report</legend>
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
            <td colspan="2" >
                
                </td>
            <td  colspan="2" >
                <asp:Button ID="btnViewReportInventory"
                     runat="server" 
                     CssClass="btn" 
                     OnClick="btnViewReport_Click" 
                     Text="View Report" />
             </td>
           
            
         </tr>
      </table>
   </fieldset>
</asp:Content>
