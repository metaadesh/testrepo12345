<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyContacts.aspx.cs"
    Inherits="METAOPTION.UI.CompanyContacts" %>

<asp:content contentplaceholderid="ContentPlaceHolder1" id="cphViewVendor" runat="server">
  <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="RightPanel">   
            <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                        class="arial-12">
                        <tr>
                        <td class="TableHeadingBg TableHeading">
                            Company Contacts
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder"> 
                <div id="dvSearch" runat="server" style="width: 100%; height: auto">
                    <div style="width: 39%; float: left; padding: 5px; padding-left: 0px;">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="TableBorder" >
                                    Job Title
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList runat="server" id="ddlJobTitle" CssClass="txt2"></asp:DropDownList>
                                </td>
                                </tr>
                                <tr>
                                <td class="TableBorder">
                                    User Name
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox runat="server" id="txtUserName" Width="147px" CssClass="txt1" ></asp:TextBox>
                                </td>                                                            
                            </tr>
                            </table>
                            </div>
                             <div style="width: 42%; float: left; padding: 5px 5px 5px 5px"">
                             <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                              <tr>
                                <td class="TableBorder" style="width: 110px">
                                    Contact Type
                                </td>
                                <td class="TableBorder" style="width: 200px">
                                   <asp:DropDownList runat="server" id="ddlContactType" CssClass="txt2"></asp:DropDownList>
                                </td>
                                </tr>
                                <tr>
                                 <td class="TableBorder">
                                    Cell Phone
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox runat="server" id="txtCellPhone" Width="147px" CssClass="txt1" ></asp:TextBox>
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
                                <td>&nbsp;&nbsp;
                                </td>
                                 <td>
                                 <asp:Button ID="btnSearch" runat="server" Text="Search" Style=" width : 80px !important; margin-top : 5px;"
                                        CssClass="btn" onclick="btnSearch_Click" /> 
                                </td>
                                </tr>
                                </table>
                                </div>   
                </div>
                </td>
                </tr>
                <tr>
                <td class="TableBorder" >                
                         
                                <asp:GridView ID="grdContactDetails" runat="server" AutoGenerateColumns="False" Width="100%"  
                                      BorderWidth="0" DataKeyNames="ContactId" OnRowDeleting="grdContactDetails_RowDeleting"
                                    OnRowDataBound="grdContactDetails_RowDataBound" >
                                    <Columns>
                                        <asp:BoundField DataField="JobTitle" HeaderText="Job Title" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="FirstName" HeaderText="First Name" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="MiddleName" HeaderText="Middle Name" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="LastName" HeaderText="Last Name" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="UserName" HeaderText="User Name" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="ContactType" HeaderText="Contact Type" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="OfficePhone" HeaderText="Office Phone" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="CellPhone" HeaderText="Cell Phone" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" Visible="false"
                                            HeaderStyle-Width="80px" ItemStyle-Width="80px">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlkcontact" runat="server" ImageUrl="~/Images/ChangePassword.png"
                                                    ToolTip="Add/Change Password"></asp:HyperLink>
                                                <a id="contactEdit" href='<%# "EditContact.aspx?ID="+Eval("ContactId")+"&TB_iframe=true&height=220&width=800" %>'
                                                    title="Edit Details" class="thickbox" >
                                                    <img alt="" src="../Images/newedit.gif" border="0" /></a>
                                                <img alt="" id="imgEditDummy" src="../Images/newedit.gif" border="0" title="Edit Details" style="display:none" />
                                                <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/DeleteButton.jpg"
                                                    OnClientClick="javascript:return confirm('Really do you want to delete?\n\nClick Ok to Confirm\nClick Cancel to ignore');" />
                                                <asp:HiddenField ID="hfSecurityUserID" runat="server" Value='<%# Eval("SecurityUserID") %>' />
                                                <asp:HiddenField ID="hfUserName" runat="server" Value='<%# Eval("UserName") %>' />
                                                <asp:HiddenField ID="hfContactID" runat="server" Value='<%# Eval("ContactId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                  <EmptyDataTemplate>
                                     <asp:Label ID="lblDataMsg" runat="server"  CssClass="leftpanelcontentheading" Text="No Contact Details"></asp:Label>                                        
                                    </EmptyDataTemplate>
                                    <EmptyDataRowStyle CssClass="empty" />
                                     <AlternatingRowStyle BackColor="#E4EDF4" />
                                </asp:GridView>
                        </td>
                    </tr> 
                   <tr>
                    <td class="FooterContentDetails">
                                <a id="AddNewContact" href='<%="AddNewContact.aspx?EntityId="+EntityId+"&type="+type+"&TB_iframe=true&height=220&width=800" %>'
                                    title="Add New Contact" style="font-size: 11px; font-weight: bold; color: #535152;
                                    font-family: Arial, Helvetica, sans-serif; text-decoration: underline;" class="thickbox">
                                    Add New Contact</a>
                                    <a id="AddContactdummy" style="display:none; cursor:pointer">Add New Contact</a>
                            </td>
                        </tr>
                    </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <style type="text/css">
    .empty
    {
        border : none;
    }
    
    </style>
    <script language="javascript" type="text/javascript">
        function pageLoad()
     {
            var EntityType = "<%= Session["LoginEntityTypeID"]%>";
            if (EntityType == "3" || EntityType == "1") {
                $("table[id$='grdContactDetails']").find('a[id="contactEdit"]').hide();
                $("table[id$='grdContactDetails']").find("#imgEditDummy").show();
                $('#AddNewContact').hide();
              //  $('#AddContactdummy').show();
            }
        }
    </script>
</asp:content>
