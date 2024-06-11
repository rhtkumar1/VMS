using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.Models.ViewModel
{
    public class BillCreation
    {
        public int Id { get; set; }
        public int Bill_Type_Id { get; set; }
        public int Billing_Office_Id { get; set; }
        public DateTime Bill_Date { get; set; }
        public int Bill_No_Id { get; set; }
        public int Client_Id { get; set; }
        public int State_Id { get; set; }
        public int Gst_Type_Id { get; set; }
        public DateTime Submition_Date { get; set; }
        public string Reamrk { get; set; }

        public int Consignee_Id { get; set; }
        public int Consigner_Id { get; set; }
        public int Route_Id { get; set; }
        public int Metarial_Id { get; set; }
        public int Vehicle_Id { get; set; }
        public int Date_Range { get; set; }
        public int GR_Office_Id { get; set; }

        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public int Loginid { get; set; }
    }
}