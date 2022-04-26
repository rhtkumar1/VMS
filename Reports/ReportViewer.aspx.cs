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
        string Reportquerystring;
        int ReportId;
        string ReportName;
        string SPName;
        string QueryType;
        int ReportType;
        protected void Page_Load(object sender, EventArgs e)
        {
            string querystring = "";
            if (!IsPostBack)
            {
                querystring = Request.QueryString.ToString();
                ReportId = Convert.ToInt32(Request.QueryString["ReportId"]);
            }
            if (ReportId > 0)
            {
                MapReportConfig(ReportId);
                switch(ReportType)
                {
                    case 0:
                        break;
                    case 1:
                        MapQueryStringParams(querystring);
                        break;
                    case 2:
                        Reportquerystring = Convert.ToString(Request.QueryString["Reportquerystring"]);
                        MapReportquerystring(Reportquerystring);
                        break;
                }
                
                ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;//ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Reports/Report/" + ReportName + ".rdlc");
                DataTable reportdt = GetReportData();
                ReportDataSource ReportDataSource = new ReportDataSource(ReportName, reportdt);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(ReportDataSource);//new Microsoft.Reporting.WebForms.ReportDataSource(ReportName, reportdt));
                ReportViewer1.LocalReport.Refresh();
                //ReportViewer1.RefreshReport(); // refresh report
            }
        }

        public DataTable GetReportData()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                string paramvalue="";
                string ParamName = "";
                for (int i = 0; i < lstparams.Count(); i++)
                {
                    ParamName = lstparams[i].ToString();
                    for (int j = 0; j < paramcol.Count(); j++)
                    {
                        if (paramcol.ContainsKey(lstparams[i].ToString()))
                        {
                            if (ParamName.Contains("date"))
                            {
                                if (!string.IsNullOrEmpty(paramcol[lstparams[i].ToString()]))
                                 paramvalue = CommonUtility.GetDateYYYYMMDD(paramcol[lstparams[i].ToString()]);
                            }
                            else
                            {
                                paramvalue = paramcol[lstparams[i].ToString()];
                            }
                            break;
                        }
                    }
                    SqlParameters.Add(new SqlParameter(ParamName, paramvalue));
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
        public void MapReportquerystring(string querystring)
        {
            DataSet reportdata = new DataSet();
            string[] querystr = querystring.Split(',');
            for (int i = 0; i < querystr.Count(); i++)
            {
                string[] param = querystr[i].Split(':');
                paramcol.Add(param[0].ToString().ToLower(), param[1].ToString().ToLower());
            }
        }

        public void MapReportConfig(int ReportId)
        {
            DataTable dt = new DataTable();
            try
            {
                //string sql = "SELECT ReportName,QueryType,Query,Params FROM Report_Config (nolock) WHERE IsActive=1 AND Report_Id=" + ReportId.ToString() + "";
                //List<SqlParameter> SqlParameters = new List<SqlParameter>();
                //SqlParameters.Add(new SqlParameter("@Party_Id", Party_Id));
                string sql = "Report_Config_GetData";
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Report_Id", ReportId));
                dt = DBManager.ExecuteDataTableWithParameter(sql, CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    ReportName = Convert.ToString(dr["ReportName"]);
                    QueryType = Convert.ToString(dr["QueryType"]);
                    SPName = Convert.ToString(dr["Query"]);
                    ReportType = Convert.ToInt32(dr["ReportType"]);
                    string[] strparam = dr["Params"].ToString().Split(',');
                    for (int i = 0; i < strparam.Count(); i++)
                    {
                        lstparams.Add(strparam[i].ToString().ToLower());
                    }

                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        
    }
}