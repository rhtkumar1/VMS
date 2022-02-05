using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;


namespace IMS.Reports
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        Dictionary<string, string> paramcol = new Dictionary<string, string>();
        List<string> lstparams = new List<string>();
        int ReportId;
        string ReportName;
        string SPName;
        string QueryType;
        List<ReportParameter> reportparam = new List<ReportParameter>();
        protected void Page_Load(object sender, EventArgs e)
        {
            string querystring = "";
            if (!IsPostBack)
            {
                querystring = Request.QueryString.ToString();
                ReportId = Convert.ToInt32(Request.QueryString["ReportId"]);
            }
            MapQueryStringParams(querystring);
            MapReportConfig(ReportId);

            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Reports/Report/"+ReportName+".rdlc");
            DataTable reportdt = GetReportData();


            //if (reportparam.Count() > 0)
            //{
            //    ReportViewer1.ServerReport.SetParameters(reportparam);
            //}
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource(ReportName, reportdt));
        }

        public DataTable GetReportData()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                string paramvalue="";
                for (int i = 0; i < lstparams.Count(); i++)
                {
                    for (int j = 0; j < paramcol.Count(); j++)
                    {
                        if (paramcol.ContainsKey(lstparams[i].ToString()))
                        {
                            paramvalue = paramcol[lstparams[i].ToString()];
                            break;
                        }
                    }
                    SqlParameters.Add(new SqlParameter(lstparams[i].ToString(), paramvalue));
                    //reportparam.Add(new ReportParameter(lstparams[i].ToString(), paramvalue));
                }
                if (lstparams.Count() > 0)
                {
                    dt = DBManager.ExecuteDataTableWithParameter(SPName, CommandType.StoredProcedure, SqlParameters);
                }
                else
                {
                    dt = DBManager.ExecuteDataTable(SPName, CommandType.StoredProcedure);
                }
                return dt;
            }
            catch (Exception ex)
            { throw ex; }
        }

        public void MapQueryStringParams(string querystring)
        {
            DataSet reportdata = new DataSet();
            string[] querystr = querystring.Split('&');
            for (int i = 0; i < querystr.Count(); i++)
            {
                string[] param = querystr[i].Split('=');
                paramcol.Add(param[0].ToString().ToLower(), param[1].ToString().ToLower());
            }
        }

        public void MapReportConfig(int ReportId)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT ReportName,QueryType,Query,Params FROM Report_Config (nolock) WHERE IsActive=1 AND Fin_Id="+ CommonUtility.GetFYID().ToString() +" AND Company_Id="+ CommonUtility.GetCompanyID().ToString() + " AND Report_Id=" + ReportId.ToString() + "";
                //List<SqlParameter> SqlParameters = new List<SqlParameter>();
                //SqlParameters.Add(new SqlParameter("@Party_Id", Party_Id));
                dt = DBManager.ExecuteDataTable(sql, CommandType.Text);
                foreach (DataRow dr in dt.Rows)
                {
                    ReportName = Convert.ToString(dr[0]);
                    QueryType = Convert.ToString(dr[1]);
                    SPName = Convert.ToString(dr[2]);
                    string[] strparam = dr[3].ToString().Split(',');
                    for (int i = 0; i < strparam.Count(); i++)
                    {
                        lstparams.Add(strparam[0].ToString().ToLower());
                    }

                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        
    }
}