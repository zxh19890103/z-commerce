using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.Model;
using Nt.DAL;
using System.Data.SqlClient;
using System.Data;
using Nt.Model.View;
using System.IO;

namespace Nt.BLL
{
    public class UserService : BaseService
    {

        #region Authorized

        public bool Authorized(int userLevel, int permission)
        {
            var level = DbAccessor.GetById<UserLevel>(userLevel);
            if (level == null)
                return false;
            if (level.IsAdmin)
                return true;
            string filter = string.Format("UserlevelId={0} And PermissionId={1}", userLevel, permission);
            int existed = DbAccessor.GetRecordCount("Permission_UserLevel_Mapping", filter);
            return existed > 0;
        }

        public bool Authorized(int userLevel, string permissionSysN)
        {
            var level = DbAccessor.GetById<UserLevel>(userLevel);
            if (level == null)
                return false;
            if (level.IsAdmin)
                return true;
            string sql = string.Format("Select Id from View_User_Permission Where UserlevelId={0} And SystemName='{1}'\r\n",
                userLevel, permissionSysN);
            object existed = SqlHelper.ExecuteScalar(sql);
            return existed != null;
        }

        #endregion

        /// <summary>
        /// 重设密码
        /// </summary>
        /// <param name="id">用户id</param>
        /// <param name="newpass">新密码</param>
        public void ResetPassword(int id, string newpass)
        {
            SecurityService ss = new SecurityService();
            string md5pass = ss.Md5(newpass);
            string sql = string.Format("update [{0}] Set [Password]='{1}' Where ID={2}",
                DbAccessor.GetModifiedTableName("User"), md5pass, id);
            SqlHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 寻找userid的所有隶属用户的id
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetAllSubUser(int userID)
        {
            List<User> all = DbAccessor.GetList<User>();
            StringBuilder holder = new StringBuilder();
            FindSubUser(all, userID, holder);
            if (holder.Length > 1)
                holder.Remove(0, 1);//去掉前面的一个逗号
            return holder.ToString();
        }

        /// <summary>
        /// 递归
        /// </summary>
        void FindSubUser(List<User> all, int userID, StringBuilder holder)
        {
            holder.Append("," + userID);
            foreach (var sub in all.Where(x => x.CreatedUserId == userID))
            {
                FindSubUser(all, sub.Id, holder);
            }
        }

        /// <summary>
        /// 获取指定用户组所有的权限
        /// </summary>
        /// <param name="level"></param>
        /// <returns>,1,2,3,4,</returns>
        public string GetPermissionRecordsByLevel(int level)
        {
            string ids = ",";
            string sql = string.Format(
                "Select [PermissionId] from [{0}] where UserLevelId={1}",
                DbAccessor.GetModifiedTableName("Permission_UserLevel_Mapping"),
                level);
            using (SqlDataReader rs = SqlHelper.ExecuteReader(
                SqlHelper.GetConnection(),
                CommandType.Text, sql))
            {
                while (rs.Read())
                {
                    ids += rs[0].ToString();
                    ids += ",";
                }
            }
            return ids;
        }

        /// <summary>
        /// 保存授权
        /// </summary>
        /// <param name="permissions"></param>
        /// <param name="level"></param>
        public void SaveAuthorize(string permissions, int level)
        {
            if (string.IsNullOrEmpty(permissions))
                return;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("Delete From [{1}] Where [UserLevelId]={0};\r\n",
                level,
                DbAccessor.GetModifiedTableName("Permission_UserLevel_Mapping"));
            foreach (var i in permissions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                sql.AppendFormat("INSERT INTO [{2}] (PermissionId,UserLevelId)VALUES({0},{1});\r\n",
                    i, level,
                    DbAccessor.GetModifiedTableName("Permission_UserLevel_Mapping"));
            SqlHelper.ExecuteNonQuery(sql.ToString());
        }


        int _depth = 0;
        List<Permission> _menu = null;
        /// <summary>
        /// 创建导航
        /// </summary>
        /// <param name="level">int</param>
        public void CreateMenu(int level)
        {
            var levelM = DbAccessor.GetById<UserLevel>(level);
            if (levelM == null)//没有此记录
                return;
            if (levelM.IsAdmin)
                _menu = DbAccessor.GetList<Permission>("Display=1", "DisplayOrder");
            else
                _menu = DbAccessor
                    .GetList<View_User_Permission>("Display=1 And UserLevelId=" + level, "DisplayOrder")
                    .Cast<Permission>().ToList();

            string path = string.Format("/app_data/menu/{0}.txt", level);//0--level
            path = WebHelper.MapPath(path);//取物理位置

            _depth = 0;

            using (StreamWriter sw = File.CreateText(path))
            {
                CreateSubMenu(0, sw);
            }
        }

        /// <summary>
        /// 输出次级导航
        /// </summary>
        /// <param name="fid"></param>
        /// <param name="sw"></param>
        void CreateSubMenu(int fid, StreamWriter sw)
        {
            var sub = _menu.Where(x => x.FatherId == fid);
            if (sub.Count() == 0) return;
            _depth++;
            sw.Write("<ul class=\"sidebar-catalog sidebar-catalog-depth-");
            sw.Write(_depth);
            sw.WriteLine("\">");
            foreach (var item in sub)
            {
                if (item.IsCategory)//文件夹
                {
                    sw.WriteLine("<li class=\"sidebar-catalog-item\">");
                    sw.Write("<a href=\"javascript:;\" onclick=\"openSubMenu(this);\" id=\"menu-item-");
                    sw.Write(item.Id);
                    sw.Write("\"><i class=\"sidebar-catalog-item-ico\"></i>");
                    sw.Write("<span class=\"sidebar-catalog-item-text\">");
                    sw.Write(item.Name);
                    sw.Write("<br/><font class=\"font-size-12 Arial\">");
                    sw.Write(item.EnglishName);
                    sw.Write("</font></span><i class=\"sidebar-catalog-item-arrow sidebar-catalog-item-arrow-down\"></i></a>");
                    CreateSubMenu(item.Id, sw);
                    sw.WriteLine("</li>");
                }
                else//非文件夹
                {
                    sw.WriteLine("<li class=\"sidebar-catalog-item\">");
                    sw.Write("<a href=\"javascript:;\" data-item-father-id=\"");
                    sw.Write(item.FatherId);
                    sw.Write("\" src=\"");
                    sw.Write(item.Src);
                    sw.Write("\" onclick=\"setAnchor(this);\"><span>");
                    sw.Write(item.Name);
                    sw.WriteLine("<br/><font class=\"font-size-9 Arial\">");
                    sw.Write(item.EnglishName);
                    sw.Write("</font></span>");
                    sw.Write("</a>");
                    sw.WriteLine("</li>");
                }
            }
            sw.WriteLine("</ul>");
            _depth--;
        }
    }
}
