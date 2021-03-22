using System;
using System.Collections.Generic;
using System.Text;

namespace CRPT.SharedLib.Extensions
{
    public static class ShaExtensions
    {
        public static string ToSha384(this string str)
        {
            using (var cryptoSha384 = System.Security.Cryptography.SHA384.Create())
            {
                //將字串編碼成 UTF8 位元組陣列
                var bytes = Encoding.UTF8.GetBytes(str);

                //取得雜湊值位元組陣列
                var hash = cryptoSha384.ComputeHash(bytes);

                ////取得 MD5
                var result = BitConverter.ToString(hash)
                    .Replace("-", "")
                    .ToUpper();

                return result;
            }
        }
    }
}
