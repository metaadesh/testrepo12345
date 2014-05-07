<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/MasterPage.Master" CodeBehind="MakeUser.aspx.cs" Inherits="METAOPTION.UI.MakeUser" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="cphMakeUser" runat="server">
<div class="RightPanel">
<asp:UpdatePanel ID="pnlEmpList" runat="server">
<Triggers>
<asp:PostBackTrigger ControlID="btnUpdate" />
<asp:PostBackTrigger ControlID="btnSubmit" />
</Triggers>
    <ContentTemplate>
    
<table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
<tr>
							<td class="AddHeading">Make User</td>
						</tr>
						<tr>
							<td class="height30">&nbsp;</td>
						</tr>
						<tr>
							<td>
							
								<fieldset class="ForFieldSet">
    <legend class="ForLegend">
    <asp:Label ID="lblLegHeading" Text="Add New User" runat="server"></asp:Label>
    </legend><br>
     <table border="0" class="TableBorder" width="100%" cellpadding="0" style="border-collapse: collapse">
        <tr>
            
            <td class="TableBorder">
                Full Name</td>
            <td class="TableBorder">
                <asp:TextBox ID="txtFullName" runat="server" CssClass="txtMan2"></asp:TextBox>
                <asp:LinkButton ID="lnkPopUp" runat="server" CssClass="OrangeText_LeftPanel">Click 
                to select employee</asp:LinkButton>
                <asp:HiddenField ID="HiddenField1" runat="server" />
            </td>
        </tr>
        <tr>
            
            <td class="TableBorder">
                <asp:Label ID="Label1" runat="server" Text="Login ID" 
                    AssociatedControlID="txtUserName"></asp:Label></td>
            <td class="TableBorder">
                <asp:TextBox ID="txtUserName" runat="server" CssClass="txtMan2"></asp:TextBox>
            </td>
        </tr>
        <tr>
            
            <td class="TableBorder">
                <asp:Label ID="lblPassword" runat="server" AssociatedControlID="txtPassword" 
                    Text="Password"></asp:Label>
            </td>
            <td class="TableBorder">
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" 
                    CssClass="txtMan2"></asp:TextBox>
            </td>
        </tr>
        <tr>
          
            <td class="TableBorder">
                <asp:Label ID="lblConPassword" runat="server" Text="Confirm Password"></asp:Label>
            </td>
            <td class="TableBorder">
                <asp:TextBox ID="txtConpassword" runat="server" TextMode="Password" 
                    CssClass="txtMan2"></asp:TextBox>
            </td>
        </tr>
        <tr>
          
            <td class="TableBorder">
                <asp:Label ID="lblDisName" runat="server" AssociatedControlID="txtDisplayName" 
                    Text="Display Name"></asp:Label>
            </td>
            <td class="TableBorder">
                <asp:TextBox ID="txtDisplayName" runat="server" CssClass="txtMan2"></asp:TextBox>
            </td>
        </tr>
        <tr>
           
            <td class="TableBorder">
                <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
            </td>
            <td class="TableBorder">
                <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
           
            <td class="TableBorder">
                <asp:Label ID="lblActive" runat="server" Text="IsActive"></asp:Label>
            </td>
            <td class="TableBorder">
                <asp:CheckBox ID="ChkActive" runat="server" Checked="True" />
            </td>
        </tr>
        <tr>
          
            <td>
                &nbsp;</td>
            <td class="TableBorder">
                <asp:Button ID="btnSubmit" CssClass="Btn_Form" runat="server" Text="Submit" 
                    onclick="btnSubmit_Click" />
                &nbsp;<asp:Button ID="btnUpdate" CssClass="Btn_Form" Text="Update" runat="server" 
                    onclick="btnUpdate_Click" />
                   
                &nbsp;<asp:Button ID="btnCancel" CssClass="Btn_Form" runat="server" onclick="btnCancel_Click" 
            Text="Cancel" />
            </td>
        </tr>
    </table>
    </fieldset>
    
   
     <asp:Panel ID="pnlEmployeeList" Width="700" runat="server" CssClass="modalPopup">
       <table border="0" width="100%" cellpadding="0" >
		<tr>
			<td class="PopUpBoxHeading">Employee List</td>
			<td class="PopUpBoxHeading" align="right"><img border="0" src="./Images/close.gif" width="21" height="17" /></td>
		</tr>
		<tr>
			<td align="left" colspan="2" style="padding:10px">
           <asp:GridView ID="GrdEmployee" 
                RowStyle-CssClass="gvRow"
                HeaderStyle-CssClass="gvHeading"
                AlternatingRowStyle-CssClass="gvAlternateRow"
                runat="server"
                AutoGenerateColumns="False" 
                Width="100%" CellPadding="4" 
                GridLines="None" AllowPaging="True" 
                onpageindexchanging="GrdEmployee_PageIndexChanging" PageSize="5" 
                onrowcommand="GrdEmployee_RowCommand" DataKeyNames="EmployeeId"  >

        <Columns>
         <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
         <ItemTemplate>
         <asp:ImageButton ID="imgbtnSelect" CommandName="SelectEmp" 
                 CommandArgument='<%#Eval("EmployeeId") %>' runat="server" 
                 ImageUrl="~/Images/confirm.gif" onclick="imgbtnSelect_Click" />
         </ItemTemplate>
             <HeaderStyle CssClass="GridHeader" />
             <ItemStyle CssClass="GridContent" />
         </asp:TemplateField>     
        <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" 
                HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" >
            <HeaderStyle CssClass="GridHeader" />
            <ItemStyle CssClass="GridContent" />
            </asp:BoundField>
        <asp:BoundField DataField="EmployeeType" HeaderText="Employee Type" 
                HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" >
            <HeaderStyle CssClass="GridHeader" />
            <ItemStyle CssClass="GridContent" />
            </asp:BoundField>
            <asp:BoundField DataField="FirstName" HeaderText="First Name" 
                HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" >
                <HeaderStyle CssClass="GridHeader" />
                <ItemStyle CssClass="GridContent" />
            </asp:BoundField>
            <asp:BoundField DataField="MiddleName" HeaderText="Middle Name" 
                HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                <HeaderStyle CssClass="GridHeader" />
                <ItemStyle CssClass="GridContent" />
            </asp:BoundField>
            <asp:BoundField DataField="LastName" HeaderText="Last Name" 
                HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                <HeaderStyle CssClass="GridHeader" />
                <ItemStyle CssClass="GridContent" />
            </asp:BoundField>
            <%--<asp:BoundField DataField="IsActive" HeaderText="User Status" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"/>--%>
        </Columns>
        
    </asp:GridView>
                
                </td>
		</tr>
		<tr><td colspan="2" style="padding:10px" align="center">
                <asp:Button ID="btnEditCancel" runat="server" Text="Cancel" CssClass="Btn_Form" 
                Width="75px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
          
            </td></tr>
	</table>
        </asp:Panel>
    
    <Ajax:ModalPopupExtender ID="modPopUp" runat="server"
         TargetControlID="lnkPopUp" PopupControlID="pnlEmployeeList"
         CancelControlID="btnEditCancel" BackgroundCssClass="modalBackground">
         </Ajax:ModalPopupExtender>
    
							</td>
						</tr>
						<tr>
							<td class="height30">&nbsp;</td>
						</tr>
						<tr>
							<td>
								<fieldset class="ForFieldSet" id="fsetAssGroup" runat="server">
    <legend class="ForLegend">
        Associated Groups     </legend><br>
     <table style="width:100%;">
                <tr>
                    <td>
        <asp:GridView ID="GrdGroup" runat="server" AutoGenerateColumns="False" 
             CellPadding="4" 
            GridLines="None" Width="100%" >
          <Columns>
                
                <asp:TemplateField HeaderStyle-Width="20px" ItemStyle-Width="20px" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                <ItemTemplate>
                <asp:ImageButton ID="ImgbtnDelRight" runat="server" 
                                CommandArgument='<% #Eval("SecurityUserGroupId") %>' CommandName="DeleteGroup" 
                                ImageUrl="~/Images/DeleteButton.jpg" />
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="GroupName"  HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" HeaderText="Group Name" />
                 <asp:BoundField DataField="GroupDesc"  HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" HeaderText="Group Description" />
               
            </Columns>
           
        </asp:GridView>
                        </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMessage" runat="server" Visible="False" 
                            CssClass="LeftPanelContentHeading"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnAdd" CssClass="Btn_Form" runat="server" Text="Add" />
                    </td>
                </tr>
            </table>
   
    </fieldset>
							</td>
						</tr>
						<tr>
							<td class="height30">&nbsp;</td>
						</tr>
						<tr>
							<td>
								
							</td>
						</tr>
						</table>
	</ContentTemplate>
   
    </asp:UpdatePanel>
</div>
    
 </asp:Content>