using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nt.BLL
{
    public static class RegexUtility
    {
        /// <summary>
        /// Verifies that a string is in valid e-mail format
        /// </summary>
        /// <param name="email">Email to verify</param>
        /// <returns>true if the string is a valid e-mail address and false if it's not</returns>
        public static bool IsValidEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                return false;
            email = email.Trim();
            var result = Regex.IsMatch(email, "^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$", RegexOptions.IgnoreCase);
            return result;
        }

        /// <summary>
        /// 判断字符串是否是形似1,2,3,4,5,...
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static bool IsInt32Range(string input)
        {
            if (String.IsNullOrEmpty(input))
                return false;
            input = input.Trim();
            var result = Regex.IsMatch(input, @"^([0-9]\d*)(,[0-9]\d*)*$");
            return result;
        }

        /// <summary>
        /// 是否电话号码，包括固话和手机
        /// </summary>
        /// <returns></returns>
        public static bool IsPhoneNumber(string input)
        {
            if (String.IsNullOrEmpty(input))
                return false;
            input = input.Trim();
            var result = Regex.IsMatch(input, @"(^[0-9]{3,4}\-[0-9]{3,8}$)|(^[0-9]{3,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)");
            return result;
        }

        /// <summary>
        /// 是否纯中文汉字
        /// </summary>
        /// <returns></returns>
        public static bool IsPureChinese(string input)
        {
            if (String.IsNullOrEmpty(input))
                return false;
            input = input.Trim();
            var result = Regex.IsMatch(input, @"^[\u4e00-\u9fa5]{0,}$");
            return result;
        }

        /// <summary>
        /// 是否中文
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsChinese(string input)
        {
            if (String.IsNullOrEmpty(input))
                return false;
            input = input.Trim();
            var result = Regex.IsMatch(input, @"^([\u4e00-\u9fa5]+|[a-zA-Z0-9]+)$");
            return result;
        }

        /// <summary>
        /// 是否省份证ID
        /// </summary>
        /// <returns></returns>
        public static bool IsPersonID(string input)
        {
            if (String.IsNullOrEmpty(input))
                return false;
            input = input.Trim();
            var result = Regex.IsMatch(input, @"^\d{15}|\d{18}$");
            return result;
        }

        /// <summary>
        /// 是否是IP格式
        /// </summary>
        /// <returns></returns>
        public static bool IsIP(string input)
        {
            if (String.IsNullOrEmpty(input))
                return false;
            input = input.Trim();
            var result = Regex.IsMatch(input, @"^([0-9]{1,3})\.([0-9]{1,3})\.([0-9]{1,3})\.([0-9]{1,3})$");
            return result;
        }

        /*
         pattern:
         * d>=0,d>0,d<=0,d<0,d,
         * f>=0,f>0,f<=0,f<0,f,
         * aA,A,a,aA0,
         * w,
         */
        public static bool IsSpecifiedNumber(string pattern, string input)
        {
            if (String.IsNullOrEmpty(input))
                return false;
            input = input.Trim();
            string regexPattern = string.Empty;
            switch (pattern)
            {
                case "d>=0":
                    regexPattern = @"^\d+$";
                    break;
                case "d>0":
                    regexPattern = @"^[0-9]*[1-9][0-9]*$";
                    break;
                case "d<=0":
                    regexPattern = @"^((-\d+)|(0+))$";
                    break;
                case "d<0":
                    regexPattern = @"^-[0-9]*[1-9][0-9]*$";
                    break;
                case "d":
                    regexPattern = @"^-?\d+$";
                    break;
                case "f>=0":
                    regexPattern = @"^\d+(\.\d+)?$";
                    break;
                case "f>0":
                    regexPattern = @"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$";
                    break;
                case "f<=0":
                    regexPattern = @"^((-\d+(\.\d+)?)|(0+(\.0+)?))$";
                    break;
                case "f<0":
                    regexPattern = @"^(-(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*)))$";
                    break;
                case "f":
                    regexPattern = @"^(-?\d+)(\.\d+)?$";
                    break;
                case "w":
                    regexPattern = @"^\w+$";
                    break;
                case "aA":
                    regexPattern = @"^[A-Za-z]+$";
                    break;
                case "A":
                    regexPattern = @"^[A-Z]+$";
                    break;
                case "a":
                    regexPattern = @"^[a-z]+$";
                    break;
                case "aA0":
                    regexPattern = @"^[A-Za-z0-9]+$";
                    break;
                default:
                    break;
            }

            if (regexPattern == string.Empty)
                return false;
            var result = Regex.IsMatch(input, regexPattern);
            return result;
        }

        /// <summary>
        ///  用户名
        /// </summary>
        /// <returns></returns>
        public static bool IsUserName(string input)
        {
            if (String.IsNullOrEmpty(input))
                return false;
            input = input.Trim();
            var result = Regex.IsMatch(input, @"^[a-zA-Z][a-zA-Z0-9]{4,20}$");
            return result;
        }

        /// <summary>
        /// 密码
        /// </summary>
        public static bool IsPassword(string input)
        {
            if (String.IsNullOrEmpty(input))
                return false;
            input = input.Trim();
            var result = Regex.IsMatch(input, @"^(\w){6,20}$");
            return result;
        }

        /// <summary>
        /// 是否是Url格式
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public static bool IsUrl(string input)
        {
            if (String.IsNullOrEmpty(input))
                return false;
            input = input.Trim();
            var result = Regex.IsMatch(input, @"^(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?$");
            return result;
        }

        /// <summary>
        /// 是否是绝对Url格式
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public static bool IsAbsUrl(string input)
        {
            if (String.IsNullOrEmpty(input))
                return false;
            input = input.Trim();
            var result = Regex.IsMatch(input, @"^(/[\w\.-]+)+\.(\w+)$");
            return result;
        }

    }

}
