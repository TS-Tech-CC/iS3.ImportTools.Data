using iS3.ImportTools.Core.Interface;
using iS3.ImportTools.Core.Models;
using iS3.ImportTools.Core.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataWriting_MultiTableTime
{
    public class DataWriting_MultiTableTime : IDataWriting
    {
        private SqlConnection sqlConnDstn;        //数据存储的目标数据库
        private SqlConnection sqlConnProj;        //项目数据库，记录分表信息等

        public void Writing(DataSchemaWritingConfig config, DataCarrier PendingDC)
        {

            if (PendingDC.DataContainer.Rows.Count != 0)
            {
                if (sqlConnDstn == null)
                {
                    sqlConnDstn = DBHelper.DBConn(config.IP, config.DBName, config.UserID, config.Pwd);
                }


                foreach (DataTable dt in SplitDataTableByTime(PendingDC.DataContainer, config.TimeSpan, config.TimeFieldName).Tables)
                {
                    string tableName = $"{config.TableName}_{dt.TableName}";
                    //没表  建表
                    if (DBHelper.SQLExecuteReader($"select * from sysobjects where xtype='U' and Name = '{tableName}'", sqlConnDstn).Rows.Count == 0)
                    {
                        string CreateTableSQL = PendingDC.DataDef.GetTableCreateSQL().Replace("TableName", tableName);
                        //新建表
                        DBHelper.SQLExecuteNonQuery(CreateTableSQL, sqlConnDstn);


                        ////把表名、扩展表名写入TEI
                        //DBHelper.SQLExecuteNonQuery($@"INSERT INTO [{Projectdsi.DBName}].[dbo].[Project_TabExtendInfo]
                        //                                                        (   
                        //                                                            [TDBID],
                        //                                                            [BaseTabName],
                        //                                                            [ExtTabName],
                        //                                                            [CountStart],
                        //                                                            [CountEnd],
                        //                                                            [TimeStart],
                        //                                                            [TimeEnd]
                        //                                                        )
                        //                                                        VALUES
                        //                                                        (   
                        //                                                            {dac.TDBID},
                        //                                                            '{dac.TDBTabName}',
                        //                                                            '{dt.TableName}',
                        //                                                            1,
                        //                                                            {dt.Rows.Count},
                        //                                                            '{dt.AsEnumerable().Select(x => Convert.ToDateTime(x["AcqTime"])).Min()}',
                        //                                                            '{dt.AsEnumerable().Select(x => Convert.ToDateTime(x["AcqTime"])).Max()}'
                        //                                                        )", sqlProject);
                    }
                    else //有表，更新TEI
                    {
                        //int countend = (int)DBHelper.SQLExecuteScalar($@"SELECT [CountEnd] FROM [{Projectdsi.DBName}].[dbo].[Project_TabExtendInfo] 
                        //                                                        where 
                        //                                                        BaseTabName = '{dac.TDBTabName}' and 
                        //                                                        ExtTabName = '{dt.TableName}'
                        //                                                        ", sqlProject);


                        ////update [TS_iS3_FS].[dbo].[Project_TabExtendInfo] set [CountEnd] = 6 where BaseTabName = 'FS1' and ExtTabName = '20190110193330000_20190110193400000'
                        //DBHelper.SQLExecuteNonQuery($@"Update [{Projectdsi.DBName}].[dbo].[Project_TabExtendInfo] set
                        //                                                        CountEnd = {countend + dt.Rows.Count},
                        //                                                        TimeEnd = '{dt.AsEnumerable().Select(x => Convert.ToDateTime(x["AcqTime"])).Max()}'

                        //                                                        where 
                        //                                                        BaseTabName = '{dac.TDBTabName}' and 
                        //                                                        ExtTabName = '{dt.TableName}'
                        //                                                        ", sqlProject);


                    }

                    DBHelper.BulkCopy(tableName, dt, sqlConnDstn);


                }
            }
        }


        private DataSet SplitDataTableByTime(DataTable originalTab, int TimeSpan, string TimeFieldName)
        {
            try
            {
                DateTime minimumTime = Convert.ToDateTime(originalTab.Select($"{TimeFieldName} is not null", TimeFieldName)[0][TimeFieldName]);
                int chazhi = (int)( minimumTime - Convert.ToDateTime("2010-01-01 00:00:00.000")).TotalSeconds;
                int yushu = chazhi % TimeSpan;
                string TimeStart = ( minimumTime - System.TimeSpan.FromSeconds(yushu)).ToString("yyyy-MM-dd HH:mm:ss.000");



                DataSet ds = new DataSet();
                int i = 0;
                do
                {
                    i++;
                    DateTime nTimeStart = Convert.ToDateTime(TimeStart) + System.TimeSpan.FromSeconds(TimeSpan * (i - 1));
                    DateTime nTimeEnd = Convert.ToDateTime(TimeStart) + System.TimeSpan.FromSeconds(TimeSpan * i);


                    DataRow[] drs = originalTab.Select($"{TimeFieldName} >= '{nTimeStart}' and {TimeFieldName} < '{nTimeEnd}'");

                    if (drs.Count() != 0)
                    {
                        DataTable dt = originalTab.Clone();
                        dt.TableName = nTimeStart.ToString("yyyyMMddHHmmss") + "_" + nTimeEnd.ToString("yyyyMMddHHmmss");

                        foreach (DataRow dr in drs)
                        {
                            dt.ImportRow(dr);
                        }

                        ds.Tables.Add(dt);
                    }



                }
                while (Convert.ToDateTime(originalTab.Rows[originalTab.Rows.Count - 1][TimeFieldName]) > Convert.ToDateTime(TimeStart) + System.TimeSpan.FromSeconds((TimeSpan * i) - 0.001));
                //当源数据最后一条的时间     依然大于    已分表的结束时间时  继续



                DataRow[] drsNULL = originalTab.Select($"{TimeFieldName} is null");
                if (drsNULL.Count() != 0)
                {
                    DataTable dt = originalTab.Clone();
                    dt.TableName = "TimeNull";

                    foreach (DataRow dr in drsNULL)
                    {
                        dt.ImportRow(dr);
                    }

                    ds.Tables.Add(dt);
                }







                return ds;
            }
            catch (Exception err)
            {
                throw new Exception("分表错误:  " + err.Message);
            }
        }

    }
}
