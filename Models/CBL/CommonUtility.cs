using IMS.Models;
using IMS.Models.CBL;
using IMS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web.Mvc.Filters;

public static class CommonUtility
{


    public static List<T> ConvertToList<T>(DataTable dt)
    {
        List<T> data = new List<T>();
        foreach (DataRow row in dt.Rows)
        {
            T item = GetItem<T>(row);
            data.Add(item);
        }
        return data;
    }
    private static T GetItem<T>(DataRow dr)
    {
        Type temp = typeof(T);
        T obj = Activator.CreateInstance<T>();

        foreach (DataColumn column in dr.Table.Columns)
        {
            foreach (PropertyInfo pro in temp.GetProperties())
            {
                if (pro.Name == column.ColumnName)
                    pro.SetValue(obj, dr[column.ColumnName], null);
                else
                    continue;
            }
        }
        return obj;
    }
    public static int ConvertInt(object value)
    {
        int result = 0;
        try
        {
            if (value != null && !Convert.IsDBNull(value))
            {
                if (System.Convert.ToString(value).Trim().Length > 0)
                {
                    result = System.Convert.ToInt32(value);
                }
            }
        }
        catch (System.Exception xe)
        {
            throw xe;
        }
        return result;
    }
    public static string GetDateDDMMYYYY(string strDate)
    {
        string[] splitDate = strDate.Split('-');
        return (splitDate[1].Length == 2 ? splitDate[1] : "0" + splitDate[1]) + "/" + (splitDate[0].Length == 2 ? splitDate[0] : "0" + splitDate[0]) + "/" + splitDate[2];
    }
    public static int GetAuthMode(string AppToken)
    {
       
        string AT = URLEncryption.Decrypt(AppToken);
        return CommonUtility.ConvertInt(AT.Split(';')[1].Split('=')[1]);
       
    }
    public static int GetMenuID(string AppToken)
    {
        string AT = URLEncryption.Decrypt(AppToken);
        return CommonUtility.ConvertInt(AT.Split(';')[0].Split('=')[1]);
       
    }
    public static int GetLoginID()
    {
        try
        {
            Authenticate ObjAuthenticate = (Authenticate)System.Web.HttpContext.Current.Session["SYSSOFTECHSession"];
            return Convert.ToInt32(ObjAuthenticate.UserId);
        }
        catch(Exception Ex)
        {
            throw Ex;
        }
    }
    public static string URLAppToken(string AppToken)
    {
        
        return AppToken;
    }
    public static Menu_Master_Role_Wise GetMenu(string AppToken)
    {
        Menu_Master_Role_Wise obj=null;
        Authenticate ObjAuthenticate = (Authenticate)System.Web.HttpContext.Current.Session["SYSSOFTECHSession"];
        if (ObjAuthenticate != null)
        {
            int MenuId = CommonUtility.GetMenuID(AppToken);
            obj = ObjAuthenticate.ObjMenu_Master_Role_Wise.Find(X => X.MenuID == MenuId);
        }
        return obj;
    }
}