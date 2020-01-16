using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace NuclearTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var headerData = new Dictionary<string, object>{
                {"alg","HS256" },
                {"typ","JWT" }
            };
            var payloadData = new Dictionary<string, object>
            {
                {"user_id",007 },
                {"name","Robin" }
            };
            var header = JsonConvert.SerializeObject(headerData);
            var payload = JsonConvert.SerializeObject(payloadData);
            var secret = "robinisawsome";
            var result = GetJwtTokenSha256String(header, payload, secret);
            Console.WriteLine(result);

            var token = result;

            var rtnToken = token.Split('.');
            var backHeader = rtnToken[0];
            var backData = rtnToken[1];
            var backSign = rtnToken[2];

            var head = backHeader.ToBase64UrlDecode();
            var data = backData.ToBase64UrlDecode();

            Console.WriteLine(head);
            Console.WriteLine(data);
            Console.WriteLine(GetJwtTokenSha256String(head, data, secret));
            Console.WriteLine(token == GetJwtTokenSha256String(head, data, secret));

            Console.ReadLine();
        }

        public static string GetJwtTokenSha256String(string headerJsonString, string payloadJsonString, string key)
        {
            var headerToBase64UrlEncode = headerJsonString.ToBase64UrlEncode();
            var payloadToBase64UrlEncode = payloadJsonString.ToBase64UrlEncode();
            return string.Format("{0}.{1}.{2}",
                headerToBase64UrlEncode,
                payloadToBase64UrlEncode,
                EncryptByHmacSha256(
                    string.Format("{0}.{1}",
                        headerToBase64UrlEncode,
                        payloadToBase64UrlEncode), key, true).Replace('+', '-').Replace('/', '_').Replace("=", ""));

        }
        public static string EncryptByHmacSha256(string text, string key, bool toBase64 = false, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            using (HMACSHA256 hmacsha256 = new HMACSHA256(encoding.GetBytes(key)))
            {
                return EncryptByHmac(hmacsha256, text, key, toBase64, encoding);
            }
        }
        private static string EncryptByHmac(HMAC hmac, string text, string key, bool toBase64, Encoding encoding)
        {
            byte[] data = hmac.ComputeHash(encoding.GetBytes(text));

            if (toBase64)
            {
                return Convert.ToBase64String(data);
            }

            //Create a new String to store the bytes to string
            string sbinary = "";

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sbinary += data[i].ToString("X2"); // hex format
            }
            return sbinary.ToString();
        }
    }
    public static class Extention
    {
        public static string ToBase64UrlEncode(this string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            // Special "url-safe" base64 encode.
            return Convert.ToBase64String(inputBytes)
            .Replace('+', '-')
            .Replace('/', '_')
            .Replace("=", "");
        }
        public static string ToBase64UrlDecode(this string input)
        {
            if (String.IsNullOrWhiteSpace(input))
                throw new ArgumentException(nameof(input));

            var output = input;
            output = output.Replace('-', '+'); // 62nd char of encoding
            output = output.Replace('_', '/'); // 63rd char of encoding
            switch (output.Length % 4) // Pad with trailing '='s
            {
                case 0:
                    break; // No pad chars in this case
                case 2:
                    output += "==";
                    break; // Two pad chars
                case 3:
                    output += "=";
                    break; // One pad char
                default:
                    throw new FormatException("Illegal base64url string.");
            }
            var converted = Convert.FromBase64String(output); // Standard base64 decoder
            var result = System.Text.Encoding.UTF8.GetString(converted);
            return result;
        }
    }
}
