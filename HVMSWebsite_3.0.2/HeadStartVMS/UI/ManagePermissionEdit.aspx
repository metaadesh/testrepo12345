<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagePermissionEdit.aspx.cs"
    Inherits="METAOPTION.UI.ManagePermissionEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../CSS/AjaxRelated.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ControlStyle.css" rel="stylesheet" type="text/css" />
    <script src="../CSS/ext-core-debug.js" type="text/javascript"></script>
    <link href="../CSS/MainStyle.css" rel="stylesheet" type="text/css" />
    <script src="../CSS/jquery-1.2.6.min.js" type="text/javascript"></script>
    <script src="../CSS/jquery-1.7.2.min.js" type="text/javascript"></script>
</head>
<body style="background-color: White;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hdnEntity" runat="server" />
    <asp:HiddenField ID="hdnEntitiesId" runat="server" />
    <asp:HiddenField ID="hdnIPPermissionId" runat="server" />
    <asp:UpdatePanel ID="update1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <div class="PopUpBoxHeading">
                    <table>
                        <tr style="position: relative;">
                            <td style="width: 100%; color: White">
                                &nbsp;&nbsp;Edit IP Permission
                            </td>
                            <td style="padding-right: 5px;">
                                <img border="0" src="../Images/close.gif" id="img3" alt="close" onclick="parent.HideModelpopup();" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="float: left; width: 98.5%; padding: 5px">
                    <div style="width: 30%; float: left; padding: 5px;">
                        <table>
                            <tr>
                                <td class="TableBorder" style="width: 200px">
                                    IP Address&nbsp;
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtIP" runat="server" CssClass="txt2" onblur="validate(this);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtIP"
                                        Display="Dynamic" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 38%; float: left; padding: 5px">
                        <table>
                            <tr>
                                <td class="TableBorder">
                                    Description&nbsp;
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="txt2" Width="210px"></asp:TextBox>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 25%; float: left; padding: 5px">
                    </div>
                    <div style="clear: both;">
                        <asp:GridView ID="gvEditIPPermission" runat="server" AutoGenerateColumns="False"
                            Width="100%" GridLines="None" EmptyDataText="No record found" DataKeyNames="IPUserID"
                            OnRowDataBound="gvEditIPPermission_RowDataBound">
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" HeaderText="Type">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdSystemIPRestriction" runat="server" Value='<%#Eval("IPRestriction") %>' />                                       
                                        <asp:HiddenField ID="hdIPUserId" runat="server" Value='<%#Eval("IPUserID") %>' />
                                        <asp:HiddenField ID="hdIPAddress" runat="server" Value='<%#Eval("IPAddress") %>' />
                                        <asp:HiddenField ID="hdnEntityType" runat="server" Value='<%#Eval("EntityTypeID") %>' />
                                        <asp:HiddenField ID="hdnIPType" runat="server" Value='<%#Eval("IPType") %>' />
                                        <asp:Label ID="lblEntityType" runat="server" Text='<%#Eval("IPTypeValue") %>' Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="EntityName" HeaderText="User" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Date Added<br />AddedBy"
                                    ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader">
                                    <ItemTemplate>
                                        <%#Eval("DateAdded")%><br />
                                        <%#Eval("AddedBy")%><br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Date Modified<br />ModifiedBy"
                                    ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader">
                                    <ItemTemplate>
                                        <%#Eval("DateModified")%><br />
                                        <%#Eval("ModifiedBy")%><br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Action" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td style=" display :none;">
                                                    <img border="0" src="../Images/on.png" runat="server" id="imgOn" alt="close" title="IP Restriction" onclick="SetIPRestriction(this,'1');" />
                                                    <img border="0" src="../Images/off.png" runat="server" id="imgOff" alt="close" title="IP Restriction" onclick="SetIPRestriction(this,'0');" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="lnkDelete" ToolTip="Delete" runat="server" ImageUrl="~/Images/DeleteButton.jpg"
                                                        OnClick="lnkDelete_Click" CausesValidation="false" Width="32px" OnClientClick="javascript:return confirm('Do you want to remove user from this IP group?\n\nClick Ok to delete.\nYou won\'t be able to undo those changes.');" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle CssClass="gvRow" />
                            <AlternatingRowStyle CssClass="gvAlternateRow" />
                            <HeaderStyle CssClass="gvHeading" />
                            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                            <EmptyDataRowStyle CssClass="gvEmpty" />
                        </asp:GridView>
                    </div>
                    <div style="width: 99%; float: left; text-align: right; margin: 5px">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn" Text="Update" OnClick="btnSubmit_Click" Width="80px" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Close" Width="80px" OnClientClick="parent.HideModelpopup();" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
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
       
    </script>
    <script language="javascript" type="text/javascript">

        function SetIPRestriction(ctrl, ShowHideFlag) {
            var ID = ctrl.id;
            var imgID = ID.split('_');
            var IPuserId = document.getElementById('gvEditIPPermission_' + imgID[1] + '_hdIPUserId').value; //gvEditIPPermission_ctl02_imgOn
            var hdnIPType = document.getElementById('gvEditIPPermission_' + imgID[1] + '_hdnIPType').value;
            var IPPermissionID = document.getElementById("hdnIPPermissionId").value;

            if (IPuserId == "")
                IPuserId = "2";
            else
                IPuserId = IPuserId;

            var SentFlag;

            if (ShowHideFlag == "1")
                SentFlag = "0";
            else
                SentFlag = 1;

            $.ajax({ type: "POST",
                url: "ManagePermissionEdit.aspx/UpdateAutoIPApproval",
                data: "{Flag:" + SentFlag + ",IPUserID:" + IPuserId + ",IPPermissionId:" + IPPermissionID + ",IPType:" + hdnIPType + "}",
                contentType: "application/json; charset=utf-8",
                async: true,
                cache: false,
                error: Failed,
                success: function () {
                    if (hdnIPType == "1") {
                        if (ShowHideFlag == "1") {
                            $("#gvEditIPPermission_" + imgID[1] + "_imgOn").hide();
                            $("#gvEditIPPermission_" + imgID[1] + "_imgOff").show();
                        }
                        else {
                            $("#gvEditIPPermission_" + imgID[1] + "_imgOn").show();
                            $("#gvEditIPPermission_" + imgID[1] + "_imgOff").hide();
                        }
                    }
                    else {
                        if (ShowHideFlag == "1") {
                            $("#gvEditIPPermission_" + imgID[1] + "_imgOn").hide();
                            $("#gvEditIPPermission_" + imgID[1] + "_imgOff").show();
                        }
                        else {
                            $("#gvEditIPPermission_" + imgID[1] + "_imgOn").show();
                            $("#gvEditIPPermission_" + imgID[1] + "_imgOff").hide();
                        }
                    }
                }
            });
        }

        //Failed Function
        function Failed(result) {
            alert(result.status + " " + result.statusText);
        }


    </script>
    </form>
</body>
</html>
