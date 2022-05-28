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
    public class ReportInterFace
    {
        public int PartyId { get; set; }
        public string PartyName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int OfficeId { get; set; }
        public SelectList OfficeLists { get; set; }
        public SelectList PartyLists { get; set; }
        public int ItemId { get; set; }
        public SelectList Item_Lists { get; set; }
        public int ReportId { get; set; }
        public SelectList Report_Lists { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public SelectList LedgerGroupLists { get; set; }


        public ReportInterFace()
        {
            FromDate = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
            ToDate = DateTime.Now.ToString("dd/MM/yyyy");
            OfficeId = CommonUtility.GetDefault_OfficeID();
            string PartyListWhereClouse = "And IsActive=1 ";
            PartyLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Party_Id", "Title", "Party_Master", PartyListWhereClouse), "Id", "Value");
            Item_Lists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Item_Id", "Title", "Item_Master", "And IsActive=1"), "Id", "Value");
            Report_Lists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Report_Id", "ReportName", "Report_Config", "And IsActive=1 and ReportType=2"), "Id", "Value");
            OfficeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Office_Id", "Title", "Office_Master", "And IsActive=1"), "Id", "Value");
            LedgerGroupLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Group_Id", "Title", "Group_Master", "And IsActive=1"), "Id", "Value");
        }
        public DataTable Report_StockMovement()
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
        //public void MapReportConfig(int ReportId)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        //string sql = "SELECT ReportName,QueryType,Query,Params FROM Report_Config (nolock) WHERE IsActive=1 AND Report_Id=" + ReportId.ToString() + "";
        //        //List<SqlParameter> SqlParameters = new List<SqlParameter>();
        //        //SqlParameters.Add(new SqlParameter("@Party_Id", Party_Id));
        //        string sql = "Report_Config_GetData";
        //        List<SqlParameter> SqlParameters = new List<SqlParameter>();
        //        SqlParameters.Add(new SqlParameter("@Report_Id", ReportId));
        //        dt = DBManager.ExecuteDataTableWithParameter(sql, CommandType.StoredProcedure, SqlParameters);
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            ReportName = Convert.ToString(dr["ReportName"]);
        //            QueryType = Convert.ToString(dr["QueryType"]);
        //            SPName = Convert.ToString(dr["Query"]);
        //            string[] strparam = dr["Params"].ToString().Split(',');
        //            for (int i = 0; i < strparam.Count(); i++)
        //            {
        //                lstparams.Add(strparam[i].ToString().ToLower());
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    { throw ex; }
        //}
       
    }
}