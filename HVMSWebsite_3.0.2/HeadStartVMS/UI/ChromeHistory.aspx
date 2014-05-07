<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="ChromeHistory.aspx.cs" Inherits="METAOPTION.UI.ChromeHistory" Title="Chrome History" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upnlChromeHistory" runat="server">
        <ContentTemplate>
        <div class="AddHeading">
            Chrome History
         </div>
            <table border="0" width="100%" cellpadding="0"  style="border-collapse: collapse">
                
                <tr>
                    <td align="left">
                  
                        <asp:GridView runat="server" Width="100%" ID="gvChromeHistory" widh="100px%" DataKeyNames="ChromeUpdateId"
                            AllowPaging="true" PageSize="20" AutoGenerateColumns="False"  
                            EmptyDataText="No Rows found" 
                            onpageindexchanging="gvChromeHistory_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="ChromeDate" HeaderText="Chrome Date" />
                                <asp:BoundField DataField="ChromeDescription" HeaderText="Chrome Description" />
                            </Columns>
                            <AlternatingRowStyle CssClass="gvAlternateRow" />
                            <HeaderStyle CssClass="gvHeading" />
                            <RowStyle CssClass="gvRow" />
                             <EmptyDataRowStyle HorizontalAlign="Center" />
                             <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                        </asp:GridView>
                       
                    </td>
                </tr>
      
            </table>
           
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
