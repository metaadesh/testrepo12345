<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeList.aspx.cs" Inherits="METAOPTION.UI.UserList"
    Title="HeadstartVMS::User List" %>

<asp:content contentplaceholderid="ContentPlaceHolder1" id="cphEmpList" runat="server">
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
                <%-- <div class="AddHeading" style="padding-bottom: 10px;">
                    User List
                </div>--%>
                <div class="TableHeadingBg TableHeading" style="text-align: left">
                    <div style="width: 90%; padding: 5px 0px; float: left">
                        User List</div>
                    <div id="dvCollapse" runat="server" class="TOtoggle" style="float: right; padding-right: 10px;
                        padding-top: 5px;">
                    </div>
                </div>
                <div id="dvSearch" runat="server" style="width: 100%; height: auto">
                    <div style="width: 31%; float: left; padding: 5px;">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="TableBorder">
                                    Roles
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlEntityType" runat="server" CssClass="txt2" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlEntityType_SelectedIndexChanged" /><%----%>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder" style="width: 100px">
                                    Status
                                </td>
                                <td class="TableBorder" style="width: 200px">
                                    <div style="float: left; margin-right: 10px">
                                        <asp:DropDownList ID="ddlActiveStatus" runat="server" CssClass="txt2" 
                                            ><%--OnSelectedIndexChanged="ddlActiveStatus_SelectedIndexChanged"--%>
                                            <asp:ListItem Text="ALL" Value="-1" />
                                            <asp:ListItem Text="Active" Value="1" />
                                            <asp:ListItem Text="In-Active" Value="0" />
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 28%; float: left; padding: 5px 5px 5px 5px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="TableBorder">
                                    User Name
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtUserName" runat="server" MaxLength="17" CssClass="txt2" Height="16px" />
                                </td>
                            </tr>
                            <%--      <tr>
                                <td class="TableBorder">
                                    Permission
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtPermission" runat="server" autocomplete="off" Wrap="false" CssClass="txt2"
                                        Height="16px" ToolTip="Type at least two characters to find customer name started with i.e MA or %MA to find all customer names having characters entered" />
                                    <div style="float: left;">
                                        <ajax:AutoCompleteExtender ID="txtTest_AutoCompleteExtender" runat="server" TargetControlID="txtPermission"
                                            ServicePath="../WS/AutoFillCustomers.asmx" ServiceMethod="AutoFillCustomersDetails"
                                            EnableCaching="true" MinimumPrefixLength="2" CompletionSetCount="25" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            DelimiterCharacters=";, :" OnClientPopulated="onListPopulated" BehaviorID="AutoCompleteset">
                                        </ajax:AutoCompleteExtender>
                                    </div>
                                </td>
                            </tr>--%>
                        </table>
                    </div>
                    <div style="width: 30%; float: left; padding: 5px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="TableBorder" style="vertical-align: top; width: 85px">
                                    Display Name
                                </td>
                                <td class="TableBorder" style="width: 250px">
                                    <asp:TextBox ID="txtDisplayName" runat="server" MaxLength="17" CssClass="txt2" Height="16px" />
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
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" 
                                        CssClass="btn" onclick="btnSearch_Click" />                                    
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse;">
                    <tr>
                        <td>
                          <%--  <fieldset class="ForFieldset">--%>
                               <%-- <legend class="ForLegend">User List</legend>--%>
                                <asp:GridView ID="GrdUser" runat="server" AutoGenerateColumns="False" Width="100%"
                                    EmptyDataText="Record not found" GridLines="None" AllowSorting="true" DataKeyNames="EmployeeID"
                                    OnSorting="GrdUser_OnSorting" OnRowDataBound="GrdUser_RowDataBound">
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
                                            ItemStyle-Width="100px" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent"
                                            HeaderStyle-ForeColor="Blue" />
                                        <asp:BoundField DataField="DisplayName" HeaderText="Display Name" SortExpression="DisplayName"
                                            ItemStyle-Width="120px" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent"
                                            HeaderStyle-ForeColor="Blue" />
                                        <asp:BoundField DataField="UserNote" HeaderText="User Note" ItemStyle-Width="150px"
                                            ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="EntityType" HeaderText="Roles" SortExpression="EntityType"
                                            ItemStyle-Width="100px" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent"
                                            HeaderStyle-ForeColor="Blue" />
                                        <asp:BoundField DataField="Permission" HeaderText="Permission" ItemStyle-CssClass="GridContent"
                                            HeaderStyle-CssClass="GridContent" />
                                        <asp:TemplateField ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hylnkEdit" runat="server" ImageUrl="~/Images/newedit.gif" NavigateUrl='<%# "User.aspx?Code="+Eval("EmployeeId")+"&Mode=edit"+ "&ReturnUrl="+HttpUtility.UrlEncode(Request.Url.AbsoluteUri).ToString() %>'
                                                    ToolTip="Edit"></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hylnkSetUsrPwd" runat="server" ImageUrl="~/Images/ChangePassword.png"
                                                    NavigateUrl='<%# "AdminChangePassword.aspx?Code="+Eval("EmployeeId")+ " + &UserName="+Eval("UserName")+ " &ReturnUrl="+HttpUtility.UrlEncode(Request.Url.AbsoluteUri).ToString() %>'
                                                    ToolTip="Change Password"></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
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
                                    <EmptyDataRowStyle CssClass="gvEmpty" />
                                </asp:GridView>
                                <div style="padding: 10px">
                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Add New User"
                                        CssClass="Btn_Form" />
                                    <asp:Button ID="btnSendPass" runat="server" Text="Send Email" CssClass="Btn_Form"
                                        OnClick="btnSendPass_Click" OnClientClick="return ShowEmailAlert();" />
                                </div>
                           <%-- </fieldset>--%>
                        </td>
                    </tr>
                    <caption>
                        <br />
                    </caption>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:content>
