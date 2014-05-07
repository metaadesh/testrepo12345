<%@ Page Title="" Language="C#" 
    AutoEventWireup="true" CodeBehind="UCRUpdateLog.aspx.cs" Inherits="METAOPTION.UI.UCRUpdateLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div onmousemove="SetProgressPosition(event)">
        <asp:UpdatePanel ID="upUpdateLog" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="TableHeadingBg TableHeading" style="text-align: left">
                    <div style="width: 90%; padding: 5px 0px; float: left">
                        UCR Update Log Details</div>
                    <div id="dvCollapse" runat="server" class="TOtoggle" style="float: right; padding-right: 10px;
                        padding-top: 5px;">
                    </div>
                </div>
                <div id="dvSearch" runat="server" style="width: 100%; height: auto">
                    <div style="width: 31%; float: left; padding: 5px;">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="TableBorder">
                                    VIN#
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtVin" runat="server" MaxLength="17" CssClass="txt2" Height="16px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder" style="width: 100px">
                                    Year
                                </td>
                                <td class="TableBorder" style="width: 200px">
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="txt1" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Make
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlMake" runat="server" CssClass="txt2" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Model
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlModel" runat="server" CssClass="txt2" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Body
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlBody" runat="server" CssClass="txt2" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 28%; float: left; padding: 5px 5px 5px 5px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="TableBorder">
                                    CR#
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtCRID" runat="server" MaxLength="17" CssClass="txt2" Height="16px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Transaction Status
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlTranStatus" runat="server" CssClass="txt2" Height="16px">
                                        <asp:ListItem Text="ALL" Value="-1" Selected="True" />
                                        <asp:ListItem Text="Success" Value="1" />
                                        <asp:ListItem Text="Failed" Value="0" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Data Status
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlDataStatus" runat="server" CssClass="txt2">
                                        <asp:ListItem Text="ALL" Value="-1" Selected="True" />
                                        <asp:ListItem Text="Success" Value="1" />
                                        <asp:ListItem Text="Failed" Value="0" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Added Date
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtSyncDateFrom" runat="server" CssClass="txt1" Width="78px" />
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSyncDateFrom"
                                        PopupButtonID="txtSyncDateFrom" />
                                    <asp:TextBox ID="txtSyncDateTo" runat="server" CssClass="txt1" Width="78px" />
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSyncDateTo"
                                        PopupButtonID="txtSyncDateTo" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Reference
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtRefrence" runat="server" MaxLength="17" CssClass="txt2" Height="16px" />
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
                                <td>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="150" AssociatedUpdatePanelID="upUpdateLog">
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
                <div style="clear: both;">
                    <table border="0" style="width: 100%;" class="TableHeadingBg TableHeading">
                        <tr>
                            <td style="width: 40%">
                                <asp:Label ID="lblCount" runat="server" BorderColor="Transparent" BackColor="Transparent"
                                    ForeColor="#21618C" />
                            </td>
                            <td style="width: 25%; text-align: right; white-space: nowrap; color: #21618C">
                                Page&nbsp;&nbsp;
                                <asp:DropDownList ID="ddlPaging" runat="server" CssClass="txt1" Width="50px" OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged"
                                    AutoPostBack="true" />
                                of
                                <%= gvUpdateLog.PageCount%>
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
                        <asp:GridView ID="gvUpdateLog" runat="server" DataKeyNames="UCRUpdateID" Width="100%"
                            AutoGenerateColumns="False" EmptyDataText="No record found for this search criteria."
                            RowStyle-CssClass="gvRow" GridLines="None" PagerSettings-Visible="false" AllowPaging="true"
                            PageSize="50" AllowSorting="true" OnSorting="gvUpdateLog_Sorting" CssClass="Grid"
                            OnRowDataBound="gvUpdateLog_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="CR_ID" HeaderText="CR#" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" SortExpression="objUCRUpdate.CR_ID" />
                                <asp:BoundField DataField="VIN" HeaderText="VIN#" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" />
                                <asp:BoundField DataField="Year" HeaderText="Year" ItemStyle-CssClass="GridContentNumbers"
                                    HeaderStyle-CssClass="GridHeader" SortExpression="Year" />
                                <asp:BoundField DataField="MakeName" HeaderText="Make" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" SortExpression="VINDivisionName" />
                                <asp:BoundField DataField="ModelName" HeaderText="Model" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" SortExpression="VINModelName" />
                                <asp:BoundField DataField="Body" HeaderText="Body" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" SortExpression="VINStyleName" />
                                <asp:BoundField DataField="DateAdded" HeaderText="DateAdded" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="140px" ItemStyle-Width="140px"
                                    SortExpression="objUCRUpdate.DateAdded" />
                                <asp:BoundField DataField="TransactionStatus" HeaderText="Transaction Status" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" />
                                <asp:BoundField DataField="MethodName" HeaderText="Method Name" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" />
                                <asp:BoundField DataField="TransactionErrCode" HeaderText="Transaction ErrCode" ItemStyle-CssClass="GridContentNumbers"
                                    HeaderStyle-CssClass="GridHeader" />
                                <asp:BoundField DataField="TransactionErrMsg" HeaderText="Transaction ErrMsg" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" />
                                <asp:BoundField DataField="Reference" HeaderText="Reference" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" />
                                <asp:BoundField DataField="DataStatus" HeaderText="Data Status" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" />
                                <asp:BoundField DataField="DataErrMsg" HeaderText="Data ErrMsg" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" />
                                <asp:BoundField DataField="RefNo" HeaderText="RefNo" ItemStyle-CssClass="GridContentNumbers"
                                    HeaderStyle-CssClass="GridHeader" />
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-Width="90px" ItemStyle-CssClass="GridContentCentre"
                                    HeaderText="Action" HeaderStyle-Width="90px" ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnRequestCount" Value='<%# Eval("RequestCount") %>' runat="server" />
                                        <asp:HiddenField ID="hdnResponseCount" Value='<%# Eval("ResponseCount") %>' runat="server" />
                                        <asp:ImageButton ID="btnUCRRequest" runat="server" CommandArgument='<%#Eval("UCRUpdateID") %>'
                                            Style="padding-bottom: 7px" OnClick="btnUCRRequest_Click" ToolTip="UCR Request Details"
                                            ImageUrl="../Images/XML.jpg" />
                                        <asp:ImageButton ID="btnUCRResponse" runat="server" CommandArgument='<%#Eval("UCRUpdateID") %>'
                                            Style="padding-bottom: 7px" OnClick="btnUCRResponse_Click" ToolTip="UCR Response Details"
                                            ImageUrl="../Images/XML.jpg" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings Mode="NumericFirstLast" />
                            <RowStyle CssClass="gvRow" />
                            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
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
                                Page
                                <asp:DropDownList ID="ddlPaging1" runat="server" CssClass="txt1" Width="50px" OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged"
                                    AutoPostBack="true" />
                                of
                                <%= gvUpdateLog.PageCount%>
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
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script language="javascript" type="text/javascript">
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
    <script type="text/javascript" language="javascript">
        function ShowRequestXML() {
            window.open("../UI/UCRUpdateLodRequestData.xml", "mywindow", "location=1,status=1,scrollbars=1, width=745,height=475,left=212,top=100");
        }
        function ShowResponseXML() {
            window.open("../UI/UCRUpdateLodResponseData.xml", "mywindow", "location=1,status=1,scrollbars=1, width=745,height=475,left=212,top=100");
        }
    </script>
</asp:Content>
