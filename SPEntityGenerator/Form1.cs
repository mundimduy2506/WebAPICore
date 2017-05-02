/*
 * This tool is used to generate class reading from Stored Procedure Resultset
 * April 25 2017
 * Status: Incomplete
 * */
using AutoMapper;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Editing;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Microsoft.CodeAnalysis.Options;
using Microsoft.CodeAnalysis.CSharp.Formatting;

namespace SPEntityGenerator
{
    public partial class Form1 : Form
    {
        StringBuilder _property = null;
        Dictionary<string, string> datatypes = new Dictionary<string, string>();
        Dictionary<string, SpecialType> specialDataType = new Dictionary<string, SpecialType>();
        Dictionary<string, SyntaxKind> syntaxKinds = new Dictionary<string, SyntaxKind>();
        string _classname = "";
        string _namespace = "";
        string _connection = "";

        public Form1()
        {
            InitializeComponent();
            InitializeData();
            InitializeCSharpDataType();
        }

        private void InitializeCSharpDataType()
        {
            datatypes.Add("System.Boolean", "bool");
            datatypes.Add("System.Byte", "byte");
            datatypes.Add("System.SByte", "sbyte");
            datatypes.Add("System.Char", "char");
            datatypes.Add("System.Decimal", "decimal");
            datatypes.Add("System.Double", "double");
            datatypes.Add("System.Single", "float");
            datatypes.Add("System.Int32", "int");
            datatypes.Add("System.UInt32", "unit");
            datatypes.Add("System.Int64", "long");
            datatypes.Add("System.UInt64", "ulong");
            datatypes.Add("System.Object", "object");
            datatypes.Add("System.Int16", "short");
            datatypes.Add("System.UInt16", "ushort");
            datatypes.Add("System.String", "string");
            datatypes.Add("System.Void", "void");

            //initialize Roslyn SpecialType
            specialDataType.Add("System.Boolean", SpecialType.System_Boolean);
            specialDataType.Add("System.Byte", SpecialType.System_Byte);
            specialDataType.Add("System.SByte", SpecialType.System_SByte);
            specialDataType.Add("System.Char", SpecialType.System_Char);
            specialDataType.Add("System.Decimal", SpecialType.System_Decimal);
            specialDataType.Add("System.Double", SpecialType.System_Double);
            specialDataType.Add("System.Single", SpecialType.System_Single);
            specialDataType.Add("System.Int32", SpecialType.System_Int32);
            specialDataType.Add("System.UInt32", SpecialType.System_UInt32);
            specialDataType.Add("System.Int64", SpecialType.System_Int64);
            specialDataType.Add("System.UInt64", SpecialType.System_UInt64);
            specialDataType.Add("System.Object", SpecialType.System_Object);
            specialDataType.Add("System.Int16", SpecialType.System_Int16);
            specialDataType.Add("System.UInt16", SpecialType.System_UInt16);
            specialDataType.Add("System.String", SpecialType.System_String);
            specialDataType.Add("System.Void", SpecialType.System_Void);


            //initialize Roslyn SyntaxKind
            syntaxKinds.Add("System.Boolean", SyntaxKind.BoolKeyword);
            syntaxKinds.Add("System.Byte", SyntaxKind.ByteKeyword);
            syntaxKinds.Add("System.SByte", SyntaxKind.SByteKeyword);
            syntaxKinds.Add("System.Char", SyntaxKind.CharKeyword);
            syntaxKinds.Add("System.Decimal", SyntaxKind.DecimalKeyword);
            syntaxKinds.Add("System.Double", SyntaxKind.DoubleKeyword);
            syntaxKinds.Add("System.Single", SyntaxKind.FloatKeyword);
            syntaxKinds.Add("System.Int32", SyntaxKind.IntKeyword);
            syntaxKinds.Add("System.UInt32", SyntaxKind.UIntKeyword);
            syntaxKinds.Add("System.Int64", SyntaxKind.LongKeyword);
            syntaxKinds.Add("System.UInt64", SyntaxKind.ULongKeyword);
            syntaxKinds.Add("System.Object", SyntaxKind.ObjectKeyword);
            syntaxKinds.Add("System.Int16", SyntaxKind.ShortKeyword);
            syntaxKinds.Add("System.UInt16", SyntaxKind.UShortKeyword);
            syntaxKinds.Add("System.String", SyntaxKind.StringKeyword);
            syntaxKinds.Add("System.Void", SyntaxKind.VoidKeyword);
        }

        private void InitializeData()
        {
            txtConnectionString.Text = @"Data Source=LT-00005495\SQLEXPRESS;Initial Catalog=WebAPI;Persist Security Info=True;User ID=sa;Password=Abcde12345-";
            txt_SaveFolder.Text = @"E:\Duy\aaa";
            HideParamaterGrid();
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                SqlCommand cmd = new SqlCommand();
                cn.ConnectionString = _connection;
                cn.Open();
                var paramDic = new Dictionary<string, object>();
                var spName = dataGridView1.CurrentRow.Cells[0].Value;
                var schema = dataGridView1.CurrentRow.Cells[1].Value;
                var isValidInput = true;
                if (String.IsNullOrEmpty(txt_ClassName.Text.Trim()))
                {
                    MessageBox.Show("Class name are required.");
                    isValidInput = false;
                }
                else
                {
                    _classname = CapitalizeFirstLetter(txt_ClassName.Text);
                    foreach (DataGridViewRow Datarow in dataGridView2.Rows)
                    {
                        if (!String.IsNullOrEmpty(Datarow.Cells[0].Value.ToString().Trim())
                            && !String.IsNullOrEmpty(Datarow.Cells[1].Value.ToString().Trim())
                            && !String.IsNullOrEmpty(Datarow.Cells[2].Value.ToString().Trim()))
                        {
                            var key = Datarow.Cells[0].Value.ToString();
                            var value = Datarow.Cells[2].Value.ToString();
                            paramDic.Add(key, value);
                        }
                        else
                        {
                            MessageBox.Show("All parameter fields and class name are required.");
                            isValidInput = false;
                            break;
                        }

                    }
                }

                if (isValidInput)
                {
                    cmd = cn.CreateCommand();
                    var sql = String.Format("exec {0}.{1} {2}", schema, spName, String.Join(", ", paramDic.Select(t => t.Key).ToArray()));

                    var rs = DBExtension.DynamicDataFromSql(cmd, sql, paramDic);
                    List<EntityBase> listProp = new List<EntityBase>();
                    if (rs != null)
                    {
                        foreach (DataRow myField in rs.Rows)
                        {
                            var dataRow = new ExpandoObject() as IDictionary<string, object>;
                            foreach (DataColumn myProperty in rs.Columns)
                            {
                                var column = myProperty.ColumnName.Trim();
                                dataRow.Add(myProperty.ColumnName.Trim(), myField[myProperty].ToString());
                            }
                            listProp.Add(new MapperConfiguration(cfg => { }).CreateMapper().Map<EntityBase>(dataRow));
                        }
                        //PrepareContent(listProp);
                        //GenerateClass(listProp);
                        GenerateRoslynClassNew(listProp);
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
                cn.Close();
            }
        }


        private void BtnLoad_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtConnectionString.Text))
            {
                MessageBox.Show("Connection string is required.");
            }
            else
            {
                _connection = txtConnectionString.Text;
            }
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                BindStoredProcedureGrid();
                dataGridView1.Columns[0].Width = 650;
                dataGridView1.Columns[1].Width = 180;

                //autopopulate first store procedure parameters
                if (dataGridView1.Rows.Count > 0)
                {
                    DataGridViewCellEventArgs temp = new DataGridViewCellEventArgs(0, 0);
                    var text = dataGridView1.Rows[0].Cells[0].Value.ToString();
                    BindParametterGrid(text);
                    ShowParamaterGrid();
                    btnGenerate.Enabled = true;
                }
                else
                {
                    MessageBox.Show("No stored procedure found.");
                    HideParamaterGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong. \n" + ex.Message);
                HideParamaterGrid();
                dataGridView1.DataSource = null;
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var sp_Name = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                _classname = CapitalizeFirstLetter(sp_Name);
                BindParametterGrid(sp_Name);
                ShowParamaterGrid();
            }
        }

        private void Btn_Browse_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txt_SaveFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        #region Bind DataGrid
        /// <summary>
        /// Fill stored procedures into grid
        /// </summary>
        private void BindStoredProcedureGrid()
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
        /// <summary>
        /// Fill parameters of specific stored procedure into grid
        /// </summary>
        /// <param name="spName"></param>
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
                txt_ClassName.Text = CapitalizeFirstLetter(spName);
            }
        }
        #endregion

        #region Generate source with Roslyn: new version
        private void GenerateRoslynClassNew(List<EntityBase> listProp)
        {
            CompilationUnitSyntax cu = SF.CompilationUnit();
            cu = SF.CompilationUnit()
            .AddUsings(SF.UsingDirective(SF.IdentifierName("System")))
            .AddUsings(SF.UsingDirective(SF.IdentifierName("System.Generic")));

            NamespaceDeclarationSyntax ns = SF.NamespaceDeclaration(SF.IdentifierName("MyNamespace"));
            ClassDeclarationSyntax c = SF.ClassDeclaration(_classname)
            .AddModifiers(SF.Token(SyntaxKind.PublicKeyword))
            .AddModifiers(SF.Token(SyntaxKind.PartialKeyword))
    ;
            // Add a property
            PropertyDeclarationSyntax @property;
            foreach (var item in listProp)
            {
                @property =GetNullableCheck(item).AddModifiers(SF.Token(SyntaxKind.PublicKeyword));
                // Add a getter
                @property = @property.AddAccessorListAccessors(
                        SF.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                        .WithSemicolonToken(SF.Token(SyntaxKind.SemicolonToken)
                        ));
                // Add a setter
                @property = @property.AddAccessorListAccessors(
                        SF.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                        //.AddModifiers(SF.Token(SyntaxKind.PrivateKeyword))
                        .WithSemicolonToken(SF.Token(SyntaxKind.SemicolonToken)
                        ));
                // Add the property to the class
                c = c.AddMembers(@property);
            }
            ns = ns.AddMembers(c);
            cu = cu.AddMembers(ns);
            AdhocWorkspace cw = new AdhocWorkspace();
            OptionSet options = cw.Options;
            //options = options.WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInProperties, true);
            //options = options.WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInTypes, true);
            SyntaxNode formattedNode = Formatter.Format(cu, cw, options);
            Directory.CreateDirectory(txt_SaveFolder.Text);
            var filePath = String.Format(@"{0}\{1}.cs", txt_SaveFolder.Text, _classname);
            File.WriteAllText(filePath, formattedNode.ToFullString());
        }

        private PropertyDeclarationSyntax GetNullableCheck(EntityBase item)
        {
            var nullable = "True".Equals(item.AllowDBNull);
            var type = Type.GetType(item.DataType);

            SyntaxKind _syntaxKind = ToSyntaxKind(type);

            if (_syntaxKind == SyntaxKind.None)
            {
                if (nullable && type.IsValueType)
                {
                    return SF.PropertyDeclaration(
                    SF.NullableType(SF.IdentifierName(type.Name)), SF.Identifier(item.ColumnName));
                }
                else
                {
                    return SF.PropertyDeclaration(
                    SF.IdentifierName(type.Name), SF.Identifier(item.ColumnName));
                }
            }
            else
            {
                if (nullable && type.IsValueType)
                {
                    return SF.PropertyDeclaration(
                    SF.NullableType(SF.PredefinedType(SF.Token(_syntaxKind))), SF.Identifier(item.ColumnName)
                    );
                }
                else
                {
                    return SF.PropertyDeclaration(
                    SF.PredefinedType(SF.Token(_syntaxKind)), SF.Identifier(item.ColumnName));
                }
            }

        }


        private SyntaxKind ToSyntaxKind(Type type)
        {
            if (specialDataType.ContainsKey(type.FullName))
            {
                return syntaxKinds.FirstOrDefault(t => type.FullName.Equals(t.Key)).Value;
            }
            return SyntaxKind.None;
        }
        #endregion

        #region Generate source code with Roslyn: Old version
        /// <summary>
        /// Generate code utilize Roslyn 
        /// </summary>
        /// <param name="listProp"></param>
        private void GenerateClass(List<EntityBase> listProp)
        {
            var members = new List<SyntaxNode>();
            var constructorParam = new List<SyntaxNode>();
            var constructorBody = new List<SyntaxNode>();

            // Get a workspace
            var workspace = new AdhocWorkspace();
            // Get the SyntaxGenerator for the specified language
            var generator = SyntaxGenerator.GetGenerator(workspace, LanguageNames.CSharp);
            // Create using/Imports directives
            var usingDirectives = generator.NamespaceImportDeclaration("System");

            // Generate backing fields
            foreach (var item in listProp)
            {
                var backingField = LowerCaseFirstLetter(item.ColumnName);
                var tempfile = generator.FieldDeclaration(backingField,
                  ToTypeExpression(Type.GetType(item.DataType), "True".Equals(item.AllowDBNull), generator),
                  Accessibility.Private);
                members.Add(tempfile);
            }

            // Generate properties with explicit get/set
            foreach (var item in listProp)
            {
                var backingField = LowerCaseFirstLetter(item.ColumnName);
                var tempProperty = generator.PropertyDeclaration(item.ColumnName,
                    ToTypeExpression(Type.GetType(item.DataType), "True".Equals(item.AllowDBNull), generator),
                    Accessibility.Public,
                    getAccessorStatements: new SyntaxNode[] { generator.ReturnStatement(generator.IdentifierName(backingField)) },
                    setAccessorStatements: new SyntaxNode[] { generator.AssignmentStatement(generator.IdentifierName(backingField),
                generator.IdentifierName("value")) });
                members.Add(tempProperty);

                //generate constructor parameters
                constructorParam.Add(
                    generator.ParameterDeclaration(item.ColumnName,
                    ToTypeExpression(Type.GetType(item.DataType), "True".Equals(item.AllowDBNull), generator)));
                //generate constructor body
                constructorBody.Add(
                    generator.AssignmentStatement(generator.IdentifierName(backingField),
                    generator.IdentifierName(item.ColumnName)));
            }

            // Generate the class's default constructor
            var defaultConstructor = generator.ConstructorDeclaration(_classname, null, Accessibility.Public, statements: null);
            members.Add(defaultConstructor);

            // Generate the class' parameterized constructor
            var constructor = generator.ConstructorDeclaration(_classname, constructorParam, Accessibility.Public, statements: constructorBody);
            members.Add(constructor);

            // Generate the class
            var classDefinition = generator.ClassDeclaration(
              _classname, typeParameters: null,
              accessibility: Accessibility.Public,
              modifiers: DeclarationModifiers.Partial,
              baseType: null, //change baseNote to null if you want to remove inherance from Base class
              members: members);

            // Declare a namespace
            var namespaceDeclaration = generator.NamespaceDeclaration("MyNameSpace", classDefinition);

            // Get a CompilationUnit (code file) for the generated code
            var newNode = generator.CompilationUnit(usingDirectives, namespaceDeclaration);//.NormalizeWhitespace();

            Directory.CreateDirectory(txt_SaveFolder.Text);
            var filePath = String.Format(@"{0}\{1}.cs", txt_SaveFolder.Text, _classname);
            var outStr = newNode.ToFullString();
            File.WriteAllText(filePath, outStr);
            /*
            Task.Run(async () =>
            {
                await WriteFileToFolder(outStr, filePath);
            })
            .GetAwaiter()
            .GetResult();
            */
        }


        /// <summary>
        /// Cast CLR Data Type to Roslyn SpecialType
        /// </summary>
        /// <param name="type"></param>
        /// <param name="nullable"></param>
        /// <param name="generator"></param>
        /// <returns></returns>
        private SyntaxNode ToTypeExpression(Type type, bool nullable, SyntaxGenerator generator)
        {
            SyntaxNode baseType;
            SyntaxNode propType;

            SpecialType specialType = ToSpecialType(type);

            if (specialType == SpecialType.None)
            {
                baseType = generator.IdentifierName(type.Name);
            }
            else
            {
                baseType = generator.TypeExpression(specialType);
            }

            if (nullable && type.IsValueType)
            {
                propType = generator.NullableTypeExpression(baseType);
            }
            else
            {
                propType = baseType;
            }

            return propType;

        }

        private SpecialType ToSpecialType(Type type)
        {
            if (specialDataType.ContainsKey(type.FullName))
            {
                return specialDataType.FirstOrDefault(t => type.FullName.Equals(t.Key)).Value;
            }
            return SpecialType.None;
        }
        #endregion

        #region Write file using template
        /// <summary>
        /// Prepare all class content prior writing to file
        /// </summary>
        /// <param name="lst"></param>
        private void PrepareContent(List<EntityBase> lst)
        {
            var template = String.Join("\n", File.ReadAllLines("../../Template/ClassTemplate.txt"));
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
                var type = Type.GetType(item.DataType).FullName;
                var dtype = String.IsNullOrEmpty(datatypes.FirstOrDefault(t => type.Equals(t.Key)).Value)
                            ? Type.GetType(item.DataType).Name
                            : datatypes.FirstOrDefault(t => type.Equals(t.Key)).Value;
                _property.Append("public " + dtype + ("True".Equals(item.AllowDBNull) && Type.GetType(item.DataType).IsValueType ? "? " : " "))
                            .Append(CapitalizeFirstLetter(item.ColumnName))
                            .Append(" { get; set; }\n");
                isFirst = false;
            }
            var outstr = template.Replace("<Property>", _property.ToString())
                                .Replace("<NameSpace>", _namespace)
                                .Replace("<ClassName>", _classname);
            Directory.CreateDirectory(txt_SaveFolder.Text);
            var filePath = String.Format(@"{0}\{1}_normal.cs", txt_SaveFolder.Text, _classname);
            //File.WriteAllText(filePath, outstr);


            Task.Run(async () =>
            {
                await WriteFileToFolder(outstr, filePath);
            })
            .GetAwaiter()
            .GetResult();

        }

        /// <summary>
        /// Use Roslyn to format the source code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        static async Task WriteFileToFolder(string code, string @filePath)
        {
            // Parse the code into a SyntaxTree.
            var tree = CSharpSyntaxTree.ParseText(code);

            // Get the root CompilationUnitSyntax.
            var root = await tree.GetRootAsync().ConfigureAwait(false) as CompilationUnitSyntax;
            var formattedResult = Formatter.Format(root.NormalizeWhitespace(), MSBuildWorkspace.Create());

            // Write the new file.
            File.WriteAllText(filePath, formattedResult.ToFullString());
        }
        #endregion

        #region additional functions
        /// <summary>
        /// Capitalize the first letter of class name to match convention
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string CapitalizeFirstLetter(string s)
        {
            if (String.IsNullOrEmpty(s)) return s;
            if (s.Length == 1) return s.ToUpper();
            return s.Remove(1).ToUpper() + s.Substring(1);
        }


        /// <summary>
        /// Lowercase the first letter of class name to match convention
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string LowerCaseFirstLetter(string s)
        {
            if (String.IsNullOrEmpty(s)) return s;
            if (s.Length == 1) return "_" + s.ToLower();
            return "_" + s.Remove(1).ToLower() + s.Substring(1);
        }


        private void ShowParamaterGrid()
        {
            dataGridView2.Visible = true;
            label_Parameter.Visible = true;
            label_ClassName.Visible = true;
            txt_ClassName.Visible = true;
        }

        private void HideParamaterGrid()
        {
            dataGridView2.Visible = false;
            label_Parameter.Visible = false;
            label_ClassName.Visible = false;
            txt_ClassName.Visible = false;
            btnGenerate.Enabled = false;
        }
        #endregion
    }
}
