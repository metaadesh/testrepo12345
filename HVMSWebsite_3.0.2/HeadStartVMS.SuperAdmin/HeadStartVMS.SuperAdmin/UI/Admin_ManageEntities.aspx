<%@ Page Language="C#" MasterPageFile="~/UI/Admin_MasterLeftPanel.Master" AutoEventWireup="true"
    CodeBehind="Admin_ManageEntities.aspx.cs" Inherits="METAOPTION.UI.Admin_ManageEntities"
    Title="Admin Panel :: Add Entity" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .dropDownEntity
        {
            font-size: 16px;
            color: Black;
            width: 205px;
        }
        .txtEntity
        {
            width: 200px;
        }
        .lblTextDisplayEntity
        {
            font-size: 14px;
            color: #292929;
            font-style: normal;
        }
        .tblMidSpace
        {
            width: 100px;
        }
        
        .EntityDropDown
        {
            margin: 0px 0px 0px 0px !important;
            padding: 0px 0px 0px 0px !important;
            font-weight: 100;
            font-size: 14px;
            font-family: Microsoft Sans Serif;
        }
        
        .EntityDropDown option
        {
            background: #fff;
            font-family: Microsoft Sans Serif;
        }
    </style>
    <script type="text/javascript" language="javascript">
        window.onload = SetTop;
        function SetTop() {
            var verticalTop = document.getElementById('<%=hScrollPosition.ClientID %>').value;
            var boolIsType = checkUrlForType();
            //            alert(boolIsType);

            if (boolIsType && verticalTop == "0") {

                verticalTop = 50;
                //alert(verticalTop);
            }
            document.documentElement.scrollTop = verticalTop;
            window.scrollTo(0, verticalTop);
        }

        function checkUrlForType() {
            var curUrl = window.location.search;
            curUrl = curUrl.toLowerCase();
            //            alert(curUrl);
            //            alert(curUrl.indexOf("type="));
            if (curUrl.indexOf("type=") != -1) {
                return true;
            }
            return false;
        }

    </script>
    <table width="100%">
        <asp:HiddenField ID="hScrollPosition" runat="server" Value="0" />
        <tr>
            <td align="left">
                <fieldset class="ForFieldSet">
                    <legend class="ForLegend" align="left">Manage Entity</legend>
                    <div onmousemove="SetProgressPosition(event)">
                        <asp:UpdatePanel ID="upManageEntity_Search" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td align="left" valign="top" class="TableBorderB" style="background-color: #F0FAFF;">
                                            <div style="float: left;">
                                                <table style="margin-left: 2%;">
                                                    <tr style="background-color: #F0FAFF;">
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="Organization" CssClass="TableBorderLabel"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlOrganization" runat="server" Width="200px" AutoPostBack="false"
                                                                CssClass="txtMan2" Style="font-size: 11px;">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #F0FAFF;">
                                                        <td>
                                                            <asp:Label ID="Label94" runat="server" Text="Add New" CssClass="TableBorderLabel"></asp:Label>
                                                        </td>
                                                        <td style="white-space: nowrap;">
                                                            <asp:DropDownList ID="ddlEntityType" runat="server" Width="200px" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlEntityType_SelectedIndexChanged" CssClass="txtMan2"
                                                                Style="font-size: 11px;">
                                                            </asp:DropDownList>
                                                            <%-- <asp:RequiredFieldValidator ID="rfvEntityType" runat="server" ControlToValidate="ddlEntityType"
                                                                SetFocusOnError="true" Font-Bold="true" ErrorMessage="*" ValidationGroup="Buyer" />--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="150" AssociatedUpdatePanelID="upManageEntity_Search">
                                                                <ProgressTemplate>
                                                                    <div id="dvProg" class="overlay">
                                                                        <img id="image" src="../Images/Wait.gif" alt="Wait..." style="vertical-align: middle" />&nbsp;Please
                                                                        wait...
                                                                    </div>
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr style="height: 10px;">
                                        <td align="center">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" valign="top">
                                            <div>
                                                <div style="min-height: 390px;">
                                                    <%--Below is Buyer Table--%>
                                                    <table id="tblBuyer" runat="server" border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                                                        <tr>
                                                            <td class="TableBorderB" style="width: 220px">
                                                                First Name
                                                            </td>
                                                            <td class="TableBorder" style="white-space: nowrap;">
                                                                <asp:TextBox ID="txtBuyerFirstName" runat="server" CssClass="txtMan2" />
                                                                <asp:RequiredFieldValidator ID="rfvBuyerFirstName" runat="server" ControlToValidate="txtBuyerFirstName"
                                                                    SetFocusOnError="true" Font-Bold="true" ErrorMessage="*" ValidationGroup="Buyer" />
                                                            </td>
                                                            <td class="TableBorderB" style="width: 200px">
                                                                Middle Name
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtBuyerMiddleName" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB" style="width: 177px">
                                                                Last Name
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtBuyerLastName" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Buyer Code
                                                            </td>
                                                            <td class="TableBorder" style="white-space: nowrap;">
                                                                <div style="float: left;">
                                                                    <asp:TextBox ID="txtBuyerCode" runat="server" CssClass="txtMan1" onblur="CheckBuyerCode(this)" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBuyerCode"
                                                                        SetFocusOnError="true" Font-Bold="true" ErrorMessage="*" ValidationGroup="Buyer" />
                                                                </div>
                                                                <div style="margin-top: 4px;">
                                                                    <img id="imgBuyerCode" name="BuyerCodeAvailability" src="" alt="" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Tax Id Number
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtBuyerIdNumber" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td class="TableBorderB" style="width: 177px">
                                                                Payments Terms
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:DropDownList ID="ddlBuyerPaymentTerms" runat="server" CssClass="txtMan2" DataSourceID="objBuyerPaymentTerm"
                                                                    DataTextField="PaymentTerm1" DataValueField="PaymentTermId">
                                                                </asp:DropDownList>
                                                                <asp:ObjectDataSource ID="objBuyerPaymentTerm" runat="server" SelectMethod="GetPaymentTerms"
                                                                    TypeName="METAOPTION.BAL.Admin_MasterBAL"></asp:ObjectDataSource>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Commission Type
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:DropDownList ID="ddlBuyerCommisionType" runat="server" CssClass="txtMan2" DataSourceID="objBuyerCommisionType"
                                                                    DataTextField="CommissionType1" DataValueField="CommissionTypeId">
                                                                </asp:DropDownList>
                                                                <asp:ObjectDataSource ID="objBuyerCommisionType" runat="server" SelectMethod="GetCommisionType"
                                                                    TypeName="METAOPTION.BAL.Admin_MasterBAL"></asp:ObjectDataSource>
                                                            </td>
                                                            <td class="TableBorderB" style="width: 177px">
                                                                Commission Value
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtBuyerCommisionValue" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                                <ajax:FilteredTextBoxExtender ID="txtBuyerCommisionValue_Ext" runat="server" FilterType="Custom,Numbers"
                                                                    TargetControlID="txtBuyerCommisionValue" ValidChars=".">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                State Salesman License Number
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtBuyerSSLNumber" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td class="TableBorderB" style="width: 177px">
                                                                License Plate Number
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtBuyerLPnumber" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Street
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtBuyerStreet" runat="server" CssClass="txtMan2" />
                                                            </td>
                                                            <td class="TableBorderB" style="width: 177px">
                                                                Suite
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtBuyerSuite" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                City
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtBuyerCity" runat="server" CssClass="txtMan2" />
                                                            </td>
                                                            <td class="TableBorderB" style="width: 177px">
                                                                State
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:UpdatePanel ID="updBuyerPnlState" UpdateMode="Conditional" runat="server">
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="ddlBuyerCountry" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="ddlBuyerState" CssClass="txtMan2" DataTextField="State" DataValueField="StateId"
                                                                            runat="server">
                                                                        </asp:DropDownList>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Country
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:DropDownList ID="ddlBuyerCountry" runat="server" AutoPostBack="True" CssClass="txt2"
                                                                    DataTextField="CountryName" DataValueField="CountryId" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvBuyerCountry" runat="server" ControlToValidate="ddlBuyerCountry"
                                                                    Font-Bold="true" ErrorMessage="*" InitialValue="0" SetFocusOnError="True" ValidationGroup="Buyer" />
                                                            </td>
                                                            <td class="TableBorderB" style="width: 177px">
                                                                Zip
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtBuyerZip" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                <ajax:FilteredTextBoxExtender ID="txtBuyerZip_Extender" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txtBuyerZip">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Phone
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtBuyerPhone" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                <ajax:FilteredTextBoxExtender ID="txtBuyerPhone_FilteredTextBoxExtender" runat="server"
                                                                    FilterType="Numbers,Custom" ValidChars="+,-" TargetControlID="txtBuyerPhone">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                            <td class="TableBorderB" style="width: 177px">
                                                                Cell Phone
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtBuyerCellPhone" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                <ajax:FilteredTextBoxExtender ID="FilteredBuyerTextBoxExtender2" runat="server" FilterType="Numbers,Custom"
                                                                    ValidChars="+,-" TargetControlID="txtBuyerCellPhone">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Fax
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtBuyerFax" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                <ajax:FilteredTextBoxExtender ID="FilteredBuyerTextBoxExtender3" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txtBuyerFax">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                            <td class="TableBorderB" style="width: 177px">
                                                                Other Number
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtBuyerOtherNumber" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                <ajax:FilteredTextBoxExtender ID="txtBuyerOtherNumber_Extender" runat="server" FilterType="Numbers,Custom"
                                                                    ValidChars="+,-" TargetControlID="txtBuyerOtherNumber">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Email
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtBuyerEmail" runat="server" CssClass="txtMan2" />
                                                                <asp:RegularExpressionValidator ID="revBuyerEmail" runat="server" ControlToValidate="txtBuyerEmail"
                                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="*"
                                                                    ValidationGroup="Buyer" />
                                                            </td>
                                                            <td class="TableBorderB" style="width: 177px">
                                                                When Commission gets paid
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:ObjectDataSource ID="objBuyerCommisionTerm" runat="server" SelectMethod="GetCommisionPaid"
                                                                    TypeName="METAOPTION.BAL.Admin_MasterBAL"></asp:ObjectDataSource>
                                                                <asp:DropDownList ID="ddlBuyerCGetPaid" CssClass="txtMan2" runat="server" DataSourceID="objBuyerCommisionTerm"
                                                                    DataTextField="CommissionTerm1" DataValueField="CommissionTermId">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Comments
                                                            </td>
                                                            <td class="TableBorder" colspan="3">
                                                                <asp:TextBox ID="txtBuyerComment" runat="server" Rows="2" Width="100%" CssClass="txtMulti"
                                                                    TextMode="MultiLine" onkeyup="MaxCharLimit(this, 'txtcount', 255)" />
                                                                <asp:TextBox ID="txtBuyercount" runat="server" ReadOnly="true" CssClass="WordCounter"
                                                                    Width="30px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Direct Buyer
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:DropDownList ID="ddlBuyerDirectBuyer" runat="server" CssClass="txt1" onchange="onDirectBuyerChange()">
                                                                    <asp:ListItem Text="Yes" Value="1" Selected="True" />
                                                                    <asp:ListItem Text="No" Value="0" />
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Parent Buyer
                                                            </td>
                                                            <td class="TableBorder">
                                                                <div style="display: inline-block; float: left; padding-right: 1px">
                                                                    <asp:DropDownList ID="ddlBuyerParentBuyer" runat="server" CssClass="txt2" Width="140px"
                                                                        Enabled="false" />
                                                                </div>
                                                                <span id="spanBuyerParentB" runat="server" class="err" style="display: none; font-weight: bold;">
                                                                    *</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Access Level
                                                            </td>
                                                            <td class="TableBorder">
                                                                <div style="display: inline-block; float: left; padding-right: 1px">
                                                                    <asp:DropDownList ID="ddlBuyerAccessLevel" runat="server" CssClass="txt2" Enabled="false">
                                                                        <asp:ListItem Text="Select" Value="" Selected="True" />
                                                                        <asp:ListItem Text="Access Level 1" Value="1" Title="Buyer can access data belongs to them only" />
                                                                        <asp:ListItem Text="Access Level 2" Value="2" Title="Buyer can access data belongs to them as well as their parent" />
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <span id="spanBuyerAccessLevel" runat="server" class="err" style="display: none;
                                                                    font-weight: bold;">*</span>
                                                            </td>
                                                            <td class="TableBorder" colspan="2">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <%--Below is Dealer Table--%>
                                                    <asp:UpdatePanel ID="upAddDealer" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <table id="tblDealer" runat="server" visible="false" border="0" width="100%" cellpadding="0"
                                                                style="border-collapse: collapse;">
                                                                <tr>
                                                                    <td class="TableBorderB">
                                                                        Dealer Name
                                                                    </td>
                                                                    <td class="TableBorder">
                                                                        <asp:TextBox ID="txtDealerName" runat="server" CssClass="txtMan2" />
                                                                        <asp:RequiredFieldValidator ID="rfvDealerName" runat="server" ControlToValidate="txtDealerName"
                                                                            Font-Bold="true" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="Dealer" />
                                                                    </td>
                                                                    <td class="TableBorderB">
                                                                        Dealer DIN
                                                                    </td>
                                                                    <td class="TableBorder">
                                                                        <asp:TextBox ID="txtDealerDIN" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="TableBorderB">
                                                                        Dealer Type
                                                                    </td>
                                                                    <td class="TableBorder">
                                                                        <asp:DropDownList ID="ddlDealerType" runat="server" CssClass="txtMan2" DataSourceID="objDealerType"
                                                                            DataTextField="DealerType1" DataValueField="DealerTypeId">
                                                                        </asp:DropDownList>
                                                                        <asp:ObjectDataSource ID="objDealerType" runat="server" SelectMethod="GetDealerType"
                                                                            TypeName="METAOPTION.BAL.Admin_MasterBAL"></asp:ObjectDataSource>
                                                                    </td>
                                                                    <td class="TableBorderB" rowspan="2" valign="top">
                                                                        Franchise Make(s)
                                                                    </td>
                                                                    <td class="TableBorder" rowspan="2">
                                                                        <asp:ListBox ID="lstDealerMake" runat="server" CssClass="txtMan2" SelectionMode="Multiple"
                                                                            DataSourceID="objDealerMake" DataTextField="VINDivisionName" DataValueField="MakeID">
                                                                        </asp:ListBox>
                                                                        <asp:ObjectDataSource ID="objDealerMake" runat="server" SelectMethod="GetMakeList"
                                                                            TypeName="METAOPTION.BAL.Admin_MasterBAL"></asp:ObjectDataSource>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="TableBorderB">
                                                                        Dealer Category
                                                                    </td>
                                                                    <td class="TableBorder">
                                                                        <asp:DropDownList ID="ddlDealerCategory" runat="server" CssClass="txtMan2" DataSourceID="objDelaerCategory"
                                                                            DataTextField="Category" DataValueField="DealerCategoryId">
                                                                        </asp:DropDownList>
                                                                        <asp:ObjectDataSource ID="objDelaerCategory" runat="server" SelectMethod="GetDealerCategory"
                                                                            TypeName="METAOPTION.BAL.Admin_MasterBAL"></asp:ObjectDataSource>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="TableBorderB">
                                                                        Source
                                                                    </td>
                                                                    <td class="TableBorder">
                                                                        <asp:DropDownList ID="ddlDealerSource" runat="server" CssClass="txtMan2" DataSourceID="objDealerSource"
                                                                            DataTextField="Source" DataValueField="DealerSourceId">
                                                                        </asp:DropDownList>
                                                                        <asp:ObjectDataSource ID="objDealerSource" runat="server" SelectMethod="GetDealerSource"
                                                                            TypeName="METAOPTION.BAL.Admin_MasterBAL"></asp:ObjectDataSource>
                                                                    </td>
                                                                    <td class="TableBorderB">
                                                                        Street
                                                                    </td>
                                                                    <td class="TableBorder">
                                                                        <asp:TextBox ID="txtDealerStreet" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="TableBorderB">
                                                                        City
                                                                    </td>
                                                                    <td class="TableBorder">
                                                                        <asp:TextBox ID="txtDealerCity" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                    </td>
                                                                    <td class="TableBorderB">
                                                                        Country
                                                                    </td>
                                                                    <td class="TableBorder">
                                                                        <asp:DropDownList ID="ddlDealerCountry" runat="server" CssClass="txt2" AutoPostBack="True"
                                                                            DataTextField="CountryName" DataValueField="CountryId" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0"
                                                                            Font-Bold="true" ControlToValidate="ddlDealerCountry" ErrorMessage="*" SetFocusOnError="True"
                                                                            ValidationGroup="Dealer" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="TableBorderB">
                                                                        State
                                                                    </td>
                                                                    <td class="TableBorder">
                                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="ddlDealerState" runat="server" CssClass="txtMan2" DataTextField="State"
                                                                                    DataValueField="StateId">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="ddlDealerCountry" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                    <td class="TableBorderB">
                                                                        Suite
                                                                    </td>
                                                                    <td class="TableBorder">
                                                                        <asp:TextBox ID="txtDealerSuite" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="TableBorderB">
                                                                        Zip
                                                                    </td>
                                                                    <td class="TableBorder">
                                                                        <asp:TextBox ID="txtDealerZip" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                        <ajax:FilteredTextBoxExtender ID="txtDealerZipExten" runat="server" TargetControlID="txtDealerZip"
                                                                            FilterType="Numbers">
                                                                        </ajax:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td class="TableBorderB">
                                                                        Phone
                                                                    </td>
                                                                    <td class="TableBorder">
                                                                        <asp:TextBox ID="txtDealerPhone" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                        <ajax:FilteredTextBoxExtender ID="txtPhoneExten" runat="server" TargetControlID="txtDealerPhone"
                                                                            FilterMode="ValidChars" ValidChars="+,-" FilterType="Custom,Numbers">
                                                                        </ajax:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="TableBorderB">
                                                                        Fax
                                                                    </td>
                                                                    <td class="TableBorder">
                                                                        <asp:TextBox ID="txtDealerFax" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                        <ajax:FilteredTextBoxExtender ID="txtDealerFaxExten" runat="server" TargetControlID="txtDealerFax"
                                                                            FilterMode="ValidChars" ValidChars="+,-" FilterType="Custom,Numbers">
                                                                        </ajax:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td class="TableBorderB">
                                                                        Other Number
                                                                    </td>
                                                                    <td class="TableBorder">
                                                                        <asp:TextBox ID="txtDealerOtherNumber" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                        <ajax:FilteredTextBoxExtender ID="FilteredDealerTextBoxExtender" runat="server" TargetControlID="txtDealerOtherNumber"
                                                                            FilterMode="ValidChars" ValidChars="+,-" FilterType="Custom,Numbers">
                                                                        </ajax:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="TableBorderB">
                                                                        Email
                                                                    </td>
                                                                    <td class="TableBorder">
                                                                        <asp:TextBox ID="txtDealerEmail" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularDealerExpressionValidator" runat="server"
                                                                            ControlToValidate="txtDealerEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                            ErrorMessage="*" SetFocusOnError="True" />
                                                                    </td>
                                                                    <td class="TableBorderB">
                                                                        Auction Access Number
                                                                    </td>
                                                                    <td class="TableBorder">
                                                                        <asp:TextBox ID="txtDealerAuctionAccessNumber" runat="server" CssClass="txtMan2"
                                                                            MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="TableBorderB">
                                                                        Comments
                                                                    </td>
                                                                    <td class="TableBorder" colspan="3">
                                                                        <asp:TextBox ID="txtDealerComment" CssClass="txtMulti" runat="server" Rows="5" TextMode="MultiLine"
                                                                            onkeyup="MaxCharLimit(this,  'txtcount' , 4000)" />
                                                                        <asp:TextBox ID="txtDealercount" runat="server" ReadOnly="true" CssClass="WordCounter"
                                                                            Width="30px" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <%--Below is Vendor Table--%>
                                                    <table id="tblVendor" runat="server" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse"
                                                        width="100%" visible="false">
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Vendor Name
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtVendorName" runat="server" CssClass="txtMan2" EnableViewState="true"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvVendorName" runat="server" ControlToValidate="txtVendorName"
                                                                    Font-Bold="true" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="Vendor" />
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Vendor DIN
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtVendorDIN" runat="server" CssClass="txtMan2" EnableViewState="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Vendor Category
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:DropDownList ID="ddlVendorCategory" runat="server" CssClass="txtMan2" DataSourceID="objVendorCategory"
                                                                    DataTextField="VendorCategory1" DataValueField="VendorCategoryId">
                                                                </asp:DropDownList>
                                                                <asp:ObjectDataSource ID="objVendorCategory" runat="server" SelectMethod="GetVendorCategory"
                                                                    TypeName="METAOPTION.BAL.Admin_MasterBAL"></asp:ObjectDataSource>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Vendor Type
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:DropDownList ID="ddlVenderType" runat="server" CssClass="txtMan2" DataSourceID="objVendorType"
                                                                    DataTextField="VendorType1" DataValueField="VendorTypeId">
                                                                </asp:DropDownList>
                                                                <asp:ObjectDataSource ID="objVendorType" runat="server" SelectMethod="GetVendorType"
                                                                    TypeName="METAOPTION.BAL.Admin_MasterBAL"></asp:ObjectDataSource>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Tax Id Number
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtVendorIdNumber" runat="server" CssClass="txtMan2" />
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Payments Terms
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:DropDownList ID="ddlVendorPaymentTerms" runat="server" CssClass="txtMan2" DataSourceID="objVendorPaymentTerm"
                                                                    DataTextField="PaymentTerm1" DataValueField="PaymentTermId">
                                                                </asp:DropDownList>
                                                                <asp:ObjectDataSource ID="objVendorPaymentTerm" runat="server" SelectMethod="GetPaymentTerms"
                                                                    TypeName="METAOPTION.BAL.Admin_MasterBAL"></asp:ObjectDataSource>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Street
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtVendorStreet" runat="server" CssClass="txtMan2" />
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Suite
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtVendorSuite" runat="server" CssClass="txt2" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                City
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtVendorCity" runat="server" CssClass="txt2" />
                                                            </td>
                                                            <td class="TableBorderB">
                                                                State
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="ddlVendorCountry" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="ddlVendorState" CssClass="txtMan2" DataTextField="State" DataValueField="StateId"
                                                                            runat="server">
                                                                        </asp:DropDownList>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Country
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:DropDownList ID="ddlVendorCountry" runat="server" AutoPostBack="True" CssClass="txt2"
                                                                    DataTextField="CountryName" DataValueField="CountryId" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredVendorFieldValidator" runat="server" ControlToValidate="ddlVendorCountry"
                                                                    Font-Bold="true" ErrorMessage="*" InitialValue="0" SetFocusOnError="True" ValidationGroup="Vendor" />
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Zip
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtVendorZip" runat="server" CssClass="txt2" />
                                                                <ajax:FilteredTextBoxExtender ID="txtVendorZip_Extender" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txtVendorZip">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Phone Number
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtVendorPhoneNumber" runat="server" CssClass="txt2" />
                                                                <ajax:FilteredTextBoxExtender ID="txtVendorPhone_FilteredTextBoxExtender" runat="server"
                                                                    FilterType="Numbers,Custom" TargetControlID="txtVendorPhoneNumber" ValidChars="+,-">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Other Number
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtVendorOtherNumber" runat="server" CssClass="txt2" />
                                                                <ajax:FilteredTextBoxExtender ID="txtVendorOtherNumber_Extender" runat="server" FilterType="Numbers,Custom"
                                                                    TargetControlID="txtVendorOtherNumber" ValidChars="+,-">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Fax
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtVendorFax" runat="server" CssClass="txt2" />
                                                                <ajax:FilteredTextBoxExtender ID="txtVendorFax_Extender" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txtVendorFax">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Email
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtVendorEmail" runat="server" CssClass="txt2" />
                                                                <asp:RegularExpressionValidator ID="RegularVendorExpressionValidator" runat="server"
                                                                    Font-Bold="true" ControlToValidate="txtVendorEmail" ErrorMessage="*" SetFocusOnError="True"
                                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Vendor" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Comments
                                                            </td>
                                                            <td class="TableBorder" colspan="3">
                                                                <asp:TextBox ID="txtVendorComment" Width="100%" runat="server" CssClass="txtMan2"
                                                                    Rows="5" TextMode="MultiLine" onkeyup="MaxCharLimit(this, 'txtVendorCount', 500)" />
                                                                <asp:TextBox ID="txtVendorCount" runat="server" ReadOnly="true" CssClass="WordCounter"
                                                                    Width="30px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                TRP Expense Calculation
                                                            </td>
                                                            <td class="TableBorder" colspan="3">
                                                                <asp:DropDownList ID="ddlVendorExpenseCalcMethod" runat="server" CssClass="txt2">
                                                                    <asp:ListItem Text="Mileage" Value="1" Selected="True" />
                                                                    <asp:ListItem Text="Location" Value="2" />
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <%--Below is Utility Company Table--%>
                                                    <table id="tblUtilityCompany" runat="server" visible="false" border="0" cellspacing="0"
                                                        width="100%" cellpadding="0" style="border-collapse: collapse">
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Company Name
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtCompanyName" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ControlToValidate="txtCompanyName"
                                                                    Font-Bold="true" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="Company" />
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Tax ID
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtCompanyTaxIdNo" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Company Category
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:DropDownList ID="ddlCompanyCategory" CssClass="txtMan2" runat="server" DataSourceID="objCompanyCategory"
                                                                    DataTextField="CompanyCategory1" DataValueField="CompanyCategoryId">
                                                                </asp:DropDownList>
                                                                <asp:ObjectDataSource ID="objCompanyCategory" runat="server" SelectMethod="GetCompanyCategory"
                                                                    TypeName="METAOPTION.BAL.Admin_MasterBAL"></asp:ObjectDataSource>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Frequency of Payments
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:DropDownList ID="ddlCompanyPaymentFreq" CssClass="txtMan2" runat="server" DataSourceID="objCompanyPayFrequency"
                                                                    DataTextField="PayementFrequency1" DataValueField="PayementFrequencyId">
                                                                </asp:DropDownList>
                                                                <asp:ObjectDataSource ID="objCompanyPayFrequency" runat="server" SelectMethod="GetPaymentFrequency"
                                                                    TypeName="METAOPTION.BAL.Admin_MasterBAL"></asp:ObjectDataSource>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Account Number
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtCompanyAccountNo" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Street
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtCompanyStreet" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Suite
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtCompanySuite" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                City
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtCompanyCity" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                State
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="ddlCompanyCountry" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="ddlCompanyState" runat="server" CssClass="txtMan2" DataTextField="State"
                                                                            DataValueField="StateId">
                                                                        </asp:DropDownList>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Country
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:DropDownList ID="ddlCompanyCountry" runat="server" AutoPostBack="True" CssClass="txt2"
                                                                    DataTextField="CountryName" DataValueField="CountryId" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCompanyCountry"
                                                                    Font-Bold="true" ErrorMessage="*" InitialValue="0" SetFocusOnError="True" ValidationGroup="Company" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Zip
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtCompanyZip" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                                <ajax:FilteredTextBoxExtender ID="txtCompanyZip_Extender" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txtCompanyZip">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Phone
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtCompanyPhone" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                <ajax:FilteredTextBoxExtender ID="txtCompanyPhone_FilteredTextBoxExtender" runat="server"
                                                                    FilterType="Numbers,Custom" ValidChars="+,-" TargetControlID="txtCompanyPhone">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Fax
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtCompanyFax" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                                <ajax:FilteredTextBoxExtender ID="txtFax_Extender" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txtCompanyFax">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Other
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtCompanyOther" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                                <ajax:FilteredTextBoxExtender ID="txtOther_Extender" runat="server" FilterType="Numbers,Custom"
                                                                    ValidChars="+,-" TargetControlID="txtCompanyOther">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Email
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtCompanyEmail" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                &nbsp;
                                                            </td>
                                                            <td class="TableBorder">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Comments
                                                            </td>
                                                            <td class="TableBorder" colspan="3">
                                                                <asp:TextBox ID="txtCompanyComments" Width="100%" runat="server" CssClass="txtMan2"
                                                                    Rows="5" TextMode="MultiLine" onkeyup="MaxCharLimit(this, 'txtcount', 200)" />
                                                                <asp:TextBox ID="TextBox24" runat="server" ReadOnly="true" CssClass="WordCounter"
                                                                    Width="30px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <%--Below is Employee Table--%>
                                                    <table id="tblEmployee" runat="server" visible="false" border="0" width="100%" cellpadding="0"
                                                        style="border-collapse: collapse">
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Title
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:DropDownList ID="ddlEmployeeTitle" runat="server" CssClass="txtMan2">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                First Name
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtEmployeeFirstName" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvFName" runat="server" ControlToValidate="txtEmployeeFirstName"
                                                                    Font-Bold="true" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="Employee" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Middle Name
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtEmployeeMiddleName" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Last Name
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtEmployeeLastName" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Employee Code
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtEmployeeCode" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Employee Type
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:DropDownList ID="ddlEmployeeType" CssClass="txtMan2" runat="server">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Street
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtEmployeeStreet" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Suite
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtEmployeeSuite" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                City
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtEmployeeCity" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                State
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:UpdatePanel ID="updEmployeePnlState" UpdateMode="Conditional" runat="server">
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="ddlEmployeeCountry" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="ddlEmployeeState" CssClass="txtMan2" DataTextField="State"
                                                                            DataValueField="StateId" runat="server">
                                                                        </asp:DropDownList>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Country
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:DropDownList ID="ddlEmployeeCountry" runat="server" AutoPostBack="True" CssClass="txt2"
                                                                    DataTextField="CountryName" DataValueField="CountryId" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvEmployeeCountry" runat="server" ControlToValidate="ddlEmployeeCountry"
                                                                    Font-Bold="true" InitialValue="0" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="Employee" />
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Zip
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtEmployeeZip" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                                <ajax:FilteredTextBoxExtender ID="txtEmployeeZip_Extender" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txtEmployeeZip">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Phone
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtEmployeePhone" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                                <ajax:FilteredTextBoxExtender ID="txtEmployeePhone_FilteredTextBoxExtender" runat="server"
                                                                    FilterType="Numbers,Custom" ValidChars="+,-" TargetControlID="txtEmployeePhone">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Ext.
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtEmployeeExt" runat="server" CssClass="txtMan2" Style="height: 22px"></asp:TextBox>
                                                                <ajax:FilteredTextBoxExtender ID="txtEmployeeExt_Extender1" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txtEmployeeExt">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Cell Phone
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtEmployeeCellPhone" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                                <ajax:FilteredTextBoxExtender ID="txtEmployeeCellPhone_Extender" runat="server" FilterType="Numbers,Custom"
                                                                    ValidChars="+,-" TargetControlID="txtEmployeeCellPhone">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Email
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtEmployeeEmail" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="revEmployeeEmail" runat="server" ControlToValidate="txtEmployeeEmail"
                                                                    Font-Bold="true" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                    ErrorMessage="*" SetFocusOnError="True" ValidationGroup="Employee" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Driver License State
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:DropDownList ID="ddlEmployeeDriverLicState" CssClass="txtMan2" runat="server"
                                                                    DataTextField="State" DataValueField="StateId">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Driver License No.
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtEmployeeDriverLicNo" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB" style="height: 22px">
                                                                Driver License Expiration Date
                                                            </td>
                                                            <td class="TableBorder" nowrap="nowrap" style="height: 22px">
                                                                <asp:TextBox ID="txtEmployeeLicExpDate" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                <ajax:CalendarExtender ID="calEmployeetxtLicExpDate" runat="server" PopupButtonID="imgEmployeeLicExpDate"
                                                                    TargetControlID="txtEmployeeLicExpDate">
                                                                </ajax:CalendarExtender>
                                                                <asp:Image ID="imgEmployeeLicExpDate" runat="server" ImageUrl="~/Images/calender-icon.gif"
                                                                    Style="cursor: pointer;" />
                                                                <ajax:FilteredTextBoxExtender ID="FilteredEmployeeTextBoxExtender1" runat="server"
                                                                    FilterType="Numbers,Custom" ValidChars="/,-" TargetControlID="txtEmployeeLicExpDate">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                            <td class="TableBorderB" style="height: 22px">
                                                                &nbsp;
                                                            </td>
                                                            <td class="TableBorder" style="height: 22px">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Special Payroll Conditions
                                                            </td>
                                                            <td class="TableBorder" nowrap="nowrap" colspan="3">
                                                                <asp:TextBox ID="txtEmployeeSpcPayCondt" runat="server" CssClass="txtMan2" Rows="5"
                                                                    TextMode="MultiLine" Width="550px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 5%;">
                                                        <asp:Button ID="btnBack" runat="server" Text="<< Back" Width="71px" CssClass="Btn_Form"
                                                            OnClick="btnBack_Click" />
                                                    </td>
                                                    <td style="width: 55%;" align="right">
                                                        <table style="margin-top: 10px;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="btnReset" runat="server" CausesValidation="false" Text="Reset" Width="71px"
                                                                        CssClass="Btn_Form" OnClick="btnReset_Click" />
                                                                </td>
                                                                <td width="50px">
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" Width="71px"
                                                                        CssClass="Btn_Form" CausesValidation="true" ValidationGroup="Buyer" OnClientClick="return ShowMessage();" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="padding-top: 16px;">
                                                        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="lblTextDisplayEntity"
                                                            Style="font-weight: 100; font-size: 12px; margin-left: 20px;"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </fieldset>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        function ShowMessage() {
            if (Page_ClientValidate())// first check the validators in ValidationGroup 
            {
                var ddlEntityT = document.getElementById('ctl00_ContentPlaceHolder1_ddlEntityType');
                var strType = ddlEntityT.options[ddlEntityT.selectedIndex].text;
                var message = "Do you want to add this " + strType + " ?";
                return confirm(message);
            }
        }


        function CheckBuyerCode(txt) {
            METAOPTION.WS.AutoFillNames.BuyerCodeAvailability(txt.value, wsSuccess, wsError)
        }

        function wsSuccess(value) {
            if (value == false) {
                document.getElementById("imgBuyerCode").src = "../Images/H_delete.png";
                document.getElementById("imgBuyerCode").title = "Not available";
                document.getElementById("ctl00_ContentPlaceHolder1_btnAdd").disabled = "disabled";
            }
            else if (value == true) {
                document.getElementById("imgBuyerCode").src = "../Images/H_active.png";
                document.getElementById("imgBuyerCode").title = "Available";
                document.getElementById("ctl00_ContentPlaceHolder1_btnAdd").disabled = false;
            }
        }

        function wsError(value) {
            document.getElementById("imgBuyerCode").src = "../Images/H_delete.png";
            document.getElementById("ctl00_ContentPlaceHolder1_btnAdd").disabled = "disabled";
        }

        function ValidatePage() {
            var valid = true;

            var dirBuyer = document.getElementById('<%=ddlBuyerDirectBuyer.ClientID %>');
            var parentBuyer = document.getElementById('<%=ddlBuyerParentBuyer.ClientID %>');
            var accessLevel = document.getElementById('<%=ddlBuyerAccessLevel.ClientID %>');

            if (dirBuyer.value == "0" && parentBuyer.value == "-1") {
                document.getElementById('<%=spanBuyerAccessLevel.ClientID %>').style.display = 'block';
                valid = false;
            }

            if (dirBuyer.value == "0" && parentBuyer.value != "-1" && accessLevel.value == "") {
                document.getElementById('<%=spanBuyerAccessLevel.ClientID %>').style.display = 'block';
                valid = false;
            }

            return valid;
        }

        function onDirectBuyerChange() {
            var dirBuyer = document.getElementById('<%=ddlBuyerDirectBuyer.ClientID %>');
            var parentBuyer = document.getElementById('<%=ddlBuyerParentBuyer.ClientID %>');
            var accessLevel = document.getElementById('<%=ddlBuyerAccessLevel.ClientID %>');

            if (dirBuyer.value == "1") {
                parentBuyer.value = "-1";
                parentBuyer.disabled = true;
                accessLevel.value = "";
                accessLevel.disabled = true;
            }
            else {
                parentBuyer.disabled = false;
                accessLevel.disabled = false;
            }
        }

        window.onscroll = SetScrollPosition;
        function SetScrollPosition() {
            var hScroll = document.getElementById('<%=hScrollPosition.ClientID %>');
            hScroll.value = document.documentElement.scrollTop;
            if (hScroll.value == 0) {
                hScroll.value = window.pageYOffset;
            }
        }

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
</asp:Content>
