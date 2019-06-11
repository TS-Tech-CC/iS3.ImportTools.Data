using iS3.ImportTools.Core.Interface;
using iS3.ImportTools.Core.Models;
using iS3.ImportTools.Core.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.DataWriting_SingleTable
{
    public class DataWriting_SingleTable : IDataWriting
    {

        private SqlConnection sqlConnDstn;        //数据存储的目标数据库
        private SqlConnection sqlConnProj;        //项目数据库，记录分表信息等


        public void Writing(DataSchemaWritingConfig config ,DataCarrier PendingDC)
        {
            if (PendingDC.DataContainer.Rows.Count != 0)
            {

                if (sqlConnDstn == null)
                {
                    sqlConnDstn = DBHelper.DBConn(config.IP, config.DBName, config.UserID, config.Pwd);
                }

                //判断表是否存在
                if (DBHelper.SQLExecuteReader($"select * from sysobjects where xtype='U' and Name = '{config.TableName}'", sqlConnDstn).Rows.Count == 0)
                {
                    string CreateTableSQL = PendingDC.DataDef.GetTableCreateSQL().Replace("TableName", config.TableName);
                    //创建表
                    DBHelper.SQLExecuteNonQuery(CreateTableSQL, sqlConnDstn);

                    ////往TEI中记录分表信息
                    //DBHelper.SQLExecuteNonQuery($@"INSERT INTO [{Projectdsi.DBName}].[dbo].[Project_TabExtendInfo]
                    //                                                            (   
                    //                                                                [TDBID],
                    //                                                                [BaseTabName],
                    //                                                                [ExtTabName],
                    //                                                                [CountStart],
                    //                                                                [CountEnd],
                    //                                                                [TimeStart],
                    //                                                                [TimeEnd]
                    //                                                            )
                    //                                                            VALUES
                    //                                                            (   
                    //                                                                {dac.TDBID},
                    //                                                                '{dac.TDBTabName}',
                    //                                                                '',
                    //                                                                NULL,
                    //                                                                NULL,
                    //                                                                NULL,
                    //                                                                NULL
                    //                                                            )", sqlProject);

                }


                DBHelper.BulkCopy(config.TableName, PendingDC.DataContainer, sqlConnDstn);
            }
        }
    }
}
