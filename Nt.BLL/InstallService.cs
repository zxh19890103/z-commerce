using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;
using Nt.DAL;


namespace Nt.BLL
{
    public class InstallService
    {
        #region Const
        /// <summary>
        /// 0--data source
        /// 1--initial catalog
        /// 2--user id
        /// 3--password
        /// </summary>
        const string CONNECTION_STRING_SA_PATTERN = @"data source={0};initial catalog={1};Integrated Security=False;persist security info=True;user id={2};password={3};MultipleActiveResultSets=True;Max Pool Size=512";
        const string CONNECTION_STRING_WINDOWS_PATTERN = @"data source={0};initial catalog={1};Integrated Security=True;persist security info=True;MultipleActiveResultSets=True;Max Pool Size=512";
        const string SQL_SCRIPT_PATH_PATTERN = "/App_Data/Script/{0}.sql";

        #endregion

        /// <summary>
        /// 用于缓存SQL语句的容器
        /// </summary>
        private StringBuilder _sql = null;

        /// <summary>
        /// 安装时错误提示
        /// </summary>
        public string ErrorMsg { get; set; }

        #region Connection String Setting
        /// <summary>
        /// 服务器
        /// </summary>
        private string _dataSource = string.Empty;
        public string DataSource { get { return _dataSource; } set { _dataSource = value; } }

        /// <summary>
        /// 数据库名称
        /// </summary>
        private string _dbName = string.Empty;
        public string DbName { get { return _dbName; } set { _dbName = value; } }

        /// <summary>
        /// sql server登录密码
        /// </summary>
        private string _password = string.Empty;
        public string Password { get { return _password; } set { _password = value; } }

        /// <summary>
        ///sql server 用户名
        /// </summary>
        private string _userID = string.Empty;
        public string UserID { get { return _userID; } set { _userID = value; } }

        private bool _dbExisting = false;
        /// <summary>
        /// 数据库是否存在
        /// </summary>
        public bool DbExisting { get { return _dbExisting; } set { _dbExisting = value; } }

        /// <summary>
        /// 是否Windows集成验证，默认为否
        /// </summary>
        public bool IntegratedSecurity { get; set; }

        #endregion

        #region connectionString

        private string _masterConnectionString = string.Empty;
        /// <summary>
        /// 用于连接master数据库的字符串
        /// </summary>
        protected string MasterConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_masterConnectionString))
                {
                    if (IntegratedSecurity)
                        _masterConnectionString =
                            string.Format(CONNECTION_STRING_WINDOWS_PATTERN, _dataSource, "master");
                    else
                        _masterConnectionString =
                            string.Format(CONNECTION_STRING_SA_PATTERN, _dataSource, "master", _userID, _password);
                }
                return _masterConnectionString;
            }
        }

        /// <summary>
        /// 用于连接本数据库的字符串
        /// </summary>
        private string _thisDbConnectionString = string.Empty;
        protected string ThisDbConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_thisDbConnectionString))
                {
                    if (IntegratedSecurity)
                        _thisDbConnectionString = string.Format(CONNECTION_STRING_WINDOWS_PATTERN, _dataSource, _dbName);
                    else
                        _thisDbConnectionString = string.Format(CONNECTION_STRING_SA_PATTERN, _dataSource, _dbName, _userID, _password);
                }
                return _thisDbConnectionString;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Sql用户验证
        /// </summary>
        /// <param name="dbname">数据库名</param>
        /// <param name="dataSource">服务器名</param>
        /// <param name="userId">用户名</param>
        /// <param name="password">密码</param>
        public InstallService(string dbname, string dataSource,
            string userId, string password
            )
            : this(dbname, dataSource)
        {
            _userID = userId;
            _password = password;
            IntegratedSecurity = false;
        }

        /// <summary>
        /// 集成Windows验证
        /// </summary>
        /// <param name="dbname">数据库名</param>
        /// <param name="dataSource">服务器名</param>
        public InstallService(string dbname, string dataSource)
        {
            _dbName = dbname;
            _dataSource = dataSource;
            _sql = new StringBuilder();
            IntegratedSecurity = true;
        }

        public InstallService()
        {
            if (!NtContext.Current.DataBaseCreated)
                throw new Exception("constructor can not be invoked because database has not created yet!");
            _thisDbConnectionString = SqlHelper.GetConnSting();
            _sql = new StringBuilder();
        }

        #endregion

        string _phyPathToSaveDB;
        public string PhyPathToSaveDB
        {
            get
            {
                if (string.IsNullOrEmpty(_phyPathToSaveDB))
                {
                    string tp = WebHelper.MapPath("/App_Data/DBPathInDisk.txt");
                    if (File.Exists(tp))
                    {
                        _phyPathToSaveDB = File.ReadAllText(tp);
                        if (string.IsNullOrEmpty(_phyPathToSaveDB))
                            return "";
                    }
                }
                return _phyPathToSaveDB;
            }
        }

        /// <summary>
        /// 全部安装
        /// </summary>
        public void Install()
        {
            if (NtContext.Current.DataBaseCreated)
                throw new Exception("DataBase has been created.");

            if (!DbExisting)
            {
                if (!string.IsNullOrEmpty(PhyPathToSaveDB)
                   && !Directory.Exists(PhyPathToSaveDB))
                    Directory.CreateDirectory(PhyPathToSaveDB);
                InstallDb();
            }

            SetQUOTED_IDENTIFIER(false);

            //生成建表sql脚本
            Class2SqlGenerator gen = new Class2SqlGenerator(string.Format(SQL_SCRIPT_PATH_PATTERN, "sql"));
            gen.Run();

            ExecuteScriptOnFile("sql");
            string connectionFilePath = WebHelper.MapPath("/App_Data/Connection.txt");
            File.WriteAllText(connectionFilePath, ThisDbConnectionString);

            ExecuteScriptOnFile("insert");//插入基本数据
            ExecuteScriptOnFile("menu");//插入后台用导航
            ExecuteScriptOnFile("proc");//创建存储过程
        }

        #region Install
        /// <summary>
        /// 安装必要的master上的存储过程
        /// </summary>
        void InstallDb()
        {
            string sql2CreateDB = string.Empty;
            if (string.IsNullOrEmpty(PhyPathToSaveDB))
            {
                sql2CreateDB = "create database [{DB}]\r\n"
                    .Replace("{DB}", DbName);
            }
            else
            {
                string mdfPath = string.Empty;
                string ldfPath = string.Empty;
                mdfPath = PhyPathToSaveDB + "{DB}.mdf";
                ldfPath = PhyPathToSaveDB + "{DB}_log.ldf";
                sql2CreateDB = "create database [{DB}] on primary\r\n(\r\nname=N'{DB}',\r\nfilename=N'{mdfPath}',\r\nsize=4096kb,\r\nmaxsize=102400kb,\r\nfilegrowth=1024kb\r\n)\r\nlog on\r\n(\r\nname=N'{DB}_log',\r\nfilename=N'{ldfPath}',\r\nsize=3072kb,\r\nmaxsize=51200kb,\r\nfilegrowth=10%\r\n)"
                    .Replace("{mdfPath}", mdfPath)
                    .Replace("{ldfPath}", ldfPath)
                    .Replace("{DB}", DbName);
            }
            _sql.Append(sql2CreateDB);
            ExecuteOnMaster();
           //CreateNewDbUser();//给数据库分配独立用户
        }

        /// <summary>
        /// 删除数据库
        /// </summary>
        public void DropDB()
        {
            _sql.AppendFormat("if exists(select * from sysdatabases where name=N'{0}')\r\ndrop database {0};\r\n", _dbName);
            ExecuteOnMaster();
        }

        /// <summary>
        /// 删除链接字符串文件
        /// </summary>
        public void DeleteConnectionFile()
        {
            string connectionFilePath = WebHelper.MapPath("/App_Data/Connection.txt");
            if (File.Exists(connectionFilePath))
                File.Delete(connectionFilePath);
        }

        /// <summary> 
        /// create login account
        /// uid=nt_login_{_dbName}
        /// pwd = NtUtility.GetRandomPwd();     
        /// <code>
        /// script:
        /// EXEC sp_addlogin N'uid','pwd'
        /// EXEC sp_grantdbaccess N'uid'
        /// EXEC sp_addrolemember N'db_owner', N'uid'
        /// exec sp_revokedbaccess N'uid'
        /// </code>
        /// </summary>
        public void CreateNewDbUser()
        {
            string uid = string.Format("nt_login_{0}", _dbName),
                pwd = NtUtility.GetRandomPwd();
            pwd = pwd.Replace("{", "{{").Replace("}", "}}");
            _sql.AppendFormat("EXEC sp_addlogin N'{0}','{1}';\r\n", uid, pwd);
            _sql.AppendFormat("EXEC sp_grantdbaccess N'{0}';\r\n", uid);
            _sql.AppendFormat("EXEC sp_addrolemember N'db_owner',N'{0}';\r\n", uid);
            _sql.AppendFormat("EXEC sp_revokedbaccess N'{0}';\r\n", uid);
            Execute();
            _thisDbConnectionString = string.Empty;
            UserID = uid;
            Password = pwd;
            IntegratedSecurity = false;
        }

        #endregion

        #region Utility

        /// <summary>
        /// SET QUOTED_IDENTIFIER ON/OFF
        /// </summary>
        /// <param name="onOrOff">bool true--ON  false--OFF</param>
        void SetQUOTED_IDENTIFIER(bool onOrOff)
        {
            string sql = string.Format("SET QUOTED_IDENTIFIER {0}", onOrOff ? "ON" : "OFF");
            Execute(sql);
        }

        /// <summary>
        /// SET ANSI_NULLS ON/OFF
        /// </summary>
        /// <param name="onOrOff">bool true--ON  false--OFF</param>
        void SetANSI_NULLS(bool onOrOff)
        {
            string sql = string.Format("SET ANSI_NULLS {0}", onOrOff ? "ON" : "OFF");
            Execute(sql);
        }


        #endregion

        #region SQL Execute
        /// <summary>
        /// 执行保存于_sql中的SQL语句
        /// </summary>
        void Execute()
        {
            Execute(_sql.ToString());
            _sql.Remove(0, _sql.Length);
        }

        /// <summary>
        /// 使用master用户执行
        /// </summary>
        void ExecuteOnMaster()
        {
            if (_sql.Length > 0)
            {
                SqlHelper.ExecuteNonQuery(
                    MasterConnectionString,
                    CommandType.Text,
                    _sql.ToString()
                    );
            }
            _sql.Remove(0, _sql.Length);
        }

        void Execute(string sql)
        {
            if (sql.Length > 0)
            {
                SqlHelper.ExecuteNonQuery(
                    ThisDbConnectionString,
                    CommandType.Text,
                    sql
                    );
            }
        }

        /// <summary>
        /// execute script on file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="exeOnMaster"></param>
        public void ExecuteScriptOnFile(string filename, bool exeOnMaster = false)
        {
            var absPath = string.Format(SQL_SCRIPT_PATH_PATTERN, filename);
            var phy_filename = WebHelper.MapPath(absPath);
            if (!File.Exists(phy_filename))
                throw new Exception(string.Format("没有发现路径为{0}的脚本文件！", absPath));
            StreamReader reader = null;
            try
            {
                reader = File.OpenText(phy_filename);
                string line = string.Empty;
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine().Trim();
                    if (line.ToUpper() == "GO")
                    {
                        if (exeOnMaster)
                            ExecuteOnMaster();
                        else
                            Execute();
                    }
                    else
                    {
                        _sql.Append(line);
                        _sql.Append("\r\n");
                    }
                }

                if (exeOnMaster)
                    ExecuteOnMaster();
                else
                    Execute();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
                reader.Dispose();
            }
        }

        #endregion
    }
}
