using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nt.BLL;
using Nt.Framework;
using System.IO;

public partial class install_index : System.Web.UI.Page
{

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        bool databaseCreated = NtContext.Current.DataBaseCreated;
        //for ajax request
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            Response.Clear();

            #region k
            int error = 0;
            string errorMsg = string.Empty;

            //for database created
            if (databaseCreated)
            {
                error = 2;
                errorMsg = "已经安装了数据库!";
            }
            else
            {

                if (Request.HttpMethod == "POST")
                {
                    string db = Request["DbName"];
                    string server = Request["DataSource"];
                    string uid = Request["UserID"];
                    string pwd = Request["Password"];

                    bool existed = false, integratedSecurity = false;
                    NtJson.EnsureBoolean("DbExisting", "参数错误:DbExisting！", out existed);
                    NtJson.EnsureBoolean("IntegratedSecurity", "参数错误:IntegratedSecurity！", out integratedSecurity);

                    InstallService installer;
                    if (integratedSecurity)
                        installer = new InstallService(db, server);
                    else
                        installer = new InstallService(db, server, uid, pwd);
                    installer.DbExisting = existed;

                    try
                    {
                        installer.Install();
                        errorMsg = "安装成功";
                    }
                    catch (Exception ex)
                    {
                        error = 1;
                        errorMsg = ex.Message;
                        installer.DropDB();
                        installer.DeleteConnectionFile();
                    }
                }
            }
            #endregion

            NtJson json = new NtJson();
            json.ErrorCode = error;
            json.Json["message"] = errorMsg;
            Response.Write(json);

            Response.End();
        }
        else
        {
            //如果检测到数据库已经安装，则重置请求的Url
            if (databaseCreated)
            {
                Response.Redirect("/");
            }
        }
    }

}