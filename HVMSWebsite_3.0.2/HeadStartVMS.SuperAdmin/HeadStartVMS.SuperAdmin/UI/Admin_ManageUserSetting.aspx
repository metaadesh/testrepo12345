<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/Admin_Master.Master"
    CodeBehind="Admin_ManageUserSetting.aspx.cs" Inherits="METAOPTION.UI.Admin_ManageUserSetting"
    Title="Admin Panel :: User List" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="cphEmpList" runat="server">
    <style type="text/css">
        .trlightBlue
        {
            background-color: #F0FAFF;
            border-color: #F0FAFF;
        }
        .tdNoWrap
        {
            white-space: nowrap;
            text-align: center;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function SelectAllCheckboxes(chk) {
            var counter = 0;
            $('#<%=GrdUser.ClientID%>').find("input:checkbox").each(function () {
                if (this != chk) {
                    this.checked = chk.checked;
                    counter = counter + 1;
                }
            });

            document.getElementById("ctl00_ContentPlaceHolder1_hfSelectedCount").value = chk.checked == true ? counter : "0";

        }
        function ShowEmailAlert() {
            var r = confirm("Do you want to send the email?");
            if (r == true) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <asp:HiddenField ID="hfSelectedCount" runat="server" Value="0" />
    <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="RightPanel">
                <fieldset class="ForFieldSet">
                    <legend class="ForLegend" align="left">Manage User Setting</legend>
                    <div class="TableBorder" style="background-color: #F0FAFF;">
                        <table width="100%" cellspacing="0">
                            <tr style="background-color: #F0FAFF;">
                                <td>
                                    <table cellspacing="0" cellpadding="0" style="padding: 0; border-spacing: none;">
                                        <tr class="trlightBlue" style="background-color: #F0FAFF;">
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="Organization" CssClass="TableBorderLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlOrganization" runat="server" Width="180px" AutoPostBack="false"
                                                    CssClass="txtMan2" Style="font-size: 11px;">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr class="trlightBlue" style="background-color: #F0FAFF;">
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text="Status" CssClass="TableBorderLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <div style="float: left; margin-right: 10px">
                                                    <asp:DropDownList ID="ddlActiveStatus" Width="180px" runat="server" CssClass="txt2"
                                                        Style="font-size: 11px;">
                                                        <asp:ListItem Text="ALL" Value="-1" />
                                                        <asp:ListItem Text="Active" Value="1" />
                                                        <asp:ListItem Text="In-Active" Value="0" />
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr class="trlightBlue" style="background-color: #F0FAFF;">
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="User Name" CssClass="TableBorderLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtUserName" runat="server" MaxLength="17" CssClass="txt2" Width="178px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table cellspacing="0" cellpadding="0">
                                        <tr class="trlightBlue" style="background-color: #F0FAFF;">
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="Roles" CssClass="TableBorderLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlEntityType" runat="server" CssClass="txt2" AutoPostBack="false"
                                                    Style="font-size: 11px;" Width="180px" />
                                            </td>
                                        </tr>
                                        <tr class="trlightBlue" style="background-color: #F0FAFF;">
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text=" Display Name" CssClass="TableBorderLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDisplayName" runat="server" MaxLength="17" CssClass="txt2" Width="178px" />
                                            </td>
                                        </tr>
                                        <tr class="trlightBlue" style="background-color: #F0FAFF;">
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" CssClass="btn"
                                                    OnClick="btnSearch_Click" Style="margin-right: 1px;" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%--  <div id="dvSearch" runat="server" style="width: 100%; height: auto">
                        <div style="width: 31%; float: left; padding: 5px;">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td class="TableBorder">
                                        Roles
                                    </td>
                                    <td class="TableBorder">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorder" style="width: 100px">
                                    </td>
                                    <td class="TableBorder" style="width: 200px">
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="width: 28%; float: left; padding: 5px 5px 5px 5px">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td class="TableBorder">
                                    </td>
                                    <td class="TableBorder">
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="width: 30%; float: left; padding: 5px">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td class="TableBorder" style="vertical-align: top; width: 85px">
                                    </td>
                                    <td class="TableBorder" style="width: 250px">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="150" AssociatedUpdatePanelID="updMain">
                                            <ProgressTemplate>
                                                <div id="dvProg" class="overlay">
                                                    <img id="image" src="../Images/Wait.gif" alt="Wait..." style="vertical-align: middle" />&nbsp;Please
                                                    wait...
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                    <td style="padding: 7px 0px 0px 10px">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>--%>
                    <div style="min-height: 300px;">
                        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse;">
                            <tr>
                                <td>
                                    <asp:GridView ID="GrdUser" runat="server" AutoGenerateColumns="False" Width="100%"
                                        EmptyDataText="No Record Found" GridLines="None" AllowSorting="true" DataKeyNames="EmployeeID"
                                        OnSorting="GrdUser_OnSorting" OnRowDataBound="GrdUser_RowDataBound" AllowPaging="True"
                                        OnPageIndexChanging="GrdUser_PageIndexChanging" PageSize="20">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select" HeaderStyle-Width="22px" ItemStyle-CssClass="GridContent">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkall" runat="server" onclick="javascript:SelectAllCheckboxes(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="false" /><%-- OnCheckedChanged="chkSelect_OnCheckedChanged"--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName"
                                                ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" ItemStyle-Width="100px">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DisplayName" HeaderText="Display Name" SortExpression="DisplayName"
                                                ItemStyle-Width="120px" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UserNote" HeaderText="User Note" ItemStyle-Width="100px"
                                                ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent"></asp:BoundField>
                                            <asp:BoundField DataField="EntityType" HeaderText="Roles" SortExpression="EntityType"
                                                ItemStyle-Width="50px" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Permission" HeaderText="Permission" ItemStyle-CssClass="GridContent"
                                                ItemStyle-Width="350px" HeaderStyle-CssClass="GridContent"></asp:BoundField>
                                            <asp:BoundField DataField="OrgCode" HeaderText="Organization" ItemStyle-CssClass="GridContent"
                                                HeaderStyle-CssClass="GridContent"></asp:BoundField>
                                            <%--<asp:TemplateField ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="25px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Width="25px" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Center" HeaderText="Action"
                                                ItemStyle-CssClass="tdNoWrap">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hylnkEdit" runat="server" ImageUrl="~/Images/newedit.gif" NavigateUrl='<%# "Admin_User.aspx?Code="+Eval("EmployeeId")+"&Mode=edit"+ "&ReturnUrl="+HttpUtility.UrlEncode(Request.Url.AbsoluteUri).ToString() %>'
                                                        ToolTip="Edit"></asp:HyperLink>
                                                    <asp:HyperLink ID="hylnkSetUsrPwd" runat="server" ImageUrl="~/Images/ChangePassword.png"
                                                        NavigateUrl='<%# "Admin_ChangePassword.aspx?Code="+Eval("EmployeeId")+"&ReturnUrl="+HttpUtility.UrlEncode(Request.Url.AbsoluteUri).ToString() %>'
                                                        ToolTip="Change Password"></asp:HyperLink>
                                                    <asp:HiddenField ID="hdnEntityId" runat="server" Value='<%#Eval("EntityID")%>' />
                                                    <asp:HiddenField ID="hdnUserPassword" runat="server" Value='<%#Eval("UserPassword")%>' />
                                                    <asp:HiddenField ID="EntityTypeID" runat="server" Value='<%#Eval("EntityTypeID")%>' />
                                                    <asp:HiddenField ID="hdnContactId" runat="server" Value='<%#Eval("ContactId")%>' />
                                                    <asp:HiddenField ID="hfUserStatus" runat="server" Value='<%#Eval("IsActive")%>' />
                                                    <asp:ImageButton ID="ibtnUserStatus" runat="server" OnClick="ibtnUserStatus_Click"
                                                        OnClientClick="javascript:return confirm('Are you sure you want to change active status?');" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="gvRow" />
                                        <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                        <HeaderStyle CssClass="gvHeading"></HeaderStyle>
                                        <AlternatingRowStyle CssClass="gvAlternateRow"></AlternatingRowStyle>
                                        <EmptyDataRowStyle CssClass="gvEmptyBlue" />
                                    </asp:GridView>
                                    <div style="padding-top: 10px">
                                        <asp:Button ID="btnAddNewUser" runat="server" OnClick="btnAddNewUser_Click" Text="Add New User"
                                            CssClass="Btn_Form" />
                                        <asp:Button ID="btnSendPass" runat="server" Text="Send Email" CssClass="Btn_Form"
                                            OnClick="btnSendPass_Click" OnClientClick="return ShowEmailAlert();" />
                                    </div>
                                </td>
                            </tr>
                            <caption>
                                <br />
                            </caption>
                        </table>
                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
