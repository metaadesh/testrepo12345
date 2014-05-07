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
using METAOPTION.DAL;

namespace METAOPTION.UI
{
    public partial class AddNote : System.Web.UI.Page
    {
        #region [Page Load Event]
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                if (Request["mode"] == "edit")
                    GetComment();
                else
                    btnUpdate.Visible = false;

            }
           
        }
            #endregion

        #region[Get old Comment for Edit]
        protected void GetComment()
        {
            btnAddComment.Visible = false;
            txtComment.Text = InventoryBAL.GetNoteByID(Convert.ToInt64(Request["ID"]));
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            // Update Note for Employee
            if (Request["ID"] != null)
            {
                Note objNote = new Note();
                //Set Note data entered by user to note class object properties
                objNote.EntityId = Convert.ToInt64(Request["EntityId"]);
                objNote.EntityTypeId = 5;
                objNote.Notes = txtComment.Text.Trim();
                objNote.SecurityUserId = Constant.UserId;
                objNote.ModifiedBy = Constant.UserId;
                objNote.DateModified = DateTime.Now;

                //Call BAL METHOD TO Update Emplyee NOTES
                long NoteId = InventoryBAL.UpdateNotes(objNote, Convert.ToInt64(Request["ID"]));
                this.Page.ClientScript.RegisterStartupScript(typeof(AddNote), "closeThickBox", "self.parent.updated();", true);
            }
        }
        #endregion

        #region[Add New Comment/Note]
        protected void btnAddComment_Click(object sender, EventArgs e)
        {
            // Add Note for Employee
            if (Request["EntityId"] != null && Request["type"] != null)
            {
                Note objNote = new Note();
                //Set Note data entered by user to note class object properties
                objNote.EntityId = Convert.ToInt64(Request["EntityId"]);
                objNote.EntityTypeId = 5;
                objNote.Notes = txtComment.Text.Trim();
                objNote.SecurityUserId = Constant.UserId;
                objNote.AddedBy = Constant.UserId;
                objNote.DateAdded = DateTime.Now;

                //Call BAL METHOD TO ADD Emplyee NOTES,and get NoteId generated
                long NoteId = InventoryBAL.AddNotes(objNote);
                this.Page.ClientScript.RegisterStartupScript(typeof(AddNote), "closeThickBox", "self.parent.updated();", true);

            }
        }
        #endregion

    }
}
