using IMS.Models.CommonModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Models.ViewModel
{
    public class LedgerReport
    {
        public int PartyId { get; set; }
        public string PartyName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int OfficeId { get; set; }
        public int ItemId { get; set; }
        public SelectList LedgerGroupLists { get; set; }
        public int ReportId { get; set; }
        
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }



        public LedgerReport()
        {
            FromDate = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
            ToDate = DateTime.Now.ToString("dd/MM/yyyy");
            OfficeId = CommonUtility.GetDefault_OfficeID();
            LedgerGroupLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Group_Id", "Title", "Group_Master", "And IsActive=1"), "Id", "Value");
            
        }
        public DataTable Report_LedgerReport()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@ItemID", ItemId));
                if (!string.IsNullOrEmpty(FromDate))
                    SqlParameters.Add(new SqlParameter("@FromDate", CommonUtility.GetDateYYYYMMDD(FromDate)));
                if (!string.IsNullOrEmpty(ToDate))
                    SqlParameters.Add(new SqlParameter("@todate", CommonUtility.GetDateYYYYMMDD(ToDate)));
                dt = DBManager.ExecuteDataTableWithParameter("Report_StockMovement", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public DataTable GroupMaster_GetLedger(int Group_Id)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Group_Id", Group_Id));
                dt = DBManager.ExecuteDataTableWithParameter("Group_Master_GetLedger", CommandType.StoredProcedure,SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

    }
}