<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="ManagePermissions.aspx.cs"
    Inherits="METAOPTION.UI.ManagePermissions" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <script src="../CSS/jquery-1.7.2.min.js" type="text/javascript"></script>
    <asp:HiddenField ID="hdnEntity" runat="server" />
    <asp:HiddenField ID="hdnEntitiesId" runat="server" />
    <div onmousemove="SetProgressPosition(event)">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="TableHeadingBg TableHeading" style="text-align: left">
                    <asp:HiddenField ID="hdnPermissionId" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnSearch" runat="server" Value="0" />
                    <div class="TableHeadingBg TableHeading" style="text-align: left">
                        <div style="width: 40%; padding: 5px; float: left">
                            Manage IP Permission
                        </div>
                        <div style="width: 40%; padding: 5px; text-align: right; float: right">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn" OnClick="btnSubmit_Click"
                                Text="Add" Width="60px" />
                        </div>
                    </div>
                </div>
                <div class="dvSearch">
                    <div style="width: 26%; float: left; padding: 5px;">
                        <table>
                            <tr>
                                <td class="TableBorder" style="width: 200px">
                                    IP Address&nbsp;
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtIP" runat="server" CssClass="txt2" onblur="validate(this);"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtIP"
                                        Display="Dynamic" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Entity Type&nbsp;
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlEntityType" runat="server" CssClass="txt2" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlEntityType_SelectedIndexChanged">
                                        <asp:ListItem Value="-1" Text=""></asp:ListItem>
                                        <asp:ListItem Value="2">Buyer</asp:ListItem>
                                        <asp:ListItem Value="1">Dealer</asp:ListItem>
                                        <asp:ListItem Value="5">Employee</asp:ListItem>
                                        <asp:ListItem Value="4">Utility Company</asp:ListItem>
                                        <asp:ListItem Value="3">Vendor</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--  <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlEntityType"
                                        InitialValue="-1" ErrorMessage="*" Display="Dynamic" />--%>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 26%; float: left; padding: 5px">
                        <table>
                            <tr>
                                <td class="TableBorder" style="width: 200px">
                                    Description&nbsp;
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="txt2" ></asp:TextBox>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    User&nbsp;
                                </td>
                                <td class="TableBorder">
                                    <telerik:RadComboBox ID="dlEntity" runat="server" AllowCustomText="true" 
                                        EmptyMessage="">
                                        <ItemTemplate>
                                            <div onclick="StopPropagationFileType(event)" class="combo-item-template">
                                                <asp:HiddenField ID="hdnEntityId" runat="server" Value='<%#Eval("SecurityUserID")%>' />
                                                <asp:CheckBox runat="server" ID="chkEntity" CssClass="cb" Text='<%#Eval("EntityName")%>'
                                                    onclick="onEntityTypeClick(this)" />
                                            </div>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 25%; float: left; padding: 5px">
                        <table>
                            <tr>
                                <td class="TableBorder" style="width: 200px">
                                    <asp:RadioButtonList ID="rdbIPType" runat="server" CssClass="rbl" RepeatDirection="Horizontal">
                                        <%-- OnSelectedIndexChanged="rdbIPType_SelectedIndexChanged" AutoPostBack="true" --%>
                                        <asp:ListItem Selected="True" Value="-1">All</asp:ListItem>
                                        <asp:ListItem Value="1">User IP</asp:ListItem>
                                        <asp:ListItem Value="2">System IP</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 18%; float: left; padding: 5px">
                        <table>
                            <tr>
                                <td style="width: 175px; text-align: right; padding-top: 5px">
                                    <asp:Button ID="btnSearch" runat="server" CausesValidation="false" CssClass="btn"
                                        OnClick="btnSearch_Click" Text="Search" Width="70px" />
                                    <asp:Button ID="btnClear" runat="server" CausesValidation="false" CssClass="btn"
                                        OnClick="btnClear_Click" Text="View All" Width="70px" />
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                        DisplayAfter="150">
                                        <ProgressTemplate>
                                            <div id="dvProg" class="overlay">
                                                <img id="image" src="../Images/Wait.gif" alt="Wait..." style="vertical-align: middle" />&nbsp;Please wait...
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div style="clear: both;">
                    <asp:GridView ID="gvIPPermission" runat="server" AutoGenerateColumns="False" Width="100%"
                        AllowPaging="true" PageSize="20" GridLines="None" EmptyDataText="No record found"
                        DataKeyNames="IPPermissionID" OnPageIndexChanging="gvIPPermission_PageIndexChanging"
                        OnRowDataBound="gvIPPermission_RowDataBound" AllowSorting="true" OnSorting="gvIPPermission_Sorting">
                        <Columns>
                            <asp:BoundField DataField="IPAddress" HeaderText="IP Address" SortExpression="IPAddress"
                                ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="GridContent"
                                HeaderStyle-CssClass="GridHeader" HeaderText="Type">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdIPAddress" runat="server" Value='<%#Eval("IPAddress") %>' />
                                    <asp:HiddenField ID="hdnEntityType" runat="server" Value='<%#Eval("EntityTypeID") %>' />
                                    <asp:HiddenField ID="hdnIPType" runat="server" Value='<%#Eval("IPTypeValue") %>' />
                                    <asp:Label ID="lblIPType" runat="server" Text='<%#Eval("IPType") %>' Width="100px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="User" ItemStyle-CssClass="GridContent"
                                HeaderStyle-CssClass="GridHeader">
                                <ItemTemplate>
                                    <asp:Label ID="lblEntityType" runat="server" Width="280px"></asp:Label>
                                     <asp:Label ID="lblSUID" runat="server" Visible="false" Width="280px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-CssClass="GridContent"
                                HeaderStyle-CssClass="GridHeader" SortExpression="Description" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Date Added<br />AddedBy"
                                ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader" SortExpression="IP.DateAdded">
                                <ItemTemplate>
                                    <%#Eval("DateAdded")%><br />
                                    <%#Eval("AddedBy")%><br />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Date Modified<br />ModifiedBy"
                                ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader" SortExpression="IP.DateModified">
                                <ItemTemplate>
                                    <%#Eval("DateModified")%><br />
                                    <%#Eval("ModifiedBy")%><br />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Action" ItemStyle-CssClass="GridContent"
                                HeaderStyle-CssClass="GridHeader">
                                <ItemTemplate>
                                    <asp:ImageButton ID="lnkEdit" ToolTip="Edit" runat="server" OnClick="lnkEdit_Click"
                                        ImageUrl="~/Images/newedit.gif" CausesValidation="false" />
                                    <asp:ImageButton ID="lnkDelete" ToolTip="Delete" runat="server" ImageUrl="~/Images/DeleteButton.jpg"
                                        OnClick="lnkDelete_Click" CausesValidation="false" OnClientClick="javascript:return confirm('Do you want to delete this IP group?\n\nClick Ok to delete.\nYou won\'t be able to undo those changes.');" />
                                </ItemTemplate>
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="gvRow" />
                        <AlternatingRowStyle CssClass="gvAlternateRow" />
                        <HeaderStyle CssClass="gvHeading" />
                        <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                        <EmptyDataRowStyle CssClass="gvEmpty" />
                    </asp:GridView>
                </div>
                <asp:HiddenField ID="hfPopup" runat="server" />
                <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="hfPopup"
                    PopupControlID="panOpen" BackgroundCssClass="modalBackground" CancelControlID="imgCancel"
                    PopupDragHandleControlID="panOpen" BehaviorID="ImageModelPopup" />
                <asp:Panel ID="panOpen" runat="server" ScrollBars="Auto"  Width="850px"
                    CssClass="ModalWindow" Style="background-color: #FFFFFF; min-height: 300px; max-height: 700px; border: 3px solid Gray;
                    font-family: Verdana; font-size: 10pt">
                    <div style="display: none">
                        <img src="../Images/close_icon.gif" alt="close" id="imgCancel" runat="server" />
                    </div>
                    <iframe id="ifrmEditIP" runat="server" scrolling="auto" style="min-height: 290px; max-height: 670px; width: 850px;"
                        frameborder="0"></iframe>
                </asp:Panel>
                <%--Added for Add new IP Permission--%>
                <asp:HiddenField ID="hdnAdd" runat="server" />
                <ajax:ModalPopupExtender ID="MPEAdd" runat="server" TargetControlID="hdnAdd" PopupControlID="pnlAdd"
                    BackgroundCssClass="modalBackground" CancelControlID="imgAdd" PopupDragHandleControlID="pnlAdd"
                    BehaviorID="MPEAddPopup" />
                <asp:Panel ID="pnlAdd" runat="server" ScrollBars="Auto" Height="250px" Width="820px"
                    CssClass="ModalWindow" Style="background-color: #FFFFFF; border: 3px solid Gray;
                    font-family: Verdana; font-size: 10pt">
                    <div style="display: none">
                        <img src="../Images/close_icon.gif" alt="close" id="imgAdd" runat="server" />
                    </div>
                    <iframe id="IframeAdd" runat="server" scrolling="auto" style="height: 250px; width: 820px;"
                        frameborder="0"></iframe>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <style type="text/css">
        .rbl input[type="radio"]
        {
            margin-left: 10px;
            margin-right: 1px;
            padding-bottom: 5px;
        }
        .cb label
        {
            margin-left: 5px;
        }
    </style>
    <script type='text/javascript' language="javascript">
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


        function validate(ctrl) {
            var ID = ctrl.id;
            var value = document.getElementById(ID).value;
            if (value != '') {
                var ipRE = new RegExp('^\\d+\\.\\d+\\.\\d+\\.\\d+$');
                var ret = (ipRE.test(value) ? '' : 'in') + 'valid';
                if (ret.toLowerCase() == 'invalid') {
                    alert('Please Enter Valid IP Address.');
                    document.getElementById(ID).value = '';
                }
            }
        }


        var returnValue = 0;
        function CheckDuplicateIP() {
            CheckDuplicateIPAddress();

        }

        function CheckDuplicateIPAddress() {
            var IPAddress = $("input[id$='txtIP']").val();

            $("#show1").load('../UI/CheckIPAddress.ashx?IPAddress=' + IPAddress, {
            },

                function (data) {
                    if (data == "1") {

                        alert('This IP Address already exists.');
                        //$("input[id$='txtIP']").val() = '';
                        returnValue = 0;
                    }
                    else {
                        returnValue = 1;
                        return false;
                    }

                });

            return false;
        }

        var hdnEntity = ("<%= this.hdnEntity.ClientID %>");
        var hdnEntitiesId = ("<%= this.hdnEntitiesId.ClientID %>");
        var canceldlFileTypeClosing = false;

        function StopPropagationFileType(e) {
            //cancel bubbling
            e.cancelBubble = true;
            if (e.stopPropagation) {
                e.stopPropagation();
            }
        }

        function ondlDescriptionClosing() {
            canceldlFileTypeClosing = false;
        }


        function onEntityTypeClick(chk) {
            var combodlFileType = $find("<%= dlEntity.ClientID %>");
            //holds the text of all checked items
            var text = "";
            //holds the values of all checked items
            var values = "";
            EntitiesId = "";
            //get the collection of all items
            var items = combodlFileType.get_items();
            //enumerate all items
            for (var i = 0; i < items.get_count(); i++) {
                var item = items.getItem(i);
                //get the checkbox element of the current item
                var EntityId = $get(combodlFileType.get_id() + "_i" + i + "_hdnEntityId");
                var chkEntity = $get(combodlFileType.get_id() + "_i" + i + "_chkEntity");
                if (chkEntity.checked) {
                    text += item.get_text() + ",";
                    values += item.get_text() + ",";
                    //alert(item.get_value());
                    EntitiesId += EntityId.value + ",";
                }
            }
            //remove the last comma from the string
            text = removeLastComma(text);
            values = removeLastComma(values);
            EntitiesId = removeLastComma(EntitiesId);

            if (text.length > 0) {
                //set the text of the combobox                   
                combodlFileType.set_text(text);
                document.getElementById(hdnEntity).value = values;
                document.getElementById(hdnEntitiesId).value = EntitiesId;
            }
            else {
                //all checkboxes are unchecked
                //so reset the controls
                combodlFileType.set_text("");
                document.getElementById(hdnEntity).value = "";
                document.getElementById(hdnEntitiesId).value = "";
            }
        }
        //this method removes the ending comma from a string
        function removeLastComma(str) {
            return str.replace(/,$/, "");
        }


        function CheckIPType() {
            if (Page_ClientValidate()) {
                var IPTypeValue = $("input[name='<%=rdbIPType.UniqueID%>']:radio:checked").val();
                if (IPTypeValue == -1) {
                    alert("Please Select User IP or System IP.");
                    return false;
                }
            }
            else
                return true;
        }



        function HideModelpopup() {
            $find('ImageModelPopup').hide();
            window.parent.location.reload();
            return false;
        }
        function HideEditDialogEmail() {
            $find('ImageModelPopup').hide();
            return false;
        }


        function HideAddModelpopup() {
            $find('MPEAddPopup').hide();
            window.parent.location.reload();
            return false;
        }
        function HideAddEditDialogEmail() {
            $find('MPEAddPopup').hide();
            return false;
        }
    </script>
</asp:content>
