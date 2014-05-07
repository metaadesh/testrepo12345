<%@ Page Title="" Language="C#"  AutoEventWireup="true"
    CodeBehind="ContactLogin.aspx.cs" Inherits="METAOPTION.UI.ContactLogin" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="cphUser" runat="server">
    <div class="RightPanel">
        <asp:UpdatePanel ID="pnlEmpList" runat="server">
            <ContentTemplate>
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
                                        <td class="TableBorder" colspan="2" align="center">
                                            <asp:Button ID="btnCancel" CssClass="btn" Text="   Cancel   " runat="server" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnSubmit" CssClass="btn" Text="   Save     " runat="server" OnClick="btnSubmit_Click" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnUpdate" CssClass="btn" Text="   Update   " runat="server" Visible="false" />
                                            <asp:Label ID="lblError" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <%--<tr>
                  <td>
                     <fieldset class="ForFieldSet" id="fsetAssGroup" runat="server">
                        <legend class="ForLegend">Associated Groups </legend>
                        <br>
                        <asp:Label ID="lblGroupAssociation" runat="server" CssClass="err" Visible="false" />
                        <table style="width: 100%;">
                           <tr>
                              <td>
                                 <asp:GridView 
                                    ID="GrdGroup" 
                                    runat="server" 
                                    AutoGenerateColumns="False" 
                                    CellPadding="4"
                                    GridLines="None" 
                                    DataKeyNames="SecurityUserGroupId"
                                    AllowSorting="true"
                                    Width="100%">
                                    <Columns>
                                       <asp:TemplateField ItemStyle-Width="15px">
                                          <ItemTemplate>
                                             <asp:ImageButton ID="ImgbtnDelRight" runat="server" CommandArgument='<% #Eval("SecurityUserGroupId") %>'
                                                CommandName="DeleteGroup" ImageUrl="~/Images/DeleteButton.jpg" 
                                                onclick="ImgbtnDelRight_Click" 
                                                OnClientClick="javascript:return confirm('Are you sure to de-associate this group with this user?\n\nClick Ok to confirm\nElse Cancel')" />
                                          </ItemTemplate>
                                          <ItemStyle Width="15px" />
                                       </asp:TemplateField>
                                       <asp:BoundField DataField="GroupName" HeaderText="Group Name" SortExpression="GroupName" />
                                       <asp:BoundField DataField="GroupDesc" HeaderText="Description" />
                                    </Columns>                                    
                                    <RowStyle CssClass="gvRow" />
                                    <AlternatingRowStyle CssClass="gvAlternateRow" />
                                    <HeaderStyle CssClass ="gvHeading" />
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
                                 <asp:Button ID="btnAssociateGroup" CssClass="btn" runat="server" 
                                    Text="  Add To Group  " />
                              </td>
                           </tr>
                        </table>
                       <asp:Panel ID="pnlGroups" Width="700" runat="server" CssClass="modalPopup">
                        <table border="0" width="100%" cellpadding="0" cellspacing="0">
                           <tr>
                              <td class="PopUpBoxHeading" colspan="4" style="padding-left:5px">
                                 Group List
                              </td>
                              <td class="PopUpBoxHeading" align="right" >
                                 <img border="0" src="../Images/close.gif" alt="Close" style="padding-left:5px" />
                              </td>
                           </tr>
                           <tr>
                              <td class="lblb">
                                 Group Name
                              </td>
                              <td class="lbl">
                                  <asp:TextBox
                                    ID="txtGroupNamegrp"
                                    runat="server"                                     
                                    CssClass="txt2" />
                               </td>                              
                               <td class="lblb">
                                  Description
                               </td>
                               <td class="lbl">
                                 <asp:TextBox
                                    ID="txtGroupDescgrp"
                                    runat="server"                                     
                                    CssClass="txt2" />                                    
                              </td>
                              <td class="lblb" align="center">
                                 <asp:Button ID="btnSearchGroup" runat="server" Text="  Search  " CssClass="btn" 
                                    onclick="btnSearchGroup_Click" />
                              </td>
                           </tr>                          
                           <tr>
                              <td align="left" colspan="5" style="padding: 10px">
                                 <asp:GridView 
                                    ID="gvGroups" 
                                    runat="server" 
                                    AutoGenerateColumns="False"
                                    Width="100%" 
                                    GridLines="None" 
                                    AllowPaging="True" 
                                    OnPageIndexChanging="gvGroups_PageIndexChanging"
                                    PageSize="15" 
                                    OnRowCommand="gvGroups_RowCommand" 
                                    DataKeyNames="SecurityGroupId">
                                    <Columns>
                                       <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:ImageButton ID="ibtnAddGroup" CommandName="SelectEmp" CommandArgument='<%#Eval("SecurityGroupId") %>'
                                                runat="server" ImageUrl="~/Images/confirm.gif" 
                                                onclick="ibtnAddGroup_Click" />
                                          </ItemTemplate>
                                          <ItemStyle HorizontalAlign="Center" />
                                       </asp:TemplateField>
                                       <asp:BoundField DataField="GroupName" HeaderText="Name"  />
                                       <asp:BoundField DataField="GroupDesc" HeaderText="Description" />
                                    </Columns>
                                    <RowStyle CssClass="gvRow" />
                                    <AlternatingRowStyle CssClass="gvAlternateRow" />
                                    <HeaderStyle CssClass ="gvHeading" />
                                    <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                 </asp:GridView>
                              </td>
                           </tr>
                           <tr>
                              <td colspan="6" style="padding: 10px" align="center">
                                 <asp:Button ID="btnCanelGroup" runat="server" Text="    Cancel   " CssClass="btn"  />
                              </td>
                           </tr>
                        </table>
                       </asp:Panel>
                        <ajax:ModalPopupExtender 
                           ID="mpeModelGroup" 
                           runat="server" 
                           TargetControlID="btnAssociateGroup"
                           PopupControlID="pnlGroups" 
                           CancelControlID="btnCanelGroup" 
                           BackgroundCssClass="modalBackground" />                    
                     </fieldset>
                  </td>
               </tr>--%>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
