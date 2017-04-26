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
        StringBuilder _property = null;
        string _classname = "";
        string _namespace = "";
        string _connection = "";
        public Form1()
        {
            InitializeComponent();
            InitializeData();
        }
        private void InitializeData()
        {
            txtConnectionString.Text = @"Data Source=DESKTOP-Q8N5MOD\sqlexpress;Initial Catalog=AdventureWorks2012;Persist Security Info=True;User ID=sa;Password=123456";
            txt_SaveFolder.Text = @"C:\Duy";
            btnGenerate.Enabled = false;
            dataGridView2.Visible = false;
            label_Parameter.Visible = false;
            label_ClassName.Visible = false;
            txt_ClassName.Visible = false;
        }

        private void WriteClass(List<EntityBase> lst)
        {

            //var path = System.IO.Directory.GetFiles("../../Template/").FirstOrDefault();
            var template = String.Join("\n", System.IO.File.ReadAllLines("../../Template/ClassTemplate.txt"));
            _namespace = GetType().Namespace;
            _property = new StringBuilder();
            _classname = CapitalizeFirstLetter(txt_ClassName.Text);
            bool isFirst = true;
            foreach (var item in lst)
            {
                if (!isFirst) _property.Append("\t\t");
                if ("True".Equals(item.IsKey))
                {
                    _property.Append("[Key]\n");
                }
                _property.Append("public "+Type.GetType(item.DataType).Name + ("True".Equals(item.AllowDBNull) && !"System.String".Equals(item.DataType) ? "? " : " "))
                            .Append(item.ColumnName)
                            .Append(" { get; set; }\n");
                isFirst = false;
            }
            var outstr = template.Replace("<Property>", _property.ToString())
                                .Replace("<NameSpace>", _namespace)
                                .Replace("<ClassName>", _classname);
            System.IO.File.WriteAllText(String.Format(@"{0}\{1}.cs", txt_SaveFolder.Text, _classname), outstr);

        }
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //Open a connection to the SQL Server Northwind database.
            SqlConnection cn = new SqlConnection();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cn.ConnectionString = _connection;
                cn.Open();
                var paramDic = new Dictionary<string, object>();
                var spName = dataGridView1.CurrentRow.Cells[0].Value;
                var schema = dataGridView1.CurrentRow.Cells[1].Value;
                var isValidParamInput = true;
                foreach (DataGridViewRow Datarow in dataGridView2.Rows)
                {
                    var xx = Datarow.Cells[2].Value.ToString().Trim();
                    if (!String.IsNullOrEmpty(Datarow.Cells[0].Value.ToString().Trim()) && !String.IsNullOrEmpty(Datarow.Cells[1].Value.ToString().Trim()) && !String.IsNullOrEmpty(Datarow.Cells[2].Value.ToString().Trim()))
                    {
                        var key = Datarow.Cells[0].Value.ToString();
                        var value = Datarow.Cells[2].Value.ToString();
                        paramDic.Add(key, value);
                    }
                    else
                    {
                        MessageBox.Show("All parameter fields are required.");
                        isValidParamInput = false;
                        break;
                    }

                }

                if (isValidParamInput)
                {
                    cmd = cn.CreateCommand();
                    var sql = String.Format("exec {0}.{1} {2}",schema, spName, String.Join(", ", paramDic.Select(t => t.Key).ToArray()));

                    var rs = DBExtension.DynamicDataFromSql(cmd, sql, paramDic);
                    List<EntityBase> listProp = new List<EntityBase>();
                    var s = "";
                    if (rs != null)
                    {
                        foreach (DataRow myField in rs.Rows)
                        {
                            var dataRow = new ExpandoObject() as IDictionary<string, object>;
                            foreach (DataColumn myProperty in rs.Columns)
                            {
                                var column = myProperty.ColumnName.Trim();
                                dataRow.Add(myProperty.ColumnName.Trim(), myField[myProperty].ToString());
                                s += myProperty.ColumnName.Trim() + " : " + myField[myProperty].ToString() + "\n";
                            }
                            listProp.Add(new MapperConfiguration(cfg => { }).CreateMapper().Map<EntityBase>(dataRow));
                            s += "==========================================\n";
                        }
                        System.IO.File.WriteAllText(@"C:\Duy\raw.txt", s);
                        WriteClass(listProp);
                        MessageBox.Show("Class is generated.");
                    }
                    else
                    {
                        MessageBox.Show("No schema found.");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong.\n" + ex.Message);
            }
            finally
            {
                //Always close the DataReader and connection.
                cn.Close();
            }
        }

        private void BindGrid()
        {
            using (SqlConnection con = new SqlConnection(_connection))
            {
                con.Open();
                var sql = "SELECT pr.name as 'Stored Procedure', s.name as 'Schema' FROM sys.procedures pr"
                            + " INNER JOIN sys.schemas s ON pr.schema_id = s.schema_id";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }
        }
        private void BindParametterGrid(string spName)
        {
            using (SqlConnection con = new SqlConnection(txtConnectionString.Text))
            {
                con.Open();
                var sql = "select parameters.name as 'Paramater', sys.types.name as 'DataType', '' as 'Value' from sys.parameters"
                        + " inner join sys.procedures on parameters.object_id = procedures.object_id"
                        + " inner join sys.types on parameters.system_type_id = types.system_type_id"
                        + " AND parameters.user_type_id = types.user_type_id where procedures.name = '{0}'";
                using (SqlCommand cmd = new SqlCommand(String.Format(sql, spName), con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dataGridView2.DataSource = dt;
                        }
                    }
                }
                dataGridView2.Columns[0].Width = 300;
                dataGridView2.Columns[1].Width = 300;
                dataGridView2.Columns[2].Width = 230;
                txt_ClassName.Text = spName;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtConnectionString.Text))
            {
                MessageBox.Show("Connection string is required.");
            }
            else
            {
                _connection = txtConnectionString.Text;
            }
            BindGrid();
            dataGridView1.Columns[0].Width = 650;
            dataGridView1.Columns[1].Width = 180;

            //auto populate first store procedure parameters
            DataGridViewCellEventArgs temp = new DataGridViewCellEventArgs(0, 0);
            var text = dataGridView1.Rows[0].Cells[0].Value.ToString();
            BindParametterGrid(text);
            ShowParamaterGrid();
            btnGenerate.Enabled = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var sp_Name = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txt_ClassName.Text = sp_Name;
                _classname = sp_Name;
                BindParametterGrid(sp_Name);
                ShowParamaterGrid();
            }
        }
        private void ShowParamaterGrid()
        {
            dataGridView2.Visible = true;
            label_Parameter.Visible = true;
            label_ClassName.Visible = true;
            txt_ClassName.Visible = true;
        }

        private void btn_Browse_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txt_SaveFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        public string CapitalizeFirstLetter(string s)
        {
            if (String.IsNullOrEmpty(s)) return s;
            if (s.Length == 1) return s.ToUpper();
            return s.Remove(1).ToUpper() + s.Substring(1);
        }
    }
}
