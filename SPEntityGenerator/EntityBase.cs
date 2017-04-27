using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEntityGenerator
{
    public class EntityBase
    {
        public string ColumnName { get; set; }
        public string ColumnOrdinal { get; set; }
        public string ColumnSize { get; set; }
        public string NumericPrecision { get; set; }
        public string IsUnique { get; set; }
        public string IsKey { get; set; }
        public string BaseTableName { get; set; }
        public string DataType { get; set; }
        public string AllowDBNull { get; set; }
        public string IsIdentity { get; set; }
        public string DataTypeName { get; set; }

    }
}
