using Nt.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Nt.BLL
{
    public static class NtUtility
    {
        public static int GetPageCount(int recordsCount, int pageSize)
        {
            int rawPageCount = recordsCount / pageSize;
            if (recordsCount % pageSize > 0)
                rawPageCount++;
            return rawPageCount;
        }

        /// <summary>
        /// join an int array into a string with specified separator
        /// </summary>
        /// <param name="separator">separator</param>
        /// <param name="value">string</param>
        /// <returns></returns>
        public static string Join(string separator, int[] value)
        {
            if (value == null || value.Length < 1)
                return string.Empty;
            var t = string.Empty;
            t += value[0];
            for (int i = 1; i < value.Length; i++)
                t += separator + value[i];
            return t;
        }

        /// <summary>
        /// separate a string into an array of int32
        /// </summary>
        /// <param name="separator">separator</param>
        /// <param name="value">string</param>
        /// <returns></returns>
        public static int[] SeparateToIntArray(char separator, string value)
        {
            if (string.IsNullOrEmpty(value))
                return new int[0];
            var arr = value.Split(separator);
            int[] arr_int32 = new int[arr.Length];
            for (var i = 0; i < arr.Length; i++)
                arr_int32[i] = Convert.ToInt32(arr[i]);
            return arr_int32;
        }

        #region ListItemSelect

        /// <summary>
        /// 为ListItem集合做选中处理
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="selecteds">值为数字</param>
        /// <returns>处理后的原数据</returns>
        public static List<NtListItem> ListItemSelect(List<NtListItem> data, int[] selecteds)
        {
            if (data == null
                || selecteds == null
                || selecteds.Length < 1)
                return data;
            foreach (var i in data)
            {
                i.Selected = false;
                foreach (var j in selecteds)
                {
                    if (i.Value.Equals(j.ToString()))
                    {
                        i.Selected = true; break;
                    }
                }
            }
            return data;
        }

        /// <summary>
        /// 为ListItem集合做选中处理
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="selecteds">值为字符串</param>
        /// <returns>处理后的原数据</returns>
        public static List<NtListItem> ListItemSelect(List<NtListItem> data, string[] selecteds)
        {
            if (data == null
                || selecteds == null
                || selecteds.Length < 1)
                return data;
            foreach (var i in data)
            {
                i.Selected = false;
                foreach (var j in selecteds)
                {
                    if (j.Equals(i.Value))
                    {
                        i.Selected = true;
                        break;
                    }
                }
            }
            return data;
        }

        /// <summary>
        ///  为ListItem集合做选中处理 适合单选
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="selected">值为数字</param>
        /// <returns>处理后的原数据</returns>
        public static List<NtListItem> ListItemSelect(List<NtListItem> data, int selected)
        {
            if (data == null || selected < 0)
                return data;
            foreach (var i in data)
            {
                i.Selected = false;
                if (i.Value == selected.ToString())
                    i.Selected = true;
            }
            return data;
        }

        /// <summary>
        ///  为ListItem集合做选中处理 适合单选
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="selected">值为字符串,多值时用英文逗号隔开</param>
        /// <returns>处理后的原数据</returns>
        public static List<NtListItem> ListItemSelect(List<NtListItem> data, string selected)
        {
            if (data == null
                || string.IsNullOrEmpty(selected))
                return data;
            string[] selected_array = selected.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return ListItemSelect(data, selected_array);
        }

        #endregion

        /// <summary>
        /// 获取截断的字符串
        /// </summary>
        /// <param name="inputString">原始字符串</param>
        /// <param name="len">保留的长度</param>
        /// <returns>截断的字符串</returns>
        public static string GetSubString(string inputString, int len)
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }

                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (tempLen > len)
                    break;
            }
            //如果截过则加上半个省略号
            byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
            if (mybyte.Length > len)
                tempString += "…";
            return tempString;
        }

        /// <summary>
        /// remove all html tags and get substring
        /// </summary>
        /// <param name="text">text</param>
        /// <param name="length">length</param>
        /// <returns>modified string</returns>
        public static string SubStringWithoutHtml(string text, int length)
        {
            string c = text;
            c = RemoveHTML(c);
            c = GetSubString(c, length);
            return c;
        }

        /// <summary>
        /// Generate random digit code
        /// </summary>
        /// <param name="length">Length</param>
        /// <returns>Result string</returns>
        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            string str = string.Empty;
            for (int i = 0; i < length; i++)
                str = String.Concat(str, random.Next(10).ToString());
            return str;
        }

        /// <summary>
        /// Returns an random interger number within a specified rage
        /// </summary>
        /// <param name="min">Minimum number</param>
        /// <param name="max">Maximum number</param>
        /// <returns>Result</returns>
        public static int GenerateRandomInteger(int min = 0, int max = 2147483647)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        /// <summary>
        /// Ensure that a string doesn't exceed maximum allowed length
        /// </summary>
        /// <param name="str">Input string</param>
        /// <param name="maxLength">Maximum length</param>
        /// <param name="postfix">A string to add to the end if the original string was shorten</param>
        /// <returns>Input string if its lengh is OK; otherwise, truncated input string</returns>
        public static string EnsureMaximumLength(string str, int maxLength, string postfix = null)
        {
            if (String.IsNullOrEmpty(str))
                return str;

            if (str.Length > maxLength)
            {
                var result = str.Substring(0, maxLength);
                if (!String.IsNullOrEmpty(postfix))
                {
                    result += postfix;
                }
                return result;
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// Ensures that a string only contains numeric values
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Input string with only numeric values, empty string if input is null/empty</returns>
        public static string EnsureNumericOnly(string str)
        {
            if (String.IsNullOrEmpty(str))
                return string.Empty;

            var result = new StringBuilder();
            foreach (char c in str)
            {
                if (Char.IsDigit(c))
                    result.Append(c);
            }
            return result.ToString();
        }

        /// <summary>
        /// Ensure that a string is not null
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Result</returns>
        public static string EnsureNotNull(string str)
        {
            if (str == null)
                return string.Empty;
            return str;
        }

        /// <summary>
        /// Indicates whether the specified strings are null or empty strings
        /// </summary>
        /// <param name="stringsToValidate">Array of strings to validate</param>
        /// <returns>Boolean</returns>
        public static bool AreNullOrEmpty(params string[] stringsToValidate)
        {
            bool result = false;
            Array.ForEach(stringsToValidate, str =>
            {
                if (string.IsNullOrEmpty(str)) result = true;
            });
            return result;
        }

        /// <summary>
        /// 删除文本中的Html标记
        /// </summary>
        /// <param name="Htmlstring">带有Html的文本</param>
        /// <returns></returns>
        public static string RemoveHTML(string Htmlstring)
        {
            string strhtml = Regex.Replace(Htmlstring, "<.+?>", "");
            strhtml = Regex.Replace(strhtml, "<br>", "", RegexOptions.IgnoreCase);
            return strhtml;
        }

        /// <summary>
        /// 彻底删除文本中的Html标记
        /// </summary>
        /// <param name="Htmlstring">带有Html的文本</param>
        /// <returns></returns>
        public static string RemoveHTML2(string Htmlstring)
        {
            //删除脚本  
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML  
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

            return Htmlstring;
        }

        /// <summary> 
        /// 获取文中图片地址 
        /// </summary> 
        /// <param name="content">内容</param> 
        /// <returns>地址字符串</returns> 
        public static string GetImageUrl(string content)
        {
            int mouse = 0;
            int cat = 0;
            string imageLabel = "";
            string imgSrc = "";
            string[] Attributes;
            do                                                                    //得到第一张图片的连接作为主要图片 
            {
                cat = content.IndexOf("<IMG", mouse, StringComparison.OrdinalIgnoreCase);
                if (cat < 0)
                    break;
                mouse = content.IndexOf('>', cat);
                imageLabel = content.Substring(cat, mouse - cat);                //图像标签  

                Attributes = imageLabel.Split(' ');                                //将图片属性分开 

                foreach (string temp_Attributes in Attributes)                    //得到图片地址属性 
                    if (temp_Attributes.IndexOf("src", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        imgSrc = temp_Attributes.ToString();
                        break;
                    }
                imgSrc = imgSrc.Substring(imgSrc.IndexOf('"') + 1, imgSrc.LastIndexOf('"') - imgSrc.IndexOf('"') - 1);    //丛地址属性中提取地址 

            } while (imgSrc == "" && cat > 0);

            return (imgSrc);
        }

        /// <summary>
        /// 由ListItem的列表数据获取Js形式的对象数组，类似[{text:'',value:12},...]
        /// </summary>
        /// <param name="list">数据</param>
        /// <returns></returns>
        public static string GetJsObjectArrayFromList(NtListItem prepend, List<NtListItem> list, params NtListItem[] append)
        {
            StringBuilder html = new StringBuilder();
            html.Append("[");

            int j = 0;
            if (list != null)
            {
                if (prepend != null)
                {
                    html.AppendFormat("{{text:'{0}',value:'{1}'}}", prepend.Text, prepend.Value);
                    j++;
                }

                foreach (var i in list)
                {
                    if (j > 0)
                        html.Append(",");
                    html.AppendFormat("{{text:'{0}',value:'{1}'}}", i.Text, i.Value);
                    j++;
                }

                if (append != null)
                {
                    foreach (var i in append)
                    {
                        html.Append(",");
                        html.AppendFormat("{{text:'{0}',value:'{1}'}}", i.Text, i.Value);
                    }
                }
            }
            html.Append("]");

            return html.ToString();
        }

        public static string GetJsObjectArrayFromList(List<NtListItem> list)
        {
            return GetJsObjectArrayFromList(null, list);
        }

        /// <summary>
        /// 解析请求字符串，返回键值对
        /// </summary>
        /// <param name="query">查询字符串</param>
        /// <returns></returns>
        public static Dictionary<string, string> ParseQuery(string query)
        {
            if (string.IsNullOrEmpty(query)
                || query == "?")
                return null;
            if (query[0] == '?')
                query = query.Substring(1);
            Dictionary<string, string> dict = new Dictionary<string, string>();
            string[] parts = query.Split(new char[] { '&' });
            for (int i = 0; i < parts.Length; i++)
            {
                int firstEqual = parts[i].IndexOf('=');
                if (firstEqual < 0)
                {
                    dict.Add(parts[i], string.Empty);
                }
                else
                {
                    dict.Add(parts[i].Substring(0, firstEqual), parts[i].Substring(firstEqual + 1));
                }
            }
            return dict;
        }

        /// <summary>
        /// 生成随机密码
        ///  33-126  
        /// 可见常用字符
        /// </summary>
        /// <param name="size">长度(>14)</param>
        /// <returns></returns>
        public static string GetRandomPwd(int size)
        {
            string pwd = string.Empty;
            size = size < 15 ? 15 : size;
            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                pwd += (char)(rand.Next(33, 126));
            }
            return pwd;
        }

        /// <summary>
        /// size=20
        /// </summary>
        /// <returns></returns>
        public static string GetRandomPwd()
        {
            return GetRandomPwd(20);
        }

    }
}
