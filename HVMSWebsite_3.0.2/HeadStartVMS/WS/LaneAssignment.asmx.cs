using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using METAOPTION.BAL;
using METAOPTION.UI;

namespace METAOPTION.WS
{
    /// <summary>
    /// Summary description for LaneAssignment
    /// </summary>
    [WebService(Namespace = "http://www.metaOption.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [ScriptService]
    public class LaneAssignment : System.Web.Services.WebService
    {

        #region [To update lane Regular #]
        /// <summary>
        /// To update lane Regular #
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="RegularLane">RegularLane #</param>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public Int32 UpdateRegularLane(long inventoryId, String RegularLane, long userId)
        {
            LaneAssignmentBAL objLane = new LaneAssignmentBAL();
            return objLane.UpdateRegularLane(inventoryId, RegularLane, userId);
        }
        #endregion
        
        #region [To update lane Exotic #]
        /// <summary>
        /// To update lane Exotic #
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="ExoticLane">ExoticLane #</param>
        /// <param name="modifiedBy">modifiedBy</param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public Int32 UpdateExoticLane(long inventoryId, String ExoticLane, long modifiedBy)
        {
            LaneAssignmentBAL objLane = new LaneAssignmentBAL();

            return objLane.UpdateExoticLane(inventoryId, ExoticLane, modifiedBy);

        }
        #endregion

        #region [To update lane online #]
        /// <summary>
        /// To update lane online #
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="OnlineLane">OnlineLane #</param>
        /// <param name="modifiedBy">modifiedBy</param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public Int32 UpdateOnlineLane(long inventoryId, String OnlineLane, long modifiedBy)
        {
            LaneAssignmentBAL objLane = new LaneAssignmentBAL();
            return objLane.UpdateOnlineLane(inventoryId, OnlineLane, modifiedBy);
        }
        #endregion

        #region [To Update lane Virtual #]
        /// <summary>
        /// To Update lane Virtual #
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="VirtualLane">VirtualLane #</param>
        /// <param name="modifiedBy">modifiedBy</param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public Int32 UpdateVirtualLane(long inventoryId, String VirtualLane, long modifiedBy)
        {
            LaneAssignmentBAL objLane = new LaneAssignmentBAL();
            return objLane.UpdateVirtualLane(inventoryId, VirtualLane, modifiedBy);
        }
        #endregion

        #region [To Update lane VIN #]
        /// <summary>
        /// To Update lane VIN #
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="VIN">VIN #</param>
        /// <param name="modifiedBy">modifiedBy</param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public Int32 UpdateVIN(long inventoryId, String VIN, long modifiedBy)
        {
            LaneAssignmentBAL objLane = new LaneAssignmentBAL();
            return objLane.UpdateVIN(inventoryId, VIN, modifiedBy);
        }
        #endregion

        #region [To Update lane Mileage]
        /// <summary>
        /// To Update lane Mileage
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="Mileage">Mileage</param>
        /// <param name="modifiedBy">modifiedBy</param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public Int32 UpdateMileage(long inventoryId, long Mileage, long modifiedBy)
        {
            LaneAssignmentBAL objLane = new LaneAssignmentBAL();
            return objLane.UpdateMileage(inventoryId, Mileage, modifiedBy);
        }
        #endregion


        #region [To Update lane Market Price]
        /// <summary>
        /// To Update lane Market Price
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="Marketprice">Marketprice</param>
        /// <param name="modifiedBy">modifiedBy</param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public Int32 UpdateMarketprice(long inventoryId, Decimal Marketprice, long modifiedBy)
        {
          LaneAssignmentBAL objLane = new LaneAssignmentBAL();
          return objLane.UpdateMarketPrice(inventoryId, Marketprice, modifiedBy);
        }
        #endregion

        #region [To Update lane Note]
        /// <summary>
        /// To Update lane Note
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="Notes">Notes</param>
        /// <param name="modifiedBy">modifiedBy</param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public Int32 UpdateLaneNotes(long inventoryId, string Notes, long modifiedBy)
        {
            LaneAssignmentBAL objLane = new LaneAssignmentBAL();
            return objLane.UpdateLaneNotes(inventoryId, Notes, modifiedBy);
        }
        #endregion

        #region [Update lane groups]
        /// <summary>
        /// Update lane groups
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="G1"> Group Abbriviation 1</param>
        /// <param name="G2">Group Abbriviation 2</param>
        /// <param name="G3">Group Abbriviation 3</param>
        /// <param name="G4">Group Abbriviation 4</param>
        /// <param name="G5">Group Abbriviation 5</param>
        /// <param name="GN1">Group Name 1</param>
        /// <param name="GN2">Group Name  2</param>
        /// <param name="GN3">Group Name  3</param>
        /// <param name="GN4">Group Name  4</param>
        /// <param name="GN5">Group Name  5</param>
        /// <param name="Addedby">Addedby</param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public long EditLaneGroups(Int32 inventoryId, bool G1, bool G2, bool G3, bool G4, bool G5, string GN1, string GN2, string GN3, string GN4, string GN5, Int64 Addedby)
        {
            UpdateLaneGroupsBAL objLAGrp = new UpdateLaneGroupsBAL();
            return objLAGrp.EditLaneGroups(inventoryId, G1, G2, G3, G4, G5, GN1, GN2, GN3, GN4, GN5, Addedby);
        }
        #endregion

        #region [ Update Lane IsExotic ]
        /// <summary>
        /// Update Lane IsExotic
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="IsExotic">IsExotic</param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public long EditLaneIsExotic(Int32 inventoryId, bool IsExotic)
        {
            LaneAssignmentBAL objLAGrp = new LaneAssignmentBAL();
            return objLAGrp.UpdateIsExotic(inventoryId, IsExotic);
        }
        #endregion

        #region "Insert into temp(It will create at run time) table of GridView Inner HTML code for email"
        /// <summary>
        /// Insert into temp(It will create at run time) table of GridView Inner HTML code for email
        /// </summary>
        /// <param name="InnserHTML">InnserHTML</param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public string InnserHTML(string InnserHTML)
        {
            //InsertInnerHTMLOfInventoryListBAL objHtml = new InsertInnerHTMLOfInventoryListBAL();
            LaneAssignmentBAL objLane = new LaneAssignmentBAL();
            return objLane.InsertInnerHTML(InnserHTML).ToString();
        }
        #endregion

        #region [get modified records Counts]
        /// <summary>
        /// Get modified records Counts
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public string GetCount(long userId)
        {
            string sCount = "";
            LaneAssignmentBAL objLane = new LaneAssignmentBAL();
            try
            {
                //Count upto 20 recods modified in current session
                sCount = objLane.Ge20RecordsCount(userId).ToString();
            }
            catch(Exception ex){}
            try
            {
                //Count upto 10 recods for pending email & announcement
                sCount = sCount + "," + objLane.Ge10RecordsCount(userId);
            }
            catch (Exception ex) { }
            try
            {
                //Count upto 100 recods for pending email & announcement
                sCount = sCount + "," + objLane.SelectLaneAnnouncementAndEmail(userId);
            }
            catch (Exception ex) { }
            return sCount;
        }
        #endregion
    }
}
