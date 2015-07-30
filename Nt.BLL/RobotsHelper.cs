using Nt.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Nt.BLL
{
    public delegate void RenderMethod(string s, bool b, int i);

    public class RobotsHelper
    {
        RenderMethod _renderMethod;

        int _subStartPos;
        int _depth = 0;
        StreamWriter _robotsFileWriter;
        string _userAgent = "*";
        string _robotsBlock;
        string _sitemap = "/sitemap.xml";

        public string UserAgent { get { return _userAgent; } }
        public string RobotsBlock { get { return _robotsBlock; } }
        public string Sitemap { get { return _sitemap; } }

        public RobotsHelper()
        {
            _subStartPos = AppDomain.CurrentDomain.BaseDirectory.Length - 1;
        }

        /// <summary>
        /// 获取文件和目录信息
        /// </summary>
        public void OutFileAndFolderInfo(RenderMethod renderMethod)
        {
            DirectoryInfo root = new DirectoryInfo(WebHelper.MapPath("/"));
            _renderMethod = renderMethod;
            DG(root);
        }

        /// <summary>
        /// 递归访问文件和文件夹
        /// </summary>
        void DG(DirectoryInfo dir)
        {
            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                if (NtContext.Current.SysReservedDirs.IndexOf(subDir.Name.ToLower()) > -1)
                    continue;
                _renderMethod(subDir.FullName.Replace((char)92, (char)47).Substring(_subStartPos), true, _depth);
                _depth++;
                DG(subDir);
                _depth--;
            }

            foreach (FileInfo file in dir.GetFiles())
            {
                _renderMethod(file.FullName.Replace((char)92, (char)47).Substring(_subStartPos), false, _depth);
            }
        }

        /// <summary>
        /// 获取已保存数据
        /// </summary>
        public void Fetch()
        {
            string pathOnDisk = WebHelper.MapPath("/robots.txt");
            if (!File.Exists(pathOnDisk)) return;

            string[] lines = File.ReadAllLines(pathOnDisk);
            int i = NtContext.Current.SysReservedDirs.Split(',').Length;
            _userAgent = lines[0].Split(':')[1];
            StringBuilder txt = new StringBuilder();

            for (int j = i + 1; j < lines.Length - 1; j++)
                txt.AppendLine(lines[j]);

            _robotsBlock = txt.ToString();
            _sitemap = lines[lines.Length - 1].Split(':')[1];
        }

        /// <summary>
        /// 开始写/robots.txt文件
        /// </summary>
        public void PrepareReWriteRobotsFile(string userAgent)
        {
            _robotsFileWriter = new StreamWriter(WebHelper.MapPath("/robots.txt"), false, Encoding.UTF8);
            AddRobotsItem("User-agent", userAgent);
            foreach (var item in NtContext.Current.SysReservedDirs.Split(','))
            {
                AddRobotsItem("Disallow", "/" + item + "/");
            }
        }

        /// <summary>
        /// 结束
        /// </summary>
        public void CompleteReWriteRobotsFile(string sitemap)
        {
            AddRobotsItem("Sitemap", sitemap);
            _robotsFileWriter.Flush();
            _robotsFileWriter.Close();
            _robotsFileWriter.Dispose();
        }

        /// <summary>
        /// 向robots.txt文件中添加一项
        /// </summary>
        /// <param name="key">robots属性</param>
        /// <param name="value">属性值</param>
        public void AddRobotsItem(string key, string value)
        {
            _robotsFileWriter.WriteLine("{0}:{1}", key, value);
        }

        /// <summary>
        /// 向robots.txt文件中追加一段内容
        /// </summary>
        /// <param name="value">内容</param>
        public void AppendBlock(string value)
        {
            _robotsFileWriter.WriteLine(value);
        }

    }
}
