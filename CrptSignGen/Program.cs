using CRPT.SharedLib.Extensions;
using CRPT.SharedLib.Helper;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace CrptSignGen
{
    class Program
    {
        private static SignHelper SignHelper { get { return new SignHelper(ShaExtensions.ToSha384, "SecretKey", sortByProperty: true); } }
        static void Main(string[] args)
        {
            Console.Write("SecretKey: ");
            var secretKey = Console.ReadLine();
            while (true)
            {
                Console.WriteLine(new string('=', 30));
                var inputData = "";
                string line;
                Console.WriteLine("Input data: ");
                while (!string.IsNullOrEmpty(line = Console.ReadLine()))
                {
                    inputData += line + "\n";
                }
                var result = GetSign(inputData, secretKey);
                Console.WriteLine(new string('-', 30));
                Console.WriteLine(result);
            }
        }

        static string GetSign(string inputData, string secretKey)
        {
            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(inputData);
            data.Remove("RequestTime");
            data.Remove("Sign");

            data.Add("RequestTime", DateTime.Now.ToUnixTimestamp());
            var result = SignHelper.InsertSign(data, secretKey);

            return JsonSerializer.Serialize(result);
        }
    }
}
