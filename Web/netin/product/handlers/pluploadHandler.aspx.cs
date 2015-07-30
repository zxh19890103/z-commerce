using Nt.BLL;
using Nt.DAL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class netin_product_handlers_pluploadHandler : System.Web.UI.Page
{
    protected override void OnLoad(EventArgs e)
    {
        MediaService ms = new MediaService();
        ms.CurrentUploadCategory = "product";
        ms.AllowedUploadFormats = MediaService.ALLOWED_PIC_FORMAT;
        object newid = 0;
        string thumbnail = string.Empty;

        NtJson json = new NtJson();

        try
        {
            int productId;
            NtJson.EnsureNumber("productId", "参数错误:productId", out productId);

            json.ErrorCode = NtJson.OK;
            json.Message = "上传成功";

            bool isRemote = false;
            bool.TryParse(Request["isremote"], out isRemote);

            if (!isRemote)
            {
                ms.AsyncUpload();
                thumbnail = ms.MakeThumbnail(ms.FileUrl, 120);

                json.Json["imgUrl"] = ms.FileUrl;
                json.Json["size"] = ms.FileSize;
                json.Json["thumbnail"] = thumbnail;
            }
            else
            {
                string url = string.Empty;
                NtJson.EnsureUrl("remoteUrl", out url);
                json.Json["imgUrl"] = url;
                json.Json["size"] = 0;
                json.Json["thumbnail"] = url;
            }

            #region insert a record

            Picture p = new Picture();
            p.InitData();
            p.Src = json.Json["imgUrl"].ToString();
            p.Display = true;
            string sql = "insert into Nt_Picture([Display],[DisplayOrder],[Title],[Description],[Src],[Alt])";
            sql += "values(@Display,@DisplayOrder,@Title,@Description,@Src,@Alt);\r\n";
            sql += "declare @newId int;\r\n set @newId=(Select @@IDENTITY);\r\n";
            sql += "insert into Nt_Product_Picture_Mapping(ProductId,PictureId)values(" + productId + ",@newid)\r\n";
            sql += "select @newId;\r\n";
            SqlParameter[] parameters = new SqlParameter[12];
            parameters[0] = new SqlParameter("@Display", p.Display);
            parameters[1] = new SqlParameter("@DisplayOrder", p.DisplayOrder);
            parameters[2] = new SqlParameter("@Title", p.Title);
            parameters[3] = new SqlParameter("@Description", p.Description);
            parameters[4] = new SqlParameter("@Src", p.Src);
            parameters[5] = new SqlParameter("@Alt", p.Alt);
            newid = SqlHelper.ExecuteScalar(SqlHelper.GetConnSting(),
                CommandType.Text, sql.ToString(), parameters);
            #endregion

        }
        catch (Exception ex)
        {
            json.ErrorCode = NtJson.ERROR;
            json.Message = ex.Message;
        }

        Response.Write(json);

    }
}