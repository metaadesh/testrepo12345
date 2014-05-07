<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LiveUsers.aspx.cs" Inherits="METAOPTION.UI.LiveUsers"
    Title="HeadstartVMS::Live Users" MasterPageFile="~/UI/MasterPage.Master" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="cphEmpList" runat="server">
    <script language="JavaScript" type="text/javascript">
        function showHideContent() {
            var elem = document.getElementById('<%=divShowWebHistory.ClientID %>');
            var imgObj = document.getElementById('imgArrow');
            if (elem.style.display == 'none') {
                elem.style.display = 'block';
                elem.style.display = '';
                imgObj.src = "../Images/arrow_right.jpg";
            }
            else {
                elem.style.display = 'none';
                imgObj.src = "../Images/arrow_down.jpg";
            }
            return false;
        }       
    </script>
    <style type="text/css">
        #aLoginHistory:hover
        {
            color: #6490AE;
        }
    </style>
    <div class="RightPanel">
        <div style="margin-bottom: 8px;">
            <a id="aLoginHistory" onclick="return showHideContent();" class="AddHeading" style="cursor: pointer;">
                Login History<img alt="" style="margin-left: 10px;" src="../Images/arrow_down.jpg"
                    id="imgArrow" name="arrow_up" />
            </a>
            <%--<asp:LinkButton ID="lnkWebHistory" class="AddHeading" runat="server" OnClientClick="return showHideContent();"
                Text="Login History"></asp:LinkButton>
            <img alt="" src="../Images/arrow_down.jpg" id="imgColapse" name="arrow_up" />--%>
        </div>
        <div id="divShowWebHistory" runat="server" style="display: none; margin-left: 6px;">
            <asp:GridView ID="gvWebLoginHistory" runat="server" Width="100%" GridLines="None"
                AllowPaging="true" PageSize="20" AutoGenerateColumns="false" EmptyDataText="No Rows found"
                OnPageIndexChanging="gvWebLoginHistory_PageIndexChanging">
                <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                <Columns>
                    <asp:BoundField DataField="LastLogin" HeaderText="Last Login" />
                    <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                    <asp:BoundField DataField="EntityType" HeaderText="Employee Type" />
                    <asp:BoundField DataField="IpAddress" HeaderText="IP Address" />
                    <asp:BoundField DataField="Is_Attempt_Successful" HeaderText="Successful" />
                </Columns>
                <AlternatingRowStyle CssClass="gvAlternateRow" />
                <HeaderStyle CssClass="gvHeading" />
                <RowStyle CssClass="gvRow" />
                <EmptyDataRowStyle HorizontalAlign="Center" />
            </asp:GridView>
        </div>
        <br />
        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
            <tr>
                <td class="AddHeading">
                    Live Users
                </td>
            </tr>
            <tr>
                <td>
                    <div style="margin-left: 6px;">
                        <asp:GridView ID="gvLiveUsers" runat="server" Width="100%" GridLines="None" DataKeyNames="EntityId"
                            AllowPaging="true" PageSize="20" AutoGenerateColumns="False" EmptyDataText="No Rows found"
                            OnPageIndexChanging="gvLiveUsers_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkViewEmployee" NavigateUrl='<%# GetEditURL(Eval("EntityTypeID")) %>'
                                            ImageUrl="~/Images/edit-icon.jpg" Text="" runat="server"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="UserName" HeaderText="User Name" />
                                <asp:BoundField DataField="DisplayName" HeaderText="Display Name" />
                                <asp:BoundField DataField="EntityType" HeaderText="User Type" />
                                <asp:BoundField DataField="UserRole" HeaderText="User Role" />
                                <asp:BoundField DataField="LoginTime" HeaderText="Login Time" />
                            </Columns>
                            <AlternatingRowStyle CssClass="gvAlternateRow" />
                            <HeaderStyle CssClass="gvHeading" />
                            <RowStyle CssClass="gvRow" />
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                        </asp:GridView>
                    </div>
                    <asp:Timer ID="timerLiverUser" runat="server" Interval="300000" OnTick="timerLiverUser_Tick">
                    </asp:Timer>
                </td>
            </tr>
            <tr>
                <td class="height30">
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td class="AddHeading">
                    Access Login History
                </td>
            </tr>
            <tr>
                <td>
                    <div style="margin-left: 6px;">
                        <asp:GridView ID="gvAccessLoginHistory" runat="server" Width="100%" GridLines="None"
                            AllowPaging="true" PageSize="20" AutoGenerateColumns="False" EmptyDataText="No Rows found"
                            OnPageIndexChanging="gvAccessLoginHistory_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="LastLogin" HeaderText="Last Login" />
                                <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                <asp:BoundField DataField="EntityType" HeaderText="Employee Type" />
                                <asp:BoundField DataField="IpAddress" HeaderText="IP Address" />
                                <asp:BoundField DataField="Is_Attempt_Successful" HeaderText="Successful" />
                            </Columns>
                            <AlternatingRowStyle CssClass="gvAlternateRow" />
                            <HeaderStyle CssClass="gvHeading" />
                            <RowStyle CssClass="gvRow" />
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                        </asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
