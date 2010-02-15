using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NIntegrate.Data;
using System.Configuration;
using NIntegrate.Web;
using NIntegrate.Data.SqlClient;
using System.Data;
using System.IO;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.Data.SqlClient;
using System.Data.Common;

namespace NIntegrate.CodeGenerator
{
    public partial class QueryCodeGenerator : System.Web.UI.Page
    {
        private DataView Query(QueryCriteria criteria)
        {
            var queryService = new QueryService(new CodeGeneratorQueryCommandFactory(tbConnStr.Text));
            return new DataView(queryService.Query(criteria));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            tbOutputDir.Text = Path.Combine(Server.MapPath("~/App_Data/"), "NIntegrateQueryClasses");
            tbOutputNamespace.Text = "NIntegrateQueryClasses";
            tbDefaultConnStrName.Text = "Default";
        }

        protected void btnConnect_Click(object sender, EventArgs e)
        {
            var sysObjs = new sysobjects();
            cblTables.DataSource = Query(sysObjs.Select(sysObjs.name).And(sysObjs.xtype == "U" && sysObjs.name != "sysdiagrams").SortBy(sysObjs.name, false));
            cblViews.DataSource = Query(sysObjs.Select(sysObjs.name).And(sysObjs.xtype == "V").SortBy(sysObjs.name, false));
            cblSprocs.DataSource = Query(sysObjs.Select(sysObjs.name).And(sysObjs.xtype == "P" && !sysObjs.name.StartsWith("dt_") && !sysObjs.name.Like("sp_%diagram%")).SortBy(sysObjs.name, false));

            DataBind();
        }

        protected void btnGen_Click(object sender, EventArgs e)
        {
            if (cbDeleteBeforeGen.Checked)
            {
                if (Directory.Exists(tbOutputDir.Text))
                    Directory.Delete(tbOutputDir.Text, true);
            }

            if (!Directory.Exists(tbOutputDir.Text))
                Directory.CreateDirectory(tbOutputDir.Text);

            foreach (ListItem item in cblTables.Items)
            {
                if (item.Selected)
                    new QueryTableCodeGenerator(
                        GetOutputFileName(item.Text, ddlOutputLangs.SelectedValue),
                        ddlOutputLangs.SelectedValue,
                        tbOutputNamespace.Text,
                        tbDefaultConnStrName.Text,
                        tbConnStr.Text,
                        item.Text,
                        false).Generate();
            }
            foreach (ListItem item in cblViews.Items)
            {
                if (item.Selected)
                    new QueryTableCodeGenerator(
                        GetOutputFileName(item.Text, ddlOutputLangs.SelectedValue),
                        ddlOutputLangs.SelectedValue,
                        tbOutputNamespace.Text,
                        tbDefaultConnStrName.Text,
                        tbConnStr.Text,
                        item.Text,
                        true).Generate();
            }
            foreach (ListItem item in cblSprocs.Items)
            {
                if (item.Selected)
                    new QuerySprocCodeGenerator(
                        GetOutputFileName(item.Text, ddlOutputLangs.SelectedValue),
                        ddlOutputLangs.SelectedValue,
                        tbOutputNamespace.Text,
                        tbDefaultConnStrName.Text,
                        tbConnStr.Text,
                        item.Text).Generate();
            }
        }

        private string GetOutputFileName(string name, string outputLang)
        {
            return Path.Combine(tbOutputDir.Text, name + (outputLang == "C#" ? ".cs" : ".vb"));
        }
    }

    #region Query Classes

    public class CodeGeneratorQueryCommandFactory : QueryCommandFactory
    {
        private readonly ConnectionStringSettings _connStr;

        public CodeGeneratorQueryCommandFactory(string connStr)
        {
            _connStr = new ConnectionStringSettings("Default", connStr);
        }

        protected override System.Configuration.ConnectionStringSettings GetConnectionString(string connectionStringName)
        {
            return _connStr;
        }
    }

    public class EmptyQueryTable : QueryTable
    {
        public EmptyQueryTable(string tableName, string connStrName)
            : base(tableName, connStrName, true)
        {
        }
    }

    public class EmptyQuerySproc : QuerySproc
    {
        public EmptyQuerySproc(string sprocName, string connStrName)
            : base(sprocName, connStrName)
        {
        }
    }

    public class sysobjects : QueryTable
    {
        public sysobjects()
            : base("sysobjects", "Default", true)
        {
        }

        public StringColumn name = new StringColumn("name", true);
        public StringColumn xtype = new StringColumn("xtype", false);
    }

    #endregion

    #region Code Generation Classes

    public abstract class CodeGeneratorBase
    {
        protected readonly CodeGeneratorQueryCommandFactory _cmdFac;
        protected readonly QueryService _queryService;

        public string OutputFileName { get; private set; }
        public string OutputLanguage { get; private set; }
        public string OutputNamespace { get; private set; }
        public string OutputDefaultConnectionStringName { get; private set; }
        public string ConnectionString { get; private set; }
        public string Name { get; private set; }

        public string NameWithoutSpaces { get { return Name.Replace(" ", string.Empty); } }

        public CodeGeneratorBase(string outputFileName, string outputLang, string outputNamespace, string outputDefaultConnStr, string connStr, string name)
        {
            OutputFileName = outputFileName;
            OutputLanguage = outputLang;
            OutputNamespace = outputNamespace;
            OutputDefaultConnectionStringName = outputDefaultConnStr;
            ConnectionString = connStr;
            Name = name;

            _cmdFac = new CodeGeneratorQueryCommandFactory(ConnectionString);
            _queryService = new QueryService(_cmdFac);
        }

        public void Generate()
        {
            CodeCompileUnit unit = new CodeCompileUnit();

            CodeNamespace ns = new CodeNamespace(string.IsNullOrEmpty(OutputNamespace) ? "NIntegrateQueryClasses" : OutputNamespace);
            unit.Namespaces.Add(ns);

            CodeTypeDeclaration genClass = new CodeTypeDeclaration(NameWithoutSpaces);
            ns.Types.Add(genClass);
            genClass.Attributes = MemberAttributes.Public;
            genClass.IsClass = true;
            genClass.IsPartial = true;

            DoGenerate(genClass);

            CodeDomProvider provider;
            if (OutputLanguage == "C#")
                provider = new Microsoft.CSharp.CSharpCodeProvider();
            else
                provider = new Microsoft.VisualBasic.VBCodeProvider();

            using (StreamWriter sw = new StreamWriter(OutputFileName))
            {
                IndentedTextWriter indentedWriter = new IndentedTextWriter(sw, "  ");
                var options = new CodeGeneratorOptions
                {
                    BlankLinesBetweenMembers = true,
                    BracingStyle = "C"
                };
                provider.GenerateCodeFromCompileUnit(unit, indentedWriter, options);
                sw.Close();
            }
        }

        protected abstract void DoGenerate(CodeTypeDeclaration genClass);
    }

    public class QueryTableCodeGenerator : CodeGeneratorBase
    {
        public bool IsReadOnly { get; private set; }

        public QueryTableCodeGenerator(string outputFileName, string outputLang, string outputNamespace, string outputDefaultConnStr, string connStr, string name, bool isReadOnly)
            : base(outputFileName, outputLang, outputNamespace, outputDefaultConnStr, connStr, name)
        {
        }

        protected override void DoGenerate(CodeTypeDeclaration genClass)
        {
            genClass.BaseTypes.Add(typeof(QueryTable));

            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes = MemberAttributes.Public;
            constructor.Name = NameWithoutSpaces;
            constructor.BaseConstructorArgs.Add(new CodePrimitiveExpression(NameWithoutSpaces));
            constructor.BaseConstructorArgs.Add(new CodePrimitiveExpression(OutputDefaultConnectionStringName));
            constructor.BaseConstructorArgs.Add(new CodePrimitiveExpression(IsReadOnly));
            genClass.Members.Add(constructor);

            var criteria = new EmptyQueryTable(Name, OutputDefaultConnectionStringName).Select();
            var dt = _queryService.Query(criteria);

            foreach (DataColumn column in dt.Columns)
            {
                var field = new CodeMemberField();
                field.Name = "_" + column.ColumnName.Replace(" ", string.Empty);
                field.Attributes = MemberAttributes.Private;
                var fieldType = GetQueryColumnType(column.DataType);
                field.Type = new CodeTypeReference(fieldType);
                field.InitExpression = new CodeObjectCreateExpression(
                    field.Type,
                    fieldType == typeof(StringColumn) ? 
                        new CodeExpression[] { new CodePrimitiveExpression(column.ColumnName), new CodePrimitiveExpression(true) }
                        :
                        new CodeExpression[] { new CodePrimitiveExpression(column.ColumnName) }
                );
                genClass.Members.Add(field);

                var property = new CodeMemberProperty();
                property.Name = column.ColumnName.Replace(" ", string.Empty);
                property.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                property.Type = field.Type;
                property.HasGet = true;
                property.HasSet = false;
                property.GetStatements.Add(
                    new CodeMethodReturnStatement(
                        new CodeFieldReferenceExpression(
                            new CodeThisReferenceExpression(),
                            field.Name
                        )
                    )
                );
                genClass.Members.Add(property);
            }
        }

        protected Type GetQueryColumnType(Type columnType)
        {
            if (columnType == typeof(byte[]))
                return typeof(BinaryColumn);

            if (columnType == typeof(sbyte))
                columnType = typeof(byte);
            else if (columnType == typeof(ushort))
                columnType = typeof(short);
            else if (columnType == typeof(ushort))
                columnType = typeof(short);
            else if (columnType == typeof(uint))
                columnType = typeof(int);
            else if (columnType == typeof(ulong))
                columnType = typeof(long);
            else if (columnType == typeof(char))
                columnType = typeof(string);
            else if (columnType == typeof(float))
                columnType = typeof(double);

            return typeof(Int32Column).Assembly.GetType("NIntegrate.Data." + columnType.Name + "Column");
        }
    }

    public class QuerySprocCodeGenerator : CodeGeneratorBase
    {
        public QuerySprocCodeGenerator(string outputFileName, string outputLang, string outputNamespace, string outputDefaultConnStr, string connStr, string name)
            : base(outputFileName, outputLang, outputNamespace, outputDefaultConnStr, connStr, name)
        {
        }

        protected override void DoGenerate(CodeTypeDeclaration genClass)
        {
            genClass.BaseTypes.Add(typeof(QuerySproc));

            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes = MemberAttributes.Public;
            constructor.Name = NameWithoutSpaces;
            constructor.BaseConstructorArgs.Add(new CodePrimitiveExpression(NameWithoutSpaces));
            constructor.BaseConstructorArgs.Add(new CodePrimitiveExpression(OutputDefaultConnectionStringName));
            genClass.Members.Add(constructor);

            var criteria = new EmptyQuerySproc(Name, OutputDefaultConnectionStringName).CreateSprocCriteria();
            var cmd = _cmdFac.CreateCommand(criteria, false);
            var sqlCommand = new SqlCommand
            {
                CommandText = cmd.CommandText,
                CommandType = CommandType.StoredProcedure,
                Connection = (SqlConnection)cmd.Connection
            };
            using (var conn = sqlCommand.Connection)
            {
                conn.Open();
                SqlCommandBuilder.DeriveParameters(sqlCommand);
                conn.Close();
            }

            foreach (SqlParameter parameter in sqlCommand.Parameters)
            {
                var paramName = parameter.ParameterName.TrimStart('@');
                var field = new CodeMemberField();
                field.Name = "_" + paramName;
                field.Attributes = MemberAttributes.Private;
                var paramType = GetSprocParameterType(parameter.DbType);
                field.Type = new CodeTypeReference(paramType);
                field.InitExpression = new CodeObjectCreateExpression(
                    field.Type,
                    paramType == typeof(StringParameterExpression) ?
                        new CodeExpression[] { new CodePrimitiveExpression(paramName), CreateSprocParameterDirectionEnumExpression(parameter.Direction), new CodePrimitiveExpression(IsUnicodeDbType(parameter.DbType)) }
                        :
                        new CodeExpression[] { new CodePrimitiveExpression(paramName), CreateSprocParameterDirectionEnumExpression(parameter.Direction) }
                );
                genClass.Members.Add(field);

                var property = new CodeMemberProperty();
                property.Name = paramName;
                property.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                property.Type = field.Type;
                property.HasGet = true;
                property.HasSet = false;
                property.GetStatements.Add(
                    new CodeMethodReturnStatement(
                        new CodeFieldReferenceExpression(
                            new CodeThisReferenceExpression(),
                            field.Name
                        )
                    )
                );
                genClass.Members.Add(property);
            }
        }

        private CodeExpression CreateSprocParameterDirectionEnumExpression(ParameterDirection parameterDirection)
        {
            return new CodeFieldReferenceExpression(
                new CodeTypeReferenceExpression(
                    typeof(SprocParameterDirection)
                ),
                parameterDirection.ToString()
            );
        }

        private Type GetSprocParameterType(DbType dbType)
        {
            switch (dbType)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.String:
                case DbType.StringFixedLength:
                case DbType.Xml:
                    return typeof(StringParameterExpression);
                case DbType.Binary:
                    return typeof(BinaryParameterExpression);
                case DbType.Boolean:
                    return typeof(BooleanParameterExpression);
                case DbType.SByte:
                case DbType.Byte:
                    return typeof(ByteParameterExpression);
                case DbType.Single:
                case DbType.Double:
                    return typeof(DoubleParameterExpression);
                case DbType.Currency:
                    return typeof(DecimalParameterExpression);
                case DbType.Date:
                case DbType.DateTime:
                case DbType.DateTime2:
                case DbType.Time:
                    return typeof(DateTimeParameterExpression);
                case DbType.Guid:
                    return typeof(GuidParameterExpression);
                case DbType.Int16:
                case DbType.UInt16:
                    return typeof(Int16ParameterExpression);
                case DbType.Int32:
                case DbType.UInt32:
                    return typeof(Int32ParameterExpression);
                case DbType.Int64:
                case DbType.UInt64:
                    return typeof(Int64ParameterExpression);
            }

            return typeof(StringParameterExpression);
        }

        private bool IsUnicodeDbType(DbType dbType)
        {
            if (dbType == DbType.AnsiString || dbType == DbType.AnsiStringFixedLength)
                return false;
            
            return true;
        }
    }

    #endregion
}