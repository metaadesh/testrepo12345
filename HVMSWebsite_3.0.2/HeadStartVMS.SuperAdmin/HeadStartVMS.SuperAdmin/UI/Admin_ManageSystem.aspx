<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/Admin_MasterLeftPanel.Master"
    CodeBehind="Admin_ManageSystem.aspx.cs" Inherits="METAOPTION.UI.Admin_ManageSystem"
    Title="Admin Panel :: Manage System" %>

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
            $('#<%=GrdSystem.ClientID%>').find("input:checkbox").each(function () {
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
            <div style="min-height: 450px; vertical-align: top;">
                <fieldset class="ForFieldSet">
                    <legend class="ForLegend" align="left">Manage System</legend>
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
                                                <asp:DropDownList ID="ddlActiveStatus" Width="180px" runat="server" CssClass="txt2"
                                                    Style="font-size: 11px;">
                                                    <asp:ListItem Text="ALL" Value="-1" />
                                                    <asp:ListItem Text="Active" Value="1" />
                                                    <asp:ListItem Text="In-Active" Value="0" />
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr class="trlightBlue" style="background-color: #F0FAFF;">
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text="System Name" CssClass="TableBorderLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSystemName" runat="server" MaxLength="17" CssClass="txt2" Width="175px" />
                                            </td>
                                        </tr>
                                        <tr class="trlightBlue" style="background-color: #F0FAFF;">
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" CssClass="btn"
                                                    OnClick="btnSearch_Click" Style="margin-right: 1px; margin-top: 2px;" />
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
                                    <asp:GridView ID="GrdSystem" runat="server" AutoGenerateColumns="False" Width="100%"
                                        EmptyDataText="No Record Found" GridLines="None" AllowSorting="false" DataKeyNames="SystemID"
                                        OnRowDataBound="GrdSystem_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="Organisation" HeaderText="Organization Name" SortExpression="SystemName"
                                                ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" />
                                            <asp:BoundField DataField="SystemName" HeaderText="System Name" SortExpression="SystemName"
                                                ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" />
                                            <asp:BoundField DataField="SystemActiveStatus" HeaderText="Active" ItemStyle-Width="150px"
                                                ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" />
                                            <asp:BoundField DataField="IsPeachTree" HeaderText="PeachTree" ItemStyle-Width="150px"
                                                ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" />
                                            <asp:TemplateField ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="tdNoWrap"
                                                HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hylnkEdit" runat="server" ImageUrl="~/Images/newedit.gif" NavigateUrl='<%# "Admin_AddSystem.aspx?Code="+Eval("SystemID")+"&Mode=edit"+ "&ReturnUrl="+HttpUtility.UrlEncode(Request.Url.AbsoluteUri).ToString() %>'
                                                        ToolTip="Edit"></asp:HyperLink>
                                                    <asp:HiddenField ID="hSystemId" runat="server" Value='<%#Eval("SystemID")%>' />
                                                    <asp:HiddenField ID="hSystemActiveStatus" runat="server" Value='<%#Eval("SystemActiveStatus")%>' />
                                                    <asp:ImageButton ID="ibtnSystemStatus" runat="server" OnClick="ibtnSystemStatus_Click"
                                                        OnClientClick="javascript:return confirm('Are you sure you want to change active status?');" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center" HeaderText="Action">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <RowStyle CssClass="gvRow" />
                                        <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                        <HeaderStyle CssClass="gvHeading"></HeaderStyle>
                                        <AlternatingRowStyle CssClass="gvAlternateRow"></AlternatingRowStyle>
                                        <EmptyDataRowStyle CssClass="gvEmptyBlue" />
                                    </asp:GridView>
                                    <div style="padding-top: 10px">
                                        <asp:Button ID="btnAddNewSystem" runat="server" OnClick="btnAddNewSystem_Click" Text="Add New System"
                                            CssClass="Btn_Form" />
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
