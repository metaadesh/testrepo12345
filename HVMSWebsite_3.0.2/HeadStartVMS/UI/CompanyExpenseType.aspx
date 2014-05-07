<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyExpenseType.aspx.cs"
    Inherits="METAOPTION.UI.CompanyExpenseType" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<asp:content contentplaceholderid="ContentPlaceHolder1" id="cphViewVendor" runat="server">
  <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="RightPanel">   
            <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                        class="arial-12">
                        <tr>
                        <td class="TableHeadingBg TableHeading">
                            Company Expense Type
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder"> 
                <div id="dvSearch" runat="server" style="width: 100%; height: auto">
                    <div style="width: 39%; float: left; padding: 5px; padding-left: 0px;">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="TableBorder" >
                                   Expense Type
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList runat="server" id="ddlExpenseType" CssClass="txt2"></asp:DropDownList>
                                </td>
                                </tr>
                                <tr>
                                <td class="TableBorder">
                                   From Date
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtSyncDateFrom" runat="server" CssClass="txt2" Width="147px" />
                                        <Ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSyncDateFrom"
                                            PopupButtonID="txtSyncDateFrom" />
                                </td>                                                            
                            </tr>
                            </table>
                            </div>
                             <div style="width: 42%; float: left; padding: 5px 5px 5px 5px"">
                             <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                              <tr>
                                <td class="TableBorder" style="width: 110px">
                                 Price
                                   
                                </td>
                                <td class="TableBorder" style="width: 200px">
                                 <asp:TextBox runat="server" id="txtPrice" Width="120px" CssClass="txt2"
                                     ></asp:TextBox>
                                    <Ajax:FilteredTextBoxExtender ID="ftbe1" runat="server" TargetControlID="txtPrice" 
                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                   
                                        
                                </td>
                                </tr>
                                <tr>
                                 <td class="TableBorder">
                                    To Date
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtSyncDateTo" runat="server" CssClass="txt1" Width="120px" />
                                        <Ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSyncDateTo"
                                            PopupButtonID="txtSyncDateTo" />
                                </td>                                
                            </tr>                          
                        </table>
                    </div>
                    <div style="width: 10%; float: left; padding: 5px 5px 5px 5px"">
                             <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                              <tr>
                                <td >
                                  &nbsp;&nbsp; &nbsp;
                                </td> 
                                <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td> 

                                </tr>
                                <tr>
                                <td style=" height : 30px;">&nbsp;&nbsp;
                                </td>
                                 <td>                                 
                                 <asp:Button ID="btnSearch" runat="server" Text="Search" Style=" width : 80px !important;"
                                        CssClass="btn" onclick="btnSearch_Click" /> 
                                </td>
                                </tr>
                                </table>
                                </div>   
                </div>
                </td>
                </tr>
                <tr>
                <td class="TableBorder">                
                         <asp:GridView ID="grvexpensetypes" runat="server" AutoGenerateColumns="False" Width="100%"
                            DataKeyNames="EntityExpenseID"  CellPadding="4" GridLines="None"
                            AllowPaging="True" PageSize="50" PagerSettings-Mode="NumericFirstLast" 
                            OnPageIndexChanging="grvexpensetypes_PageIndexChanging"   OnRowDataBound="grvexpensetypes_RowDataBound"
                            >
                            <Columns>
                                <asp:BoundField DataField="ExpenseType" HeaderText="Expense Type" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" SortExpression="" HeaderStyle-Width="10px" ItemStyle-Width="10px"
                                    ItemStyle-Wrap="true" HeaderStyle-Wrap="true">
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DateAdded" HeaderText="Added On" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" SortExpression="" HeaderStyle-Width="10px" ItemStyle-Width="10px"
                                    ItemStyle-Wrap="true" HeaderStyle-Wrap="true">
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="MinCount" HeaderText="Min Count" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContentRight" SortExpression="" HeaderStyle-Width="10px"
                                    ItemStyle-Width="10px" ItemStyle-Wrap="true" HeaderStyle-Wrap="true">
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <ItemStyle CssClass="GridContentRight"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="MaxCount" HeaderText="Max Count" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" SortExpression="" HeaderStyle-Width="10px" ItemStyle-Width="10px"
                                    ItemStyle-Wrap="true" HeaderStyle-Wrap="true">
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <ItemStyle CssClass="GridContentRight"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DefaultPrice" HeaderText="Default Price($)" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent" SortExpression="" HeaderStyle-Width="10px" ItemStyle-Width="10px"
                                    ItemStyle-Wrap="true" HeaderStyle-Wrap="true">
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <ItemStyle CssClass="GridContentRight"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentRight" Visible="false"
                                    HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <a id="imgExpenseEdit" href='<%# "EditExpenses.aspx?ID="+Eval("EntityExpenseID")+"&TB_iframe=true&height=220&width=800" %>'
                                            title="Edit Details" class="thickbox">
                                            <img alt="" src="../Images/newedit.gif" border="0" /></a>
                                        <img alt="" id="imgExpenseEditDummy" src="../Images/newedit.gif" border="0" title="Edit Details"
                                            style="display: none" />
                                        <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/Images/DeleteButton.png"
                                            OnClientClick="javascript:return confirm('Really do you want to delete?\n\nClick Ok to Confirm\nClick Cancel to ignore');"
                                            OnClick="ibtnDelete_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="lblDataMsg" runat="server" CssClass="leftpanelcontentheading" Text="No Expense found"></asp:Label>
                            </EmptyDataTemplate>
                            <AlternatingRowStyle BackColor="#E4EDF4" />
                            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                        </asp:GridView>
                        </td>
                    </tr> 
                   <tr>
                 
                              <td class="FooterContentDetails">
                    <a id="ancAddExpense" runat="server" visible="false" class="AddNewExpenseTxt">Add New Expense</a>
                    <a id="ancAddExpenseDummy" runat="server" class="AddNewExpenseTxt" style="cursor:pointer; display : none;">Add New Expense</a>
                </td>
                          
                        </tr>
                    </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script language="javascript" type="text/javascript">
        function pageLoad()
     {
            var EntityType = "<%= Session["LoginEntityTypeID"]%>";
            if (EntityType == "3") {
                $("table[id$='grvexpensetypes']").find('a[id="imgExpenseEdit"]').hide();
                $("table[id$='grvexpensetypes']").find("#imgExpenseEditDummy").show();
                $('#ancAddExpense').hide();
               // $('#ancAddExpenseDummy').show();
                $("table[id$='grvexpensetypes']").find("#imgbtnDelete").attr('enabled','false');
                
            }
        }
    </script>
</asp:content>
