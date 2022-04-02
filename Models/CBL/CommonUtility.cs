using IMS.Models;
using IMS.Models.CBL;
using IMS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Web.Mvc.Filters;
using ZXing;

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
        string[] splitDate = strDate.Split('/');
        return (splitDate[1].Length == 2 ? splitDate[1] : "0" + splitDate[1]) + "/" + (splitDate[0].Length == 2 ? splitDate[0] : "0" + splitDate[0]) + "/" + splitDate[2];
    }
    public static string GetDateYYYYMMDD(string strDate)
    {
        string[] splitDate="1/1/1900".Split('/');
        if (strDate.Contains("/"))
        {
            splitDate = strDate.Split('/');
           
        }
        else if(strDate.Contains("-"))
        {
            splitDate = strDate.Split('-');          

        }
        return splitDate[2] + "-" + (splitDate[1].Length == 2 ? splitDate[1] : "0" + splitDate[1]) + "-" + (splitDate[1].Length == 2 ? splitDate[0] : "0" + splitDate[0]);
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
    public static int GetCompanyID()
    {
        try
        {
            Authenticate ObjAuthenticate = (Authenticate)System.Web.HttpContext.Current.Session["SYSSOFTECHSession"];
            return Convert.ToInt32(ObjAuthenticate.CompanyID);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    public static int GetDefault_OfficeID()
    {
        try
        {
            Authenticate ObjAuthenticate = (Authenticate)System.Web.HttpContext.Current.Session["SYSSOFTECHSession"];
            return Convert.ToInt32(ObjAuthenticate.Default_OfficeId);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    
    public static int GetFYID()
    {
        try
        {
            //Authenticate ObjAuthenticate = (Authenticate);
            return Convert.ToInt32(System.Web.HttpContext.Current.Session["OpenFYID"]);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    public static int GetActiveMenuID()
    {
        try
        {
            //Authenticate ObjAuthenticate = (Authenticate);
            return Convert.ToInt32(System.Web.HttpContext.Current.Session["ActiveMenuID"]);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    public static bool GenerateBarCode(string Data, string Location,string Code,string MRP)
    {
        try
        {
            
            BarcodeWriter writter = new BarcodeWriter()
            {
                Format = (BarcodeFormat)BarcodeFormat.CODE_128 //  BarcodeFormat.CODE_128
            };


            writter.Options.Width = int.Parse("100");
            writter.Options.Height = int.Parse("50");
            writter.Options.Margin = int.Parse("0");
            writter.Options.PureBarcode = false;
            Bitmap Image = writter.Write(Data);
            Bitmap NewBitMap = new Bitmap(Image.Width+60, (Image.Height + 60));
            using (Graphics graphic = Graphics.FromImage(NewBitMap))
            {
                Font newfont = new Font("IDAutomationHC39M", 10, FontStyle.Regular);
                PointF point = new PointF(0, 5);
                SolidBrush black = new SolidBrush(Color.Black);
                //SolidBrush white = new SolidBrush(Color.White);
                // graphic
                // graphic.FillRectangle(white, 0, Image.Height - 20, Image.Width, Image.Height);
                graphic.DrawString(" Code" + " : "+ Code, newfont, black, point);
                graphic.DrawString(" MRP      " + " : " + MRP, newfont, black, new PointF(0, 25));
                graphic.DrawImage(Image, new PointF(0, 50));
            }

            NewBitMap.Save(Location);            
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return true;
    }
                
         
    //public static bool GenerateQrcode(string Data, string Location)
    //{
    //    try
    //    {
    //        QRCode qrcode = new QRCode();
    //        qrcode.Data = Data;
    //        qrcode.DataMode = QRCodeDataMode.Byte;
    //        qrcode.UOM = UnitOfMeasure.PIXEL;
    //        qrcode.X = 3;
    //        qrcode.LeftMargin = 0;
    //        qrcode.RightMargin = 0;
    //        qrcode.TopMargin = 0;
    //        qrcode.BottomMargin = 0;
    //        qrcode.Resolution = 72;
    //        qrcode.Rotate = Rotate.Rotate0;
    //        qrcode.ImageFormat = ImageFormat.Gif;
    //        qrcode.drawBarcode(Location);
    //        return true;
    //    }
    //    catch (Exception Ex)
    //    {
    //        return false;
    //    }
    //}
}