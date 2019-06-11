using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.Core.Models
{
    /// <summary>
    /// attributes of perporty
    /// </summary>
    public class PropertyMeta:LangBase
    {
        public bool IsKey { get; set; }

        public string PropertyName { get; set; }
        public string DataType { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }


        public bool Nullable { get; set; }


        /// <summary>
        /// Regulation Expression for data Checking    
        /// </summary>
        public string RegularExp { get; set; }
        public PropertyMeta()
        {

        }
        public PropertyMeta(string propertyName, string dataType, string unit, string description, string langStr, bool IsKey = false, bool nullable = true, string regularExpression = null)
        {
            this.LangStr = langStr;
            this.PropertyName = propertyName;
            this.DataType = dataType;
            this.Unit = unit;
            this.Description = description;
            this.Nullable = nullable;
            this.RegularExp = regularExpression;
            this.IsKey = IsKey;
        }
    }
}
