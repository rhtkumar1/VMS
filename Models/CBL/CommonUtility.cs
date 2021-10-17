using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

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

}