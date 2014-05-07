using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using METAOPTION.BAL;

namespace METAOPTION.UI
{
   public partial class InventoryNotes : System.Web.UI.Page,IPagePermission
   {

       #region [Public variables & Constants]
        public const string PAGE                                =   "INVENTORYNOTE";
        public const string INVENTORYNOTE_INVENTORYNOTE_ADD     =   "INVENTORYNOTE.ADD";
        public const string INVENTORYNOTE_INVENTORYNOTE_VIEW    =   "INVENTORYNOTE.VIEW";
        public const string INVENTORYNOTE_INVENTORYNOTE_EDIT    =   "INVENTORYNOTE.EDIT";
        public const string INVENTORYNOTE_INVENTORYNOTE_DELETE  =   "INVENTORYNOTE.DELETE";
       #endregion
       Int32 Code = -1;

       #region IPagePermission Members
       /// <summary>
       /// Check Logged-In User Page Level Permissions
       /// </summary>
       public void CheckUserPagePermissions()
       {
           System.Collections.Generic.List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, PAGE);

           //Check If any permission found for this page 
           if (dict == null || dict.Count < 1)
               Response.Redirect("Permission.aspx?MSG=INVENTORYNOTE:ADD/EDIT/VIEW/DELETE");

           //Disable Add Link, If No Rights
           if (!dict.Contains(INVENTORYNOTE_INVENTORYNOTE_ADD))
           {
               lnkAddNewNote.Visible = false;
           }

           else if (!dict.Contains(INVENTORYNOTE_INVENTORYNOTE_EDIT))
           {
               //EnableDisableEditButtons(false);
               Button btnEdit = gvNotes.FindControl("ibtnNoteEdit") as Button;
               btnEdit.Visible = false;
           }
           else if (!dict.Contains(INVENTORYNOTE_INVENTORYNOTE_DELETE))
           {
               Button btnDelete = gvNotes.FindControl("btnDelNotes") as Button;
               btnDelete.Visible = false;
           }
       }

       #endregion

       protected void Page_PreInit(object sender, EventArgs e)
       {
           try
           {
               if (!String.IsNullOrEmpty(Convert.ToString(Session["MasterPage"])))
                   this.MasterPageFile = Convert.ToString(Session["MasterPage"]);
               else
                   Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + Server.UrlEncode(Request.Url.ToString()));
           }
           catch (Exception ex)
           {
               Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + Server.UrlEncode(Request.Url.ToString()));
           }
          
       }


      #region[ Page Load ]
      protected void Page_Load(object sender, EventArgs e)
      {
          if (!Page.IsPostBack)
          {
              try
              {
                  if (Request.QueryString["Code"] != null && Request.QueryString["Code"].ToString() != "")
                  {
                      Util.Validate_QueryString_Value(6, Request.QueryString["Code"].ToString(), Constant.OrgID); // EntityTypeID=6 is for Inventory
                  }
              }
              catch { }
          }
         //Check Page Level Permission for Logged-In User
          CheckUserPagePermissions();

         if (Request["Code"] != null)
            {
               try
               {  // Using try to avoid page crash on invalid code.
                  this.Code = Convert.ToInt32(Request["Code"]);
               }
               catch { }
            }

         //Do nothing if Code=-1
         if (Code == -1)
             return;
        //Load Notes in gridview control 
        if (!Page.IsPostBack)
         {
             //Show Inventory Header Information which tell about teh current expense being edited for which inventory
             lblInventoryHeader.Text = "Inventory Notes For " + InventoryBAL.GetCurrentInventoryHeader(Code);

             LoadData(this.Code);
         }
       


      }
      #endregion

      #region [ Load Grid Date ]
      private void LoadData(Int32 InventId)
      {
         gvNotes.DataSource = InventoryBAL.FetchNotesByInventoryID(InventId, (int)EntityTypes.Inventory,(int)NoteType.OTHER_NOTE);
         gvNotes.DataBind();
         if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
         {
             lnkAddNewNote.Visible = false;
             if(gvNotes.Rows.Count>0)
             gvNotes.Columns[4].Visible = false;
         }
      }
      #endregion

      #region[ Page Index Change ]
      protected void gvNotes_PageIndexChanging(object sender, GridViewPageEventArgs e)
      {
         gvNotes.PageIndex = e.NewPageIndex;
         LoadData(this.Code);
      }
      #endregion

      #region[ Insert Date to the DataBase ]
      protected void btnInsert_Click(object sender, EventArgs e)
      { 
        //Check for inventory id & Note Id
          if (Code == -1 && hdUpdateNoteId.Value=="-1")
              return;

        //Add Note
        long NoteId = 0;
            Note objNote = new Note();
            objNote.NoteId = Convert.ToInt64(hdUpdateNoteId.Value);
            objNote.EntityId = this.Code;
            objNote.EntityTypeId = (int)EntityTypes.Inventory;
            objNote.Notes = txtNote.Text.Trim();
            objNote.SecurityUserId = Constant.UserId;
            objNote.DateAdded = DateTime.Now;
            objNote.AddedBy = Constant.UserId;
            objNote.DateModified = DateTime.Now;
            objNote.ModifiedBy = Constant.UserId;
            objNote.NoteTypeID = (int)NoteType.OTHER_NOTE;// Convert.ToInt32(ddlNoteType.SelectedValue);
            objNote.IsActive = 1;
            if (hdUpdateNoteId.Value == "-1")
                NoteId = InventoryBAL.UpdateNote(objNote, (int)NoteSave_Mode.Add);
            else
                NoteId = InventoryBAL.UpdateNote(objNote, (int)NoteSave_Mode.Update);
        
        //Refresh Gridview Records
        LoadData(Code);
      }
      #endregion

       /// <summary>
       /// Handle Click event of Image button for Edit Car Notes
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
      protected void ibtnNoteEdit_Click(object sender, ImageClickEventArgs e)
      {
          Note objNote=new Note();
          ImageButton ibtnNoteEdit = (ImageButton)sender;
          lblHeading.Text = "Edit Note";
          GridViewRow row = (GridViewRow)ibtnNoteEdit.NamingContainer;
          long UpdateNoteId = Convert.ToInt64(gvNotes.DataKeys[row.RowIndex].Values[0]);
          hdUpdateNoteId.Value = UpdateNoteId.ToString();
          
          //Set Note value for Editing
          if(row.Cells[1].Text != "&nbsp;")
            txtNote.Text= row.Cells[1].Text;
          //ddlNoteType.SelectedValue = gvNotes.DataKeys[row.RowIndex].Values[1].ToString();
          //Show Notes Popup In EDIT Mode
          MPEAddNewNote.Show();
          
      }
     

      protected void lnkAddNewNote_Click(object sender, EventArgs e)
      {
          //return if inventoryid not provided
          if (Code == -1)
              return;

          hdUpdateNoteId.Value = "-1";
          
          //Clear textbox value;
          txtNote.Text = "";
          
          //Show PopUp for add new notes
          MPEAddNewNote.Show();
      }
     
      /// <summary>
      /// Handle click event of Delete note button(Mark IsActive=0) in db
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
 
      protected void btnDelNotes_Click(object sender, ImageClickEventArgs e)
      {
          ImageButton ibtnNoteDelete = (ImageButton)sender;
          Note objNote = new Note();
         
          GridViewRow row = (GridViewRow)ibtnNoteDelete.NamingContainer;
          //Get ExpenseId to be deleted(Mark IsActive=0) In db
          long DelNoteId = Convert.ToInt64(gvNotes.DataKeys[row.RowIndex].Values[0]);
         

       
          objNote.IsActive = 1;
          objNote.DeletedBy = Constant.UserId;
          objNote.DateDeleted = DateTime.Now;

          //CALL BAL Method to Delete Notes,(Mark IsActive=0)
          InventoryBAL.DeleteNotes(objNote, Convert.ToInt64(DelNoteId));

          //Load Data
          LoadData(Code);
      }
      /// <summary>
      /// Handle button click event for moving back to parent screen i.e ManageInventoryScreen
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      protected void lnkbtnBack_Click(object sender, EventArgs e)
      {
          Response.Redirect("InventoryDetail.aspx?Code=" + Code);
      }
     
   }
}
