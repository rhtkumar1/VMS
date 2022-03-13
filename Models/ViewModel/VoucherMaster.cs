using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using IMS.Models.CommonModel;

namespace IMS.Models.ViewModel
{
    public class VoucherMaster
    {
        public int VoucherId { get; set; } = 0;
        public int OfficeId { get; set; }
        public int OfficeIdVocherLine { get; set; }
        public int Constant_Id { get; set; }
        public string VoucherType { get; set; }
        public string VoucherNumber { get; set; }
        public string VoucherDate { get; set; }
        public string CheckNumber { get; set; }
        public string CheckDate { get; set; }
        public int FinId { get; set; }
        public int CompanyId { get; set; }
        public int Loginid { get; set; }
        public string Remarks { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public string VoucherLine { get; set; }
        public int PartyId { get; set; }
        public int MENU_Id { get; set; }
        public decimal Amount_DR { get; set; }
        public decimal Amount_CR { get; set; }
        public decimal Total_Amount { get; set; }
        public SelectList OfficeLists { get; set; }
        public SelectList Vouchers { get; set; }
        public SelectList PartyLists { get; set; }
        public List<VoucherMapping> VoucherMappings { get; set; }

        public VoucherMaster()
        {
            OfficeId = CommonUtility.GetDefault_OfficeID();
            OfficeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("office_Id", "Title", "Office_Master", "And IsActive=1"), "Id", "Value");
            string PartyListWhereClouse = "And IsActive=1 and Office_id =" + OfficeId.ToString();
            PartyLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Party_Id", "Title", "Party_Master", PartyListWhereClouse), "Id", "Value");
            Vouchers = new SelectList(DDLValueFromDB.GETDATAFROMDB("Constant_Id", "Constant_Value", "Constant_Values", "And IsActive=1"), "Id", "Value");
            Loginid = CommonUtility.GetLoginID();
            FinId = CommonUtility.GetFYID();
            CompanyId = CommonUtility.GetCompanyID();
            VoucherMappings = new List<VoucherMapping>();
        }
        public VoucherMaster Voucher_InsertUpdate()
        {
            try
            {
                var sb = new System.Text.StringBuilder();
                foreach (var item in VoucherMappings)
                {
                    sb.AppendLine(@"<listnode Line_Id=""" + item.Line_Id + @""" Ledger_Id=""" + item.Ledger_Id + @"""   
                                    Entry_Type=""" + item.Entry_Type + @""" Amount=""" + item.Amount + @"""  />");
                }
                VoucherLine = "<Line>" + sb + "</Line>";

                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@VoucherId", VoucherId));
                SqlParameters.Add(new SqlParameter("@Office_Id", OfficeId));
                SqlParameters.Add(new SqlParameter("@Voucher_Type", Constant_Id));
                SqlParameters.Add(new SqlParameter("@Voucher_No", VoucherNumber));
                SqlParameters.Add(new SqlParameter("@Cheque_No", CheckNumber));
                SqlParameters.Add(new SqlParameter("@Amount_DR", Amount_DR));
                SqlParameters.Add(new SqlParameter("@Amount_CR", Amount_CR));
                SqlParameters.Add(new SqlParameter("@Total_Amount", Total_Amount));
                if (!string.IsNullOrEmpty(VoucherDate))
                    SqlParameters.Add(new SqlParameter("@Voucher_Date", CommonUtility.GetDateYYYYMMDD(VoucherDate)));
                if (!string.IsNullOrEmpty(CheckDate))
                    SqlParameters.Add(new SqlParameter("@Cheque_Date", CommonUtility.GetDateYYYYMMDD(CheckDate)));
                SqlParameters.Add(new SqlParameter("@Fin_Id", FinId));
                SqlParameters.Add(new SqlParameter("@Company_Id", CompanyId));
                SqlParameters.Add(new SqlParameter("@Remarks", Remarks));
                SqlParameters.Add(new SqlParameter("@Voucher_Line", VoucherLine));
                SqlParameters.Add(new SqlParameter("@LoginId", Loginid));
                SqlParameters.Add(new SqlParameter("@MENU_Id", CommonUtility.GetMenuID(AppToken)));

                DataTable dt = DBManager.ExecuteDataTableWithParameter("Material_Purchase_Insertupdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    VoucherId = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return this;
        }

        public DataTable Voucher_Get_Data(int voucherId)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Voucher_Id", voucherId));
                dt = DBManager.ExecuteDataTableWithParameter("Voucher_Entry_Getdata", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }
    }

    public class VoucherMapping
    {
        public string Line_Id { get; set; }
        public string Ledger_Id { get; set; }
        public string Entry_Type { get; set; }
        public string Amount { get; set; }
    }
}