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
    public class AdvanceExpenseType
    {
        public int Id { get; set; }
        public int Type_Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Dr_Ledger_Id { get; set; }
        public int Cr_Ledger_Id { get; set; }
        public bool Not_Impact_On_Driver_Ac { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public int Loginid { get; set; }
        public SelectList TypeLists { get; set; }
        public SelectList PartyLists { get; set; }

        public AdvanceExpenseType()
        {
             TypeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Constant_Id", "Constant_Value", "Constant_Values", "And Menu_Id=60001 And IsActive=1"), "Id", "Value");
             PartyLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Party_Id", "Title", "Party_Master", "And IsActive=1"), "Id", "Value");

            Loginid = CommonUtility.GetLoginID();
        }

        public AdvanceExpenseType AdvanceExpenseType_InsertUpdate(AdvanceExpenseType advanceExpenseType)
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Id", advanceExpenseType.Id));
                SqlParameters.Add(new SqlParameter("@Name", advanceExpenseType.Name));
                SqlParameters.Add(new SqlParameter("@Code", advanceExpenseType.Code));
                SqlParameters.Add(new SqlParameter("@Type_Id", advanceExpenseType.Type_Id));
                SqlParameters.Add(new SqlParameter("@Dr_Ledger_Id", advanceExpenseType.Dr_Ledger_Id));
                SqlParameters.Add(new SqlParameter("@Cr_Ledger_Id", advanceExpenseType.Cr_Ledger_Id));
                SqlParameters.Add(new SqlParameter("@Not_Impact_On_Driver_Ac", advanceExpenseType.Not_Impact_On_Driver_Ac));
                SqlParameters.Add(new SqlParameter("@Loginid", Loginid));

                DataTable dt = DBManager.ExecuteDataTableWithParameter("AdvanceExpenseType_Insertupdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    Id = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }

            }
            catch (Exception ex)
            { throw ex; }

            return advanceExpenseType;
        }

        public DataTable AdvanceExpenseType_Get()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DBManager.ExecuteDataTable("AdvanceExpenseType_Getdata", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public AdvanceExpenseType AdvanceExpenseType_Delete(AdvanceExpenseType advanceExpenseType)
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Id", advanceExpenseType.Id));
                SqlParameters.Add(new SqlParameter("@Loginid", advanceExpenseType.Loginid));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("AdvanceExpenseType_Delete", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    Id = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return advanceExpenseType;
        }
    }
}