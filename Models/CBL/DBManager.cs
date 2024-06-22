using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class DBManager
{
    private static string ConnStr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
    public static DataTable ExecuteDataTableWithParameter(string Query, CommandType commandType, List<SqlParameter> Parameter)
    {
        DataTable DT = new DataTable();
        using (SqlConnection sqlcon = new SqlConnection((ConnStr)))
        {
            using (SqlCommand Command = new SqlCommand(Query, sqlcon))
            {
                sqlcon.Open();
                Command.CommandType = commandType;
                foreach (SqlParameter Pr in Parameter)
                {
                    Command.Parameters.Add(Pr);
                }
                using (var Obj = Command.ExecuteReader())
                {
                    DT.Load(Obj);
                    sqlcon.Close();
                    return DT;
                }
            }
        }
    }

    

    public static DataTable ExecuteDataTable(string Query, CommandType commandType)
    {
        DataTable DT = new DataTable();
        using (SqlConnection sqlcon = new SqlConnection((ConnStr)))
        {
            using (SqlCommand Command = new SqlCommand(Query, sqlcon))
            {
                sqlcon.Open();
                Command.CommandType = commandType;
                using (var Obj = Command.ExecuteReader())
                {
                    DT.Load(Obj);
                    sqlcon.Close();
                    return DT;
                }
            }
        }
    }

    public static int ExecuteScalar(string Query, CommandType commandType, List<SqlParameter> Parameter)
    {
        using (SqlConnection sqlcon = new SqlConnection((ConnStr)))
        {
            using (SqlCommand Command = new SqlCommand(Query, sqlcon))
            {
                sqlcon.Open();
                Command.CommandType = commandType;
                foreach (SqlParameter Pr in Parameter)
                {
                    Command.Parameters.Add(Pr);
                }
                int Obj2 = Convert.ToInt32(Command.ExecuteScalar());
                sqlcon.Close();
                return Obj2;
            }
        }
    }
    public static DataSet ExecuteDataSetWithParameter(string Query, CommandType commandType, List<SqlParameter> Parameter)
    {
        using (SqlConnection Connection = new SqlConnection(ConnStr))
        {
            using (SqlCommand Command = new SqlCommand(Query, Connection))
            {
                Connection.Open();
                Command.CommandType = commandType;
                foreach (SqlParameter Pr in Parameter)
                {
                    Command.Parameters.Add(Pr);
                }
                SqlDataAdapter DataAdapter = new SqlDataAdapter(Command);
                DataSet Dataset = new DataSet();
                DataAdapter.Fill(Dataset);
                Connection.Close();
                return Dataset;
            }
        }
    }


    public static string ExecuteScalarSUB(string Query, CommandType commandType, List<SqlParameter> Parameter,string outputparameter)
    {
        using (SqlConnection sqlcon = new SqlConnection((ConnStr)))
        {
            using (SqlCommand Command = new SqlCommand(Query, sqlcon))
            {
                sqlcon.Open();
                Command.CommandType = commandType;
                foreach (SqlParameter Pr in Parameter)
                {
                    Command.Parameters.Add(Pr);
                }

                SqlParameter pr1 = new SqlParameter();
                pr1.ParameterName = outputparameter;
                pr1.DbType = DbType.String;
                pr1.Direction = ParameterDirection.Output;
                pr1.Size = -1;
                Command.Parameters.Add(pr1);

                int Obj2 = Convert.ToInt32(Command.ExecuteScalar());
                string outputvalue = Command.Parameters["@StationeryNumber"].Value.ToString();
                sqlcon.Close();
                return outputvalue;
            }
        }
    }



}

