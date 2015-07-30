using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.BLL
{
    public class SecurityService
    {
        /// <summary>
        /// 获取加密的字符串
        /// </summary>
        /// <param name="s">输入值</param>
        /// <returns></returns>
        public string Md5(string s)
        {
                //获取加密服务
                System.Security.Cryptography.MD5CryptoServiceProvider md5CSP
                    = new System.Security.Cryptography.MD5CryptoServiceProvider();
                //获取要加密的字段，并转化为Byte[]数组
                byte[] testEncrypt = System.Text.Encoding.Unicode.GetBytes(s);
                //加密Byte[]数组
                byte[] resultEncrypt = md5CSP.ComputeHash(testEncrypt);
                //将加密后的数组转化为字段(普通加密)
                //string testResult = System.Text.Encoding.Unicode.GetString(resultEncrypt);
                //作为密码方式加密
                string EncryptPWD = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "MD5");
                return EncryptPWD;
        }

        /// <summary>
        /// verify if Encrypted s equals to md5
        /// </summary>
        /// <param name="s">pwd</param>
        /// <param name="md5">Encrypted pwd</param>
        /// <returns></returns>
        public bool VerifyMd5(string s, string md5)
        {
            string md5OfS = Md5(s);
            // Create a StringComparer an comare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(md5OfS, md5))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
