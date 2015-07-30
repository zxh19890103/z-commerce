using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nt.Framework;
using Nt.Framework.Admin;
using System.IO;
using Nt.Model.Enum;
using System.Data.SqlClient;
using Nt.DAL;
using System.Data;

public partial class netin_index : PageBase
{

    protected int TotalPendingOrder = 0;
    protected int TotalUnViewedMessage = 0;
    protected int TotalProduct = 0;
    protected int TotalArticle = 0;
    protected int TotalGoods = 0;
    protected int TotalCustomer = 0;

    public long fileTotalSize = 0;
    public string dbSize = "0.00MB";


    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Title = "系统首页";
        StandSize();

        string statistic = string.Format(
                "Select (Select Count(0) From [Nt_Order] Where Status={0}) As C0,(Select Count(0) From [Nt_GuestBook] Where Viewed=0) As C1,(Select Count(0) From [Nt_Product]) As C2,(Select Count(0) From [Nt_Article]) As C3,(Select Count(0) From [Nt_Goods]) As C4,(Select Count(0) From [Nt_Customer]) As C5;\r\n",
                (int)OrderStatus.Pending);

        using (SqlDataReader rs = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, statistic))
        {
            if (rs.Read())
            {
                TotalPendingOrder = rs.GetInt32(0);
                TotalUnViewedMessage = rs.GetInt32(1);
                TotalProduct = rs.GetInt32(2);
                TotalArticle = rs.GetInt32(3);
                TotalGoods = rs.GetInt32(4);
                TotalCustomer = rs.GetInt32(5);
            }
        }
    }

    void StandSize()
    {
        SqlDataReader rs = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), "sp_spaceused");
        if (rs.Read())
            dbSize = rs.GetString(1);
        string websitePath = AppDomain.CurrentDomain.BaseDirectory;
        DirectoryInfo dir = new DirectoryInfo(websitePath);
        StandSize(dir);
    }

    void StandSize(DirectoryInfo dir)
    {
        foreach (var FileInfo in dir.GetFiles())
            fileTotalSize += FileInfo.Length;
        foreach (var subDir in dir.GetDirectories())
            StandSize(subDir);
    }

}