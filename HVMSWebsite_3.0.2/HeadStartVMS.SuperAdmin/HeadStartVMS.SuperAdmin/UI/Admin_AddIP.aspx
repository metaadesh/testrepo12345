﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_AddIP.aspx.cs" Inherits="METAOPTION.UI.Admin_AddIP" Title="Admin Panel :: Add IP"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/AjaxRelated.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/ControlStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Styles/ext-core-debug.js" type="text/javascript"></script>
    <link href="../Styles/MainStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Styles/jquery-1.2.6.min.js" type="text/javascript"></script>
    <script src="../Styles/jquery-1.7.2.min.js" type="text/javascript"></script>
</head>
<body style="background-color: White;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hdnEntity" runat="server" />
    <asp:HiddenField ID="hdnEntitiesId" runat="server" />
    <asp:UpdatePanel ID="update1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="width: 900px">
                <div class="PopUpBoxHeading">
                    <table>
                        <tr style="position: relative;">
                            <td style="width: 100%; color: White">
                                &nbsp;&nbsp;Add IP Permission
                            </td>
                            <td style="padding-right: 5px;">
                                <img border="0" src="../Images/close.gif" id="img3" alt="close" onclick="parent.HideAddEditDialogEmail();" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="float: left; width: 100%">
                    <div>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="TableBorder">
                                    <asp:Label ID="Label1" runat="server" Text="Organization" CssClass="TableBorderLabel"></asp:Label>
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlOrganization" runat="server" Width="180px" AutoPostBack="true"
                                        CssClass="txtMan2" Style="font-size: 11px;" OnSelectedIndexChanged="ddlOrganization_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="TableBorder">
                                    <asp:Label ID="Label2" runat="server" Text="IP Address" CssClass="TableBorderLabel"></asp:Label>
                                </td>
                                <td class="TableBorder" style="white-space: nowrap;">
                                    <asp:TextBox ID="txtIP" runat="server" CssClass="txt2" Width="175px" onblur="validate(this);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtIP"
                                        Display="Dynamic" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                                <td class="TableBorder">
                                    <asp:Label ID="Label5" runat="server" Text="Description" CssClass="TableBorderLabel"></asp:Label>
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="txt2" Width="175px"></asp:TextBox>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    <asp:Label ID="Label3" runat="server" Text="Entity Type" CssClass="TableBorderLabel"></asp:Label>
                                </td>
                                <td class="TableBorder" style="white-space: nowrap;">
                                    <asp:DropDownList ID="ddlEntityType" runat="server" CssClass="txt2" AutoPostBack="true"
                                        Width="180px" OnSelectedIndexChanged="ddlEntityType_SelectedIndexChanged">
                                      
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlEntityType"
                                        InitialValue="-1" ErrorMessage="*" Display="Dynamic" />
                                </td>
                                <td class="TableBorder">
                                    <asp:Label ID="Label4" runat="server" Text="User" CssClass="TableBorderLabel"></asp:Label>
                                </td>
                                <td class="TableBorder" style="white-space: nowrap;">
                                    <telerik:RadComboBox ID="dlEntity" runat="server" Width="180px" AllowCustomText="true"
                                        EmptyMessage="">
                                        <ItemTemplate>
                                            <div onclick="StopPropagationFileType(event)" class="combo-item-template">
                                                <asp:HiddenField ID="hdnEntityId" runat="server" Value='<%#Eval("SecurityUserID")%>' />
                                                <asp:CheckBox runat="server" ID="chkEntity" CssClass="cb" Text='<%#Eval("EntityName")%>'
                                                    onclick="onEntityTypeClick(this)" />
                                            </div>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="dlEntity"
                                        InitialValue=" " ErrorMessage="*" Display="Dynamic" />
                                </td>
                                <td align="center" colspan="2" class="TableBorder">
                                    <asp:RadioButtonList ID="rdbIPType" runat="server" Width="180px" CssClass="rbl" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="rdbIPType_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="1" Selected="True">User IP</asp:ListItem>
                                        <asp:ListItem Value="2">System IP</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 100%; float: left; height: 70px">
                    </div>
                    <div style="width: 99%; float: left; text-align: center; margin: 5px">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn" Text="Save" OnClick="btnSubmit_Click"
                            OnClientClick="return CheckIPType();" Width="80px" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" Width="80px"
                            OnClientClick="parent.HideAddEditDialogEmail();" Style="margin-left: 20px" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script language="javascript" type="text/javascript">
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
    </script>
    </form>
</body>
</html>
