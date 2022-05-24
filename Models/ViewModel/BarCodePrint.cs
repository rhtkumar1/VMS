using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using ZXing;

namespace IMS.Models.ViewModel
{

    public class BarCodePrint
    {
        public string ItemID { get;set; }
        public string MRP { get; set; }
        public string ItemCode  { get; set; }
        public string ItemName { get; set; }
        public string BaseUnit { get; set; }
        public string Unit { get; set; }
        public string HelpLine { get; set; }
        public string email { get; set; }
        public bool IsmrpPrint { get; set; }
        public string NetQty { get; set; }
        public string TextVal { get; set; }
        public DataTable BarCodaPrint = new DataTable();
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public void GenerateBarcoad()
        {             
            CreateDatatable();
            try
            {
                if (String.IsNullOrWhiteSpace(TextVal)) return;

                BarcodeWriter writter = new BarcodeWriter()
                {
                    Format = BarcodeFormat.CODE_128
                };
                writter.Options.Width = int.Parse("100");
                writter.Options.Height = int.Parse("40");
                writter.Options.Margin = int.Parse("0");
                writter.Options.PureBarcode = false;
                Bitmap Image = writter.Write(TextVal);
                Bitmap NewBitMap = new Bitmap(Image.Width + 60, (Image.Height + 90));
                using (Graphics graphic = Graphics.FromImage(NewBitMap))
                {                  
                    string data = "";
                    
                    // Insert Reord in DataTable
                    InsertOneRecordInDataTable(ItemName, Image);
                    InsertOneRecordInDataTable(NetQty, Image);
                    if (IsmrpPrint)
                        InsertOneRecordInDataTable(MRP, Image);
                    InsertOneRecordInDataTable("Help  : " + HelpLine, Image);
                    InsertOneRecordInDataTable("Email : " + email, Image);
                    // End record 
                    
                }
            }
            catch (Exception ex)
            {
              //  MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertOneRecordInDataTable(string TEXT, Bitmap Image)
        {
            DataRow ObjDataRow = BarCodaPrint.NewRow();
            ObjDataRow["ID"] = 1;
            ObjDataRow["TEXT"] = TEXT;
            ObjDataRow["Image"] = Image;
            BarCodaPrint.Rows.Add(ObjDataRow);
        }

        private void CreateDatatable()
        {
            //1 AS ID 'Text' AS TEXT, 'T' AS Image
            BarCodaPrint = new DataTable("BarcodeData");
            DataColumn dtColumn;
            // Create ID column.
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Int32);
            dtColumn.ColumnName = "ID";
            dtColumn.Caption = "ID";
            dtColumn.AutoIncrement = false;
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;
            BarCodaPrint.Columns.Add(dtColumn);
            // Create TEXT column.
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "TEXT";
            dtColumn.Caption = "TEXT";
            dtColumn.AutoIncrement = false;
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;
            BarCodaPrint.Columns.Add(dtColumn);
            // Create Image column.
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Bitmap);
            dtColumn.ColumnName = "Image";
            dtColumn.Caption = "Image";
            dtColumn.AutoIncrement = false;
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;
            BarCodaPrint.Columns.Add(dtColumn);
            // return ObjDT;
        }
        public BarCodePrint(int item_Id)
        {
            ItemID = item_Id.ToString();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@ItemID", item_Id));
                DataSet ds = DBManager.ExecuteDataSetWithParameter("Item_Master_Getdata_ForBarcodePrint_Web", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ItemCode = dr["Code"].ToString();
                    ItemName = dr["Title"].ToString();
                    MRP = dr["MRP"].ToString();                    
                    BaseUnit = dr["BaseUnit"].ToString();                    
                    HelpLine = dr["HelpLine"].ToString();
                    email = dr["Email"].ToString();
                    NetQty = dr["BarcodeQty"].ToString();
                    Unit = dr["BarcodeUnit"].ToString();
                    TextVal = dr["BARCOADID"].ToString();
                    NetQty = "NetQty :  " + NetQty + " " + BaseUnit + " In " + Unit;
                    MRP = "MRP    :  " + MRP + " Per " + BaseUnit;
                }
            }
            catch (Exception ex)
            { throw ex; }

            
        }

    }
}