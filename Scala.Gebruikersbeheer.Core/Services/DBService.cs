using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Scala.Gebruikersbeheer.Core.Services
{
    public class DBService
    {
        public static DataTable ExecuteSelect(string sqlInstruction)
        {
            DataSet dataSet = new DataSet();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlInstruction, Helper.GetConnectionString());
            try
            {
                sqlDataAdapter.Fill(dataSet);
            }
            catch (Exception error)
            {
                string errorMessage = error.Message;  // t.b.v. debugging
                return null;
            }
            return dataSet.Tables[0];
        }
        public static bool ExecuteCommand(string sqlInstruction)
        {
            SqlConnection sqlConnection = new SqlConnection(Helper.GetConnectionString());
            SqlCommand sqlCommand = new SqlCommand(sqlInstruction, sqlConnection);
            try
            {
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception error)
            {
                string errorMessage = error.Message;  // t.b.v. debugging
                return false;
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }
        public static string ExecuteScalar(string sqlScalarInstruction)
        {
            SqlConnection sqlConnection = new SqlConnection(Helper.GetConnectionString());
            SqlCommand sqlCommand = new SqlCommand(sqlScalarInstruction, sqlConnection);
            sqlConnection.Open();
            try
            {
                return sqlCommand.ExecuteScalar().ToString();
            }
            catch (Exception error)
            {
                string errorMessage = error.Message;  // t.b.v. debugging
                return null;
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }


    }
}
