using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNameComparer
{
    class Program
    {
        const string RootPath = @"File\";
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var bankList = File.ReadAllLines($"{RootPath}BankList.txt").ToList();

            var rawData = File.ReadAllText($"{RootPath}test.txt");
            var data = JsonConvert.DeserializeObject<List<MfbBankCode>>(rawData);
            var banks = data.Select(t => t.BankName);

            Console.WriteLine(data.Select((t) => { return $"case \"{t.BankName}\": return \"{t.BankCode}\";"; }).Aggregate((a, b) => $"{a}\n{b}"));

            var result = bankList.Where(t => !banks.Contains(t)).Aggregate((a,b)=> $"{a}\n{b}");
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }

    class MfbBankCode
    {
        public string BankCode;
        public string BankName;
    }

}
