using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEntityGenerator
{
    public static class DBExtension
    {
        public static DataTable DynamicDataFromSql(this SqlCommand cmd,
                                                             string sql,
                                                             Dictionary<string, object> Parameters)
        {
                cmd.CommandText = sql;
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                foreach (KeyValuePair<string, object> param in Parameters)
                {
                    DbParameter dbParameter = cmd.CreateParameter();
                    dbParameter.ParameterName = param.Key;
                    dbParameter.Value = param.Value;
                    cmd.Parameters.Add(dbParameter);
                }

                var myReader = cmd.ExecuteReader(CommandBehavior.KeyInfo);

                //Retrieve column schema into a DataTable.
                return myReader.GetSchemaTable();
        }
    }
}
