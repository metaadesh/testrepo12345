﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION.DAL;
namespace METAOPTION.BAL
{
    public class UpdateLaneGroupsBAL
    {
        UpdateLaneGroupsDAL objUpLA = new UpdateLaneGroupsDAL();
        /// <summary>
        /// Update Lane Groups
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <param name="G1"></param>
        /// <param name="G2"></param>
        /// <param name="G3"></param>
        /// <param name="G4"></param>
        /// <param name="G5"></param>
        /// <returns></returns>
        public long UpdateLaneGroups(Int32 inventoryId, bool G1, bool G2, bool G3, bool G4, bool G5)
        {
            return objUpLA.UpdateLaneAssignmentsGroups(inventoryId, G1, G2, G3, G4, G5);
        }
        /// <summary>
        /// Update Lane Groups
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="G1">Group 1</param>
        /// <param name="G2">Group 2</param>
        /// <param name="G3">Group 3</param>
        /// <param name="G4">Group 4</param>
        /// <param name="G5">Group 5</param>
        /// <param name="GN1">Group Name 1</param>
        /// <param name="GN2">Group Name 2</param>
        /// <param name="GN3">Group Name 3</param>
        /// <param name="GN4">Group Name 4</param>
        /// <param name="GN5">Group Name 5</param>
        /// <param name="Addedby">Addedby</param>
        /// <returns></returns>
        public long EditLaneGroups(Int32 inventoryId, bool G1, bool G2, bool G3, bool G4, bool G5,string GN1,string GN2, string GN3, string GN4, string GN5,Int64 Addedby)
        {
            return objUpLA.EditLaneAssignmentsGroups(inventoryId, G1, G2, G3, G4, G5, GN1, GN2, GN3, GN4, GN5, Addedby);

        }
    }
}