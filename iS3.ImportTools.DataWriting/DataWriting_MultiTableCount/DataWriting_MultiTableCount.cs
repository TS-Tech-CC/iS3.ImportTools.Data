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

namespace DataWriting_MultiTableCount
{
    public class DataWriting_MultiTableCount : IDataWriting
    {
        private SqlConnection sqlConnDstn;        //数据存储的目标数据库
        private SqlConnection sqlConnProj;        //项目数据库，记录分表信息等

        private int totalWidth = 10;//表名称计数部分总长
        private int CountStart = -1;
        private int CountHave = -1;


        public void Writing(DataSchemaWritingConfig config, DataCarrier PendingDC)
        {
            if (PendingDC.DataContainer.Rows.Count != 0)
            {
                if (sqlConnDstn == null)
                {
                    sqlConnDstn = DBHelper.DBConn(config.IP, config.DBName, config.UserID, config.Pwd);
                }

                if (CountStart == -1 || CountHave == -1)
                {
                    DataTable dt = DBHelper.SQLExecuteReader($"select * from sys.tables where name like '{config.TableName}_%' and LEN(name) = {config.TableName.Length + 2 + 2* totalWidth}", sqlConnDstn);
                    if (dt.Rows.Count == 0)
                    {
                        CountStart = 1;
                        CountHave = 0;
                    }
                    else
                    {
                        string tableName = dt.Select("", "name desc")[0]["name"].ToString();
                        CountStart = Convert.ToInt32(tableName.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1]);
                        CountHave = (int)DBHelper.SQLExecuteScalar($"select Count(*) from {tableName}", sqlConnDstn);
                    }
                }


                foreach (DataTable dt in SplitDataTableByCount(PendingDC.DataContainer, config.NumberCount, CountStart, CountHave).Tables)
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


                //刷新CountStart、CountHave
                int xx = (CountStart + CountHave + PendingDC.DataContainer.Rows.Count);
                CountStart = ((xx - 1) / config.NumberCount) * config.NumberCount + 1;
                CountHave = (xx - 1) % config.NumberCount;

            }
        }


        private DataSet SplitDataTableByCount(DataTable originalTab,int numberCount, int pCountStart, int pCountHave)
        {

            DataSet ds = new DataSet();


            if (originalTab.Rows.Count <= numberCount - pCountHave)
            {
                //表未满，可以在原表中继续添加
                originalTab.TableName = pCountStart.ToString().PadLeft(totalWidth, '0') + "_" + (pCountStart + numberCount - 1).ToString().PadLeft(totalWidth, '0');
                ds.Tables.Add(originalTab);
            }
            else
            {
                //表快满，需建新表


                //先将现表填满
                int OneAdd = numberCount - pCountHave;
                DataTable dt = originalTab.Clone();
                dt.TableName = pCountStart.ToString().PadLeft(totalWidth, '0') + "_" + (pCountStart + numberCount - 1).ToString().PadLeft(totalWidth, '0');
                for (int i = 0; i < OneAdd; i++)
                {
                    dt.ImportRow(originalTab.Rows[i]);
                }
                ds.Tables.Add(dt);


                //将后续数据分表
                int x = 0;
                do
                {
                    DataTable dt2 = originalTab.Clone();
                    x++;
                    dt2.TableName = (pCountStart + numberCount * x).ToString().PadLeft(totalWidth, '0') + "_" + (pCountStart + numberCount * (x + 1) - 1).ToString().PadLeft(totalWidth, '0');

                    for (int i = (OneAdd + numberCount * (x - 1)); i < (OneAdd + numberCount * x); i++)
                    {
                        if (i <= originalTab.Rows.Count - 1)
                        {
                            dt2.ImportRow(originalTab.Rows[i]);
                        }
                    }
                    ds.Tables.Add(dt2);

                }
                while (originalTab.Rows.Count + pCountHave > (x + 1) * numberCount);
                //当源数据的条数 + CountHave     依然大于  已分表的总计条数时  继续
            }
            return ds;
        }


    }
}
