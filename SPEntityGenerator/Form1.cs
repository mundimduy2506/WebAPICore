/*
 * Author: DuyTran
 * This tool is used to generate class readng from Stored Procedure Resultset
 * April 25 2017
 * Status: Incomplete
 * */
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPEntityGenerator
{
    public partial class Form1 : Form
    {
        private string[] RequiredProperties = { "ColumnName", "DataType", "IsKey", "AllowDBNull" };
        private Dictionary<string, string> dataTypes = new Dictionary<string, string>();
        StringBuilder _property = new StringBuilder();
        string _classname = "";
        string _namespace = "";
        public Form1()
        {
            InitializeComponent();
            InitializeData();
        }
        private void InitializeData()
        {
            dataTypes.Add("System.Int32", "int");
            dataTypes.Add("System.String", "string");
            dataTypes.Add("System.DateTime", "DateTime");
        }

        private void WriteClass(List<EntityBase> lst)
        {

            //var path = System.IO.Directory.GetFiles("../../Template/").FirstOrDefault();
            var template = String.Join("\n", System.IO.File.ReadAllLines("../../Template/ClassTemplate.txt"));
            _namespace = GetType().Namespace;
            _classname = "TempClassName";
            foreach (var item in lst)
            {
                if ("True".Equals(item.IsKey))
                {
                    _property.Append("[Key]\n");
                }
                _property.Append("\t\t");
                _property.Append(dataTypes.First(t => t.Key == item.DataType).Value + ("True".Equals(item.AllowDBNull) ? "? " : " "))
                            .Append(item.ColumnName)
                            .Append(" { get; set; }\n");
            }
            var outstr = template.Replace("<Property>", _property.ToString())
                                .Replace("<NameSpace>", _namespace)
                                .Replace("<ClassName>", _classname);
            System.IO.File.WriteAllText(@"C:\Duy\result.txt", outstr);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            //Open a connection to the SQL Server Northwind database.
            try
            {
                cn.ConnectionString = "Data Source=DESKTOP-Q8N5MOD\\sqlexpress;Initial Catalog=WebAPI;Persist Security Info=True;User ID=sa;Password=123456";
                cn.Open();

                //Retrieve records from the Employees table into a DataReader.
                cmd = cn.CreateCommand();
                var rs = DBExtension.DynamicDataFromSql(cmd, "exec webapi_GetCustomerById @Id",
                                    new Dictionary<string, object>() { { "@Id", "2" } });

                //Retrieve column schema into a DataTable.
                List<EntityBase> listProp = new List<EntityBase>();
                //For each field in the table...
                foreach (DataRow myField in rs.Rows)
                {
                    //For each property of the field...
                    var dataRow = new ExpandoObject() as IDictionary<string, object>;
                    foreach (DataColumn myProperty in rs.Columns)
                    {
                        var column = myProperty.ColumnName.Trim();
                        if (RequiredProperties.Contains(column))
                        {
                            dataRow.Add(myProperty.ColumnName.Trim(), myField[myProperty].ToString());

                        }
                    }
                    listProp.Add(new MapperConfiguration(cfg => { }).CreateMapper().Map<EntityBase>(dataRow));
                }
                WriteClass(listProp);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                //Always close the DataReader and connection.
                cn.Close();
            }
        }
    }
}
