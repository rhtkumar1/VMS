using IMS.Models.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Models.ViewModel
{
    public class StoreTransfer
    {
        public int ToOffice_Id { get; set; }
        public SelectList ToOfficeLists { get; set; }
        public int FromOffice_Id { get; set; }
        public SelectList FromOfficeLists { get; set; }
        public List<StoreTransferList> StoreTransferList { get; set; }
        public string Date { get; set; }
        public string Remarks { get; set; }

        public SelectList Item_Lists { get; set; }
        public SelectList UnitLists { get; set; }
        public int AvailableQty { get; set; }
        public decimal LastPrice { get; set; }
        public int OrderQty { get; set; }


        public int FinId { get; set; }
        public int CompanyId { get; set; }
        public int Loginid { get; set; }
        public string MaterialLine { get; set; }
        public bool IsActive { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public int MENU_Id { get; set; }

        public StoreTransfer()
        {
            ToOffice_Id = CommonUtility.GetDefault_OfficeID();
            Loginid = CommonUtility.GetLoginID();
            MENU_Id = CommonUtility.GetActiveMenuID();
            ToOfficeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Office_Id", "Title", "Office_Master", "And IsActive=1"), "Id", "Value");
            FromOfficeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Office_Id", "Title", "Office_Master", "And IsActive=1 AND Office_Id !=" + ToOffice_Id.ToString()), "Id", "Value");
            Item_Lists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Item_Id", "Title", "Item_Master", "And IsActive=1"), "Id", "Value");
            UnitLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Unit_Id", "Title", "Unit_Master", "And IsActive=1"), "Id", "Value");
            StoreTransferList = new List<StoreTransferList>();
            
        }

    }

    public class StoreTransferList
    {
        public int ItemId { get; set; }
        public int AvailableQty { get; set; }
        public decimal LastPrice { get; set; }
        public int OrderQty { get; set; }


    }
}