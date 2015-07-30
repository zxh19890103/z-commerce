using Nt.DAL;
using Nt.Model.NtAttribute;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Nt.BLL
{
    public class Class2SqlGenerator
    {
        string _virtualPath2SaveScript;
        /// <summary>
        /// 保存脚本文件的虚拟路径
        /// </summary>
        public string VirtualPath2SaveScript
        {
            get { return _virtualPath2SaveScript; }
            set { _virtualPath2SaveScript = value; }
        }

        /// <summary>
        /// Class2SqlGenerator 的构造函数
        /// </summary>
        /// <param name="path2saveSql">保存脚本文件的虚拟路径</param>
        public Class2SqlGenerator(string path2saveSql)
        {
            _virtualPath2SaveScript = path2saveSql;
        }

        public void Run()
        {
            StreamWriter sw = File.CreateText(WebHelper.MapPath(VirtualPath2SaveScript));
            sw.Write("--Drop All Views\r\n");
            GenerateSqlToDropViews(sw);
            sw.WriteLine();
            sw.WriteLine();
            sw.Write("--Drop All Foreign Key...\r\n");
            GenerateSqlToDropFk(sw);
            sw.WriteLine();
            sw.WriteLine();
            sw.Write("--Drop All tables...\r\n");
            GenerateSqlToDropTables(sw);
            sw.WriteLine();
            sw.WriteLine();
            sw.Write("--Create All tables...\r\n");
            GenerateSqlToCtreateTables(sw);
            sw.WriteLine();
            sw.WriteLine();
            sw.Write("--Create All Foreign Key...\r\n");
            GenerateSqlToCreateFK(sw);
            sw.WriteLine();
            sw.WriteLine();
            sw.Write("--Create All Views\r\n");
            GenerateSqlToCreateViews(sw);
            sw.Flush();
            sw.Close();
        }

        /// <summary>
        /// 未实现
        /// </summary>
        public void CheckUpdate()
        {
            StreamWriter sw = File.CreateText(WebHelper.MapPath(VirtualPath2SaveScript));
            
            //添加表

            //添加字段
            //添加视图
            //添加外键

            //减少表
            //减少字段
            //减少视图
            //减少外键

            sw.Flush();
            sw.Close();
        }

        /// <summary>
        /// 生成创建表的脚本
        /// </summary>
        void GenerateSqlToCtreateTables(StreamWriter sw)
        {
            Assembly assembly = Assembly.Load("Nt.Model");
            Module module = assembly.GetModule("Nt.Model.dll");
            TypeFilter filter = new TypeFilter(Filterate);
            Type[] types = module.FindTypes(filter, "");
            string indent = "    ";
            string table = string.Empty;
            foreach (var item in types)
            {
                table = DbAccessor.GetModifiedTableName(item.Name);
                sw.Write(string.Format("--Creating Table '{0}' ", table));
                sw.WriteLine();
                sw.Write(string.Format("Create Table [dbo].[{0}] (", table));
                sw.WriteLine();
                foreach (var pi in item.GetProperties())
                {
                    string code = pi.PropertyType.Name;
                    if (pi.Name.ToLower().Equals("id"))
                        sw.Write(string.Format("{0}[{1}] int primary key IDENTITY(1,1) not null,", indent, pi.Name));
                    else
                    {
                        object[] attrs = pi.GetCustomAttributes(typeof(FieldSizeAttribute), true);
                        if (code == CSBaseType.String
                            && (attrs != null && attrs.Length > 0))
                        {
                            FieldSizeAttribute at = attrs[0] as FieldSizeAttribute;
                            sw.Write(string.Format("{0}[{1}] nvarchar({2}) not null,", indent, pi.Name, at.Size));
                        }
                        else
                        {
                            sw.Write(string.Format("{0}[{1}] {2} not null,", indent, pi.Name, MapType(code)));
                        }
                    }
                    sw.WriteLine();
                }
                sw.Write(");");
                sw.WriteLine();
                sw.Write("GO\r\n");
            }
        }

        /// <summary>
        /// 生成删除表的脚本
        /// </summary>
        /// <param name="sw"></param>
        void GenerateSqlToDropTables(StreamWriter sw)
        {
            Assembly assembly = Assembly.Load("Nt.Model");
            Module module = assembly.GetModule("Nt.Model.dll");
            TypeFilter filter = new TypeFilter(Filterate);
            Type[] types = module.FindTypes(filter, "");
            string table = string.Empty;
            foreach (var item in types)
            {
                table = DbAccessor.GetModifiedTableName(item.Name);
                sw.Write(string.Format("IF OBJECT_ID(N'[dbo].[{0}]', 'U') IS NOT NULL\r\n", table));
                sw.Write(string.Format("    DROP TABLE [dbo].[{0}];\r\n", table));
                sw.Write("GO\r\n");
                sw.WriteLine();
            }
        }

        /// <summary>
        /// 生成创建外键的脚本
        /// </summary>
        void GenerateSqlToCreateFK(StreamWriter sw)
        {
            Assembly assembly = Assembly.Load("Nt.Model");
            Module module = assembly.GetModule("Nt.Model.dll");
            TypeFilter filter = new TypeFilter(Filterate);
            Type[] types = module.FindTypes(filter, "");

            string thisTable = string.Empty;
            string thisField = string.Empty;
            string foreignKey = string.Empty;

            foreach (var item in types)
            {
                thisTable = DbAccessor.GetModifiedTableName(item.Name);
                foreach (var pi in item.GetProperties())
                {
                    thisField = pi.Name;
                    object[] attrs = pi.GetCustomAttributes(typeof(FKConstraintAttribute), true);
                    if (attrs != null && attrs.Length > 0)
                    {
                        foreach (var attr in attrs)
                        {
                            FKConstraintAttribute at = attr as FKConstraintAttribute;
                            foreignKey = DbAccessor.GetForeignKey(item.Name, at.ForeignTableName);
                            sw.Write(string.Format("--Creating Foreign Key '{0}' ", foreignKey));
                            sw.WriteLine();
                            sw.Write(string.Format(
                                "ALTER TABLE [dbo].[{0}]\r\nADD CONSTRAINT [{1}]\r\nFOREIGN KEY ([{2}]) REFERENCES [dbo].[{3}]([{4}])\r\nON DELETE CASCADE ON UPDATE NO ACTION;\r\nGO"
                            , thisTable, foreignKey, thisField,
                            DbAccessor.GetModifiedTableName(at.ForeignTableName), at.Field));
                            sw.WriteLine();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 生成删除外键的脚本
        /// </summary>
        void GenerateSqlToDropFk(StreamWriter sw)
        {
            Assembly assembly = Assembly.Load("Nt.Model");
            Module module = assembly.GetModule("Nt.Model.dll");
            TypeFilter filter = new TypeFilter(Filterate);
            Type[] types = module.FindTypes(filter, "");

            string thisTable = string.Empty;
            string foreignKey = string.Empty;

            foreach (var item in types)
            {
                thisTable = DbAccessor.GetModifiedTableName(item.Name);
                foreach (var pi in item.GetProperties())
                {
                    object[] attrs = pi.GetCustomAttributes(typeof(FKConstraintAttribute), true);
                    if (attrs != null && attrs.Length > 0)
                    {
                        foreach (var attr in attrs)
                        {
                            FKConstraintAttribute at = attr as FKConstraintAttribute;
                            foreignKey = DbAccessor.GetForeignKey(item.Name, at.ForeignTableName);
                            sw.Write(string.Format("--drop Foreign Key '{0}' ", foreignKey));
                            sw.WriteLine();
                            sw.Write(string.Format(
                                "IF OBJECT_ID(N'[dbo].[{0}]','F') IS NOT NULL\r\n  ALTER TABLE [dbo].[{1}] DROP CONSTRAINT [{0}];\r\nGO",
                foreignKey, thisTable));
                            sw.WriteLine();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 生成创建视图的脚本,在App_Data/script/createviews.sql
        /// </summary>
        void GenerateSqlToCreateViews(StreamWriter sw)
        {
            Assembly assembly = Assembly.Load("Nt.Model");
            Module module = assembly.GetModule("Nt.Model.dll");

            //从低级视图到高级视图生成

            string viewName = string.Empty;
            Action<Func<Type, bool>> a = new Action<Func<Type, bool>>(f =>
            {
                Type[] types = module.FindTypes(new TypeFilter((t, filterCriteria) =>
                {
                    bool result = false;
                    if (t.Namespace == "Nt.Model.View"
                        && t.GetInterface("IView") != null
                        && f(t)
                        && !t.IsAbstract)
                        result = true;
                    return result;
                }), "");

                foreach (var item in types)
                {
                    var constructor = item.GetConstructor(new Type[0]);
                    var view = constructor.Invoke(null) as IView;
                    if (view != null)
                    {
                        var body = view.GetScript();
                        if (body != "")
                        {
                            viewName = item.Name;
                            sw.WriteLine(string.Format("--Creating View '{0}' ", viewName));
                            sw.WriteLine(string.Format("Create View [dbo].[{0}]", viewName));
                            sw.WriteLine("As");
                            sw.WriteLine(Regex.Replace(body, @"##\w+##", m =>
                            {
                                string oldStr = m.Value;
                                string newStr = oldStr.Substring(2, m.Length - 4);
                                newStr = DbAccessor.GetModifiedTableName(newStr);
                                return newStr;
                            }));
                            sw.WriteLine("GO");
                        }
                    }
                }
            });
            //primary
            a.Invoke(t => { return t.GetInterface("IViewMedium") == null && t.GetInterface("IViewHigh") == null; });
            //medium
            a.Invoke(t => { return t.GetInterface("IViewMedium") != null; });
            //high
            a.Invoke(t => { return t.GetInterface("IViewHigh") != null; });
        }

        /// <summary>
        /// 生成删除视图的脚本,在App_Data/script/dropviews.sql
        /// </summary>
        void GenerateSqlToDropViews(StreamWriter sw)
        {
            Assembly assembly = Assembly.Load("Nt.Model");
            Module module = assembly.GetModule("Nt.Model.dll");

            //从高级视图到低级视图逐级删除
            string indent = "    ";
            string viewName = string.Empty;

            Action<Func<Type, bool>> a = new Action<Func<Type, bool>>(f =>
            {
                Type[] types = module.FindTypes(new TypeFilter((t, filterCriteria) =>
                {
                    bool result = false;
                    if (t.Namespace == "Nt.Model.View"
                        && t.GetInterface("IView") != null
                        && f(t)
                        && !t.IsAbstract)
                        result = true;
                    return result;
                }), "");

                foreach (var item in types)
                {
                    viewName = item.Name;
                    sw.WriteLine(string.Format("--Drop View '{0}' ", viewName));
                    sw.WriteLine(string.Format("IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[{0}]'))", viewName));
                    sw.Write(indent);
                    sw.WriteLine(string.Format("Drop VIEW [dbo].[{0}]", viewName));
                    sw.WriteLine("GO");
                }
            });

            //high
            a.Invoke(t => { return t.GetInterface("IViewHigh") != null; });
            //medium
            a.Invoke(t => { return t.GetInterface("IViewMedium") != null; });
            //primary
            a.Invoke(t => { return t.GetInterface("IViewMedium") == null && t.GetInterface("IViewHigh") == null; });

        }

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="m">类型</param>
        /// <param name="filterCriteria"></param>
        /// <returns></returns>
        bool Filterate(Type m, object filterCriteria)
        {
            bool result = false;
            if (m.Namespace == "Nt.Model"
                && m.GetInterface("IEntity") != null)
                result = true;
            if (m.IsAbstract)
                result = false;
            return result;
        }

        public string MapType(string code)
        {
            switch (code)
            {
                case CSBaseType.Boolean:
                    return "bit";
                case CSBaseType.Byte:
                    return "tinyint";
                case CSBaseType.DateTime:
                    return "datetime";
                case CSBaseType.Decimal:
                    return "decimal(18,4)";
                case CSBaseType.Double:
                    return "double";
                case CSBaseType.Int16:
                    return "smallint";
                case CSBaseType.Int32:
                    return "int";
                case CSBaseType.Int64:
                    return "bigint";
                case CSBaseType.String:
                    return "nvarchar(1024)";
                case CSBaseType.Single:
                    return "float";
                case CSBaseType.Guid:
                    return "uniqueidentifier";
                default:
                    throw new Exception("不接受的数据类型");
            }
        }
    }
}
