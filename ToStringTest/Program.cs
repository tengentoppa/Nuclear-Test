using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToStringTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = DateTime.Parse("0001-01-01T00:00:00");
            var foo = t == DateTime.MinValue;
            var test = true;
            if (test) { throw new Exception(); }
            Console.WriteLine(111.9345m.ToString("0"));
            Console.ReadKey();
            var cardInfo = new MfbRsp();
            cardInfo.Data.First(x => x.CardType == "1");
        }
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
        public string CardType;
        public string BankCode;
        public string BankName;
        public string CardNumber;
        public string CardName;
    }
}
