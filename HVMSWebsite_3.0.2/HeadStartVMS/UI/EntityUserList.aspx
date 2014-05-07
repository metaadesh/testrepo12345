<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="EntityUserList.aspx.cs"
    Inherits="METAOPTION.UI.EntityUserList" %>

<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfSelectedCount" runat="server" Value="0" />
    <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="RightPanel">
                <div class="TableHeadingBg TableHeading" style="text-align: left">
                    <div style="width: 90%; padding: 5px 0px; float: left">
                        User List</div>
                    <div id="dvCollapse" runat="server" class="TOtoggle" style="float: right; padding-right: 10px;
                        padding-top: 5px;">
                    </div>
                </div>
                <div id="dvSearch" runat="server" style="width: 100%; height: auto">
                   
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
                          
                        </table>
                    </div>
                     <div style="width: 31%; float: left; padding: 5px;">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                          
                            <tr>
                                <td class="TableBorder" style="width: 100px">
                                    Status
                                </td>
                                <td class="TableBorder" style="width: 200px">
                                    <div style="float: left; margin-right: 10px">
                                        <asp:DropDownList ID="ddlActiveStatus" runat="server" CssClass="txt2">
                                            <asp:ListItem Text="ALL" Value="-1" />
                                            <asp:ListItem Text="Active" Value="1" />
                                            <asp:ListItem Text="In-Active" Value="0" />
                                        </asp:DropDownList>
                                    </div>
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
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" CssClass="btn"
                                        OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse;">
                    <tr>
                        <td>
                            <asp:GridView ID="GrdUser" runat="server" AutoGenerateColumns="False" Width="100%"
                                EmptyDataText="Record not found" GridLines="None" AllowSorting="true" DataKeyNames="EmployeeID"
                                OnSorting="GrdUser_OnSorting" OnRowDataBound="GrdUser_RowDataBound" AllowPaging="true" 
                                OnPageIndexChanging="GrdUser_PageIndexChanging" PageSize="20">
                                <Columns>
                                   <%-- <asp:TemplateField HeaderText="Select" HeaderStyle-Width="22px" ItemStyle-CssClass="GridContent">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkall" runat="server" onclick="javascript:SelectAllCheckboxes(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName"
                                        ItemStyle-Width="100px" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent"
                                        />
                                    <asp:BoundField DataField="DisplayName" HeaderText="Display Name" SortExpression="DisplayName"
                                        ItemStyle-Width="120px" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent"
                                         />
                                    <asp:BoundField DataField="UserNote" HeaderText="User Note" ItemStyle-Width="150px"
                                        ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" />
                                    <asp:BoundField DataField="EntityType" HeaderText="Roles" 
                                        ItemStyle-Width="100px" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent"
                                       />
                                    <asp:BoundField DataField="Permission" HeaderText="Permission" ItemStyle-CssClass="GridContent"
                                        HeaderStyle-CssClass="GridContent" />
                                  <%--  <asp:TemplateField ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="EntityTypeID" runat="server" Value='<%#Eval("EntityTypeID")%>' />
                                            <asp:HiddenField ID="hfUserStatus" runat="server" Value='<%#Eval("IsActive")%>' />
                                             <asp:HyperLink ID="hylnkEdit" runat="server" ImageUrl="~/Images/newedit.gif" NavigateUrl='<%# "User.aspx?Code="+Eval("EmployeeId")+"&Mode=edit"+ "&ReturnUrl="+HttpUtility.UrlEncode(Request.Url.AbsoluteUri).ToString() %>'
                                                ToolTip="Edit"></asp:HyperLink>
                                             <asp:HyperLink ID="hylnkSetUsrPwd" runat="server" ImageUrl="~/Images/ChangePassword.png"
                                                NavigateUrl='<%# "AdminChangePassword.aspx?Code="+Eval("EmployeeId")+ " + &UserName="+Eval("UserName")+ " &ReturnUrl="+HttpUtility.UrlEncode(Request.Url.AbsoluteUri).ToString() %>'
                                                ToolTip="Change Password"></asp:HyperLink>
                                            <asp:ImageButton ID="ibtnUserStatus" runat="server" OnClick="ibtnUserStatus_Click"
                                                OnClientClick="javascript:return confirm('Are you sure you want to change active status?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                                <RowStyle CssClass="gvRow" />
                                <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                <HeaderStyle CssClass="gvHeading"></HeaderStyle>
                                <AlternatingRowStyle CssClass="gvAlternateRow"></AlternatingRowStyle>
                                <EmptyDataRowStyle CssClass="gvEmpty" />
                            </asp:GridView>                            
                        </td>
                    </tr>
                    <caption>
                        <br />
                    </caption>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
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
</asp:content>
