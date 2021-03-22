using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace NuclearTest
{
    class Program
    {
        class tttt
        {
            public long? a;
            public long? b;
        }
        static async Task Main(string[] args)
        {
            //var text = File.ReadAllLines(@"C:\Users\simon.chen\Downloads\Telegram Desktop\admin_access.log");

            //Console.WriteLine(string.Join("\n", text.Where(x => x.Contains("POST /login"))));
            //Console.ReadLine();
            //var test = new Test();
            //try
            //{
            //    await test.Foo();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //}
            Console.WriteLine(0.00000005000m.ToString());
            Console.WriteLine(0.00000005000m.ToString("G29"));
            Console.WriteLine((0.00000005000m/ 1.000000000000000000000000000000000m).ToString());
            Console.ReadLine();
        }


        #region End
        class test
        {
            public Dictionary<int, string> tmp = new Dictionary<int, string>();
        }
        static void GroupTest()
        {
            Console.WriteLine(new Foobar(1, 2, 3).Equals(new Foobar(1, 2, 4)));
            var foo = new List<Foobar>();
            for (int i = 0; i < 3; i++)
            {
                foo.Add(new Foobar(i, 1, i * 7));
                foo.Add(new Foobar(i, 1, i * 11));
            }
            Console.WriteLine(foo[0] == foo[1]);
            var bar = foo.ToLookup(o => new Foobar(o.A, o.B, o.X), o => o.X);
            Console.WriteLine(bar
                .Select(a => $"{a.Key.A}, {a.Key.B}:\n" +
                    $"{a.Select(b => b.ToString()).Aggregate((x, y) => { return $"{x}\n{y}"; })}")
                .Aggregate((x, y) => { return $"{x}\n{y}"; }));
        }
        class Foobar
        {
            public int A;
            public int B;
            public int X;
            public Foobar(int a, int b, int x)
            {
                A = a;
                B = b;
                X = x;
            }
            public override string ToString()
            {
                return $"{nameof(A)}: {A}, {nameof(B)}: {B}, {nameof(X)}: {X}";
            }
            public bool Equals(Foobar other)
            {
                if (other is null)
                {
                    return false;
                }

                // Optimization for a common success case.
                if (ReferenceEquals(this, other))
                {
                    return true;
                }
                if (GetType() != other.GetType())
                {
                    return false;
                }
                return (A == other.A) && (B == other.B);
            }
            public override bool Equals(object obj)
            {
                if (obj is null) { return false; }
                return Equals(obj as Foobar);
            }
            public override int GetHashCode()
            {
                return A * 31 * B.GetHashCode();
            }

            public static bool operator ==(Foobar lhs, Foobar rhs)
            {
                if (ReferenceEquals(lhs, null))
                {
                    if (ReferenceEquals(rhs, null))
                    {
                        return true;
                    }
                    return false;
                }
                return lhs.Equals(rhs);
            }
            public static bool operator !=(Foobar lhs, Foobar rhs)
            {
                return !(lhs == rhs);
            }
        }
        static void EqualsTest()
        {
            int testTimes = 999999999;

            var sw = new System.Diagnostics.Stopwatch();
            string test1 = "123";
            int test2 = 123;

            sw.Start();
            for (int i = 0; i < testTimes; i++)
            {
                var t = Equals(test1, test2);
            }
            sw.Stop();
            Console.WriteLine($"Equal: {sw.ElapsedMilliseconds}ms");
            sw.Reset();

            sw.Start();
            for (int i = 0; i < testTimes; i++)
            {
                string.Equals(test1, test1);
            }
            sw.Stop();

            Console.WriteLine($"string.Equal: {sw.ElapsedMilliseconds}ms");
        }
        static void test1()
        {
            var test = "1*2*3|3*2*1".Split('|').Select(t =>
            {
                var tmp = t.Split('*');
                Array.Resize(ref tmp, 5);
                return new RawGameInfo(tmp[0], tmp[1], tmp[2], tmp[3], tmp[4]);
            });
        }
        static void test2()
        {
            var foo = new test();
            for (int i = 0; i < 10; i++)
            {
                foo.tmp.Add(i, i.ToString());
            }
            var bar = foo.tmp;
            Console.WriteLine(string.Join("\t", foo.tmp.Select(t => $"{t.Key},{t.Value}")));

            for (int i = 10; i < 20; i++)
            {
                bar.Add(i, i.ToString());
            }
            var a = bar[1] = "4";
            a = "5";
            Console.WriteLine(string.Join("\t", foo.tmp.Select(t => $"{t.Key},{t.Value}")));
        }

        static void GbParseTest()
        {
            var tmp = GetBetOnBySpv("G3M;Y;-1;2,5,7;3;-1;-1");
            Console.WriteLine($"{tmp.gameName}\n{tmp.betContent}\n{string.Join(";", tmp.betTypes)}");
        }
        public static (string gameName, string betContent, IEnumerable<string> betTypes) GetBetOnBySpv(string spv)
        {
            var splitedData = spv.Split(';');
            if (splitedData[0].Length != 3) { return ("N/A", $"SPV 格式不正確{spv}，必須為 XXX;Z1;Z2;.....", null); }
            var gameNameCode = splitedData[0].Substring(0, 2);
            var betContentCode = splitedData[0][2].ToString();
            var betTypes = splitedData.ToList().GetRange(2, splitedData.Length - 2);

            return (GetGbLottoGameName(gameNameCode),
                GetGbLottoBetContent(betContentCode),
                betTypes.Select(t => GetGb1LottoBetType(t)));
        }
        private static string GetGbLottoGameName(string code)
        {
            switch (code)
            {
                case "XX": return "珠仔";
                case "XM": return "珠仔單雙";
                case "XZ": return "珠仔大小";
                case "G2": return "二星組選";
                case "G3": return "三星組選三";
                case "G6": return "三星組選六";
                case "SS": return "和值";
                case "SR": return "和值七彩";
                case "SM": return "和值單雙";
                case "SZ": return "和值大小";
                default: return $"此代碼無對應名稱 {code}";
            }
        }
        private static string GetGbLottoBetContent(string code)
        {
            switch (code)
            {
                case "X": return "不分區位置";
                case "1": return "萬";
                case "2": return "千";
                case "3": return "百";
                case "4": return "十";
                case "5": return "個";
                case "F": return "前三或前二";
                case "M": return "中三";
                case "B": return "後三或後二";
                default: return $"此代碼無對應名稱 {code}";
            }
        }
        private static string GetGb1LottoBetType(string codes)
        {
            var tmp = codes.Split(',');
            return string.Join(",", tmp.Select(t => getType(t)));
            string getType(string code)
            {
                switch (code)
                {
                    case "Y": return "分位";
                    case "N": return "組選";
                    case "B": return "大";
                    case "S": return "小";
                    case "E": return "單";
                    case "O": return "雙";
                    case "1": return "紅";
                    case "2": return "橙";
                    case "3": return "黃";
                    case "4": return "綠";
                    case "5": return "藍";
                    case "6": return "靛";
                    case "7": return "紫";
                    case "-1": return "無球號";
                    default: return $"此代碼無對應名稱 {code}";
                }
            }
        }
        class RawGameInfo
        {
            private string gameName;
            private string rawKey1;
            private string rawKey2;
            private string rawKey3;
            private string rawKey4;
            private string rawKey5;

            public string GameName { get => gameName; set => gameName = $"N'{value ?? "N/A"}'"; }
            public string RawKey1 { get => rawKey1; set => rawKey1 = Value2Sql(value); }
            public string RawKey2 { get => rawKey2; set => rawKey2 = Value2Sql(value); }
            public string RawKey3 { get => rawKey3; set => rawKey3 = Value2Sql(value); }
            public string RawKey4 { get => rawKey4; set => rawKey4 = Value2Sql(value); }
            public string RawKey5 { get => rawKey5; set => rawKey5 = Value2Sql(value); }

            static string Value2Sql(string value)
            {
                return value == null ? "null" : $"N'{value}'";
            }

            public RawGameInfo(string gameName, string rawKey1 = null, string rawKey2 = null, string rawKey3 = null, string rawKey4 = null, string rawKey5 = null)
            {
                GameName = gameName;
                RawKey1 = rawKey1;
                RawKey2 = rawKey2;
                RawKey3 = rawKey3;
                RawKey4 = rawKey4;
                RawKey5 = rawKey5;
            }
        }
        #endregion
    }

    class Test
    {
        public async Task Foo()
        {
            await Bar();

            async Task Bar()
            {
                try
                {
                    await Task.Delay(5);
                    var test = new string[] { };

                    Console.WriteLine(test[2]);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine(new string('-', 15));
                    throw;
                }

            }

        }
    }
}
