<%@ Page Language="C#" MasterPageFile="~/VMSReports/ReportMaster.Master" 
    EnableEventValidation="false"
    AutoEventWireup="true" 
    CodeBehind="InventoryReportJS.aspx.cs" 
    Inherits="METAOPTION.Reports.InventoryReportJS" %>

<asp:Content ID="cphAfterSales" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <fieldset class="ForFieldSet">
        <legend class="ForLegend">Inventory Report (JS)</legend>        
            <asp:UpdatePanel ID="upnlInventoryReportJS" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                <table border="0" cellspacing="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                    <tr>
                        
                        <td colspan="4" align="center"> 
                    
                            <asp:Label ID="lblerrmsg" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorderB">
                            Start Date
                        </td>
                        <td class="TableBorder">
                            <asp:TextBox ID="txtStartDate" runat="server" onkeydown="return false;" CssClass="date" />
                            <asp:Image ID="imgStartDate" runat="server" ImageUrl="~/Images/calender-icon.gif" />                    
                            <ajax:CalendarExtender ID="Calendarextender1" runat="server" PopupButtonID="imgStartDate"
                                TargetControlID="txtStartDate" />
                            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ErrorMessage="*" ControlToValidate="txtStartDate" />                   
                        </td>
                        <td class="TableBorderB">
                            End Date
                        </td>
                        <td class="TableBorder">
                            <asp:TextBox ID="txtEndDate" runat="server" onkeydown="return false;" CssClass="date" />
                            <asp:Image ID="imgEndDate" runat="server" ImageUrl="~/Images/calender-icon.gif" />                   
                            <ajax:CalendarExtender 
                                ID="txtEndDate_CalendarExtender" 
                                runat="server" 
                                PopupButtonID="imgEndDate"
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
                            <asp:DropDownList ID="ddlMake" runat="server" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged"
                                AutoPostBack="True" Width="168px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorderB">
                            Model
                        </td>
                        <td class="TableBorder">                            
                            <asp:DropDownList ID="ddlModel" runat="server" Width="168px">
                                <asp:ListItem Selected="True" Value="-1">NIL</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="TableBorderB">
                            <asp:UpdateProgress ID="uprogInventoryReportJS" runat="server" AssociatedUpdatePanelID="upnlInventoryReportJS">
                                <ProgressTemplate>
                                    <img src="../Images/Wait.gif" alt="" style="border:none; margin:0; padding:0" />Please Wait...
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                        <td class="TableBorder">
                            <asp:Button ID="btnViewReportAfterSales" runat="server" CssClass="btn" 
                                OnClick="btnViewReport_Click" Text="View Report" />
                        </td>
                    </tr>  
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
</asp:Content>
