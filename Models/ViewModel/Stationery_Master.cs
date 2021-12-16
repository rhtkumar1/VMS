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
    public class StationeryMaster
    {
        public int Stationery_ID;
        public string Title { get; set; }
        public int Menu_Id { get; set; }
        public SelectList MenuLists { get; set; }
        public int Office_Id { get; set; }
        public SelectList OfficeLists { get; set; }
        public int Financial_Id { get; set; }
        public SelectList FinancialLists { get; set; }
        public string  PreFix { get; set; }
        public string Suffix { get; set; }
        public int StartPoint { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }

        public StationeryMaster()
        {
            OfficeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("office_Id", "Title", "Office_Master", "And IsActive=1"), "Id", "Value");
            MenuLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Menu_Id", "Menu_Name", "Menu_Master", "And IsActive = 1 AND IsStationery=1"), "Id", "Value");
            string FYWhereClouse = "And M.IsActive = 1 and M.Company_Id= " +  CommonUtility.GetCompanyID().ToString();
            FinancialLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("M.Financial_Id", "C.Title + ' ('+ Cast(YEAR(M.From_date) AS nvarchar)+'-'+Cast(YEAR(M.To_date) AS nvarchar) +')' ", "Financial_Master AS M inner join Company_Master AS C ON M.Company_Id=C.Company_Id", FYWhereClouse), "Id", "Value");
            
        }

        public StationeryMaster Stationery_Master_InsertUpdate()
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Stationery_ID", Stationery_ID));
                SqlParameters.Add(new SqlParameter("@Title", Title));
                SqlParameters.Add(new SqlParameter("@Company_Id", CommonUtility.GetCompanyID()));
                SqlParameters.Add(new SqlParameter("@Office_Id", Office_Id));
                SqlParameters.Add(new SqlParameter("@Menu_Id", Menu_Id));
                SqlParameters.Add(new SqlParameter("@PreFix", PreFix));
                SqlParameters.Add(new SqlParameter("@Suffix", Suffix));
                SqlParameters.Add(new SqlParameter("@StartPoint", StartPoint));
                SqlParameters.Add(new SqlParameter("@CreatedBy", CommonUtility.GetLoginID()));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Financial_Master_Insertupdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    Stationery_ID = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return this;
        }

    }
}