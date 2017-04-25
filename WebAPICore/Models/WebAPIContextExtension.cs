using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;

namespace WebAPICore.Models
{
    public static class WebAPIContextExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Generic datatype</typeparam>
        /// <param name="dbContext">Database context</param>
        /// <param name="sql">sql query</param>
        /// <param name="Parameters">parameters</param>
        /// <returns>List of dynamic objects</returns>
        public static IEnumerable<T> DynamicDataFromSql<T>(this WebAPIContext dbContext,
                                                             string sql,
                                                             Dictionary<string, object> Parameters)
        {
            using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
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
                var schemaTable = myReader.GetColumnSchema();

                var rs = new List<T>();
                using (var dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var dataRow = new ExpandoObject() as IDictionary<string, object>;
                        for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                            dataRow.Add(dataReader.GetName(fieldCount), dataReader[fieldCount]);

                        rs.Add(new MapperConfiguration(cfg => { }).CreateMapper().Map<T>(dataRow));
                        
                    }
                }

                return rs;
            }
        }
    }
}
