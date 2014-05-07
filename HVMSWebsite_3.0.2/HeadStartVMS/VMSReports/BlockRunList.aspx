<%@ Page Language="C#"  EnableEventValidation="false"
AutoEventWireup="true" CodeBehind="BlockRunList.aspx.cs" Inherits="METAOPTION.Reports.BlockRunList" %>

<asp:Content ID="cphBlockRunListReport" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <fieldset class="ForFieldSet">
        <legend class="ForLegend">Block Run List Report</legend>
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
                    Color Rows
                </td>
                <td class="TableBorder">
                    <asp:DropDownList ID="ddlColor" runat="server">
                        <asp:ListItem Value="1">Color</asp:ListItem>
                        <asp:ListItem Value="0">No-Color</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>            
            <tr>            
                <td class="TableBorderB">
                    Regular Lane #
                </td>
                <td class="TableBorder">
                    <asp:TextBox ID="txtlane" runat="server" MaxLength ="10" Height="15px" 
                    Width="110px"></asp:TextBox>        
                </td>
                <td class="TableBorderB">
                    Sale Day
                </td>
                <td class="TableBorder">           
                    <asp:DropDownList ID="ddlSale" runat="server">
                        <asp:ListItem Value="0">Friday Sale</asp:ListItem>
                        <asp:ListItem Value="1">Exotic Sale</asp:ListItem>
                    </asp:DropDownList>
                </td>        
            </tr>          
            <tr>
                <td class="TableBorderB">
                    Buyer
                </td>
                <td class="TableBorder">
                   <asp:UpdatePanel ID="updPnlModel" runat="server" UpdateMode="Conditional">
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
                    <asp:Button ID="btnViewReportBlockList" runat="server" 
                        CssClass="btn" OnClick="btnViewReport_Click" Text="View Report" />                                                
                </td>             
            </tr>
        </table>
    </fieldset>
</asp:Content>


