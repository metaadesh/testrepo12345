<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InventoryNotes.aspx.cs"
    Inherits="METAOPTION.UI.InventoryNotes" Title="HeadstartVMS::Inventory Notes" %>

<%@ MasterType VirtualPath="~/UI/MasterPage.Master" %>
<asp:content id="cphInventoryNotes" runat="server" contentplaceholderid="ContentPlaceHolder1">

<asp:UpdatePanel ID="upInventoryNotes" runat="server">              
   <Triggers>
   <asp:AsyncPostBackTrigger ControlID="btnInsert" />
   </Triggers>
   <ContentTemplate>   
   <div class="AddHeading">
             <asp:Label ID="lblInventoryHeader" runat="server" Text=""></asp:Label>
   </div>
      <asp:GridView
         runat="server"
         Width="100%"
         DataKeyNames="RowId,NoteTypeID"
         ID="gvNotes"
         AllowPaging="True"
         PageSize="20"
         AutoGenerateColumns="False"      
         EmptyDataText="No Rows found"          
         OnPageIndexChanging="gvNotes_PageIndexChanging">
         
            <AlternatingRowStyle CssClass="gvAlternateRow" />
            <Columns>
               <asp:BoundField DataField="RowId" HeaderText="RowId" Visible="False"  HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"/>
               <asp:BoundField DataField="Notes" HeaderText="Notes"  HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                  <ItemStyle Width="400px" />
               </asp:BoundField>
               <asp:BoundField DataField="AddedBy" HeaderText="Added By"  HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"/> 
               <%--<asp:BoundField DataField="NoteType" HeaderText="Note Type"  HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"/> --%>
               <asp:BoundField DataField="DateAdded" HeaderText="Date Added" DataFormatString="{0:MM/dd/yyyy}"  HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"/> 
               <asp:TemplateField  HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                  <ItemTemplate>
                     <asp:ImageButton ID="ibtnNoteEdit" ImageUrl="~/Images/edit-icon.jpg" 
                          runat="server" onclick="ibtnNoteEdit_Click"/>
                  
                      <asp:ImageButton ID="btnDelNotes" runat="server"  OnClientClick="javascript:return confirm('Are u sure you want to delete this Note?\n\nClick Ok to Confirm\nClick Cancel to ignore');" ImageUrl="~/Images/DeleteButton.jpg"  onclick="btnDelNotes_Click" />
                  
                  </ItemTemplate>
                  <ItemStyle Width="60px" />
               </asp:TemplateField>
         </Columns>
         <HeaderStyle CssClass="gvHeading" />
         <RowStyle CssClass="gvRow" />
         <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
       
 <EmptyDataRowStyle HorizontalAlign="Center" />
      </asp:GridView> 

       <table border="0"  cellpadding="0" style="width:98%; padding-left:10px;">
            <tr>
               <td>
                  <asp:LinkButton ID="lnkbtnBack" runat="server" onclick="lnkbtnBack_Click" CausesValidation="false">
                     <img src="../Images/back.jpg" alt="back" style="border:none; padding-top:10px" />
                  </asp:LinkButton> 
               </td>
               <td align="right"> 
                <asp:LinkButton ID="lnkAddNewNote" CssClass="AddNewExpenseTxt" 
              style="border:none; padding-top:10px" runat="server" 
              onclick="lnkAddNewNote_Click">
       <img src="../Images/AddNew.gif" alt="Add New" style="border:none; padding-top:10px" />
               Add New Note
        </asp:LinkButton>   
               
               </td>
            </tr>
         </table>
      <div  class="modalPopup" id="divAddNewNotes" style="display: none; width:60%" runat="server" >
         
         <table border="0" cellpadding="0" cellspacing="0" style="width:100%" >
            <tr >
              <td class="PopUpBoxHeading" nowrap="nowrap">
                                   &nbsp;&nbsp; <asp:Label ID="lblHeading" Text="Add Note" runat="server"></asp:Label>
              </td>
               <td align="right" class="PopUpBoxHeading" style="padding-right:5px">
                  <img src="../Images/close.gif" alt="" onclick="javascipt:document.getElementById('<%=btnCancelInsert.ClientID %>').click();" />
                  </td>
            </tr>
            <tr>
               <td class="lblb"  >Note</td>
            
               <td  class="lbl">
                  <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Rows="7" 
                       CssClass="txtMulti" />
               </td>
            </tr>
             <%--<tr>
               <td class="lblb">Note Type</td>
            
               <td  class="lbl">
                  <asp:DropDownList ID="ddlNoteType" runat="server" CssClass="txt2">
                     <asp:ListItem Text="Special Note" Value="1"></asp:ListItem>
                     <asp:ListItem Text="Title Tracking Note" Value="2"></asp:ListItem>
                 </asp:DropDownList>
               </td>
            </tr>--%>
            <tr>
               <td align="center" colspan="2" class="TableBorder">
                    <asp:Button ID="btnInsert" runat="server" Text="    Save    " CssClass="btn" 
                       OnClick="btnInsert_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                    
                    <asp:Button ID="btnCancelInsert" runat="server" Text="   Cancel   " CssClass="btn" />
               </td>
               
           </tr>
           
         </table>
      </div>
           <div style="display:none; width:1px;">
                <asp:Button ID="btnNotepopupOpener" runat="server" />
                <asp:Button ID="btnDeleteNotePopUpOpener" runat="server" />
                <asp:HiddenField ID="hdUpdateNoteId" runat="server" />
           </div>
      <ajax:ModalPopupExtender 
         ID="MPEAddNewNote" 
         runat="server"
         TargetControlID="btnNotepopupOpener"
         PopupControlID="divAddNewNotes"
         CancelControlID="btnCancelInsert"
         BackgroundCssClass="modalBackground" />
     
       
      
  </ContentTemplate>
</asp:UpdatePanel>
   
</asp:content>
