using IMS.Models.CommonModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace IMS.Models.ViewModel
{
    public class DashBoard
    {
        public int PartyId { get; set; }
        public string PartyName { get; set; }
        public string Date { get; set; }
        public int OfficeId { get; set; }
        public SelectList PartyLists { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public DashBoard()
        {
            Date = DateTime.Now.ToString("dd/MM/yyyy");
            OfficeId = CommonUtility.GetDefault_OfficeID();
            string PartyListWhereClouse = "And IsActive=1 and Office_id =" + OfficeId.ToString();
            PartyLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Party_Id", "Title", "Party_Master", PartyListWhereClouse), "Id", "Value");
        }

        public DataTable OrderDashboard_Get()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@PartyId", PartyId));
                if (!string.IsNullOrEmpty(Date))
                    SqlParameters.Add(new SqlParameter("@Date", CommonUtility.GetDateYYYYMMDD(Date)));
                dt = DBManager.ExecuteDataTableWithParameter("Material_Order_Dashboard_Status", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }
    }
}