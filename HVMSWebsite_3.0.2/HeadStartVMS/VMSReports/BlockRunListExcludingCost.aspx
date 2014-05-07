<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlockRunListExcludingCost.aspx.cs" 
    Inherits="METAOPTION.Reports.BlockRunListExcludingCost"
    Title="Block Run List Excluding Cost" %>
<asp:Content ID="blockrunMainCont" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <fieldset class="ForFieldSet">
    <legend class="ForLegend">Block Run List Excluding Cost Report</legend>
        <table border="0" cellspacing="0" cellpadding="0" width="100%" style="border-collapse: collapse">         
            <tr>
                <td class="TableBorderB">
                    From Date
                </td>
                <td class="TableBorder">
                    <asp:TextBox ID="txtFromDate" runat="server" 
                        onkeydown="return false;" 
                        CssClass="date" /> 
                    <img id="imgFromDate" 
                        style="vertical-align:middle;" 
                        src="../Images/calender-icon.gif" />                    
                    <ajax:CalendarExtender ID="Calendarextender1" runat="server" 
                        PopupButtonID="imgFromDate"
                        TargetControlID="txtFromDate" />  
                    <ajax:TextBoxWatermarkExtender ID="txtFromDatWatermark" runat="server"
                        WatermarkText="Select Date" 
                        TargetControlID="txtFromDate" />
                </td>
                <td class="TableBorderB">
                    To Date
                </td>
                <td class="TableBorder">
                    <asp:TextBox ID="txtToDate" runat="server" 
                        onkeydown="return false;" 
                        CssClass="date" />
                    <img id="imgEndDate"
                        style="vertical-align:middle;"
                        src="../Images/calender-icon.gif" />                   
                    <ajax:CalendarExtender 
                        ID="txtToDate_CalendarExtender" 
                        runat="server" 
                        PopupButtonID="imgEndDate"
                        TargetControlID="txtToDate" />
                    <ajax:TextBoxWatermarkExtender  ID="txtToDateWatermark" runat="server"
                        WatermarkText="Select Date" 
                        TargetControlID="txtToDate" />  
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
                            <asp:DropDownList ID="ddlMake" runat="server" 
                                OnSelectedIndexChanged="ddlMake_SelectedIndexChanged"
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
                        CssClass="btn" 
                        OnClick="btnViewReport_Click" 
                        Text="View Report" />   
                </td>             
            </tr>
        </table>
    </fieldset>
</asp:Content>