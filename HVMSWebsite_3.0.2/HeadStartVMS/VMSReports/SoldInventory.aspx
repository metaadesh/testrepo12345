<%@ Page Language="C#"  AutoEventWireup="true" MasterPageFile="~/VMSReports/ReportMaster.Master"
    CodeBehind="SoldInventory.aspx.cs" Inherits="METAOPTION.VMSReports.SoldInventory" EnableEventValidation="false" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="contSoldInventoryReport" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <fieldset class="ForFieldSet">
    <legend class="ForLegend">Sold Inventory</legend>
        <table border="0" cellspacing="0" cellpadding="0" width="100%" style="border-collapse: collapse">
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
                <td class="TableBorder" colspan="3">
                    <telerik:RadComboBox ID="ddlBuyers" Width="250px" runat="server" AllowCustomText="true"
                        EmptyMessage="" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" AutoPostBack="true"
                        OnItemChecked="ddlBuyers_ItemChecked" CheckedItemsTexts="DisplayAllInInput" >
                        
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="TableBorderB" style="width:100px; vertical-align:top">
                    Purchased From
                </td>   
                 <td class="TableBorder" colspan="3" >
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                          <ContentTemplate>
                                <div style="max-height:150px; overflow:auto">
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
                <td class="TableBorderB" style="width:100px; vertical-align:top">
                    Sold To
                </td>    
                <td class="TableBorder" colspan="3" >
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div style="max-height:150px; overflow:auto">
                                <asp:CheckBoxList ID="cblCustomer" runat="server" RepeatDirection="Vertical" />
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
                <td class="TableBorderB" style="width:100px">
                    Show Car Note
                </td>
                <td class="TableBorder">
                    <asp:CheckBox ID="cbCarNote" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="TableBorderB" style="width:100px">
                    Group By
                </td>
                <td class="TableBorder">
                    <asp:DropDownList ID="ddlGroupBy" runat="server">
                        <asp:ListItem Value="1" Text="Sold To Dealership" Selected="True" />
                        <asp:ListItem Value="2" Text="Buyer" />
                        <asp:ListItem Value="3" Text="Purchased From Dealership" />
                    </asp:DropDownList>
                </td>
                <td colspan="2" class="TableBorder" align="center">
                    <asp:Button ID="btnViewReportBuyer" runat="server" CssClass="btn" 
                        Text="View Report" OnClick="btnViewReport_Click" />            
                </td>
            </tr>
        </table>
    </fieldset>


</asp:Content>
