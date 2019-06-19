using iS3.ImportTools.Core.Interface;
using iS3.ImportTools.Core.Models;
using iS3.ImportTools.Core.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace iS3.ImportTools.DataAcqire
{
    public class DataAcquire_SQL : IDataAcquire
    {
        private Action<object> DataProcessHandler;
        private SqlConnection sqlConn;
        private DateTime datetimeStart = DateTime.Now;

        public void Start(Action<object> DataProcessHandler)
        {
            SqlConnectionStringBuilder sqlConnBuild = new SqlConnectionStringBuilder();
            sqlConnBuild.DataSource = "127.0.0.1";
            sqlConnBuild.InitialCatalog = "EmulatedDS";
            sqlConnBuild.UserID = "sa";
            sqlConnBuild.Password = "123456";

            sqlConn = new SqlConnection(sqlConnBuild.ConnectionString);
            sqlConn.Open();

            this.DataProcessHandler = DataProcessHandler;

            Thread threadRead = new Thread(RetrieveData);
            threadRead.Start();
        }

        private void RetrieveData()
        {
            while (sqlConn.State == ConnectionState.Open)
            {
                SqlCommand sqlCmd = new SqlCommand($"Select * from DataSource where DateTime > '{datetimeStart.ToString("yyyy-MM-dd HH:mm:ss.fff")}'", sqlConn as SqlConnection);
                SqlDataReader sqlDataReader = sqlCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(sqlDataReader);

                if (dt.Rows.Count > 0)
                {
                    datetimeStart = Convert.ToDateTime((dt.Rows[dt.Rows.Count - 1]["DateTime"]));
                    DataProcessHandler?.Invoke(dt);
                }


                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            sqlConn.Close();
            this.DataProcessHandler -= DataProcessHandler;
        }
    }
}
