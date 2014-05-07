<%@ Page Title="" Language="C#" 
    AutoEventWireup="true" EnableEventValidation="false" CodeBehind="UCRLog.aspx.cs"
    Inherits="METAOPTION.UI.UCRLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div onmousemove="SetProgressPosition(event)">
        <asp:UpdatePanel ID="upResultGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="TableHeadingBg TableHeading" style="text-align: left">
                    <div style="width: 90%; padding: 5px 0px; float: left">
                        UCR Log Details</div>
                    <div id="dvCollapse" runat="server" class="TOtoggle" style="float: right; padding-right: 10px;
                        padding-top: 5px;">
                        <%-- <asp:Button ID="btnapprove" Text="Approve" class="btn" runat="server" />--%>
                    </div>
                </div>
                <div id="dvSearch" runat="server" class="dvSearch">
                    <div style="width: 21%; float: left; padding: 5px;">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="TableBorder">
                                    CR#
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtCRID" runat="server" MaxLength="17" CssClass="txt2" Height="16px" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 31%; float: left; padding: 5px 5px 5px 10px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="TableBorder">
                                    From Date
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="txt1" Width="78px" />
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateFrom"
                                        PopupButtonID="txtDateFrom" />
                                </td>
                                <td class="TableBorder">
                                    To Date
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="txt1" Width="78px" />
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateTo"
                                        PopupButtonID="txtDateTo" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 30%; float: left; padding: 5px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="150" AssociatedUpdatePanelID="upResultGrid">
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
                <div style="clear: both; margin-top: 10px">
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
                                <%= gvUCRDetailsList.PageCount%>
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
                        <asp:GridView ID="gvUCRDetailsList" runat="server" DataKeyNames="UCRLogId" Width="100%"
                            AutoGenerateColumns="False" EmptyDataText="No record found for this search criteria."
                            RowStyle-CssClass="gvRow" GridLines="None" PagerSettings-Visible="false" AllowPaging="true"
                            PageSize="50" AllowSorting="true" OnRowDataBound="gvUCRDetailsList_RowDataBound"
                            OnSorting="gvUCRDetailsList_Sorting" CssClass="Grid">
                            <Columns>
                                <%--  <asp:BoundField DataField="UCRLogId" HeaderText="UCR LogId" ItemStyle-CssClass="GridContent"
                                    DataFormatString="{0:#,###}" HeaderStyle-Width="20px" ItemStyle-Width="20px" />--%>
                                <asp:BoundField DataField="MethodName" HeaderText="Method Name" ItemStyle-CssClass="GridContent"
                                    DataFormatString="{0:#,###}" HeaderStyle-CssClass="GridHeader" HeaderStyle-Wrap="true"
                                    ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="FromDate" HeaderText="FromDate" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" ItemStyle-Width="120px" ItemStyle-Wrap="true"
                                    DataFormatString="{0:MM/dd/yyyy hh:mm tt}" SortExpression="UCR.FromDate" />
                                <asp:BoundField DataField="ToDate" HeaderText="ToDate" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" ItemStyle-Width="120px" ItemStyle-Wrap="true"
                                    DataFormatString="{0:MM/dd/yyyy hh:mm tt}" />
                                <asp:BoundField DataField="TotalRecords" HeaderText="Total Records" ItemStyle-CssClass="GridContentNumbers"
                                    HeaderStyle-CssClass="GridHeader" />
                                <asp:BoundField DataField="RecordsPerPage" HeaderText="Records PerPage" HeaderStyle-CssClass="GridHeader"
                                    HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:#,###}" ItemStyle-CssClass="GridContentNumbers"
                                    ItemStyle-HorizontalAlign="Right" ItemStyle-Width="20px" />
                                <asp:TemplateField HeaderText="Total Pages" ItemStyle-CssClass="GridContentNumbers"
                                    HeaderStyle-CssClass="GridHeader" ItemStyle-Width="30px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUCRPages" runat="server" Text=' <%# Eval("TotalPages")%>'></asp:Label><br />
                                        <asp:ImageButton ID="btnUCRPages" runat="server" ImageUrl="~/Images/expand.png" ToolTip="Show UCRLog Pages Details"
                                            OnClick="btnUCRPages_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="TotalPages" HeaderText="Total Pages" ItemStyle-CssClass="GridContentNumbers"
                                    ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="GridHeader" ItemStyle-Width="30px" />--%>
                                <asp:BoundField DataField="CurrentPage" HeaderText="Current Page" ItemStyle-CssClass="GridContentNumbers"
                                    DataFormatString="{0:#,###}" HeaderStyle-Width="30px" ItemStyle-Width="30px"
                                    HeaderStyle-CssClass="GridHeader" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Right" />
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-CssClass="GridContent" HeaderText="Transaction Status" HeaderStyle-Width="30px"
                                    ItemStyle-Width="30px">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnErrorCode" runat="server" Value=' <%# Eval("ErrCode")%>' />
                                        <asp:HiddenField ID="hdnErrormsg" runat="server" Value=' <%# Eval("ErrMsg")%>' />
                                        <asp:Label ID="lblTransactionStatus" runat="server" Text=' <%# Eval("TransactionStatus")%>'
                                            Style="cursor: pointer"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="TransactionStatus" HeaderText="Transaction Status" ItemStyle-CssClass="GridContent"
                                    DataFormatString="{0:#,###}" HeaderStyle-Width="30px" ItemStyle-Width="30px"
                                    HeaderStyle-CssClass="GridHeader" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Right" />--%>
                                <asp:BoundField DataField="TransactionTime" HeaderText="Transaction Time" ItemStyle-CssClass="GridContent"
                                    ItemStyle-Width="60px" HeaderStyle-CssClass="GridHeader" ItemStyle-Wrap="true"
                                    DataFormatString="{0:MM/dd/yyyy hh:mm tt}" />
                                <asp:BoundField DataField="DateAdded" HeaderText="DateAdded" ItemStyle-CssClass="GridContent"
                                    DataFormatString="{0:MM/dd/yyyy hh:mm tt}" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px" ItemStyle-Wrap="true"
                                    SortExpression="UCR.DateAdded" />
                                <asp:BoundField DataField="DisplayName" HeaderText="AddedBy" ItemStyle-CssClass="GridContent"
                                    HeaderStyle-CssClass="GridHeader" HeaderStyle-Wrap="true" ItemStyle-Width="80px" />
                                <asp:BoundField DataField="Reference" HeaderText="Reference" ItemStyle-CssClass="GridContent"
                                    DataFormatString="{0:#,###}" HeaderStyle-Width="30px" ItemStyle-Width="30px"
                                    HeaderStyle-CssClass="GridHeader" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalFetchRecords" HeaderText="Total Fetch Records" ItemStyle-CssClass="GridContentNumbers"
                                    DataFormatString="{0:#,###}" HeaderStyle-Width="30px" ItemStyle-Width="30px"
                                    HeaderStyle-CssClass="GridHeader" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalNotFetchRecords" HeaderText="Total Not Fetch Records"
                                    ItemStyle-CssClass="GridContentNumbers" DataFormatString="{0:#,###}" HeaderStyle-Width="30px"
                                    ItemStyle-Width="30px" HeaderStyle-CssClass="GridHeader" HeaderStyle-Wrap="true"
                                    ItemStyle-HorizontalAlign="Right" />
                                <%-- <asp:BoundField DataField="FailedCR" HeaderText="Failed CR" ItemStyle-CssClass="GridContentNumbers"
                                    DataFormatString="{0:#,###}" HeaderStyle-Width="30px" ItemStyle-Width="30px"
                                    HeaderStyle-CssClass="GridHeader" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Right" />--%>
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-CssClass="GridContentCentre" HeaderText="Action">
                                    <ItemTemplate>
                                        <%--  <asp:HiddenField ID="hdnUCRDetails" runat="server" Value=' <%# Eval("UCRHeader")%>' />--%>
                                        <asp:HiddenField ID="hdnRecord" runat="server" Value=' <%# Eval("TotalFetchRecords")%>' />
                                        <%-- <asp:ImageButton ID="btnUCRDetail" runat="server" ImageUrl="~/Images/info.png" />--%>
                                        <asp:ImageButton ID="imgbtnSelect" runat="server" ImageUrl="../Images/Select.gif"
                                            OnClick="imgbtnSelect_Click" ToolTip="Show UCRLog Details" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <tr id="trnew" runat="server" visible="false">
                                            <td colspan="16" class="GridContent" style="padding: 5px 5px 5px 30px;">
                                                <asp:GridView ID="gvUCRLogPages" runat="server" DataKeyNames="UCRPageID" Width="85%"
                                                    EmptyDataText="No record found." EmptyDataRowStyle-CssClass="GridEmptyRow" RowStyle-CssClass="gvRow"
                                                    ItemStyle-CssClass="GridContent" AutoGenerateColumns="false" HeaderStyle-CssClass="GridHeader"
                                                    GridLines="none">
                                                    <EmptyDataRowStyle CssClass="gvEmpty" />
                                                    <Columns>
                                                        <asp:BoundField DataField="RecordPerPage" HeaderText="Record PerPage" ItemStyle-CssClass="GridContentNumbers"
                                                            DataFormatString="{0:#,###}" HeaderStyle-Width="20px" ItemStyle-Width="20px" />
                                                        <asp:BoundField DataField="CurrentPage" HeaderText="Current Page" ItemStyle-CssClass="GridContentNumbers"
                                                            DataFormatString="{0:#,###}" HeaderStyle-Width="20px" ItemStyle-Width="20px" />
                                                        <asp:BoundField DataField="TransactionStatus" HeaderText="Transaction Status" ItemStyle-CssClass="GridContent"
                                                            HeaderStyle-Width="20px" ItemStyle-Width="20px" />
                                                        <asp:BoundField DataField="TotalFetchRecords" HeaderText="Total Fetch Records" ItemStyle-CssClass="GridContentNumbers"
                                                            DataFormatString="{0:#,###}" HeaderStyle-Width="20px" ItemStyle-Width="20px" />
                                                        <asp:BoundField DataField="TotalNotFetchRecords" HeaderText="Total Not Fetch Records"
                                                            ItemStyle-CssClass="GridContentNumbers" DataFormatString="{0:#,###}" HeaderStyle-Width="20px"
                                                            ItemStyle-Width="20px" />
                                                        <asp:BoundField DataField="FailedCR" HeaderText="FailedCR" ItemStyle-CssClass="GridContentNumbers"
                                                            HeaderStyle-Width="20px" ItemStyle-Width="20px" />
                                                        <asp:BoundField DataField="DateAdded" HeaderText="DateAdded" ItemStyle-CssClass="GridContent"
                                                            HeaderStyle-Width="70px" ItemStyle-Width="70px" />
                                                        <%-- <asp:BoundField DataField="UserName" HeaderText="AddedBy" ItemStyle-CssClass="GridContent"
                                                            HeaderStyle-Width="20px" ItemStyle-Width="20px" />--%>
                                                        <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-Width="20px" ItemStyle-CssClass="GridContentCentre"
                                                            HeaderText="Action" HeaderStyle-Width="20px" ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnUCRPageDetails" runat="server" CommandArgument='<%#Eval("UCRPageID") %>'
                                                                    OnClick="btnUCRPageDetails_Click" ImageUrl="../Images/XML.jpg" />
                                                                <%-- <a id="ibtnCRAvailable" runat="server" target="_blank" href="#" style="text-decoration: none;">
                                                                    <img id="imgAvailabel" runat="server" alt="No Image" src="../Images/Select.gif" style="padding-bottom: 4px" />
                                                                </a>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <ItemStyle />
                                    <HeaderStyle />
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
                                <%= gvUCRDetailsList.PageCount%>
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

        function ShowRequestXML() {
            window.open("../UI/responsedata.xml", "mywindow", "location=1,status=1,scrollbars=1, width=745,height=475,left=212,top=100");
        }
    </script>
</asp:Content>
