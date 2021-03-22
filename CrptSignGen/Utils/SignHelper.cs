using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRPT.SharedLib.Helper
{
    public class SignHelper
    {
        private readonly Func<string, string> hashGenerator;
        private readonly string splitString;
        private readonly string propKeyName;
        private readonly string propSignName;
        private readonly bool sortByProperty;
        private readonly bool ignoreEmptyValue;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propKeyName">加鹽密鑰</param>
        /// <param name="hashGenerator">採用的簽名 Func, Ex: MD5, SHA-512</param>
        /// <param name="propSignName">簽名的屬性名稱</param>
        /// <param name="splitString">字串切分的符號</param>
        /// <param name="ignoreEmptyValue">是否忽略空的鍵值組</param>
        /// <param name="sortByProperty">是否依照鍵值排列順序</param>
        public SignHelper(Func<string, string> hashGenerator, string propKeyName, string propSignName = "Sign", string splitString = "|", bool ignoreEmptyValue = false, bool sortByProperty = true)
        {
            this.hashGenerator = hashGenerator;
            this.splitString = splitString;
            this.sortByProperty = sortByProperty;
            this.propSignName = propSignName;
            this.ignoreEmptyValue = ignoreEmptyValue;
            this.propKeyName = propKeyName;
        }

        public IDictionary<string, object> InsertSign(object data, string salt = "")
        {
            var keyValues = new RouteValueDictionary(data);
            var hash = Sign(keyValues, salt);
            keyValues.Add(propSignName, hash);
            return keyValues;
        }

        public string GetHashFromData(object data)
        {
            var keyValues = new RouteValueDictionary(data);
            return keyValues[propSignName].ToString();
        }

        public string Sign(object data, string salt = "")
        {
            var keyValues = new RouteValueDictionary(data);
            return Sign(keyValues, salt);
        }

        public string Sign(IDictionary<string, object> keyValues, string salt = "")
        {
            IEnumerable<KeyValuePair<string, object>> dataList = keyValues.ToList();

            dataList = dataList.Where(d => d.Key != propSignName);

            if (ignoreEmptyValue)
            {
                dataList = dataList.Where(d =>
                {
                    var value = d.Value;
                    if (value == null)
                    {
                        return false;
                    }
                    if (value.GetType() == typeof(string))
                    {
                        return !string.IsNullOrEmpty(value.ToString());
                    }
                    return false;
                });
            }

            if (sortByProperty)
            {
                dataList = dataList.OrderBy(d => d.Key).ToList();
            }

            var saltStr = string.IsNullOrWhiteSpace(salt) ? "" : $"{splitString}{propKeyName}={salt}";
            var sign = dataList.Select(d => $"{d.Key}={d.Value}");
            var dataForHash = string.Join(splitString, sign) + saltStr;

            var result = hashGenerator(dataForHash);

            return result;
        }
    }
}
