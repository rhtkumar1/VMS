using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IMS.Models.CommonModel
{
    public class Comman_Controller
    {
    }
    public static class DDLValueFromDB
    {
        // Exampal :----------------------------------------
        // In Model
        //public SelectList DoctorData { get; set; }
        //DoctorData = new SelectList(DDLValueFromDB.GETDATAFROMDB("DoctorId", "DoctorName", "DoctorMaster",  "And Status=1"),"Id", "Value");
        // In View
        //@Html.DropDownListFor(M => M.DoctorID, new SelectList(Model.DoctorData, "Value", "Text"), "Select Docter", htmlAttributes: new { @class = "form-control" })
        // -------------------------------------------------
        public static IEnumerable<DDLSELECT> GETDATAFROMDB(string TableID, string TableValue, string TableName, string WhereCondition)
        {
            DataTable Record = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@TableID", TableID));
                SqlParameters.Add(new SqlParameter("@TableValue", TableValue));
                SqlParameters.Add(new SqlParameter("@TableName", TableName));
                SqlParameters.Add(new SqlParameter("@WhereCondition", WhereCondition));
                Record = DBManager.ExecuteDataTableWithParamiter("DDLValue_Dynamic", CommandType.StoredProcedure, SqlParameters);
                List<DDLSELECT> PD = new List<DDLSELECT>();
                foreach (DataRow DT in Record.Rows)
                {
                    DDLSELECT T = new DDLSELECT();
                    T.Id = Convert.ToInt32(DT["ID"]);
                    T.Value = Convert.ToString(DT["Value"]);
                    PD.Add(T);
                }
                return PD;
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
    public class DDLSELECT
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}