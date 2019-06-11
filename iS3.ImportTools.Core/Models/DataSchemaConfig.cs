using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.Core.Models
{
    public class DataSchemaConfig
    {
        public String DataAcquireDllName { get; set; }
        public String DataFormatConverterDllName { get; set; }
        public String DataPropertyMappingDllName { get; set; }
        public String DataVerificationDllName { get; set; }
        public String DataWritingDllName { get; set; }


        public DataSchemaWritingConfig WritingConfig { get; set; }

    }


    public class DataSchemaWritingConfig
    {
        public string IP { get; set; }
        public string DBName { get; set; }
        public string UserID { get; set; }
        public string Pwd { get; set; }
        public string TableName { get; set; }


        /// <summary>
        /// 以时间分表参数，表示每张表的记录时间总长，单位秒
        /// </summary>
        public int TimeSpan { get; set; }
        /// <summary>
        /// 以时间分表参数，表示此对象中作为时间依据的字段名称
        /// </summary>
        public string TimeFieldName { get; set; }



        /// <summary>
        /// 以个数分表参数，表示每张表的总记录数，单位个
        /// </summary>
        public int NumberCount { get; set; }
    }
}
