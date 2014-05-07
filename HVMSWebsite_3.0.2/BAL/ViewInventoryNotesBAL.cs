using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using METAOPTION;
using METAOPTION.DAL;
namespace METAOPTION.BAL
{
   public  class ViewInventoryNotesBAL
    {
        ViewInventoryNotesDAL objNote = new ViewInventoryNotesDAL();
        
       /// <summary>
        /// Get Inventory Notes List
       /// </summary>
       /// <param name="inventoryId"></param>
       /// <returns></returns>
        //public List<string> SelectInventorynotes(Int64 inventoryId)
        //{
        //    var result = objNote.SelectInventoryNotes(inventoryId).AsQueryable();
        //    List<string> lstNote = new List<string>();
        //    foreach (GetInventoryNotesResult objResult in result)
        //    {

        //        //NoteId at Index 0
        //        lstNote.Add(objResult.NoteId.ToString());
        //        //Notes at Index 1
        //        lstNote.Add(objResult.Notes.ToString());
        //        //AddedBy at Index 2
        //        lstNote.Add(objResult.AddedBy.ToString());
        //        //DateAdded at Index 3
        //        lstNote.Add(objResult.DateAdded.ToString());
        //        //DateModifiedToString at Index 1
        //        lstNote.Add(objResult.DateModified.ToString());
        //        //DateModified at Index 1
        //        lstNote.Add(objResult.DateModified.ToString());

        //    }
        //    return lstNote;
        //}
    }
}
