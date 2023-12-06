using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System.Text;

namespace FFObjectViewer
{
    public partial class frmObjetViewer : Form
    {
        public frmObjetViewer()
        {
            InitializeComponent();
        }

        private void frmObjetViewer_Load(object sender, EventArgs e)
        {
            InitForm();
            InitializeFrameworkDropdown();
        }
        private void LoadObjecInfo()
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtFile.Text)) return;

                string[] selectedFiles = txtFile.Text.Split(Environment.NewLine.ToCharArray());

                foreach (string file in selectedFiles)
                {
                    if (string.IsNullOrEmpty(file)) continue;

                    // 获取用户选择的文件路径
                    string selectedPath = txtFile.Text;
                    this.rtInfo.AppendText("Source File: " + file + Environment.NewLine);
                    try
                    {
                        // 加载文件中的类名和方法名到TreeView
                        LoadAssembly(file);
                    }
                    catch (Exception ex)
                    { }
                }
                // 保存到本地文本文件
                SaveTreeViewToFile(rtInfo, "output.txt");

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void InitForm()
        {
            this.chkPara.Checked = false;
            this.chkPro.Checked = false;
        }

        private void InitializeFrameworkDropdown()
        {
            //// 初始化下拉列表框
            //cboframework.Items.Clear();

            //using (RegistryKey ndpKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP"))
            //{
            //    if (ndpKey != null)
            //    {
            //        string[] subKeyNames = ndpKey.GetSubKeyNames();
            //        foreach (string subKeyName in subKeyNames)
            //        {
            //            if (subKeyName.StartsWith("v") || subKeyName.StartsWith("Version"))
            //            {
            //                cboframework.Items.Add(".NET Framework " + subKeyName);
            //            }
            //        }
            //    }
            //}

            //// Read installed .NET Core versions
            //using (RegistryKey ndpKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\dotnet\Setup\InstalledVersions"))
            //{
            //    if (ndpKey != null)
            //    {
            //        string[] subKeyNames = ndpKey.GetSubKeyNames();
            //        foreach (string subKeyName in subKeyNames)
            //        {
            //            cboframework.Items.Add(".NET Core " + subKeyName);
            //        }
            //    }
            //}

            //cboframework.SelectedIndex = 0;
        }

        private void LoadAssembly(string filePath)
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(filePath);
                rtInfo.AppendText("DLL Name: " + assembly.FullName + Environment.NewLine);
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (!type.IsVisible || type.IsSealed) continue;
                    rtInfo.AppendText("Class Name: " + type.Name + Environment.NewLine);

                    if (chkPro.Checked)
                    {
                        PropertyInfo[] properties = type.GetProperties(BindingFlags.DeclaredOnly |
                            BindingFlags.Public |
                            BindingFlags.NonPublic |
                            BindingFlags.Static);

                        foreach (PropertyInfo p in properties)
                        {
                            rtInfo.AppendText(p.Name);
                            TreeNode propertyNode = new TreeNode(p.Name);
                        }
                    }

                    MethodInfo[] methodInfo = type.GetMethods(BindingFlags.DeclaredOnly |
                        BindingFlags.Public |
                        BindingFlags.NonPublic |
                        BindingFlags.Static |
                        BindingFlags.Instance |
                        BindingFlags.PutDispProperty);

                    foreach (MethodInfo method in methodInfo)
                    {
                        if (method.IsAssembly) { continue; }
                        if (method.IsSpecialName || method.DeclaringType.Namespace == null) { continue; }
                        rtInfo.AppendText("   " + method.Name + GetMethodParameters(method) + Environment.NewLine);
                    }
                    rtInfo.AppendText(Environment.NewLine);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private string GetMethodParameters(MethodInfo method)
        {
            if (chkPara.Checked)
            {
                string paras = "";
                ParameterInfo[] parameters = method.GetParameters();
                foreach (ParameterInfo parameter in parameters)
                {
                    paras = paras + parameter.ParameterType.Name + " " + parameter.Name + ",";
                }
                return "(" + paras.TrimEnd(',') + ")";
            }
            else
            {
                return "()";
            }
        }

        private void SaveTreeViewToFile(RichTextBox rt, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(rt.Text);
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            try
            {
                rtInfo.Text = "";

                LoadObjecInfo();

                LoadSQLScript();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "btnGet_Click"); }
        }

        private void LoadSQLScript()
        {
            if (string.IsNullOrEmpty(this.txtDBScript.Text)) return;

            foreach (string dbfile in this.txtDBScript.Text.Split(Environment.NewLine.ToCharArray()))
            {
                if (string.IsNullOrEmpty(dbfile)) continue;
                try
                {
                    SQLGetObjectName(dbfile);
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message, "SQLGetObjectName"); }
            }
        }

        private string SQLGetObjectName(string ScriptName)
        {
            try
            {
                string infomet = string.Empty;
                FileStream stream = new FileStream(ScriptName, FileMode.Open);
                TSql130Parser parser = new TSql130Parser(initialQuotedIdentifiers: false);
                using (StreamReader reader = new StreamReader(stream))
                {
                    IList<ParseError> errors;
                    TSqlFragment tSqlFragment = parser.Parse(reader, out errors);

                    //Table
                    TableVisitor tvisitor = new TableVisitor();
                    tSqlFragment.Accept(tvisitor);
                    if (tvisitor.Statements.Count > 0)
                    {
                        foreach (CreateTableStatement view in tvisitor.Statements)
                        {
                            if (view.SchemaObjectName.Identifiers[0].Value != "dbo")
                            {
                                rtInfo.AppendText("Table " + view.SchemaObjectName.Identifiers[0].Value + Environment.NewLine);
                            }
                            else
                            {
                                rtInfo.AppendText("Table " + view.SchemaObjectName.Identifiers[1].Value + Environment.NewLine);
                            }
                        }
                        return infomet;
                    }

                    //View
                    ViewVisitor visitor = new ViewVisitor();
                    tSqlFragment.Accept(visitor);
                    if (visitor.Statements.Count > 0)
                    {
                        foreach (CreateViewStatement view in visitor.Statements)
                        {
                            rtInfo.AppendText("View " + view.SchemaObjectName.Identifiers[0].Value + Environment.NewLine);
                        }
                        return infomet;
                    }

                    //Procedure
                    ProcedureVisitor pvisitor = new ProcedureVisitor();
                    tSqlFragment.Accept(pvisitor);
                    if (pvisitor.Statements.Count > 0)
                    {
                        foreach (CreateProcedureStatement procedure in pvisitor.Statements)
                        {
                            rtInfo.AppendText("Stored Procedure " + procedure.ProcedureReference.Name.BaseIdentifier.Value + Environment.NewLine);
                        }
                        return infomet;
                    }

                    //function
                    FunctionVisitor fvisitor = new FunctionVisitor();
                    tSqlFragment.Accept(fvisitor);
                    if (fvisitor.Statements.Count > 0)
                    {
                        foreach (CreateFunctionStatement funct in fvisitor.Statements)
                        {
                            rtInfo.AppendText("Function " + funct.Name.BaseIdentifier.Value + Environment.NewLine);
                        }
                        return infomet;
                    }
                };
                return infomet;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SQLGetObjectName");
                return string.Empty;
            }
        }

        private string SQLGetViews(string ScriptName)
        {
            try
            {
                string infomet = string.Empty;
                FileStream stream = new FileStream(ScriptName, FileMode.Open);
                TSql130Parser parser = new TSql130Parser(initialQuotedIdentifiers: false);
                using (StreamReader reader = new StreamReader(stream))
                {
                    IList<ParseError> errors;
                    TSqlFragment tSqlFragment = parser.Parse(reader, out errors);
                    ViewVisitor visitor = new ViewVisitor();
                    tSqlFragment.Accept(visitor);
                    if (visitor.Statements.Count > 0)
                    {
                        foreach (CreateViewStatement view in visitor.Statements)
                        {
                            rtInfo.AppendText("View " + view.SchemaObjectName.Identifiers[0].Value + Environment.NewLine);
                        }
                    }
                };
                return infomet;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SQLGetViews");
                return string.Empty;
            }
        }

        private string SQLGetProcedures(string ScriptName)
        {
            try
            {
                string infomet = string.Empty;
                FileStream stream = new FileStream(ScriptName, FileMode.Open);
                TSql130Parser parser = new TSql130Parser(initialQuotedIdentifiers: false);
                using (StreamReader reader = new StreamReader(stream))
                {
                    IList<ParseError> errors;
                    TSqlFragment tSqlFragment = parser.Parse(reader, out errors);
                    ProcedureVisitor visitor = new ProcedureVisitor();
                    tSqlFragment.Accept(visitor);
                    if (visitor.Statements.Count > 0)
                    {
                        foreach (CreateProcedureStatement procedure in visitor.Statements)
                        {
                            rtInfo.AppendText("Stored Procedure " + procedure.ProcedureReference.Name.BaseIdentifier.Value + Environment.NewLine);
                        }
                    }
                }
                return infomet;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SQLGetProcedures");
                return string.Empty;
            }
        }

        private string SQLGetFunctions(string ScriptName)
        {
            try
            {
                string infomet = string.Empty;
                FileStream stream = new FileStream(ScriptName, FileMode.Open);
                TSql130Parser parser = new TSql130Parser(initialQuotedIdentifiers: false);
                using (StreamReader reader = new StreamReader(stream))
                {
                    IList<ParseError> errors;
                    TSqlFragment tSqlFragment = parser.Parse(reader, out errors);
                    FunctionVisitor visitor = new FunctionVisitor();
                    tSqlFragment.Accept(visitor);
                    if (visitor.Statements.Count > 0)
                    {
                        foreach (CreateFunctionStatement funct in visitor.Statements)
                        {
                            rtInfo.AppendText("Function " + funct.Name.BaseIdentifier.Value + Environment.NewLine);
                        }
                    }
                }
                return infomet;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SQLGetFunctions");
                return string.Empty;
            }
        }



        private void txtFile_DragDrop(object sender, DragEventArgs e)
        {
            // 支持拖拽DLL到文本框
            txtFile.Text = GetFiles((string[])e.Data.GetData(DataFormats.FileDrop));
        }

        private string GetFiles(string[] files)
        {
            string finalf = "";

            if (Directory.Exists(files[0]))
            {
                files = Directory.GetFiles(files[0]);
                finalf = string.Join(Environment.NewLine, files);
            }
            else
            {
                if (files.Length > 0 && (Path.GetExtension(files[0]).Equals(".dll", StringComparison.OrdinalIgnoreCase)
                        || Path.GetExtension(files[0]).Equals(".exe", StringComparison.OrdinalIgnoreCase)
                        || Path.GetExtension(files[0]).Equals(".sql", StringComparison.OrdinalIgnoreCase))
                    )
                {
                    foreach (string s in files)
                    {
                        finalf += s + Environment.NewLine;
                    }
                }
            }

            return finalf;
        }

        private void txtFile_DragEnter(object sender, DragEventArgs e)
        {
            // 支持拖拽DLL到文本框
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void cmdBrow_Click(object sender, EventArgs e)
        {
            try
            {
                txtFile.Text = LoadFiles("DLL Files|*.dll|Application Files|*.exe");
            }
            catch (Exception ex)
            {

            }
        }

        private void cmdDBScript_Click(object sender, EventArgs e)
        {
            try
            {
                txtDBScript.Text = LoadFiles("SQL Files|*.sql");
            }
            catch (Exception ex)
            {

            }
        }

        private string LoadFiles(string fileFilter)
        {
            // 用户选择 DLL 文件
            string files = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter = fileFilter;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string s in openFileDialog1.FileNames)
                {
                    files += s + Environment.NewLine;
                }
            }
            return files;
        }

        private void txtDBScript_DragDrop(object sender, DragEventArgs e)
        {
            // 支持拖拽DLL到文本框
            txtDBScript.Text = GetFiles((string[])e.Data.GetData(DataFormats.FileDrop));
        }

        private void txtDBScript_DragEnter(object sender, DragEventArgs e)
        {
            // 支持拖拽DLL到文本框
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
    }
}
