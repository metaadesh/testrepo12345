<%@ Page Language="C#"  AutoEventWireup="true" 
    CodeBehind="BuyerInventory.aspx.cs" 
    Inherits="METAOPTION.Reports.BuyerInventory" 
    Title="Buyer Inventories"%>

<asp:Content ID="blockrunMainCont" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <fieldset class="ForFieldSet">
        <legend class="ForLegend">Buyer Inventory Report</legend>
        <table border="0" cellspacing="0" cellpadding="0" width="100%" style="border-collapse: collapse">
            <tr>
                <td colspan="4" align="center" style="color:Red;text-align:left;">             
                    <asp:Literal ID="ltErr" runat="server" EnableViewState="false" />               
                </td>
            </tr>
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
                   Buyer
                </td>
                <td class="TableBorder">                 
                     <asp:DropDownList ID="ddlBuyer" runat="server"  />                    
                </td>
             </tr>
             <tr>
                 <td class="TableBorderB">
                    Sold Status
                 </td>         
                 <td class="TableBorder">
                    <asp:DropDownList ID="ddlSoldStatus" runat="server" >
                        <asp:ListItem  Value="-1" Text="ALL" />
                        <asp:ListItem Value="1" Text="Sold" />
                        <asp:ListItem Selected="True" Value="0" Text="Not Sold" />
                    </asp:DropDownList>
                 </td> 
                  <td class="TableBorderB">
                    Car Status
                 </td>         
                 <td class="TableBorder">
                    <asp:DropDownList ID="ddlCarStatus" runat="server" >
                        <asp:ListItem Selected="True" Value="-1" Text="ALL" />
                        <asp:ListItem Value="1" Text="Inventory" />
                        <asp:ListItem Value="2" Text="On-Hand" />
                    </asp:DropDownList>
                 </td>          
             </tr>
             <tr>   
                <td class="TableBorderB">
                    Title
                 </td>         
                 <td class="TableBorder">
                    <asp:DropDownList ID="ddlTitlePresent" runat="server" >
                        <asp:ListItem Selected="True" Value="-1" Text="ALL" />
                        <asp:ListItem Value="1" Text="Yes" />
                        <asp:ListItem Value="0" Text="No" />
                    </asp:DropDownList>
                 </td>
                  <td class="TableBorderB">&nbsp;</td> 
                <td class="TableBorderB" style="text-align:right; padding-right:70px;">
                    <asp:Button ID="btnViewReport" runat="server" 
                        CssClass="btn"
                        Text="View Report" onclick="btnViewReport_Click"  />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>