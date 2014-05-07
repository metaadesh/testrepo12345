<%@ Page Language="C#" EnableEventValidation="false"
AutoEventWireup="true" CodeBehind="NoTitlesReport.aspx.cs" Inherits="METAOPTION.Reports.NoTitlesReport" %>

<asp:Content ID="cphNoTitlesReport" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <fieldset class="ForFieldSet">
      <legend class="ForLegend">No Titles Report</legend>
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
            </tr>
             <tr>
            <td colspan="2" >
                
                </td>
            <td  colspan="2" >
                <asp:Button ID="btnViewReportTitles"
                     runat="server" 
                     CssClass="btn" 
                     OnClick="btnViewReport_Click" 
                     Text="View Report" />
             </td>
    
         </tr>
      </table>
   </fieldset>
</asp:Content>
