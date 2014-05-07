<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UCRDetail.aspx.cs" Inherits="METAOPTION.UI.UCRDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>HeadStart VMS</title>
    <link href="../CSS/AjaxRelated.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/MainStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ModalPopUp.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ControlStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/thickbox.css" rel="stylesheet" type="text/css" />
    <%--<script src="../CSS/jquery-1.2.6.min.js" type="text/javascript"></script>--%>
    <script src="../CSS/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../CSS/thickbox-compressed.js" type="text/javascript"></script>
    <link href="../CSS/tipTip.css" rel="stylesheet" type="text/css" />
    <script src="../CSS/jquery.tipTip.minified.js" type="text/javascript"></script>
    <script src="../CSS/jquery.tipTip.js" type="text/javascript"></script>

    <script type="text/javascript">
        function toggleSelection(Source) {
            var crvalues = '';
            var isChecked = Source.checked;
            $("#grducrdetail input[id*='rdbtn']").each(function (index) {
                $(this).attr('checked', false);
            });
            Source.checked = isChecked;
        }

        function ClosePopUpWindow() {
            var querystring=GetUrlQueryString();
            var parentpage = window.parent.location.pathname.substring(window.parent.location.pathname.lastIndexOf("/") + 1);
            if (querystring != "")
                parentpage += "?" + querystring;
            var modalPopup = window.parent.$find("mpecrbeh");
            if (modalPopup != null) {
                modalPopup.hide();
                //window.parent.location.href = "PreInventory.aspx";
                window.parent.location.href = parentpage;
            }
        }

        function GetUrlQueryString() {
            var qrystring = "";
            if (window.parent.location.href.indexOf('?') != -1) {
                var QueryString = window.parent.location.href.slice(window.parent.location.href.indexOf('?') + 1).split('&');
                for (var i = 0; i < QueryString.length; i++) {
                    var name = QueryString[i].split('=')[0];
                    var value = QueryString[i].split('=')[1];
                    qrystring += name + "=" + value + "&";
                }
            }
            if (qrystring.length > 0)
                return qrystring.substring(0, qrystring.length - 1);
            else
                return qrystring;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:700px;">
    <div style="text-align:center;">
        <asp:Label ID="lblCRError" runat="server" ForeColor="Red" EnableViewState="false" />
    </div>
    <asp:GridView ID="grducrdetail" runat="server" DataKeyNames="CR" AutoGenerateColumns="false"
        ShowHeader="true" AllowPaging="true" PageSize="15" AllowSorting="true" Width="100%"
        EmptyDataText="No record found." EmptyDataRowStyle-CssClass="GridEmptyRow" CssClass="Grid">
        <Columns>
            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="15px" ItemStyle-Width="15px">
                <ItemTemplate>
                    <asp:RadioButton ID="rdbtn" runat="server" onclick="toggleSelection(this);" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CR" HeaderText="CR" HeaderStyle-CssClass="GridHeader"
                ItemStyle-CssClass="GridContentNumbers" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="true"
                HeaderStyle-Width="15px" ItemStyle-Width="15px"></asp:BoundField>
            <asp:BoundField DataField="VIN" HeaderText="VIN#" HeaderStyle-CssClass="GridHeader"
                ItemStyle-CssClass="GridContent" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true"
                HeaderStyle-Width="15px" ItemStyle-Width="15px"></asp:BoundField>
            <asp:BoundField DataField="Year" HeaderText="Year" HeaderStyle-CssClass="GridHeader"
                ItemStyle-CssClass="GridContent" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="true"
                HeaderStyle-Width="15px" ItemStyle-Width="15px"></asp:BoundField>
            <asp:BoundField DataField="Make" HeaderText="Make" HeaderStyle-CssClass="GridHeader"
                ItemStyle-CssClass="GridContent" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true"
                HeaderStyle-Width="15px" ItemStyle-Width="15px"></asp:BoundField>
            <asp:BoundField DataField="Model" HeaderText="Model" HeaderStyle-CssClass="GridHeader"
                ItemStyle-CssClass="GridContent" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true"
                HeaderStyle-Width="15px" ItemStyle-Width="15px"></asp:BoundField>
            <asp:BoundField DataField="Body" HeaderText="Body" HeaderStyle-CssClass="GridHeader"
                ItemStyle-CssClass="GridContent" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true"
                HeaderStyle-Width="15px" ItemStyle-Width="15px"></asp:BoundField>
            <asp:BoundField DataField="CRDate" HeaderText="CR Date" HeaderStyle-CssClass="GridHeader"
                ItemStyle-CssClass="GridContent" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true"
                HeaderStyle-Width="15px" ItemStyle-Width="15px"></asp:BoundField>
            <asp:BoundField DataField="DatePublished" HeaderText="Published Date" HeaderStyle-CssClass="GridHeader"
                ItemStyle-CssClass="GridContent" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true"
                HeaderStyle-Width="15px" ItemStyle-Width="15px"></asp:BoundField>
            <asp:TemplateField HeaderText="URL" HeaderStyle-CssClass="GridHeader" 
                ItemStyle-CssClass="GridContent" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" 
                HeaderStyle-Width="15px" ItemStyle-Width="15px">
                <ItemTemplate>
                    <a href='<%#Eval("URL") %>' target="_blank" runat="server" id="aurl"><%#Eval("URL")%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-CssClass="HideGridColumn"
                ItemStyle-CssClass="HideGridColumn" ItemStyle-HorizontalAlign="Left"
                ItemStyle-Wrap="true" HeaderStyle-Width="15px" ItemStyle-Width="15px"></asp:BoundField>
        </Columns>
        <RowStyle CssClass="gvRow" />
        <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
        <HeaderStyle CssClass="gvHeading"></HeaderStyle>
        <AlternatingRowStyle CssClass="gvAlternateRow"></AlternatingRowStyle>
        <EmptyDataRowStyle CssClass="gvEmpty" />
    </asp:GridView>
    
    <div style="margin-top:10px 0 10px 0;text-align:center; padding-top:15px">
        <asp:Button ID="btnok" runat="server" Text="OK" OnClick="btnok_Click" CssClass="btn" Width="80px"/>
        <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClientClick="ClosePopUpWindow();" CssClass="btn" Width="80px" Visible="false" />
    </div>
    </div>
    </form>
</body>
</html>
