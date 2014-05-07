<%@ Page Language="C#" AutoEventWireup="true" 
   CodeBehind="AddGroup.aspx.cs" Inherits="METAOPTION.UI.AddGroup" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="cphAddNewGroup">
   <div class="AddHeading">
      Group Management</div>
   <div>
      <fieldset class="ForFieldSet">
         <legend class="ForLegend">Add New Group</legend>
         <br />
         <table border="0" style="border-collapse: collapse" cellpadding="0" cellspacing="0">
            <tr>
               <td class="TableBorderB">
                  <asp:Label ID="lblGName" runat="server" AssociatedControlID="txtGName" Text="Group Name" />
               </td>
               <td class="TableBorder">
                  <asp:TextBox ID="txtGName" runat="server" CssClass="txtMan3" />
               </td>
            </tr>
            <tr>
               <td class="TableBorderB">
                  <asp:Label ID="lblGDesc" runat="server" AssociatedControlID="txtGDesc" Text="Description" />
               </td>
               <td class="TableBorder">
                  <asp:TextBox ID="txtGDesc" runat="server" TextMode="MultiLine" CssClass="txtMulti"></asp:TextBox>
               </td>
            </tr>
            <tr>               
               <td class="TableBorder" colspan="2" align="center">
                  <asp:Button ID="btnCancel" runat="server" Text="   Canel    " CssClass="btn" 
                     onclick="btnCancel_Click" />
                     &nbsp;&nbsp;&nbsp;
                  <asp:Button ID="btnSubmit" runat="server" Text="   Insert   " OnClick="btnSubmit_Click"
                     CssClass="btn" />&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="btnUpdate" runat="server" Text="   Update   " OnClick="btnUpdate_Click"
                     CssClass="btn" />
               </td>
            </tr>
         </table>
         <br />
      </fieldset>
   </div>
   <div>
      <fieldset id="fsetRights" runat="server" class="ForFieldSet">
         <legend class="ForLegend">Associated Rights</legend>
         <br />
         <table style="width: 100%" align="left" border="0" cellpadding="0" cellspacing="0">
            <tr>
               <td align="left" colspan="2" style="width:80%">
                  <asp:UpdatePanel ID="upPanel" runat="server" UpdateMode="Conditional">
                     <ContentTemplate>
                        <asp:GridView 
                           ID="GridView1" 
                           runat="server" 
                           AutoGenerateColumns="False" 
                           GridLines="None"
                           Width="100%" 
                           AllowPaging="true"
                           PageSize="20"
                           EmptyDataText="No right is associated with this group."
                           OnRowCommand="GridView1_RowCommand" 
                           onpageindexchanging="GridView1_PageIndexChanging">
                           <Columns>
                              <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center"  >
                                 <ItemTemplate>
                                    <asp:ImageButton ID="ImgbtnDelRight" runat="server" CommandArgument='<% #Eval("SecurityGroupRightId") %>'
                                       CommandName="DeleteRight" ImageUrl="~/Images/DeleteButton.jpg" />
                                    <asp:Panel Height="100" Style="display: none;" Width="300" CssClass="modalPopup"
                                       ID="Panel1" runat="server">
                                       <table width="100%">
                                          <tr>
                                             <td class="GridHeader">
                                                Confirmation
                                             </td>
                                          </tr>
                                          <tr>
                                             <td class="LeftPanelContentHeading" align="center">
                                                Are You sure? You want to delete this item.
                                             </td>
                                          </tr>
                                          <tr>
                                             <td align="center">
                                                <asp:Button ID="btnOk"     runat="server" CssClass="Btn_Form" Text="  Ok  " /> &nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnCancel" runat="server" CssClass="Btn_Form" Text="Cancel" />
                                             </td>
                                          </tr>
                                       </table>
                                    </asp:Panel>
                                    <ajax:ConfirmButtonExtender ID="confrm" runat="server" TargetControlID="ImgbtnDelRight"
                                       DisplayModalPopupID="mod" OnClientCancel="btnCancel">
                                    </ajax:ConfirmButtonExtender>
                                    <ajax:ModalPopupExtender ID="mod" runat="server" PopupControlID="Panel1" BackgroundCssClass="modalBackground"
                                       OkControlID="btnOk" CancelControlID="btnCancel" TargetControlID="ImgbtnDelRight">
                                    </ajax:ModalPopupExtender>
                                 </ItemTemplate>
                              </asp:TemplateField>
                              <asp:BoundField DataField="RightName" HeaderText="Right Name" />
                              <asp:BoundField DataField="RightDesc" HeaderText="Description"  />
                           </Columns>
                           <HeaderStyle CssClass="gvHeading" />
                           <RowStyle CssClass="gvRow" />                           
                           <AlternatingRowStyle CssClass="gvAlternateRow" />
                           <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                        </asp:GridView>
                        <div id="divRights" runat="server" style="width:600px; background-color:White" >
                           <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
                              <tr>
                                 <td class="PopUpBoxHeading" colspan="2" style="padding-left:5px">
                                    Add Right
                                 </td>
                                 <td class="PopUpBoxHeading" align="right" style="padding-right:5px">
                                    <img border="0" src="../Images/close.gif" onclick="$find('mdpopDoc').hide();return false;"
                                       alt="" />
                                 </td>
                              </tr>
                              <tr>
                                 <td class="TableBorderB">               
                                    Right Initial
                                 </td>
                                 <td class="TableBorder">    
                                    <asp:TextBox ID="txtRightName" runat="server" CssClass="txt3" />
                                 </td>   
                                 <td class="TableBorder">    
                                    <asp:Button ID="btnSearchRight" runat="server" CssClass="btn" 
                                       Text="Search Right" onclick="btnSearchRight_Click"  />
                                 </td>              
                              </tr>
                              <tr>
                                 <td class="TableBorder" colspan="3">
                                    <asp:GridView
                                       ID="gvRights"
                                       runat="server"
                                       Width="100%"
                                       AllowPaging="true"
                                       PageSize="15"
                                       DataKeyNames="SecurityRightId"
                                       AutoGenerateColumns="false" 
                                       EmptyDataText="No record to display"
                                       OnPageIndexChanging="gvRights_PageIndexChanging">
                                          <Columns>
                                             <asp:TemplateField HeaderText="ADD" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                   <asp:UpdatePanel ID="upAddrightsOK" runat="server" >
                                                      <ContentTemplate>
                                                         <asp:ImageButton ID="ibtnOk" 
                                                            ImageUrl="~/Images/confirm.gif"
                                                            runat="server" onclick="ibtnOk_Click" />                                                 
                                                      </ContentTemplate>
                                                      <Triggers>
                                                         <asp:PostBackTrigger ControlID="ibtnOk" />
                                                      </Triggers>
                                                   </asp:UpdatePanel>
                                                 </ItemTemplate>                            
                                                <ItemStyle HorizontalAlign="Center" />
                                             </asp:TemplateField>
                                             <asp:BoundField DataField="RightName" HeaderText="RgithName" /> 
                                             <asp:BoundField DataField="RightDesc" HeaderText="Description" />                                
                                          </Columns>
                                          <RowStyle CssClass="gvRow" />
                                          <AlternatingRowStyle CssClass="gvAlternateRow" />
                                          <HeaderStyle CssClass ="gvHeading" />
                                          <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                          <EmptyDataRowStyle HorizontalAlign="Center" />
                                       </asp:GridView>                     
                                 </td>
                               </tr>
                              <tr>
                                 <td colspan="3" align="center">
                                    <asp:Button ID="btnCancelRight" runat="server" Text="   Cancel   " CssClass="btn" />
                                 </td>
                              </tr>
                           </table>
                       </div>   
                        <div class="hideCol"><asp:Button ID="btnRightOpenner" runat="server" /> 
                        <ajax:ModalPopupExtender 
                           ID="mpeOpenRights"
                           runat="server"
                           TargetControlID="btnRightOpenner"
                           PopupControlID="divRights"
                           BehaviorID="mdpopDoc" 
                           BackgroundCssClass="modalBackground" 
                           DropShadow="false"                           
                           CancelControlID="btnCancelRight" />
                     </ContentTemplate>
                  </asp:UpdatePanel>
               </td>
            </tr>
            <tr>
               <td colspan="2" >
                  <asp:Button ID="btnAddRight" runat="server" Text="  Add Right  " CssClass="btn" 
                     onclick="btnAddRight_Click"  />            
               </td>
            </tr>
         </table>
        
      </fieldset>
   </div>

</asp:Content>
