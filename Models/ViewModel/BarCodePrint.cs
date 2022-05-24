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
        public string BarCodeLocation { get; set; }
        public string BarCodeImage { get; set; }
        public string Imported { get; set; }
        public void GenerateBarcoad()
        {             
            CreateDatatable();
            try
            {
                // Insert Reord in DataTable
                InsertOneRecordInDataTable(ItemName);
                InsertOneRecordInDataTable(NetQty);
                if (IsmrpPrint)
                    InsertOneRecordInDataTable(MRP);
                InsertOneRecordInDataTable(HelpLine);
                InsertOneRecordInDataTable(email);
                InsertOneRecordInDataTable(Imported);
                // End record
            }
            catch (Exception ex)
            {
              //  MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertOneRecordInDataTable(string TEXT)
        {
            DataRow ObjDataRow = BarCodaPrint.NewRow();
            ObjDataRow["ID"] = ItemID;
            ObjDataRow["TEXT"] = TEXT;
            ObjDataRow["BarCodeImage"] = BarCodeLocation;
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
            // Create Image Location column.
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "BarCodeImage";
            dtColumn.Caption = "BarCodeImage";
            dtColumn.AutoIncrement = false;
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;
            BarCodaPrint.Columns.Add(dtColumn); 
            //// Create Image column.
            //dtColumn = new DataColumn();
            //dtColumn.DataType = typeof(Bitmap);
            //dtColumn.ColumnName = "Image";
            //dtColumn.Caption = "Image";
            //dtColumn.AutoIncrement = false;
            //dtColumn.ReadOnly = false;
            //dtColumn.Unique = false;
            //BarCodaPrint.Columns.Add(dtColumn);
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
                    HelpLine = "Help  : " + dr["HelpLine"].ToString();
                    email = "Email : " + dr["Email"].ToString();
                    NetQty = dr["BarcodeQty"].ToString();
                    Unit = dr["BarcodeUnit"].ToString();
                    TextVal = dr["BARCOADID"].ToString();
                    NetQty = "NetQty :  " + NetQty + " " + BaseUnit + " In " + Unit;
                    MRP = "MRP    :  " + MRP + " Per " + BaseUnit +  "(inclusive of all Taxes)";
                    Imported = dr["Imported"].ToString();
                    IsmrpPrint = Convert.ToBoolean(dr["IsMRPPrint"]);

                }
            }

            catch (Exception ex)
            { throw ex; }

        }
        public BarCodePrint(int item_Id,string _BarCodeLocation)
        {
            ItemID = item_Id.ToString();
            BarCodeLocation = _BarCodeLocation;
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
                    HelpLine = "Help  : " + dr["HelpLine"].ToString();
                    email = "Email : " + dr["Email"].ToString();
                    NetQty = dr["BarcodeQty"].ToString();
                    Unit = dr["BarcodeUnit"].ToString();
                    TextVal = dr["BARCOADID"].ToString();
                    NetQty = "NetQty :  " + NetQty + " " + BaseUnit + " In " + Unit;
                    MRP = "MRP    :  " + MRP + " Per " + BaseUnit + "(inclusive of all Taxes)";
                    Imported = dr["Imported"].ToString();
                    IsmrpPrint = Convert.ToBoolean(dr["IsMRPPrint"]);
                }

                string BarCodeString;
                if (string.IsNullOrEmpty(TextVal))
                {
                    BarCodeString = "SYS";
                    for (int i = item_Id.ToString().Length + 3; i < 11; i++)
                    {
                        BarCodeString = BarCodeString + "0";
                    }
                    BarCodeString = BarCodeString + "X" + ItemID;


                }
                else
                { BarCodeString = TextVal; }
                if (!string.IsNullOrEmpty(_BarCodeLocation))
                {
                    BarCodeLocation = BarCodeLocation + Convert.ToString(ItemID) + ".jpeg";
                    CommonUtility.GenerateBarCode(Convert.ToString(BarCodeString), BarCodeLocation, "", "" + " ");
                }
            }

            catch (Exception ex)
            { throw ex; }

            
        }

    }
}