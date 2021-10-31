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
    public class UserMaster
    {
        public int User_Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public SelectList StateLists { get; set; }
        public string Relegion { get; set; }
        public string Pincode { get; set; }
        public string Country { get; set; }
        public string DOJ { get; set; }
        public int PrimaryRole { get; set; }
        public SelectList RoleLists { get; set; }
        public SelectList LocationLists { get; set; }
        public string sRoles { get; set; }
        public string PrimaryRoleName { get; set; }
        DateTime LastLogin { get; set; }
        public string Remarks { get; set; }
        int IsActive { get; set; }
        int CreatedBy { get; set; }
        int ModifiedBy { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }


        private string _LoginId;
        private bool _Islogin;
        public bool Islogin { get { return _Islogin; } }

        public bool IsExists { get; internal set; }
        public dynamic Msg { get; internal set; }

        public UserMaster()
        {
            try
            {
                RoleLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Role_Id", "Title", "Role_Master", "And IsActive=1"), "Id", "Value");
                StateLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("State_Id", "Title", "State_Master", "And IsActive=1"), "Id", "Value");
                LocationLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Location_Id", "Title", "Location_Master", "And IsActive=1"), "Id", "Value");
                CreatedBy = CommonUtility.GetLoginID();
                ModifiedBy = CommonUtility.GetLoginID();
            }
            catch (Exception ex)
            { }
            _LoginId = "0";
            _Islogin = false;
        }
        public bool UserMaster_InsertUpdate(UserMaster userMaster)
        {
            try
            {
                bool bIsSuccess = false; ;
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@User_Id", userMaster.User_Id));
                SqlParameters.Add(new SqlParameter("@UserName", userMaster.UserName));
                SqlParameters.Add(new SqlParameter("@PasswordHash", userMaster.Password));
                SqlParameters.Add(new SqlParameter("@FirstName", userMaster.FirstName));
                SqlParameters.Add(new SqlParameter("@MiddleName", userMaster.MiddleName));
                SqlParameters.Add(new SqlParameter("@LastName", userMaster.LastName));
                SqlParameters.Add(new SqlParameter("@Mobile", userMaster.Mobile));
                SqlParameters.Add(new SqlParameter("@Email", userMaster.Email));
                SqlParameters.Add(new SqlParameter("@Gender", userMaster.Gender));
                SqlParameters.Add(new SqlParameter("@DOB", CommonUtility.GetDateDDMMYYYY(userMaster.DOB)));
                SqlParameters.Add(new SqlParameter("@Address1", userMaster.Address1));
                SqlParameters.Add(new SqlParameter("@Address2", userMaster.Address2));
                SqlParameters.Add(new SqlParameter("@City", userMaster.City));
                SqlParameters.Add(new SqlParameter("@State", userMaster.State));
                SqlParameters.Add(new SqlParameter("@Relegion", userMaster.Relegion));
                SqlParameters.Add(new SqlParameter("@Pincode", userMaster.Pincode));
                SqlParameters.Add(new SqlParameter("@Country", "India"));
                SqlParameters.Add(new SqlParameter("@DOJ", CommonUtility.GetDateDDMMYYYY(userMaster.DOJ)));
                SqlParameters.Add(new SqlParameter("@PrimaryRole", userMaster.PrimaryRole));
                SqlParameters.Add(new SqlParameter("@Remarks", userMaster.Remarks));
                SqlParameters.Add(new SqlParameter("@Roles", userMaster.sRoles));
                SqlParameters.Add(new SqlParameter("@CreatedBy", userMaster.CreatedBy));
                SqlParameters.Add(new SqlParameter("@ModifiedBy", userMaster.ModifiedBy));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("User_Master_Insertupdate", CommandType.StoredProcedure, SqlParameters);
                if (Convert.ToInt32(dt.Rows[0]["User_Id"]) > 0)
                {
                    bIsSuccess = true;
                }
                return bIsSuccess;
            }
            catch (Exception ex)
            { throw ex; }


        }
        public bool UserMaster_Delete(int userId)
        {
            List<SqlParameter> SqlParameters = new List<SqlParameter>();
            SqlParameters.Add(new SqlParameter("@User_Id", userId));
            SqlParameters.Add(new SqlParameter("@ModifiedBy", userId));
            DataTable dt = DBManager.ExecuteDataTableWithParameter("User_Master_Delete", CommandType.StoredProcedure, SqlParameters);
            return Convert.ToBoolean(dt.Rows[0]["IsSucceed"]);
        }
        public List<UserMaster> UserMaster_Get(string AppToken, string AuthMode)
        {
            List<UserMaster> userMaster = new List<UserMaster>();
            DataTable dt = new DataTable();
            try
            {
                dt = DBManager.ExecuteDataTable("User_Master_Getdata", CommandType.StoredProcedure);
                if (dt.AsEnumerable().Count() > 0)
                {
                    foreach (var row in dt.AsEnumerable())
                    {
                        string fullName = string.Empty;
                        if (row["MiddleName"].ToString() != null)
                            fullName = row["FirstName"].ToString() + " " + row["MiddleName"].ToString() + " " + row["LastName"].ToString();
                        else
                            fullName = row["FirstName"].ToString() + " " + row["LastName"].ToString();

                        UserMaster user = new UserMaster()
                        {
                            User_Id = Convert.ToInt32(row["User_Id"]),
                            UserName = row["UserName"].ToString(),
                            FullName = fullName,
                            Mobile = row["Mobile"].ToString(),
                            Email = row["Email"].ToString(),
                            PrimaryRoleName = row["Title"].ToString(),
                            AppToken = AppToken,
                            AuthMode = AuthMode
                        };
                        userMaster.Add(user);
                    }
                }
            }
            catch (Exception ex)
            { throw ex; }

            return userMaster;
        }
        public UserMaster GetUserById(int userId)
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Clear();
                SqlParameters.Add(new SqlParameter("@User_Id", userId));
                DataSet ds = DBManager.ExecuteDataSetWithParameter("User_Master_Get_User_By_Id", CommandType.StoredProcedure, SqlParameters);
                DataTable dtUser = ds.Tables[0];
                DataRow drUser = dtUser.Rows[0];
                DataTable dtUserRollMapping = ds.Tables[1];
                UserMaster userMaster = new UserMaster()
                {
                    User_Id = Convert.ToInt32(drUser["User_Id"]),
                    UserName = drUser["UserName"].ToString(),
                    FirstName = drUser["FirstName"].ToString(),
                    MiddleName = drUser["MiddleName"].ToString(),
                    LastName = drUser["LastName"].ToString(),
                    Mobile = drUser["Mobile"].ToString(),
                    Email = drUser["Email"].ToString(),
                    Gender = drUser["Gender"].ToString(),
                    DOB = drUser["DOB"].ToString(),
                    Address1 = drUser["Address1"].ToString(),
                    Address2 = drUser["Address2"].ToString(),
                    City = drUser["City"].ToString(),
                    State = drUser["State"].ToString(),
                    Relegion = drUser["Relegion"].ToString(),
                    Pincode = drUser["Pincode"].ToString(),
                    DOJ = drUser["DOJ"].ToString(),
                    PrimaryRole = Convert.ToInt32(drUser["PrimaryRole"]),
                    Remarks = drUser["Remarks"].ToString(),
                    sRoles = dtUserRollMapping.Rows[0]["Roles"].ToString()
                };
                return userMaster;
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}