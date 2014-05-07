<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenericImages.aspx.cs"
    Inherits="METAOPTION.UI.GenericImages" %>

<asp:content id="contGenericImages" runat="server" contentplaceholderid="ContentPlaceHolder1">
<div onmousemove="SetProgressPosition(event)">
    <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="padding: 5px">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="TableHeadingBg TableHeading" colspan="8">
                            Generic Images
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder">VIN#</td>
                        <td class="TableBorder">
                            <asp:TextBox ID="txtVIN" runat="server" CssClass="txt2" />
                        </td>
                        <td class="TableBorder">Added By</td>
                        <td class="TableBorder">
                            <asp:DropDownList ID="ddlAddedBy" runat="server" CssClass="txt2" />
                        </td>
                        <td>
                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="150" AssociatedUpdatePanelID="updMain">
                                <ProgressTemplate>
                                    <div id="dvProg" class="overlay">
                                        <img id="image" src="../Images/Wait.gif" alt="Wait..." style="vertical-align: middle" />&nbsp;Please wait...
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="Btn_Form" 
                                OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:GridView ID="gvGenericImages" runat="server" AutoGenerateColumns="false" GridLines="None"
                    CssClass="Grid" RowStyle-CssClass="gvRow" AlternatingRowStyle-CssClass="gvAlternateRow"
                    Width="100%" EmptyDataText="No record found" EmptyDataRowStyle-CssClass="gvEmpty"
                    AllowPaging="true" PageSize="20" DataKeyNames="GenericID" PagerStyle-HorizontalAlign="Right"
                    OnPageIndexChanging="gvGenericImages_PageIndexChanging" OnRowDataBound="gvGenericImages_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="VIN#" DataField="VIN" ItemStyle-CssClass="GridContent"
                            HeaderStyle-CssClass="GridHeader" ItemStyle-Width="120px" />
                        <asp:BoundField HeaderText="Year" DataField="Year" ItemStyle-CssClass="GridContent"
                            HeaderStyle-CssClass="GridHeader" ItemStyle-Width="30px" />
                        <asp:TemplateField HeaderText="Make<br />Model" HeaderStyle-CssClass="GridHeader"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-CssClass="GridContent" 
                            ItemStyle-Width="80px">
                            <ItemTemplate>
                                <%#Eval("Make") %><br /><%#Eval("Model") %>
                            </ItemTemplate>    
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Note" DataField="Note" ItemStyle-CssClass="GridContent"
                            HeaderStyle-CssClass="GridHeader" />
                        <asp:BoundField HeaderText="Added On" DataField="DateAdded" ItemStyle-CssClass="GridContent"
                            HeaderStyle-CssClass="GridHeader" ItemStyle-Width="120px" />
                        <asp:TemplateField HeaderText="Added By" HeaderStyle-CssClass="GridHeader" 
                            ItemStyle-CssClass="GridContent" >
                                <ItemTemplate>
                                    <%#Eval("DisplayName")%>&nbsp;(<%#Eval("EntityType")%>)
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:BoundField HeaderText="Latitude" DataField="Latitude" ItemStyle-CssClass="GridContent"
                            HeaderStyle-CssClass="GridHeader" ItemStyle-Width="60px" />
                        <asp:BoundField HeaderText="Longitude" DataField="Longitude" ItemStyle-CssClass="GridContent"
                            HeaderStyle-CssClass="GridHeader" ItemStyle-Width="60px" />
                        <asp:TemplateField HeaderText="Device<br/>Info" HeaderStyle-CssClass="GridHeader"
                            ItemStyle-CssClass="GridContent" ItemStyle-Width="50px">
                            <ItemTemplate>
                                <%# Eval("DeviceName")%>
                                <br />
                                <asp:Label ID="lbldeviceid" runat="server" Text='<%# FormatDeviceID(Eval("DeviceID")) %>'
                                    ToolTip='<%# Eval("DeviceID") %>' CssClass="Tooltip"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Images" HeaderStyle-CssClass="GridHeader"  
                            ItemStyle-CssClass="GridContentNumbers" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfVIN" runat="server" Value='<%#Eval("VIN") %>' />
                                <asp:ImageButton ID="ibtncars" runat="server" CssClass="Tooltip" ToolTip="PreInv Images" 
                                    ImageUrl="~/Images/car-1.png" OnClick="ibtncars_Click" />
                                <asp:ImageButton ID="ibtncars1" runat="server" CssClass="Tooltip" ToolTip="PreExp Images" 
                                    ImageUrl="~/Images/car-2.png" OnClick="ibtncars1_Click" />
                                <asp:ImageButton ID="ibtncars2" runat="server" CssClass="Tooltip" ToolTip="All Generic Images" 
                                    ImageUrl="~/Images/car-3.png" OnClick="ibtncars2_Click" />
                                <asp:ImageButton ID="ibtncars3" runat="server" CssClass="Tooltip" ToolTip="Generic Images" 
                                    ImageUrl="~/Images/car-4.png" OnClick="ibtncars3_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <asp:Panel ID="panOpen" runat="server" Height="600px" Width="700px" CssClass="ModalWindow">
                <div style="position: absolute; text-align: right; padding-top: 15px; padding-right: 10px;
                    width: 662px;">
                    <img src="../Images/close_icon.gif" alt="close" id="imgCancel" runat="server" />
                </div>
                <iframe id="ifrmImages" runat="server" scrolling="no" style="height: 600px; width: 700px;"
                    frameborder="0"></iframe>
            </asp:Panel>
            <asp:HiddenField ID="hfPopup" runat="server" />
            <ajax:ModalPopupExtender ID="mpeImages" runat="server" TargetControlID="hfPopup"
                PopupControlID="panOpen" BackgroundCssClass="modalBackground" CancelControlID="imgCancel"
                PopupDragHandleControlID="panOpen" />
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
</asp:content>
