<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="LaneHistory.aspx.cs" Inherits="HeadStartVMS.LaneHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml"  >
<head runat="server">
<link href="../CSS/MainStyle.css" rel="stylesheet" type="text/css" />
    <title>Lane History</title>
</head>
<body style="text-decoration: none;" style="background-color:White">
    <form id="form1" runat="server" style="text-decoration: none;">
    <div style="background-color:White"><br>
        <p>
            <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            Year :</b> <asp:Label ID="lblYear" runat="server"></asp:Label> &nbsp;>> <b>Make :</b> <asp:Label ID="lblMake" runat="server"> </asp:Label> &nbsp;>> 
            <b>Model :</b> <asp:Label ID="lblModel" runat="server"></asp:Label></p>
        <asp:GridView ID="gvLaneHistory" runat="server" AutoGenerateColumns="False" 
                AllowPaging="True" onpageindexchanging="gvLaneHistory_PageIndexChanging" 
                GridLines="None" PageSize="20" EmptyDataText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>History not available!</b>">
            <RowStyle CssClass="GridContent" />
            <Columns> 
                <asp:BoundField DataField="DateAdded" HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;Date&nbsp;" 
                    DataFormatString="{0:mm/dd/yyyy HH:mm: ss}" HtmlEncode="false">
                    <HeaderStyle CssClass="GridHeader" />
                    <ItemStyle CssClass="GridContent" />
                </asp:BoundField>
                <asp:BoundField DataField="NewMarketPrice" HeaderText="&nbsp;New Market Price&nbsp;" >
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  
                        Wrap="False" CssClass="GridHeader" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" 
                        CssClass="GridContent" />
                </asp:BoundField>
                <asp:BoundField DataField="OldMarketPrice" HeaderText="&nbsp;Old Market Price&nbsp;" >
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
                        CssClass="GridHeader" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" 
                        CssClass="GridContent" />
                </asp:BoundField>
                <asp:BoundField DataField="NewRegular" HeaderText="&nbsp;New Regular #&nbsp;" >
                    <HeaderStyle  Wrap="False" CssClass="GridHeader" />
                    <ItemStyle CssClass="GridContent" />
                </asp:BoundField>
                <asp:BoundField DataField="OldRegular" HeaderText="&nbsp;Old Regular #&nbsp;" >
                    <HeaderStyle Wrap="False" CssClass="GridHeader" />
                    <ItemStyle CssClass="GridContent" />
                </asp:BoundField>
                <asp:BoundField DataField="NewOnline" HeaderText="&nbsp;New Online #&nbsp;" >
                    <HeaderStyle Wrap="False" CssClass="GridHeader" />
                    <ItemStyle CssClass="GridContent" />
                </asp:BoundField>
                <asp:BoundField DataField="Oldonline" HeaderText="&nbsp;Old Online #&nbsp;" >
                    <HeaderStyle Wrap="False" CssClass="GridHeader" />
                    <ItemStyle CssClass="GridContent" />
                </asp:BoundField>
                <asp:BoundField DataField="NewExotic" HeaderText="&nbsp;New Exotic #&nbsp;" >
                    <HeaderStyle Wrap="False" CssClass="GridHeader" />
                    <ItemStyle CssClass="GridContent" />
                </asp:BoundField>
                <asp:BoundField DataField="OldExotic" HeaderText="&nbsp;Old Exotic #&nbsp;" >
                    <HeaderStyle Wrap="False" CssClass="GridHeader" />
                    <ItemStyle CssClass="GridContent" />
                </asp:BoundField>
                <asp:BoundField DataField="NewVirtual" HeaderText="&nbsp;New Virtual #&nbsp;" >
                    <HeaderStyle Wrap="False" CssClass="GridHeader" />
                    <ItemStyle CssClass="GridContent" />
                </asp:BoundField>
                <asp:BoundField DataField="OldVirtual" HeaderText="&nbsp;Old Virtual #&nbsp;" >
                    <HeaderStyle Wrap="False" CssClass="GridHeader" />
                    <ItemStyle CssClass="GridContent" />
                </asp:BoundField>
            </Columns>
            <PagerStyle BorderStyle="Solid" CssClass="FooterContentDetails" />
            <HeaderStyle CssClass="GridHeader" />
        </asp:GridView>
  <p align="center" style="Btn_Form">
        <input id="Button1" class="Btn_Form" type="submit" value="Close"  onclick="javascript:window.close();"/></p>
        
    </div>
    </form>
    
    
    
</body>
</html>
