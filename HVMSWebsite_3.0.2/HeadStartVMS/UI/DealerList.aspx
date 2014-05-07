<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DealerList.aspx.cs" Inherits="METAOPTION.UI.DealerList"
    Title="HeadstartVMS::Purchased From/Sold To List" MasterPageFile="~/UI/MasterPage.Master" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hWelcomeTasks" runat="server" />
    <div onmousemove="SetProgressPosition(event)">
        <script type="text/javascript">
            function toggleMe() {
                $(".TOtoggle").toggle();
                $('.dvSearch').toggle();
            }
        </script>
        <asp:UpdatePanel ID="upMainPage" runat="server">
            <ContentTemplate>
                <div class="TableHeadingBg TableHeading" style="text-align: left;">
                    <div style="width: 90%; padding: 5px 0px; float: left">
                        Purchased From/Sold To List</div>
                    <div id="dvCollapse" runat="server" class="TOtoggle" style="float: right; padding-right: 10px;
                        padding-top: 5px; display: none" onclick="toggleMe()">
                        <img id="imgCollapse" src="../Images/arrow_up.jpg" alt="" title="Collapse" />
                    </div>
                    <div id="dvExpand" runat="server" class="TOtoggle" style="float: right; padding-right: 10px;
                        padding-top: 5px;" onclick="toggleMe()">
                        <img id="imgExpand" src="../Images/arrow_down.jpg" alt="" title="Expand" />
                    </div>
                </div>
                <div id="dvSearch" runat="server" class="dvSearch" style="display: block">
                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse;
                        clear: both">
                        <tr>
                            <td class="TableBorder">
                                Name
                            </td>
                            <td class="TableBorder">
                                <asp:TextBox ID="txtName" runat="server" CssClass="txt2" />
                            </td>
                            <td class="TableBorder">
                                Category
                            </td>
                            <td class="TableBorder">
                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="txt2" />
                            </td>
                            <td class="TableBorder" nowrap>
                                Type
                            </td>
                            <td class="TableBorder">
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="txt2" />
                            </td>
                            <td class="TableBorder">
                                Status
                            </td>
                            <td class="TableBorder">
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="txt2">
                                    <asp:ListItem Text="ALL" Value="-1" Selected="True" />
                                    <asp:ListItem Text="Active" Value="1" />
                                    <asp:ListItem Text="Archive" Value="2" />
                                    <%--<asp:ListItem Text="Deleted" Value="0" />--%>
                                </asp:DropDownList>
                                <asp:UpdateProgress ID="uprogSearch" runat="server" AssociatedUpdatePanelID="upMainPage">
                                    <ProgressTemplate>
                                        <div id="dvProg" class="overlay">
                                            <img id="image" src="../Images/Wait.gif" alt="Wait..." style="vertical-align: middle" />&nbsp;Please
                                            wait...
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder">
                                City
                            </td>
                            <td class="TableBorder">
                                <asp:TextBox ID="txtCity" runat="server" CssClass="txt2" />
                            </td>
                            <td class="TableBorder">
                                Country
                            </td>
                            <td class="TableBorder">
                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="txt2" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" />
                            </td>
                            <td class="TableBorder">
                                State
                            </td>
                            <td class="TableBorder" nowrap>
                                <asp:UpdatePanel ID="upState" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlState" runat="server" CssClass="txt2" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlCountry" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td class="TableBorder">
                                Zip
                            </td>
                            <td class="TableBorder">
                                <asp:TextBox ID="txtZip" runat="server" CssClass="txt2" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder">
                                Days since last transaction
                            </td>
                            <td class="TableBorder">
                                <asp:TextBox ID="txtDaySinceTransaction" runat="server" CssClass="txt2" MaxLength="10" />
                                <asp:RegularExpressionValidator runat="server" ID="REVtxtDaySinceTransaction" ControlToValidate="txtDaySinceTransaction"
                                    ValidationExpression="^[0-9]+$" ErrorMessage="*" Font-Bold="true" ForeColor="Red" />
                            </td>
                            <td class="TableBorder">
                                New customer since
                            </td>
                            <td class="TableBorder">
                                <asp:TextBox ID="txtNewCustomerSince" runat="server" CssClass="txt2" Width="80px" />
                                <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtNewCustomerSince"
                                    PopupButtonID="txtNewCustomerSince" />
                            </td>
                            <td class="TableBorder">
                                Welcome Tasks
                            </td>
                            <td class="TableBorder">
                                <telerik:RadComboBox ID="ddlWelcomeTasks" runat="server" Width="150px" AllowCustomText="true"
                                    EmptyMessage="">
                                    <ItemTemplate>
                                        <div onclick="StopPropagation(event)" class="combo-item-template">
                                            <asp:CheckBox runat="server" ID="chk1" CssClass="cb" Text='<%#Eval("Task")%>' onclick="onCheckBoxClick(this,'WelcomeTasks')" />
                                        </div>
                                    </ItemTemplate>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder" style="vertical-align: top">
                                Sort 1
                            </td>
                            <td class="TableBorder">
                                <asp:DropDownList ID="ddlSort1" runat="server" CssClass="txt2" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSort1_SelectedIndexChanged" />
                                <asp:RadioButtonList ID="rbtnSort1Direction" runat="server" RepeatDirection="Horizontal"
                                    CellPadding="2">
                                    <asp:ListItem Value="ASC" Text=" A - Z &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                        Selected="True" />
                                    <asp:ListItem Value="DESC" Text=" Z - A " />
                                </asp:RadioButtonList>
                            </td>
                            <td class="TableBorder" style="vertical-align: top">
                                Sort 2
                            </td>
                            <td class="TableBorder">
                                <asp:DropDownList ID="ddlSort2" runat="server" CssClass="txt2" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSort2_SelectedIndexChanged" />
                                <asp:RadioButtonList ID="rbtnSort2Direction" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="ASC" Text=" A - Z &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                        Selected="True" />
                                    <asp:ListItem Value="DESC" Text=" Z - A " />
                                </asp:RadioButtonList>
                            </td>
                            <td class="TableBorder" style="vertical-align: top">
                                Sort 3
                            </td>
                            <td class="TableBorder">
                                <asp:DropDownList ID="ddlSort3" runat="server" CssClass="txt2" AutoPostBack="true" />
                                <asp:RadioButtonList ID="rbtnSort3Direction" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="ASC" Text=" A - Z &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                        Selected="True" />
                                    <asp:ListItem Value="DESC" Text=" Z - A " />
                                </asp:RadioButtonList>
                            </td>
                            <td class="TableBorder" colspan="2" style="text-align: center">
                                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn" OnClick="btnReset_Click"
                                    Width="75px" />
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn" OnClick="btnSearch_Click"
                                    Width="75px" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <table>
                        <tr>
                            <td>
                                <p style="color: Red; font-size: 10px">
                                    <b>* Search filters will not be reset if the browser back button is clicked after viewing
                                        the details on the next page.</b></p>
                            </td>
                        </tr>
                    </table>
                </div>
                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                    <tr>
                        <td class="TableBorder" colspan="6">
                            <asp:UpdatePanel ID="upDealerList" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
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
                                                <%= gvViewDealer.PageCount%>
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
                                            <td style="white-space: nowrap;">
                                                <asp:Button ID="btnViewMap" runat="server" Text="View Map" CssClass="btn" OnClick="btnViewMap_Click"
                                                    Style="margin-left: 30px; margin-right: 10px; background-color: #FF9900;" />
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
                                        <asp:GridView ID="gvViewDealer" runat="server" EmptyDataText="No record found for selected criteria."
                                            AutoGenerateColumns="False" Width="100%" AllowPaging="True" GridLines="None"
                                            PagerSettings-Visible="false" AllowSorting="true" OnSorting="gvViewDealer_Sorting">
                                            <Columns>
                                                <asp:ButtonField Text="SingleClick" Visible="false" CommandName="SingleClick" />
                                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                                    HeaderStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hylnkView" ToolTip="View" NavigateUrl='<%# "ViewDealer.aspx?Mode=View&EntityId="+Eval("DealerId")+"&type=1" %>'
                                                            runat="server" ImageUrl="~/Images/Select.gif" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DealerID" HeaderStyle-Wrap="false" HeaderText="ID" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent"></asp:BoundField>
                                                <asp:BoundField DataField="DealerName" HeaderStyle-Wrap="false" HeaderText="Purchased From/Sold To"
                                                    HeaderStyle-CssClass="GridHeader" HeaderStyle-Font-Underline="true" ItemStyle-CssClass="GridContent"
                                                    SortExpression="DealerName"></asp:BoundField>
                                                <asp:BoundField DataField="Street" HeaderText="Street" HeaderStyle-Wrap="false" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" ItemStyle-Width="160px"></asp:BoundField>
                                                <asp:BoundField DataField="Suite" HeaderText="Suite" HeaderStyle-Wrap="false" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" ItemStyle-Width="80px"></asp:BoundField>
                                                <asp:BoundField DataField="City" HeaderText="City" HeaderStyle-Wrap="false" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" HeaderStyle-Font-Underline="true" ItemStyle-Width="150px"
                                                    SortExpression="City"></asp:BoundField>
                                                <asp:BoundField DataField="StateCode" HeaderText="State" HeaderStyle-Wrap="false"
                                                    HeaderStyle-CssClass="GridHeader" HeaderStyle-Font-Underline="true" ItemStyle-CssClass="GridContent"
                                                    ItemStyle-Width="50px" SortExpression="State"></asp:BoundField>
                                                <asp:BoundField DataField="Zip" HeaderText="Zip" HeaderStyle-Wrap="false" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" HeaderStyle-Font-Underline="true" ItemStyle-Width="50px"
                                                    SortExpression="ZIP"></asp:BoundField>
                                                <asp:BoundField DataField="CountryName" HeaderText="Country" HeaderStyle-Wrap="false"
                                                    HeaderStyle-CssClass="GridHeader" HeaderStyle-Font-Underline="true" ItemStyle-CssClass="GridContent"
                                                    ItemStyle-Width="50px" SortExpression="CountryCode"></asp:BoundField>
                                                <asp:BoundField DataField="Phone1" HeaderText="Phone#" HeaderStyle-Wrap="false" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" ItemStyle-Width="75px"></asp:BoundField>
                                              <%--  <asp:BoundField DataField="WelcomeTask" HeaderText="WelcomeTask" HeaderStyle-Wrap="false"
                                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" ItemStyle-Width="75px">
                                                </asp:BoundField>--%>
                                                <asp:BoundField DataField="DaysSinceLastTransaction" HeaderText="Days Since" HeaderStyle-Wrap="false"
                                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" ItemStyle-Width="40px"
                                                    ItemStyle-HorizontalAlign="Right"></asp:BoundField>
                                                <asp:BoundField DataField="TotalSold" HeaderText="Sold" HeaderStyle-Wrap="false"
                                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" ItemStyle-Width="40px"
                                                    ItemStyle-HorizontalAlign="Right"></asp:BoundField>
                                                <asp:BoundField DataField="TotalPurchased" HeaderText="Purchased" HeaderStyle-Wrap="false"
                                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" ItemStyle-Width="30px"
                                                    ItemStyle-HorizontalAlign="Right"></asp:BoundField>
                                            </Columns>
                                            <RowStyle CssClass="gvRow" />
                                            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                            <HeaderStyle CssClass="gvHeading"></HeaderStyle>
                                            <AlternatingRowStyle CssClass="gvAlternateRow"></AlternatingRowStyle>
                                            <EmptyDataRowStyle CssClass="gvEmpty" />
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
                                                <%= gvViewDealer.PageCount%>
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
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="FooterContentDetails" colspan="6" width="100%">
                            <a href="AddCustomer.aspx" class="AddNewExpenseTxt">Add New Purchased From/Sold To</a>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        var objHidden;
        var combo;
        var cancelDropDownClosing = false;



        function StopPropagation(e) {
            //cancel bubbling
            e.cancelBubble = true;
            if (e.stopPropagation) {
                e.stopPropagation();
            }
        }

        function onDropDownClosing() {
            cancelDropDownClosing = false;
        }

        function onCheckBoxClick(chk, ctrlType) {
            if (ctrlType == "WelcomeTasks") {
                objHidden = document.getElementById("<%= this.hWelcomeTasks.ClientID %>");
                combo = $find("<%= ddlWelcomeTasks.ClientID %>");

            }
            //holds the text of all checked items
            var text = "";
            //holds the values of all checked items
            var values = "";
            //get the collection of all items
            var items = combo.get_items();
            //enumerate all items
            for (var i = 0; i < items.get_count(); i++) {
                var item = items.getItem(i);
                //get the checkbox element of the current item
                var chk1 = $get(combo.get_id() + "_i" + i + "_chk1");
                if (chk1.checked) {
                    text += item.get_text() + ",";
                    values += item.get_value() + ",";
                }
            }
            //remove the last comma from the string
            text = removeLastComma(text);
            values = removeLastComma(values);

            if (text.length > 0) {
                //set the text of the combobox                   
                combo.set_text(text);
                objHidden.value = values;

            }
            else {
                //all checkboxes are unchecked
                //so reset the controls
                combo.set_text("");
                objHidden.value = '';
            }

        }

        //this method removes the ending comma from a string
        function removeLastComma(str) {
            return str.replace(/,$/, "");
        }


        SetWelcomeValues();


        function SetWelcomeValues() {
            objHidden = document.getElementById("<%= this.hWelcomeTasks.ClientID %>");
            combo = document.getElementById("<%= ddlWelcomeTasks.ClientID %>");

            var arrChkItems = objHidden.value.split(',');
            var item = "";
            var Input_Field = $get(combo.id + "_Input");
            for (var j = 0; j < arrChkItems.length; j++) {
                for (var i = 0; i < combo.childElementCount; i++) {

                    //get the checkbox element of the current item
                    var chk1 = $get(combo.id + "_i" + i + "_chk1");
                    var chk1_value = chk1.nextElementSibling.innerHTML;
                         
                          if (arrChkItems[j] == i+1 ) {
                          chk1.checked = true;
                          item = item.concat(chk1_value)+',';
                          break;
                    }
                }
              } 
              Input_Field.value = item.slice(0, item.length - 1);
        }

 
    </script>
    <style type="text/css">
        .cb label
        {
            margin-left: 5px;
        }
    </style>
</asp:Content>
