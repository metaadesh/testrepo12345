<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="NotificationSettings.aspx.cs" 
    Inherits="METAOPTION.UI.NotificationSettings" Title="HeadstartVMS::Notification Settings" %>

<asp:Content ID="conNotPreference" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div onmousemove="SetProgressPosition(event)">
    <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hfEntityTypeID" runat="server" Value="0" />
            <asp:HiddenField ID="hfEntityID" runat="server" Value="0" />
            <div class="TableHeadingBg TableHeading" style="text-align: left">
                <div style="width: 75%; padding: 0px 0px; float: left">
                    NOTIFICATION SETTINGS</div>
                <div style="float: right; padding-right: 10px; padding-top: 5px;">
                    <asp:Button ID="btnManageNotification" Text="Manage Notifications" class="btn" 
                        runat="server" OnClick="btnManageNotification_Click" 
                         />
                </div>
            </div>
            <div id="dvSearch" runat="server" class="dvSearch" style="display: block; width: 100%;">
                <div style="width: 32%; float: left; padding: 5px;">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td class="TableBorder">
                                Table
                            </td>
                            <td class="TableBorder">
                                <asp:DropDownList ID="ddlTableSearch" runat="server" CssClass="txt2"
                                    OnSelectedIndexChanged="ddlTableSearch_SelectedIndexChanged" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder" style="width: 100px">
                                Notification Type
                            </td>
                            <td class="TableBorder" style="width: 200px">
                                <asp:DropDownList ID="ddlNotifTypeSearch" runat="server" CssClass="txt2" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder">
                                Mail To
                            </td>
                            <td class="TableBorder">
                                <asp:TextBox ID="txtMailToSearch" runat="server" CssClass="txt2" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width: 26%; float: left; padding: 5px 5px 5px 10px">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td class="TableBorder">
                                Added By
                            </td>
                            <td class="TableBorder">
                                <asp:DropDownList ID="ddlAddedBySearch" runat="server" CssClass="txt2" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder">
                                Entity Type
                            </td>
                            <td class="TableBorder">
                                <asp:DropDownList ID="ddlEntityTypeSearch" runat="server" CssClass="txt2"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlEntityTypeSearch_SelectedIndexChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder">
                                Entity
                            </td>
                            <td class="TableBorder">
                                <asp:DropDownList ID="ddlEntitySearch" runat="server" CssClass="txt2" />
                            </td>
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
                            <td style="padding:10px; text-align:center" colspan="2">
                                <asp:Button ID="btnSearchNotification" runat="server" Text="Search" 
                                    CssClass="btn" OnClick="btnSearchNotification_Click" Width="80px" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnAddNotificationPreference" runat="server" Text="Add New" 
                                    CssClass="btn"  Width="80px" 
                                    OnClick="btnAddNotificationPreference_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <table border="0" style="width: 100%" class="TableHeadingBg TableHeading">
                <tr>
                    <td style="width: 40%">
                        <asp:Label ID="lblCount" runat="server" BorderColor="Transparent" BackColor="Transparent"
                            ForeColor="#21618C" />
                    </td>
                    <td style="width: 25%; text-align: right; white-space: nowrap; color: #21618C">
                        Page#&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlPaging" runat="server" CssClass="txt1" Width="50px" OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged"
                            AutoPostBack="true" />
                        of
                        <%= gvNotifications.PageCount%>
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
                <asp:GridView ID="gvNotifications" runat="server" AutoGenerateColumns="false"
                    GridLines="None" AllowPaging="true" Width="100%" PagerSettings-Visible="false"
                    OnRowDataBound="gvNotifications_RowDataBound"
                    EmptyDataText="No data available" EmptyDataRowStyle-CssClass="gvEmpty" 
                    DataKeyNames="NotificationPreferenceID" CssClass="Grid"
                    AllowSorting="true" OnSorting="gvNotifications_Sorting">
                    <Columns>
                        <asp:BoundField HeaderText="Table Name" DataField="TableName" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" SortExpression="TM.TableName" />
                        <asp:BoundField HeaderText="Notification Type" DataField="NotificationType" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" SortExpression="NT.NotificationType" />
                        <asp:BoundField HeaderText="Columns" DataField="ColumnDescription" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" SortExpression="TCM.ColumnDescription" />
                        <asp:TemplateField HeaderText="Mail To" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                            <ItemTemplate>
                                <%#Eval("MailToNames")%> (<%#Eval("Role")%>)<br />[<%#Eval("MailTo")%>]
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="CC To" DataField="CCTo" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" />
                        <asp:BoundField HeaderText="BCC To" DataField="BCCTo" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" />
                        <asp:TemplateField HeaderText="Added On<br />Added By" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                            <ItemTemplate>
                                <%#Eval("DateAdded")%><br /><%#Eval("AddedBy")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" >
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEmail" runat="server" ImageUrl="~/Images/car_icon.gif" OnClick="ibtnEmail_Click" />
                                <asp:HiddenField ID="hfEmail" runat="server" Value='<%#Eval("NotifyViaEmail") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SMS" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" >
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnSMS" runat="server" ImageUrl="~/Images/car_icon.gif" OnClick="ibtnSMS_Click" />
                                <asp:HiddenField ID="hfSMS" runat="server" Value='<%#Eval("NotifyViaSMS") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" >
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnDeleteNotPref" runat="server" ImageUrl="~/Images/DeleteButton1.png" ToolTip="Delete"
                                    OnClick="ibtnDeleteNotPref_Click" 
                                    OnClientClick="javascript:return (confirm ('Do you want to delete this preference?'));" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>    
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
                        Page#
                        <asp:DropDownList ID="ddlPaging1" runat="server" CssClass="txt1" Width="50px" OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged"
                            AutoPostBack="true" />
                        of
                        <%= gvNotifications.PageCount%>
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

            <asp:Panel ID="pnlAddNewPreference" runat="server" CssClass="modalPopup" Style="display: none;"
                    HorizontalAlign="Left" Width="700px">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td colspan="5" class="PopUpBoxHeading">
                                &nbsp;&nbsp;Add New Preference
                            </td>
                            <td class="PopUpBoxHeading" align="right">
                                <img border="0" src="../Images/close.gif" id="imgCloseAddNewPrefPopUp" onclick="return false;"
                                    alt="close" />
                            </td>
                        </tr>
                    </table>
                    <div style="padding:5px">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">                    
                            <tr>
                                <td class="lblb" style="width:15%">
                                    Entity Type
                                </td>
                                <td class="lbl" colspan="3">
                                    <asp:DropDownList ID="ddlEntityTypes" runat="server" CssClass="txt2" 
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlEntityTypes_SelectedIndexChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblb">
                                    Entity
                                </td>
                                <td class="lbl" colspan="3">
                                    <asp:DropDownList ID="ddlEntities" runat="server" CssClass="txt2"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlEntities_SelectedIndexChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblb">
                                    Table
                                </td>
                                <td class="lbl" colspan="3">
                                    <asp:DropDownList ID="ddlTableType" runat="server" CssClass="txt2"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlTableType_SelectedIndexChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblb">
                                    Notification Type
                                </td>
                                <td class="lbl" colspan="3">
                                    <asp:DropDownList ID="ddlNotificationType" runat="server" CssClass="txt2"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlNotificationType_SelectedIndexChanged" />
                                </td>
                            </tr>
                            <tr id="trColumns" runat="server" visible="false">
                                <td class="lblb" valign="top">Columns</td>
                                <td class="lbl" colspan="3">
                                    <asp:CheckBoxList ID="cblColumns" runat="server" CssClass="ChkBoxLst" class="cols" RepeatDirection="Vertical" RepeatColumns="3" Width="100%" />
                            
                                </td>
                            </tr>
                            <tr>
                                <td class="lblb">Notify via Email</td>
                                <td class="lbl"><asp:CheckBox ID="chkEmail" runat="server"  /></td>
                                <td class="lblb">Notify via SMS</td>
                                <td class="lbl"><asp:CheckBox ID="chkSMS" runat="server"  /></td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    <asp:UpdateProgress ID="uprgMain" runat="server" DisplayAfter="150" AssociatedUpdatePanelID="updMain">
                                        <ProgressTemplate>
                                            <div id="dvProg" class="overlay">
                                                <img id="image" src="../Images/Wait.gif" alt="Wait..." style="vertical-align: middle" />&nbsp;Please
                                                wait...
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                                <td class="TableBorder" colspan="3" align="center">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Add" CssClass="btn" 
                                        Width="100px" OnClick="btnSubmit_Click" OnClientClick="return validateControls()" />
                                    <br />
                                    <asp:Label ID="lblError" runat="server" CssClass="err" />
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="gvUserNotifications" runat="server" AutoGenerateColumns="false"
                            GridLines="None" Width="100%" 
                            EmptyDataText="No data available"
                            EmptyDataRowStyle-CssClass="gvEmpty" DataKeyNames="NotificationPreferenceID">
                            <Columns>
                                <asp:BoundField HeaderText="Table Name" DataField="TableName" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" />
                                <asp:BoundField HeaderText="Notification Type" DataField="NotificationType" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" />
                                <asp:BoundField HeaderText="Column" DataField="ColumnDescription" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" />
                                <asp:BoundField HeaderText="Added On" DataField="DateAdded" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" DataFormatString="{0:MM/dd/yyyy}" />
                                <asp:BoundField HeaderText="Added By" DataField="AddedBy" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" />
                                <asp:BoundField HeaderText="Modified On" DataField="DateModified" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" DataFormatString="{0:MM/dd/yyyy}" />
                                <asp:BoundField HeaderText="Modified By" DataField="ModifiedBy" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" />
                                <%--<asp:TemplateField HeaderText="Email" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" >
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibtnNotifyViaEmail" runat="server" ImageUrl="~/Images/car_icon.gif" OnClick="ibtnNotifyViaEmail_Click" />
                                        <asp:HiddenField ID="hfNotifyViaEmail" runat="server" Value='<%#Eval("NotifyViaEmail") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SMS" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" >
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibtnNotifyViaSMS" runat="server" ImageUrl="~/Images/car_icon.gif" OnClick="ibtnNotifyViaSMS_Click" />
                                        <asp:HiddenField ID="hfNotifyViaSMS" runat="server" Value='<%#Eval("NotifyViaSMS") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" >
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibtnDeleteUserNotPref" runat="server" ImageUrl="~/Images/DeleteButton1.png" ToolTip="Delete"
                                            OnClick="ibtnDeleteUserNotPref_Click" 
                                            OnClientClick="javascript:return (confirm ('Do you want to delete this preference?'));" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>    
                        </asp:GridView>
                    </div>
                </asp:Panel>
            <asp:HiddenField ID="hfAddNewPreference" runat="server" />
            <ajax:ModalPopupExtender ID="mpeAddNewPreference" runat="server" BackgroundCssClass="modalBackground"
                TargetControlID="hfAddNewPreference" PopupControlID="pnlAddNewPreference" CancelControlID="imgCloseAddNewPrefPopUp">
            </ajax:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel> 
    </div>
    <script language="javascript" type="text/javascript">       
        //This function is used for showing progress bar
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
                
        // This function validates all the controls on the page
        function validateControls() {            

            var entityType = document.getElementById('<%=ddlEntityTypes.ClientID %>').value;
            var entity = document.getElementById('<%=ddlEntities.ClientID %>').value;
            var tableType = document.getElementById('<%=ddlTableType.ClientID %>').value;
            var notfType = document.getElementById('<%=ddlNotificationType.ClientID %>').value;
            var cbEmail = document.getElementById('<%=chkEmail.ClientID %>').checked;
            var cbsms = document.getElementById('<%=chkSMS.ClientID %>').checked;

            if (entityType == "-1") {
                alert("Please select entity type");
                return false;
            }

            if (entity == "-1") {
                alert("Please select entity");
                return false;
            }

            if (tableType == "-1") {
                alert("Please select table type");
                return false;
            }

            if (notfType == "-1") {
                alert("Please select notification type");
                return false;
            }

            /*
            if (notfType == "4") {
                alert("4");
                var count = $('.cols :checkbox:checked').length;
                alert(count);
                return false;
            }
            */

            //If both the notify via chkboxes are not checked
            if (cbEmail == false && cbsms == false) {
                alert("Please select notify via");
                return false;
            }
        }

        // Check if notification already set for a user
        function IsNotifAlreadySet() {

            var entityTypeid = document.getElementById('<%=ddlEntityTypes.ClientID %>').value;
            var entityid = document.getElementById('<%=ddlEntities.ClientID %>').value;
            var notftypeid = document.getElementById('<%=ddlNotificationType.ClientID %>').value;


            $.ajax({ type: "POST",
                url: "NotificationSettings.aspx/IsNotificationAlreadySet",
                data: "{EntityTypeID:" + entityTypeid + ",EntityID:" + entityid + ",NotificationTypeID:" + notftypeid + "}",
                contentType: "application/json; charset=utf-8",
                async: true,
                cache: false,
                error: Failed,
                success: function (data) {
                    if (data.d == "0") {
                        alert("Notification already exist for selected user");
                        $("#ctl00_ContentPlaceHolder1_ddlNotificationType").val("-1");
                        $('#ctl00_ContentPlaceHolder1_btnSubmit').prop('disabled', true);
                        return false;
                    }
                    else {
                        $('#ctl00_ContentPlaceHolder1_btnSubmit').prop('disabled', false);
                    }
                }
            });
        }

        function Failed(result) {
            alert(result.status + " " + result.statusText);
        }
    </script>    
</asp:Content>
