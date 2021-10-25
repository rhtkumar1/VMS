using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IMS.Models.ViewModel
{
    public class Menue_Master
    {
       public List<Menu_Master_Display> listMim = new List<Menu_Master_Display>();

        public  IEnumerable<Menu_Master_Display> GetMinu(int UserID,out List<Menu_Master_Role_Wise> ObjMenu_Master_Role_Wise)
        {
            DataTable dt = new DataTable();
            
            ObjMenu_Master_Role_Wise = new List<Menu_Master_Role_Wise>();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@UserID", UserID));
                dt = DBManager.ExecuteDataTableWithParameter("Menu_List", CommandType.StoredProcedure, SqlParameters);
                
                foreach (DataRow dr0 in dt.Rows)
                {
                    Menu_Master_Role_Wise objMenu_Master_Role_Wise = new Menu_Master_Role_Wise();
                    
                    objMenu_Master_Role_Wise.MenuID = CommonUtility.ConvertInt(dr0["menu_id"]);

                    if (!String.IsNullOrEmpty(Convert.ToString(dr0["menu_url"])))
                    {
                        objMenu_Master_Role_Wise.MenuURL = dr0["menu_url"].ToString();
                        objMenu_Master_Role_Wise.Controller = dr0["menu_url"].ToString().Split('/')[0].ToString();
                        objMenu_Master_Role_Wise.Action = dr0["menu_url"].ToString().Split('/')[1].ToString();
                    }
                    else
                    {
                        objMenu_Master_Role_Wise.MenuURL = "#";
                        objMenu_Master_Role_Wise.Controller = "#";
                        objMenu_Master_Role_Wise.Action = "#";
                    }
                    objMenu_Master_Role_Wise.AuthMode = CommonUtility.ConvertInt(dr0["Auth"].ToString());
                    objMenu_Master_Role_Wise.AppToken = URLEncryption.Encrypt(string.Format("MID={0};AuthMode={1}", objMenu_Master_Role_Wise.MenuID.ToString(), objMenu_Master_Role_Wise.AuthMode.ToString()));//Uri.EscapeDataString(EncryptDecrypt.EncryptString(string.Format("MID={0};AuthMode={1}", objMenu_Master_Role_Wise.MenuID.ToString(), dr0["Auth"].ToString())));
                    ObjMenu_Master_Role_Wise.Add(objMenu_Master_Role_Wise);
                }
                foreach (DataRow dr0 in dt.Rows)
                {
                    if (CommonUtility.ConvertInt(dr0["menu_parent_id"]) == 0)
                    {
                        Menu_Master_Display Minu0 = new Menu_Master_Display();
                        Minu0.MenuTitle = dr0["menu_name"].ToString();
                        Minu0.MenuID = CommonUtility.ConvertInt(dr0["menu_id"]);
                        
                        if (!String.IsNullOrEmpty(Convert.ToString(dr0["menu_url"])))
                        {
                            Minu0.MenuURL = dr0["menu_url"].ToString();
                            Minu0.Controller = dr0["menu_url"].ToString().Split('/')[0].ToString();
                            Minu0.Action = dr0["menu_url"].ToString().Split('/')[1].ToString();
                        }
                        else
                        {
                            Minu0.MenuURL = "#";
                            Minu0.Controller = "#";
                            Minu0.Action = "#";
                        }
                        //Minu0.AuthMode = CommonUtility.ConvertInt(dr0["Auth"].ToString());
                        Minu0.AppToken = URLEncryption.Encrypt(string.Format("MID={0};AuthMode={1}", Minu0.MenuID.ToString(), dr0["Auth"].ToString()));//Uri.EscapeDataString(EncryptDecrypt.EncryptString(string.Format("MID={0};AuthMode={1}", Minu0.MenuID.ToString(), dr0["Auth"].ToString())));
                       // 1 Lable
                        foreach (DataRow dr1 in dt.Rows)
                        {
                            if (Minu0.MenuID == (CommonUtility.ConvertInt(dr1["menu_parent_id"])))
                            {
                                Menu_Master_Display Minu1 = new Menu_Master_Display();
                                Minu1.MenuTitle = dr1["menu_name"].ToString();
                                Minu1.MenuID = CommonUtility.ConvertInt(dr1["menu_id"]);
                                if (!String.IsNullOrEmpty(Convert.ToString(dr1["menu_url"])))
                                {
                                    Minu1.MenuURL = dr1["menu_url"].ToString();
                                    Minu1.Controller = dr1["menu_url"].ToString().Split('/')[0].ToString();
                                    Minu1.Action = dr1["menu_url"].ToString().Split('/')[1].ToString();
                                }
                                else
                                {
                                    Minu1.MenuURL = "#";
                                    Minu1.Controller = "#";
                                    Minu1.Action = "#";
                                }
                                Minu1.AppToken = URLEncryption.Encrypt(string.Format("MID={0};AuthMode={1}", Minu1.MenuID.ToString(), dr1["Auth"].ToString()));//Uri.EscapeDataString(EncryptDecrypt.EncryptString(string.Format("MID={0};AuthMode={1}", Minu1.MenuID.ToString(), dr1["Auth"].ToString())));
                                Minu0.ChildList.Add(Minu1);
                                // 2 Lable
                                foreach (DataRow dr2 in dt.Rows)
                                {
                                    if (Minu1.MenuID == (CommonUtility.ConvertInt(dr2["menu_parent_id"])))
                                    {
                                        Menu_Master_Display Minu2 = new Menu_Master_Display();
                                        Minu2.MenuTitle = dr2["menu_name"].ToString();
                                        Minu2.MenuID = CommonUtility.ConvertInt(dr2["menu_id"]);
                                        if (!String.IsNullOrEmpty(Convert.ToString(dr2["menu_url"])))
                                        {
                                            Minu2.MenuURL = dr2["menu_url"].ToString();
                                            Minu2.Controller = dr2["menu_url"].ToString().Split('/')[0].ToString();
                                            Minu2.Action = dr2["menu_url"].ToString().Split('/')[1].ToString();
                                        }
                                        else
                                        {
                                            Minu2.MenuURL = "#";
                                            Minu2.Controller = "#";
                                            Minu2.Action = "#";
                                        }
                                        Minu2.AppToken = URLEncryption.Encrypt(string.Format("MID={0};AuthMode={1}", Minu2.MenuID.ToString(), dr2["Auth"].ToString()));//Uri.EscapeDataString(EncryptDecrypt.EncryptString(string.Format("MID={0};AuthMode={1}", Minu2.MenuID.ToString(), dr2["Auth"].ToString())));
                                        Minu1.ChildList.Add(Minu2);
                                        // 3 Lable
                                        foreach (DataRow dr3 in dt.Rows)
                                        {
                                            if (Minu2.MenuID == (CommonUtility.ConvertInt(dr3["menu_parent_id"])))
                                            {
                                                Menu_Master_Display Minu3 = new Menu_Master_Display();
                                                Minu3.MenuTitle = dr3["menu_name"].ToString();
                                                Minu3.MenuID = CommonUtility.ConvertInt(dr3["menu_id"]);
                                                if (!String.IsNullOrEmpty(Convert.ToString(dr3["menu_url"])))
                                                {
                                                    Minu3.MenuURL = dr3["menu_url"].ToString();
                                                    Minu3.Controller = dr3["menu_url"].ToString().Split('/')[0].ToString();
                                                    Minu3.Action = dr3["menu_url"].ToString().Split('/')[1].ToString();
                                                }
                                                else
                                                {
                                                    Minu3.MenuURL = "#";
                                                    Minu3.Controller = "#";
                                                    Minu3.Action = "#";
                                                }
                                                Minu3.AppToken = URLEncryption.Encrypt(string.Format("MID={0};AuthMode={1}", Minu3.MenuID.ToString(), dr3["Auth"].ToString()));//Uri.EscapeDataString(EncryptDecrypt.EncryptString(string.Format("MID={0};AuthMode={1}", Minu3.MenuID.ToString(), dr3["Auth"].ToString())));
                                                Minu2.ChildList.Add(Minu3);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        listMim.Add(Minu0);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return listMim;
        }
    }
    public class Menu_Master_Role_Wise
    {
        public int MenuID { get; set; }
        public string MenuURL { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string AppToken { get; set; }
        public int AuthMode { get; set; }
    }
    public class Menu_Master_Display
    {
        public int MenuID { get; set; }
        public string MenuType { get; set; }
        public string MenuURL { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string AppToken { get; set; }
        public string MenuTitle { get; set; }
        public int MenuSequence { get; set; }
        public int MenuParentID { get; set; }
        public List<Menu_Master_Display> ChildList = new List<Menu_Master_Display>();
    }
}