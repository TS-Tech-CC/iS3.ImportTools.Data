using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.Core.Models
{
    public class DataCarrier
    {
        public PmDGObjectDef DataDef { get; set; }
        public DataTable DataContainer { get; set; }


        public DataCarrier()
        {
            //在WPF界面显示时，DataTable的Name存储了数据源名称与当前时间，所以此处提前实例化DataContainer。
            DataContainer = new DataTable();
        }





        /// <summary>
        /// 通过已存在定义构造DataCarrier，用定义补全DataTable数据列名
        /// </summary>
        /// <param name="ObjDef"></param>
        public DataCarrier(PmDGObjectDef ObjDef)
        {
            DataDef = ObjDef;

            DataContainer = DataContainer == null ? new DataTable() : DataContainer;

            foreach (PropertyMeta itemPM in ObjDef.PropertyContainer)
            {
                DataContainer.Columns.Add(itemPM.PropertyName, typeof(string));                     //创建全是String的DataTable
                //DataContainer.Columns.Add(itemPM.PropertyName,itemPM.DataType);     //创建各列格式匹配的DataTable
            }

        }
    }
}
