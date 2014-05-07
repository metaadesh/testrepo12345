<%@ Page Language="C#" AutoEventWireup="true" 
    CodeBehind="User.aspx.cs" Inherits="METAOPTION.UI.User" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="cphUser" runat="server">
    <div class="RightPanel">
        <asp:UpdatePanel ID="pnlEmpList" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpdate" />
                <asp:PostBackTrigger ControlID="btnSubmit" />
            </Triggers>
            <ContentTemplate>
            <asp:HiddenField ID="hfSecurityUserID" runat="server" Value="0" />
                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                    <tr>
                        <td class="AddHeading">
                            Create New User
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset class="ForFieldSet">
                                <legend class="ForLegend">
                                    <asp:Label ID="lblLegHeading" Text="Add New User" runat="server"></asp:Label>
                                </legend>
                                <br>
                                <table border="0" class="TableBorder" width="100%" cellpadding="0" style="border-collapse: collapse">
                                    <tr id="trFullName" runat="server">
                                        <td class="TableBorderB">
                                            Full Name
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtFullName" runat="server" CssClass="txt3" onkeydown="return false;" />
                                            <asp:LinkButton ID="lnkPopUp" runat="server" CssClass="OrangeText_LeftPanel">Click 
                                 to select user</asp:LinkButton>
                                            <asp:HiddenField ID="hfEntityId" runat="server" />
                                            <asp:HiddenField ID="hfEntityType" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            <asp:Label ID="Label1" runat="server" Text="Login ID" AssociatedControlID="txtUserName"></asp:Label>
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtUserName" runat="server" CssClass="txtMan2" MaxLength="50" />
                                        </td>
                                    </tr>
                                    <tr id="trPassword" runat="server">
                                        <td class="TableBorderB">
                                            <asp:Label ID="lblPassword" runat="server" AssociatedControlID="txtPassword" Text="Password" />
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="txt2"
                                                MaxLength="50" />
                                        </td>
                                    </tr>
                                    <tr id="trCofirmPassword" runat="server">
                                        <td class="TableBorderB">
                                            <asp:Label ID="lblConPassword" runat="server" Text="Confirm Password"></asp:Label>
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtConpassword" runat="server" TextMode="Password" CssClass="txt2"
                                                MaxLength="50" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            <asp:Label ID="lblDisName" runat="server" AssociatedControlID="txtDisplayName" Text="Display Name"></asp:Label>
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtDisplayName" runat="server" CssClass="txt2" MaxLength="50" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" CssClass="txtMulti"
                                                MaxLength="255" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            <asp:Label ID="lblActive" runat="server" Text="IsActive"></asp:Label>
                                        </td>
                                        <td class="TableBorder">
                                            <asp:CheckBox ID="ChkActive" runat="server" Checked="True" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorder" colspan="2" align="center">
                                            <asp:Button ID="btnCancel" CssClass="btn" Text="Cancel" runat="server" OnClick="btnCancel_Click" />
                                            <asp:Button ID="btnSubmit" CssClass="btn" Text="Save" runat="server" OnClick="btnSubmit_Click" />
                                            <asp:Button ID="btnUpdate" CssClass="btn" Text="Update" runat="server" OnClick="btnUpdate_Click" />
                                            <asp:Button ID="btnDelete" CssClass="btn" Text="Delete" runat="server" OnClientClick="javascript:return confirm('Are you sure you want to delete user?');"
                                                OnClick="btnDelete_Click" />
                                            <asp:Label ID="lblError" runat="server" CssClass="err" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <asp:Panel ID="pnlEmployeeList" Width="700" runat="server" CssClass="modalPopup">
                                <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="PopUpBoxHeading" colspan="5" style="padding-left: 5px">
                                            Entity List
                                        </td>
                                        <td class="PopUpBoxHeading" align="right">
                                            <img id="imgCloseEntityList" border="0" src="../Images/close.gif" alt="Close" style="padding-left: 5px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblb">
                                            Entity Type
                                        </td>
                                        <td class="lbl">
                                            <asp:DropDownList ID="ddlEntityTypesh" runat="server" AutoPostBack="true" CssClass="txt2"
                                                OnSelectedIndexChanged="ddlEntityTypesh_SelectedIndexChanged" />
                                        </td>
                                        <td class="lblb">
                                            Initial
                                        </td>
                                        <td class="lbl">
                                            <asp:TextBox ID="txtInitialsh" runat="server" CssClass="txt2" MaxLength="50" />
                                        </td>
                                        <td class="lblb">
                                            City
                                        </td>
                                        <td class="lblb">
                                            <asp:TextBox ID="txtCitysh" runat="server" CssClass="txt2" MaxLength="50" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblb">
                                            State
                                        </td>
                                        <td class="lblb">
                                            <asp:TextBox ID="txtStatesh" runat="server" CssClass="txt2" MaxLength="30" />
                                        </td>
                                        <td class="lblb">
                                            Zip
                                        </td>
                                        <td class="lblb">
                                            <asp:TextBox ID="txtZipsh" runat="server" CssClass="txt2" MaxLength="10" />
                                        </td>
                                        <td class="lbl" colspan="2" align="right">
                                            <asp:Button ID="btnSearchsh" runat="server" Text=" Search " CssClass="btn" OnClick="btnSearchsh_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="6" style="padding: 10px">
                                            <asp:GridView ID="GrdEmployee" runat="server" AutoGenerateColumns="False" Width="100%"
                                                GridLines="None" AllowPaging="True" OnPageIndexChanging="GrdEmployee_PageIndexChanging"
                                                PageSize="15" OnRowCommand="GrdEmployee_RowCommand" DataKeyNames="EntityId">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgbtnSelect" CommandName="SelectEmp" CommandArgument='<%#Eval("EntityId") %>'
                                                                runat="server" ImageUrl="~/Images/confirm.gif" OnClick="imgbtnSelect_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="EntityName" HeaderText="Name" />
                                                    <asp:BoundField DataField="City" HeaderText="City" />
                                                    <asp:BoundField DataField="State" HeaderText="State" />
                                                    <asp:BoundField DataField="Zip" HeaderText="Zip" />
                                                    <%--<asp:BoundField DataField="IsActive" HeaderText="User Status" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"/>--%>
                                                </Columns>
                                                <RowStyle CssClass="gvRow" />
                                                <AlternatingRowStyle CssClass="gvAlternateRow" />
                                                <HeaderStyle CssClass="gvHeading" />
                                                <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="padding: 10px" align="center">
                                            <asp:Button ID="btnEditCancel" runat="server" Text="    Cancel   " CssClass="btn"
                                                OnClick="btnEditCancel_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <ajax:ModalPopupExtender ID="modPopUp" runat="server" TargetControlID="lnkPopUp"
                                PopupControlID="pnlEmployeeList" CancelControlID="imgCloseEntityList" BackgroundCssClass="modalBackground">
                            </ajax:ModalPopupExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset class="ForFieldSet" id="fsetAssGroup" runat="server">
                                <legend class="ForLegend">Associated Groups </legend>
                                <br>
                                <asp:Label ID="lblGroupAssociation" runat="server" CssClass="err" Visible="false" />
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GrdGroup" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                GridLines="None" DataKeyNames="SecurityUserGroupId" AllowSorting="true" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-Width="15px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImgbtnDelRight" runat="server" CommandArgument='<% #Eval("SecurityUserGroupId") %>'
                                                                CommandName="DeleteGroup" ImageUrl="~/Images/DeleteButton.jpg" OnClick="ImgbtnDelRight_Click"
                                                                OnClientClick="javascript:return confirm('Are you sure to de-associate this group with this user?\n\nClick Ok to confirm\nElse Cancel')" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="GroupName" HeaderText="Group Name" SortExpression="GroupName" />
                                                    <asp:BoundField DataField="GroupDesc" HeaderText="Description" />
                                                </Columns>
                                                <RowStyle CssClass="gvRow" />
                                                <AlternatingRowStyle CssClass="gvAlternateRow" />
                                                <HeaderStyle CssClass="gvHeading" />
                                                <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblMessage" runat="server" Visible="False" CssClass="LeftPanelContentHeading"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnAssociateGroup" CssClass="btn" runat="server" Text="  Add To Group  " />
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="pnlGroups" Width="700" runat="server" CssClass="modalPopup">
                                    <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="PopUpBoxHeading" colspan="4" style="padding-left: 5px">
                                                Group List
                                            </td>
                                            <td class="PopUpBoxHeading" align="right">
                                                <img id="CloseGroupList" border="0" src="../Images/close.gif" alt="Close" style="padding-left: 5px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblb">
                                                Group Name
                                            </td>
                                            <td class="lbl">
                                                <asp:TextBox ID="txtGroupNamegrp" runat="server" CssClass="txt2" />
                                            </td>
                                            <td class="lblb">
                                                Description
                                            </td>
                                            <td class="lbl">
                                                <asp:TextBox ID="txtGroupDescgrp" runat="server" CssClass="txt2" />
                                            </td>
                                            <td class="lblb" align="center">
                                                <asp:Button ID="btnSearchGroup" runat="server" Text="  Search  " CssClass="btn" OnClick="btnSearchGroup_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="5" style="padding: 10px">
                                                <asp:GridView ID="gvGroups" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    GridLines="None" AllowPaging="True" OnPageIndexChanging="gvGroups_PageIndexChanging"
                                                    PageSize="15" OnRowCommand="gvGroups_RowCommand" DataKeyNames="SecurityGroupId">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ibtnAddGroup" CommandName="SelectEmp" CommandArgument='<%#Eval("SecurityGroupId") %>'
                                                                    runat="server" ImageUrl="~/Images/confirm.gif" OnClick="ibtnAddGroup_Click" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="GroupName" HeaderText="Name" />
                                                        <asp:BoundField DataField="GroupDesc" HeaderText="Description" />
                                                    </Columns>
                                                    <RowStyle CssClass="gvRow" />
                                                    <AlternatingRowStyle CssClass="gvAlternateRow" />
                                                    <HeaderStyle CssClass="gvHeading" />
                                                    <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="padding: 10px" align="center">
                                                <asp:Button ID="btnCanelGroup" runat="server" Text="    Cancel   " CssClass="btn"
                                                    OnClick="btnCanelGroup_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <ajax:ModalPopupExtender ID="mpeModelGroup" runat="server" TargetControlID="btnAssociateGroup"
                                    PopupControlID="pnlGroups" CancelControlID="CloseGroupList" BackgroundCssClass="modalBackground" />
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset id="fsSettings" runat="server" class="ForFieldSet">
                                <legend class="ForLegend">Settings</legend>
                                <div>
                                    <asp:GridView ID="gvUserSettings" runat="server"
                                        AutoGenerateColumns="false" GridLines="None"
                                        Width="100%" OnRowDataBound="gvUserSettings_RowDataBound">
                                        <Columns>
                                            <asp:BoundField HeaderText="Setting Key" DataField="SettingKey" ItemStyle-Width="85%" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <img id="imgOn" runat="server" src="../Images/on.png" alt="close" border="0" onclick="ChangeSettingkeyValue('1');" />
                                                    <img id="imgOff" runat="server" src="../Images/off.png" alt="close" border="0" onclick="ChangeSettingkeyValue('0');" />
                                                    <asp:HiddenField ID="hfSettingKeyValue" runat="server" Value='<%# Eval("SettingkeyValue") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="gvRow" />
                                        <AlternatingRowStyle CssClass="gvAlternateRow" />
                                        <HeaderStyle CssClass="gvHeading" />
                                    </asp:GridView>
                                     <asp:GridView ID="gvIpPermission" runat="server"
                                        AutoGenerateColumns="false" GridLines="None"  ShowHeader="false" 
                                        Width="100%" OnRowDataBound="gvIpPermission_RowDataBound">
                                        <Columns>
                                         <asp:TemplateField HeaderText="IPRestriction" ItemStyle-Width="85%">
                                                <ItemTemplate>
                                                <asp:Label ID="lblTxt" runat="server" Text="IPRestriction"/>
                                                <asp:HiddenField ID="hdnIPAddress" runat="server" Value='<%# Eval("IPAddress") %>' />
                                                <asp:Label ID="lblIPAddress"  runat="server"  />
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                           
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <img id="img1" runat="server" src="../Images/on.png" alt="close" border="0" onclick="SetIPRestriction(this,'1');" />
                                                    <img id="img2" runat="server" src="../Images/off.png" alt="close" border="0" onclick="SetIPRestriction(this,'0');" />
                                                     <asp:HiddenField ID="hdnIPType" runat="server" Value='<%#Eval("IPType") %>' />
                                                     <asp:HiddenField ID="hdIPRestriction" runat="server" Value='<%#Eval("IPRestriction") %>' />                                                     
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="gvRow" />
                                        <AlternatingRowStyle CssClass="gvAlternateRow" />
                                        <HeaderStyle CssClass="gvHeading" />
                                    </asp:GridView>
                                </div>
                            </fieldset>
                        </td>
                    </tr>
                   
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

<script type="text/javascript">
    function ChangeSettingkeyValue(KeyValue) {
        var userid = $('#<%=hfSecurityUserID.ClientID %>').val();
        var NewKeyValue;

        if (KeyValue == "1")
            NewKeyValue = "0";
        else
            NewKeyValue = 1;
       
        $.ajax({ type: "POST",
            url: "User.aspx/UpdateUserSetting",
            data: "{UserID:" + userid + ",SettingKeyValue:" + NewKeyValue + "}",
            contentType: "application/json; charset=utf-8",
            async: true,
            cache: false,
            error: Failed,
            success: function () {
                if (KeyValue == "1") {
                    $('#ctl00_ContentPlaceHolder1_gvUserSettings_ctl02_imgOn').hide();
                    $('#ctl00_ContentPlaceHolder1_gvUserSettings_ctl02_imgOff').show();
                }
                else {
                    $('#ctl00_ContentPlaceHolder1_gvUserSettings_ctl02_imgOn').show();
                    $('#ctl00_ContentPlaceHolder1_gvUserSettings_ctl02_imgOff').hide();
                }
            }
        });
    }

    function SetIPRestriction(ctrl, ShowHideFlag) {
       var EntityType = "<%= Session["empID"]%>";
        var ID = ctrl.id;
        var imgID = ID.split('_');
        var IPuserId = $('#<%=hfSecurityUserID.ClientID %>').val(); //gvEditIPPermission_ctl02_imgOn
        var hdnIPType = document.getElementById('ctl00_ContentPlaceHolder1_gvIpPermission_ctl02_hdnIPType').value;
       
        if (hdnIPType == "")
            hdnIPType = 1;
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
            url: "../UI/User.aspx/UpdateAutoIPApproval",
            data: "{Flag:" + SentFlag + ",IPUserID:" + IPuserId +",ModifiedBy:"+ EntityType + ",IPType:" + hdnIPType + "}",
            contentType: "application/json; charset=utf-8",
            async: true,
            cache: false,
            error: Failed,
            success: function () {
                
                if (hdnIPType == "1") {
                    if (ShowHideFlag == "1") {
                        $("#ctl00_ContentPlaceHolder1_gvIpPermission_ctl02_img1").hide();
                        $("#ctl00_ContentPlaceHolder1_gvIpPermission_ctl02_img2").show();
                    }
                    else {
                        $("#ctl00_ContentPlaceHolder1_gvIpPermission_ctl02_img1").show();
                        $("#ctl00_ContentPlaceHolder1_gvIpPermission_ctl02_img2").hide();
                    }
                }
                else {
                    if (ShowHideFlag == "1") {
                        $("ctl00_ContentPlaceHolder1_gvIpPermission_ctl02_img1").hide();
                        $("#ctl00_ContentPlaceHolder1_gvIpPermission_ctl02_img2").show();
                    }
                    else {
                        $("#ctl00_ContentPlaceHolder1_gvIpPermission_ctl02_img1").show();
                        $("#ctl00_ContentPlaceHolder1_gvIpPermission_ctl02_img2").hide();
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
</asp:Content>
