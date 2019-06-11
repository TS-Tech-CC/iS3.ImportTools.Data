using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.Core.Tools
{
    public class DBHelper
    {
        public static SqlConnection DBConn(string cServer, string cDBName, string cUid, string cPwd)
        {
            SqlConnection sqlconn = new SqlConnection($"server={cServer};database={cDBName};uid={cUid};pwd={cPwd}");
            sqlconn.Open();
            return sqlconn;
        }

        public static void SQLExecuteNonQuery(string cSQL, SqlConnection cSqlConn)
        {
            SqlCommand sqlCommand = new SqlCommand(cSQL, cSqlConn);
            sqlCommand.ExecuteNonQuery();
            sqlCommand.Dispose();
        }

        public static object SQLExecuteScalar(string cSQL, SqlConnection cSqlconn)
        {
            SqlCommand sqlCommand = new SqlCommand(cSQL, cSqlconn);
            object Object = sqlCommand.ExecuteScalar();
            sqlCommand.Dispose();
            return Object;
        }

        public static DataTable SQLExecuteReader(string cSQL, SqlConnection cSqlConn)
        {
            SqlCommand sqlCommand = new SqlCommand(cSQL, cSqlConn);
            sqlCommand.CommandTimeout = 300;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlDataReader);
            sqlDataReader.Close();
            return dataTable;
        }

        public static void BulkCopy(string cDBTableName, DataTable cDT, SqlConnection cSqlConn)
        {
            SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(cSqlConn);
            sqlBulkCopy.DestinationTableName = cDBTableName;
            sqlBulkCopy.BatchSize = cDT.Columns.Count;
            sqlBulkCopy.WriteToServer(cDT);
            sqlBulkCopy.Close();
        }
    }
}
