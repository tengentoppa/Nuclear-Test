using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JsonConvertTest
{
    class Program
    {
        const string RootPath = @"File\";
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            test4();
            Console.ReadKey();
        }

        static void test0()
        {
            var rawData = File.ReadAllText($"{RootPath}test.txt");
            var data = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, List<Data>>>>(rawData);
            var result = data.SelectMany(t1 => t1.Value
                .SelectMany(t2 => t2.Value
                    .Select((t3) => { t3.Date = t1.Key; t3.Player = t2.Key; return t3; })));

            var r = JsonConvert.SerializeObject(result);
            Console.WriteLine(r);
        }
        static void test1()
        {
            var data = "{\"Success\":true,\"Code\":\"D0200\",\"Message\":\"新增存款匹配记录成功\",\"Data\":[{\"Id\":201910000000002521}]}";
            var result = JsonConvert.DeserializeObject<MfbRsp>(data);
        }
        static void test2()
        {
            var data = "{\"Success\":true,\"Code\":\"D0300\",\"Message\":\"获取有效的收款卡列表成功\",\"Data\":[{\"BankCode\":\"ABC\",\"BankName\":\"农业银行\",\"CardNumber\":\"6230520680064897975\",\"CardName\":\"杨嫦娥\",\"Balance\":null,\"BalanceLastUpdate\":null}]}";
            var result = JsonConvert.DeserializeObject<MfbRsp>(data);
            var test = JsonConvert.SerializeObject(result.Data);
        }
        static void test3()
        {
            var rawData = File.ReadAllText($"{RootPath}test1.txt");
            var data = JsonConvert.DeserializeObject<List<MfbBankCode>>(rawData);
            Console.WriteLine(data.Select((t) => { return $"{t.BankCode}|{t.BankName}"; }).Aggregate((a, b) => $"{a}\n{b}"));
        }
        static void test4()
        {
            var test = new Dictionary<string, object>()
            {
                {"1",false },
                {"2",123 },
                {"3",321.00m },
                {"4","test" }
            };
            var tmp = JsonConvert.SerializeObject(test);
            Console.WriteLine(tmp);
            var t = JsonConvert.DeserializeObject<Dictionary<string, object>>(tmp);

            foreach (var foo in t)
            {
                Console.WriteLine($"{foo.Key}: {foo.Value.ToString()}, Type-{foo.Value.GetType()}");
            }
        }
    }

    #region Container
    class MfbBankCode
    {
        public string BankCode;
        public string BankName;
    }
    class MfbRsp
    {
        public string Success;
        public string Code;
        public string Message;
        public List<MfbData> Data;
    }
    class MfbData
    {
        public string BankCode;
        public string BankName;
        public string CardNumber;
        public string CardName;
        public string Balance;
        public string BalanceLastUpdate;
    }
    public class Data
    {
        public string Date { get; set; }
        public string Player { get; set; }
        public string Sn { get; set; }
        public string Gid { get; set; }
        public string Sid { get; set; }
        public string Tm { get; set; }
        public string Bet { get; set; }
        public string Dm { get; set; }
        public string Win { get; set; }
        public string Bn { get; set; }
        public string Gb { get; set; }
        public string Jp { get; set; }
    }
    #endregion
}
