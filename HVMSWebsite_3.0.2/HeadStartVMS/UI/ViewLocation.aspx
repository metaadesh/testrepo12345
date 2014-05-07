<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="ViewLocation.aspx.cs" 
    Inherits="METAOPTION.UI.ViewLocation" Title="HeadStartVMS::View Location" %>

<asp:Content ID="contViewLocation" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
     <div onmousemove="SetProgressPosition(event)">
        <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="padding:5px">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="TableHeadingBg TableHeading" colspan="8">
                                View Location
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder">VIN#</td>
                            <td class="TableBorder">
                                <asp:TextBox ID="txtVIN" runat="server" CssClass="txt2" />
                            </td>
                            <td class="TableBorder">Location</td>
                            <td class="TableBorder">
                                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="txt2" />
                            </td>
                            <td class="TableBorder">Added By</td>
                            <td class="TableBorder">
                                <asp:DropDownList ID="ddlUsers" runat="server" CssClass="txt2" />
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="Btn_Form" 
                                    OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="padding:5px">
                    <asp:GridView ID="gvLocation" runat="server"
                        AutoGenerateColumns="false"
                        GridLines="None"
                        CssClass="Grid"
                        RowStyle-CssClass="gvRow"
                        AlternatingRowStyle-CssClass="gvAlternateRow"
                        Width="100%"
                        EmptyDataText="No record found"  
                        EmptyDataRowStyle-CssClass="gvEmpty"
                        AllowPaging="true"
                        PageSize="20"
                        DataKeyNames="ID"
                        PagerStyle-HorizontalAlign="Right"
                        OnPageIndexChanging="gvLocation_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="VIN#" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <div style="text-transform:uppercase">
                                        <%#Eval("VIN") %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Code" DataField="InventoryID" ItemStyle-CssClass="GridContentNumbers" HeaderStyle-CssClass="GridHeader" ItemStyle-Width="60px" />
                            <asp:BoundField HeaderText="Latitude" DataField="Latitude" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader" ItemStyle-Width="60px" />
                            <asp:BoundField HeaderText="Longitude" DataField="Longitude" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader" ItemStyle-Width="60px" />
                            <asp:BoundField HeaderText="Location" DataField="LocationCode" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader" />
                            <asp:TemplateField HeaderText="Added By" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                ItemStyle-Width="150px">
                                <ItemTemplate>
                                    <%#Eval("DisplayName")%>&nbsp;(<%#Eval("EntityType")%>)
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Device" DataField="DeviceName" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader" ItemStyle-Width="60px" />
                            <asp:BoundField HeaderText="Added On" DataField="DateAdded" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader" ItemStyle-Width="120px" />
                            <asp:TemplateField HeaderText="Year&nbsp;&nbsp;&nbsp;Make&nbsp;&nbsp;&nbsp;Model" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" >
                                <ItemTemplate>
                                    <%#Eval("Year")%>&nbsp;&nbsp;&nbsp;<%#Eval("Make")%>&nbsp;&nbsp;&nbsp;<%#Eval("Model")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="150" AssociatedUpdatePanelID="updMain">
                    <ProgressTemplate>
                        <div id="dvProg" class="overlay">
                            <img id="image" src="../Images/Wait.gif" alt="Wait..." style="vertical-align: middle" />&nbsp;Please
                            wait...
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
<script type="text/javascript">
function SetProgressPosition(e) {
            var posx = 0;
            var posy = 0;
            if (!e) var e = window.event;
            if (e.pageX || e.pageY) {
                posx = e.pageX;
                posy = e.pageY;
            }
            else if (e.clientX || e.clientY) {
                posx = e.clientX + document.documentElement.scrollLeft;
                posy = e.clientY + document.documentElement.scrollTop;
            }
            document.getElementById('dvProg').style.left = posx + 10 + "px";
            document.getElementById('dvProg').style.top = posy + "px";
            
        }
</script>
</asp:Content>

