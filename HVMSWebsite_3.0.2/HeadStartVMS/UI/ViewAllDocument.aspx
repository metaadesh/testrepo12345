<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="ViewAllDocument.aspx.cs"
    Inherits="METAOPTION.UI.ViewAllDocument" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="Hidden" runat="server" />
    <asp:HiddenField ID="hdnEntityType" runat="server" />
    <asp:HiddenField ID="hdnDocType" runat="server" />
    <asp:HiddenField ID="hdnDocName" runat="server" />
    <asp:HiddenField ID="hdnDscription" runat="server" />
    <asp:HiddenField ID="hdnFileType" runat="server" />
    <asp:HiddenField ID="hfPreInvID" runat="server" Value="0" />
    <div onmousemove="SetProgressPosition(event)">
        <%--onmousemove="SetProgressPosition(event)"--%>
        <%--  <asp:UpdatePanel ID="upViewAllDocument" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
        <div class="TableHeadingBg TableHeading" style="text-align: left">
            <div style="width: 90%; padding: 5px 0px; float: left">
                View All Document</div>
            <div id="dvCollapse" runat="server" class="TOtoggle" style="float: right; padding-right: 10px;
                padding-top: 5px;">
            </div>
        </div>
        <div id="dvSearch" runat="server" class="dvSearch">
            <div style="width: 32%; float: left; padding: 5px;">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="TableBorder">
                            VIN#
                        </td>
                        <td class="TableBorder">
                            <asp:TextBox ID="txtVINNumber" runat="server" MaxLength="17" CssClass="txt2" Height="16px" />
                        </td>
                    </tr>
                    <tr>
                         <td class="TableBorder">
                            Description
                        </td>
                        <td class="TableBorder">
                            <asp:TextBox ID="txtDescription" runat="server" MaxLength="17" CssClass="txt2" Height="16px" />
                          </td>

                    </tr>
                    <tr>
                        <td class="TableBorder">
                            Document Type
                        </td>
                        <td class="TableBorder">
                            <telerik:RadComboBox ID="dlDocumentType" Width="150px" runat="server" AllowCustomText="true"
                                EmptyMessage="">
                                <ItemTemplate>
                                    <div onclick="StopPropagationDocType(event)" class="combo-item-template">
                                        <asp:CheckBox runat="server" ID="chkDocType" CssClass="cb" Text='<%#Eval("DocumentType")%>'
                                            onclick="onDocTypeClick(this)" />
                                    </div>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder">
                            Document Title
                        </td>
                        <td class="TableBorder">
                            <asp:TextBox ID="txtDocName" runat="server" MaxLength="17" CssClass="txt2" Height="16px" />
                            <%-- <telerik:RadComboBox ID="dlDocumentName" runat="server" AllowCustomText="true" EmptyMessage="">
                                        <ItemTemplate>
                                            <div onclick="StopPropagationDocName(event)" class="combo-item-template">
                                                <asp:CheckBox runat="server" ID="chkDocTitle" CssClass="cb" Text='<%#Eval("DocumentTitle")%>'
                                                    onclick="onDocTitleClick(this)" />
                                            </div>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>--%>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 26%; float: left; padding: 5px 5px 5px 10px">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="TableBorder">
                            Date Added
                        </td>
                        <td class="TableBorder">
                            <asp:TextBox ID="txtDateFrom" runat="server" CssClass="txt1" Width="78px" />
                            <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateFrom"
                                PopupButtonID="txtDateFrom" />
                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="txt1" Width="78px" />
                            <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateTo"
                                PopupButtonID="txtDateTo" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder">
                            Added By
                        </td>
                        <td class="TableBorder">
                            <telerik:RadComboBox ID="dlAddedBy" runat="server" Width="150px" AllowCustomText="true"
                                EmptyMessage="">
                                <ItemTemplate>
                                    <div onclick="StopPropagation(event)" class="combo-item-template">
                                        <asp:CheckBox runat="server" ID="chk1" CssClass="cb" Text='<%#Eval("DisplayName")%>'
                                            onclick="onCheckBoxClick(this)" />
                                    </div>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder">
                            File Type
                        </td>
                        <td class="TableBorder">
                            <telerik:RadComboBox ID="dlFileType" runat="server" Width="150px" AllowCustomText="true"
                                EmptyMessage="">
                                <ItemTemplate>
                                    <div onclick="StopPropagationFileType(event)" class="combo-item-template">
                                        <asp:CheckBox runat="server" ID="chkFileType" CssClass="cb" Text='<%#Eval("FileType")%>'
                                            onclick="onFileTypeClick(this)" />
                                    </div>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr style="visibility:hidden">
                       <td class="style1" style="visibility:hidden">
                            Entity Type
                        </td>
                        <td class="style1" style="visibility:hidden">
                            <telerik:RadComboBox ID="dlEntityType" runat="server" Width="150px" AllowCustomText="true" Visible="false"
                                EmptyMessage="">
                                <ItemTemplate>
                                    <div onclick="StopPropagationEntityType(event)" class="combo-item-template">
                                        <asp:CheckBox runat="server" ID="chkEntityType" CssClass="cb" Text='<%#Eval("EntityType")%>'
                                            onclick="onEntityTypeClick(this)" Enabled="false" Visible="false" />
                                    </div>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                        </td>


                        <%--<td class="TableBorder">
                            Description
                        </td>
                        <td class="TableBorder">
                        <asp:TextBox ID="txtDescription" runat="server" MaxLength="17" CssClass="txt2" Height="16px" />--%>
                          
                          
                            <%-- <telerik:RadComboBox ID="dlDescription" runat="server" AllowCustomText="true" EmptyMessage="">
                                        <ItemTemplate>
                                            <div onclick="StopPropagationDescription(event)" class="combo-item-template">
                                                <asp:CheckBox runat="server" ID="chkDescription" CssClass="cb" Text='<%#Eval("Description")%>'
                                                    onclick="onDesctiptionClick(this)" />
                                            </div>
                                        </ItemTemplate>
                                    </telerik:RadComboBox></td>--%>
                    </tr>
                </table>
            </div>
            <div style="width: 38%; float: left; padding: 5px">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="TableBorder" style="vertical-align: top; width: 85px">
                            Sort 1
                        </td>
                        <td class="TableBorder" style="width: 250px">
                            <div style="float: left; margin-right: 10px">
                                <asp:DropDownList ID="ddlSort1" runat="server" CssClass="txt2" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSort1_SelectedIndexChanged" />
                            </div>
                            <asp:RadioButtonList ID="rbtnSort1Direction" runat="server" RepeatDirection="Horizontal"
                                CellPadding="2">
                                <asp:ListItem Value="ASC" Text=" A - Z &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                                <asp:ListItem Value="DESC" Text=" Z - A " Selected="True" />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder" style="vertical-align: top">
                            Sort 2
                        </td>
                        <td class="TableBorder">
                            <div style="float: left; margin-right: 10px">
                                <asp:DropDownList ID="ddlSort2" runat="server" CssClass="txt2" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSort2_SelectedIndexChanged" />
                            </div>
                            <asp:RadioButtonList ID="rbtnSort2Direction" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="ASC" Text=" A - Z &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" Selected="True" />
                                <asp:ListItem Value="DESC" Text=" Z - A " />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder" style="vertical-align: top">
                            Sort 3
                        </td>
                        <td class="TableBorder">
                            <div style="float: left; margin-right: 10px">
                                <asp:DropDownList ID="ddlSort3" runat="server" CssClass="txt2" AutoPostBack="true" />
                            </div>
                            <asp:RadioButtonList ID="rbtnSort3Direction" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="ASC" Text=" A - Z &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" Selected="True" />
                                <asp:ListItem Value="DESC" Text=" Z - A " />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%-- <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="150" AssociatedUpdatePanelID="upViewAllDocument">
                                        <ProgressTemplate>
                                            <div id="dvProg" class="overlay">
                                                <img id="image" src="../Images/Wait.gif" alt="Wait..." style="vertical-align: middle" />&nbsp;Please
                                                wait...
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>--%>
                        </td>
                        <td style="padding: 7px 0px 0px 10px">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" CssClass="btn"
                                OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="clear: both;">
            <table border="0" style="width: 100%;" class="TableHeadingBg TableHeading">
                <tr>
                    <td style="width: 40%">
                        <asp:Label ID="lblCount" runat="server" BorderColor="Transparent" BackColor="Transparent"
                            ForeColor="#21618C" />
                    </td>
                    <td style="width: 25%; text-align: right; white-space: nowrap; color: #21618C">
                        Page&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlPaging" runat="server" CssClass="txt1" Width="50px" OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged"
                            AutoPostBack="true" />
                        of
                                                                                                                                                                                                                                                                      <%= gvAllDocument.PageCount%>
                    </td>
                    <td style="text-align: right; padding-right: 10px; color: #21618C">
                        Page size&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlPageSize1" runat="server" CssClass="txt1" Width="50px" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged"
                            AutoPostBack="true">
                            <asp:ListItem Text="25" Value="25" />
                            <asp:ListItem Text="50" Value="50" Selected="True" />
                            <asp:ListItem Text="100" Value="100" />
                            <asp:ListItem Text="250" Value="250" />
                        </asp:DropDownList>
                    </td>
                    <td style="white-space: nowrap; text-align: right">
                        <asp:Button ID="btnFirst" runat="server" Text="First" CssClass="btn" OnClick="btnFirst_Click" />
                        <asp:Button ID="btnPrev" runat="server" Text="Prev" CssClass="btn" OnClick="btnPrev_Click" />
                        <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="btn" OnClick="btnNext_Click" />
                        <asp:Button ID="btnLast" runat="server" Text="Last" CssClass="btn" OnClick="btnLast_Click" />
                    </td>
                </tr>
            </table>
            <div>
                <asp:GridView ID="gvAllDocument" runat="server" DataKeyNames="DocumentId" Width="100%"
                    AutoGenerateColumns="False" EmptyDataText="No record found for this search criteria."
                    RowStyle-CssClass="gvRow" GridLines="None" PagerSettings-Visible="false" AllowPaging="true"
                    PageSize="50" AllowSorting="true" OnSorting="gvAllDocument_Sorting" CssClass="Grid"
                    OnRowCommand="gvAllDocument_RowCommand" OnRowDataBound="gvAllDocument_RowDataBound">
                    <%--OnRowDataBound="gvAllDocument_RowDataBound"--%>
                    <Columns>
                        <asp:TemplateField HeaderText="VIN#<br />Year<br />Make<br />Model" HeaderStyle-CssClass="GridHeader"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-CssClass="GridContent" ItemStyle-Width="120px"
                            HeaderStyle-Width="120px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkVIN" runat="server" CommandName="Select" Text='<%#Eval("VIN") %>'
                                    CommandArgument='<%#Eval("EntityId") %>'></asp:LinkButton>
                                <br />
                                <%#Eval("Year")%><br />
                                <%#Eval("Make") %><br />
                                <%#Eval("Model") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="DealerName" HeaderText="Entity Name" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" />--%>
                        <asp:BoundField DataField="DocumentType" HeaderText="Document Type" ItemStyle-Width="150px"
                            ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader" />
                        <asp:BoundField DataField="DocumentTitle" HeaderText="Document Title" ItemStyle-CssClass="GridContent"
                            HeaderStyle-CssClass="GridHeader" />
                        <asp:BoundField DataField="DocumentName" HeaderText="Document Name" ItemStyle-CssClass="GridContent"
                            HeaderStyle-CssClass="GridHeader" ItemStyle-Width="150px" ItemStyle-Wrap="true" />
                        <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-CssClass="GridContent"
                            HeaderStyle-CssClass="GridHeader" ItemStyle-Width="200px" />
                        <asp:BoundField DataField="FileType" HeaderText="File Type" ItemStyle-CssClass="GridContent"
                            HeaderStyle-CssClass="GridHeader" ItemStyle-Width="100px" />
                        <asp:TemplateField HeaderText="Added By" HeaderStyle-CssClass="GridHeader" ItemStyle-Width="120px"
                            ItemStyle-CssClass="GridContent">
                            <ItemTemplate>
                                <%#Eval("DisplayName")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Added" HeaderStyle-CssClass="GridHeader" ItemStyle-Width="130px"
                            ItemStyle-CssClass="GridContent">
                            <ItemTemplate>
                                <%#Eval("DateAdded")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="GridHeader" ItemStyle-Width="120px"
                            ItemStyle-CssClass="GridContentCentre">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdnFileFormat" runat="server" Value=' <%# Eval("FileType")%>' />
                                <%-- <a id="ibtnCRAvailable" runat="server" target="_blank" href="#" visible="false" style="text-decoration: none;">
                                            <img id="imgAvailabel" runat="server" border="0" alt="No Image" src="../Images/ucr-btn.png"
                                                style="padding-bottom: 4px" />
                                        </a>--%>
                                <%--  <a id="btnDocument" target="_blank" runat="server" href='<%# "DocumentViewer.aspx?Id="+Eval("DocumentId").ToString() %>'>
                                            <img id="btnImgDocument" runat="server" border="0" alt="No Image" src="../Images/document.png.jpg" />
                                        </a>--%>
                                <asp:ImageButton ID="btnImgDocument" runat="server" OnClick="btnImgDocument_Click"
                                    ImageUrl="../Images/view_edit.png" ToolTip="File,Image Display" />
                                <asp:ImageButton ID="imgDownload" runat="server" ImageUrl="../Images/download.png"
                                    ToolTip="Download File" OnClick="imgDownload_Click" Style="padding-bottom: 4px" /><%--OnClick="imgDownload_Click"--%>
                                <asp:ImageButton ID="imgDelete" CausesValidation="false" ImageUrl="~/Images/DeleteButton.jpg"
                                    runat="server" ToolTip="Delete Document" Style="padding-bottom: 2px" OnClientClick="return DeleteAlert();"
                                    OnClick="imgDelete_Click" />
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" />
                    <RowStyle CssClass="gvRow" />
                    <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                    <AlternatingRowStyle CssClass="gvAlternateRow"></AlternatingRowStyle>
                    <EmptyDataRowStyle CssClass="gvEmpty" />
                </asp:GridView>
            </div>
            <table border="0" style="width: 100%" class="TableHeadingBg TableHeading">
                <tr>
                    <td style="width: 40%">
                        <asp:Label ID="lblCount1" runat="server" BorderColor="Transparent" BackColor="Transparent"
                            ForeColor="#21618C" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 25%; text-align: right; white-space: nowrap; color: #21618C">
                        Page
                        <asp:DropDownList ID="ddlPaging1" runat="server" CssClass="txt1" Width="50px" OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged"
                            AutoPostBack="true" />
                        of
                        <%= gvAllDocument.PageCount%>
                    </td>
                    <td style="text-align: right; padding-right: 10px; color: #21618C">
                        Page size&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlPageSize2" runat="server" CssClass="txt1" Width="50px" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged"
                            AutoPostBack="true">
                            <asp:ListItem Text="25" Value="25" />
                            <asp:ListItem Text="50" Value="50" Selected="True" />
                            <asp:ListItem Text="100" Value="100" />
                            <asp:ListItem Text="250" Value="250" />
                        </asp:DropDownList>
                    </td>
                    <td style="white-space: nowrap; text-align: right">
                        <asp:Button ID="btnFirst1" runat="server" Text="First" CssClass="btn" OnClick="btnFirst_Click" />
                        <asp:Button ID="btnPrev1" runat="server" Text="Prev" CssClass="btn" OnClick="btnPrev_Click" />
                        <asp:Button ID="btnNext1" runat="server" Text="Next" CssClass="btn" OnClick="btnNext_Click" />
                        <asp:Button ID="btnLast1" runat="server" Text="Last" CssClass="btn" OnClick="btnLast_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%; height: auto">
            <asp:HiddenField ID="hfPopup" runat="server" />
            <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="hfPopup"
                PopupControlID="panOpen" BackgroundCssClass="modalBackground" CancelControlID="btnClose"
                PopupDragHandleControlID="panOpen" BehaviorID="ImageModelPopup" />
            <asp:Panel ID="panOpen" runat="server" Height="680px" Width="890px" Style="background: white"
                CssClass="imagevideobox">
                <div style="position: absolute; text-align: right; padding-right: 10px; width: 885px;">
                    <asp:ImageButton ID="btnClose" runat="server" ImageUrl="../Images/close_icon.gif"
                        OnClick="btnClose_Click" />
                </div>
                <iframe id="ifrmSlideShow" runat="server" scrolling="no" style="height: 780px; width: 890px;
                    margin-top: 10px;" frameborder="0"></iframe>
            </asp:Panel>
        </div>
        <%--  </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
    <script language="javascript" type="text/javascript">
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
    <script type="text/javascript">
        var hiddenfld = ("<%= this.Hidden.ClientID %>");
        var cancelDropDownClosing = false;

        function StopPropagation(e) {
            //cancel bubbling
            e.cancelBubble = true;
            if (e.stopPropagation) {
                e.stopPropagation();
            }
        }

        function onDropDownClosing() {
            cancelDropDownClosing = false;
        }

        function onCheckBoxClick(chk) {
            var combo = $find("<%= dlAddedBy.ClientID %>");
            //holds the text of all checked items
            var text = "";
            //holds the values of all checked items
            var values = "";
            //get the collection of all items
            var items = combo.get_items();
            //enumerate all items
            for (var i = 0; i < items.get_count(); i++) {
                var item = items.getItem(i);
                //get the checkbox element of the current item
                var chk1 = $get(combo.get_id() + "_i" + i + "_chk1");
                if (chk1.checked) {
                    text += item.get_text() + ",";
                    values += item.get_value() + ",";
                    //alert(item.get_value());
                }
            }
            //remove the last comma from the string
            text = removeLastComma(text);
            values = removeLastComma(values);


            if (text.length > 0) {
                //set the text of the combobox                   
                combo.set_text(text);
                document.getElementById(hiddenfld).value = values;

            }
            else {
                //all checkboxes are unchecked
                //so reset the controls
                combo.set_text("");
                document.getElementById(hiddenfld).value = '';
            }
        }

        //this method removes the ending comma from a string
        function removeLastComma(str) {
            return str.replace(/,$/, "");
        }
 
    </script>
    <script type="text/javascript">
        var hdnEntityType = ("<%= this.hdnEntityType.ClientID %>");
        var canceldlEntityTypeClosing = false;

        function StopPropagationEntityType(e) {
            //cancel bubbling
            e.cancelBubble = true;
            if (e.stopPropagation) {
                e.stopPropagation();
            }
        }

        function ondlEntityTypeClosing() {
            canceldlEntityTypeClosing = false;
        }

        function onEntityTypeClick(chk) {
            var combodlEntityType = $find("<%= dlEntityType.ClientID %>");
            //holds the text of all checked items
            var text = "";
            //holds the values of all checked items
            var values = "";
            //get the collection of all items
            var items = combodlEntityType.get_items();
            //enumerate all items
            for (var i = 0; i < items.get_count(); i++) {
                var item = items.getItem(i);
                //get the checkbox element of the current item
                var chkEntityType = $get(combodlEntityType.get_id() + "_i" + i + "_chkEntityType");
                if (chkEntityType.checked) {
                    text += item.get_text() + ",";
                    values += item.get_value() + ",";
                    //alert(item.get_value());
                }
            }
            //remove the last comma from the string
            text = removeLastComma(text);
            values = removeLastComma(values);


            if (text.length > 0) {
                //set the text of the combobox                   
                combodlEntityType.set_text(text);
                document.getElementById(hdnEntityType).value = values;

            }
            else {
                //all checkboxes are unchecked
                //so reset the controls
                combodlEntityType.set_text("");
                document.getElementById(hdnEntityType).value = '';
            }
        }


 
    </script>
    <script type="text/javascript">
        var hdnDocType = ("<%= this.hdnDocType.ClientID %>");
        var canceldlDocTypeClosing = false;

        function StopPropagationDocType(e) {
            //cancel bubbling
            e.cancelBubble = true;
            if (e.stopPropagation) {
                e.stopPropagation();
            }
        }

        function ondlDocTypeClosing() {
            canceldlDocTypeClosing = false;
        }

        function onDocTypeClick(chk) {
            var combodlDocumentType = $find("<%= dlDocumentType.ClientID %>");
            //holds the text of all checked items
            var text = "";
            //holds the values of all checked items
            var values = "";
            //get the collection of all items
            var items = combodlDocumentType.get_items();
            //enumerate all items
            for (var i = 0; i < items.get_count(); i++) {
                var item = items.getItem(i);
                //get the checkbox element of the current item
                var chkDocType = $get(combodlDocumentType.get_id() + "_i" + i + "_chkDocType");
                if (chkDocType.checked) {
                    text += item.get_text() + ",";
                    values += item.get_value() + ",";
                    //alert(item.get_value());
                }
            }
            //remove the last comma from the string
            text = removeLastComma(text);
            values = removeLastComma(values);


            if (text.length > 0) {
                //set the text of the combobox                   
                combodlDocumentType.set_text(text);
                document.getElementById(hdnDocType).value = values;

            }
            else {
                //all checkboxes are unchecked
                //so reset the controls
                combodlDocumentType.set_text("");
                document.getElementById(hdnDocType).value = '';
            }
        }
    </script>
    <%-- <script type="text/javascript">
        var hdnDocName = ("<%= this.hdnDocName.ClientID %>");
        var canceldlDocNameClosing = false;

        function StopPropagationDocName(e) {
            //cancel bubbling
            e.cancelBubble = true;
            if (e.stopPropagation) {
                e.stopPropagation();
            }
        }

        function ondlDocNameClosing() {
            canceldlDocNameClosing = false;
        }

        function onDocTitleClick(chk) {
            var combodlDocumentName = $find("<%= dlDocumentName.ClientID %>");
            //holds the text of all checked items
            var text = "";
            //holds the values of all checked items
            var values = "";
            //get the collection of all items
            var items = combodlDocumentName.get_items();
            //enumerate all items
            for (var i = 0; i < items.get_count(); i++) {
                var item = items.getItem(i);
                //get the checkbox element of the current item
                var chkDocTitle = $get(combodlDocumentName.get_id() + "_i" + i + "_chkDocTitle");
                if (chkDocTitle.checked) {
                    text += item.get_text() + ",";
                    values += item.get_value() + ",";
                    //alert(item.get_value());
                }
            }
            //remove the last comma from the string
            text = removeLastComma(text);
            values = removeLastComma(values);


            if (text.length > 0) {
                //set the text of the combobox                   
                combodlDocumentName.set_text(text);
                document.getElementById(hdnDocName).value = values;

            }
            else {
                //all checkboxes are unchecked
                //so reset the controls
                combodlDocumentName.set_text("");
                document.getElementById(hdnDocName).value = '';
            }
        }
    </script>--%>
    <%--  <script type="text/javascript">
        var hdnDscription = ("<%= this.hdnDscription.ClientID %>");
        var canceldlDesctiptionClosing = false;

        function StopPropagationDescription(e) {
            //cancel bubbling
            e.cancelBubble = true;
            if (e.stopPropagation) {
                e.stopPropagation();
            }
        }

        function ondlDescriptionClosing() {
            canceldlDesctiptionClosing = false;
        }

        function onDesctiptionClick(chk) {
            var combodlDescription = $find("<%= dlDescription.ClientID %>");
            //holds the text of all checked items
            var text = "";
            //holds the values of all checked items
            var values = "";
            //get the collection of all items
            var items = combodlDescription.get_items();
            //enumerate all items
            for (var i = 0; i < items.get_count(); i++) {
                var item = items.getItem(i);
                //get the checkbox element of the current item
                var chkDescription = $get(combodlDescription.get_id() + "_i" + i + "_chkDescription");
                if (chkDescription.checked) {
                    text += item.get_text() + ",";
                    values += item.get_value() + ",";
                    //alert(item.get_value());
                }
            }
            //remove the last comma from the string
            text = removeLastComma(text);
            values = removeLastComma(values);


            if (text.length > 0) {
                //set the text of the combobox                   
                combodlDescription.set_text(text);
                document.getElementById(hdnDscription).value = values;

            }
            else {
                //all checkboxes are unchecked
                //so reset the controls
                combodlDescription.set_text("");
                document.getElementById(hdnDscription).value
            }
        }
    </script>--%>
    <script type="text/javascript">
        var hdnFileType = ("<%= this.hdnFileType.ClientID %>");
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

        function onFileTypeClick(chk) {
            var combodlFileType = $find("<%= dlFileType.ClientID %>");
            //holds the text of all checked items
            var text = "";
            //holds the values of all checked items
            var values = "";
            //get the collection of all items
            var items = combodlFileType.get_items();
            //enumerate all items
            for (var i = 0; i < items.get_count(); i++) {
                var item = items.getItem(i);
                //get the checkbox element of the current item
                var chkFileType = $get(combodlFileType.get_id() + "_i" + i + "_chkFileType");
                if (chkFileType.checked) {
                    text += item.get_text() + ",";
                    values += item.get_text() + ",";
                    //alert(item.get_value());
                }
            }
            //remove the last comma from the string
            text = removeLastComma(text);
            values = removeLastComma(values);


            if (text.length > 0) {
                //set the text of the combobox                   
                combodlFileType.set_text(text);
                document.getElementById(hdnFileType).value = values;

            }
            else {
                //all checkboxes are unchecked
                //so reset the controls
                combodlFileType.set_text("");
                document.getElementById(hdnFileType).value = "";
            }
        }

        function DeleteAlert() {
            var r = confirm("Do you want to delete Document?");
            if (r == true) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <style type="text/css">
        .cb label
        {
            margin-left: 5px;
        }
        .style1
        {
            border: #bbdef1 1px solid;
            padding: 3px;
            font-weight: normal;
            font-size: 12px;
            color: #535152;
            font-family: Arial, Helvetica, sans-serif;
            text-decoration: none;
            height: 9px;
        }
    </style>
</asp:content>
