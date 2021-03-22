using Newtonsoft.Json;
using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace BetRecordGenerator
{
    class Program
    {
        static readonly string DestRawFileDir = $@"\\{Environment.MachineName}\SMB\Robot_Files\RawFile\Robot101\";
        static string SrcRawFilePath = "/home/axesoft_nginx/smb/Robot_Files/RawFile/Robot101";
        static string ConnectionString = "Data Source=x8play-uat.database.windows.net;Initial Catalog=CasinoRawCenter;Persist Security Info=True;User ID=axesoft;Password=zvmafjqrup/.,1029;MultipleActiveResultSets=True";

        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            ConnectionString = config.GetConnectionString("DefaultConnection");
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SrcRawFilePath = DestRawFileDir;
            }

            var parseResult = default(bool);

            Console.Write("File count: ");
            parseResult = int.TryParse(Console.ReadLine(), out int fileCount);
            if (!parseResult)
            {
                Console.WriteLine("Parse fail");
                return;
            }
            Console.Write("Records each file: ");
            parseResult = int.TryParse(Console.ReadLine(), out int recordEachFile);
            if (!parseResult)
            {
                Console.WriteLine("Parse fail");
                return;
            }

            var betRecords = GetRecords(fileCount, recordEachFile);

            foreach (var (dir, fileName, content) in betRecords)
            {
                var filePath = Path.Combine(SrcRawFilePath, dir);
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                File.WriteAllText(Path.Combine(filePath, fileName), content);
            }

            WritePath2Sql(betRecords.Select(t => Path.Combine(DestRawFileDir, t.dir, t.fileName)));

            Console.WriteLine("Jobs done.");
        }

        static void WritePath2Sql(IEnumerable<string> paths)
        {
            var queryString = "insert [CasinoRawCenter].[dbo].[ResponseRaw101]" +
                "(CreateTime,SwitchTime,InProgress,RobotDataType,Content,IsFinished,Counts)" +
                "values(@CreateTime,@SwitchTime,@InProgress,@RobotDataType,@Content,@IsFinished,@Counts)";
            var time = DateTime.Now.AddHours(-12);

            using var conn = new SqlConnection(ConnectionString);
            using SqlCommand command = new SqlCommand(queryString, conn);

            command.Parameters.Add("@CreateTime", SqlDbType.DateTime);
            command.Parameters.Add("@SwitchTime", SqlDbType.DateTime);
            command.Parameters.Add("@InProgress", SqlDbType.DateTime);
            command.Parameters.Add("@RobotDataType", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@Content", SqlDbType.NVarChar, 508);
            command.Parameters.Add("@IsFinished", SqlDbType.Bit);
            command.Parameters.Add("@Counts", SqlDbType.Int);

            conn.Open();

            foreach (var path in paths)
            {
                command.Parameters["@CreateTime"].Value = time;
                command.Parameters["@SwitchTime"].Value = time;
                command.Parameters["@InProgress"].Value = time;
                command.Parameters["@RobotDataType"].Value = "TyAll";
                command.Parameters["@Content"].Value = path.Replace('/', '\\');
                command.Parameters["@IsFinished"].Value = 0;
                command.Parameters["@Counts"].Value = 1;

                command.ExecuteNonQuery();
            }

            conn.Close();
            conn.Dispose();
        }

        static IEnumerable<(string dir, string fileName, string content)> GetRecords(int fileCount, int recordEachFile)
        {
            var time = default(DateTime);
            var destRawFileDir = default(string);
            var destRawFileName = default(string);
            var rawData = default(TyBetRecordDetail);
            Random random = new Random((int)time.Ticks);

            var result = new List<(string dir, string fileName, string content)>();
            while (fileCount-- > 0)
            {
                time = DateTime.Now.AddHours(-12);
                destRawFileDir = Path.Combine("TY", $"{time:yyyyMM}", $"{time:dd}", $"{time:HH}");
                destRawFileName = $@"TyAll-{time.Ticks}.raw";
                var contents = new List<TyBetRecordDetail>();
                for (int i = 0; i < recordEachFile; i++)
                {
                    rawData = new TyBetRecordDetail
                    {
                        KindID = 119,
                        CurScore = 186.05m,
                        AllBet = 0,
                        UserCount = 0,
                        LineCode = "gj",
                        GameEndTime = time.AddSeconds(10),
                        CardValue = "13a340614132081c110319316242b212842a0a3c0c2752c3912361d",
                        ServerID = 11901,
                        ChairID = 17,
                        Profit = 0,
                        Revenue = 0,
                        GameID = $"10445-{random.Next(0, 999999):D6}-1591933834-120-17",
                        CellScore = 0,
                        TableID = random.Next(0, 999999),
                        Accounts = "test92017",
                        ChannelID = 10045,
                        GameStartTime = time
                    };
                    contents.Add(rawData);
                }

                result.Add((destRawFileDir, destRawFileName, JsonConvert.SerializeObject(contents)));
            }
            return result;
        }

        void OldFunc()
        {
            //var time = default(DateTime);
            //var destRawFilePath = default(string);
            //var destRawFilePath2 = default(string);
            //var destRawFileName = default(string);
            //var rawData = default(TyBetRecordDetail);
            //string destRawFileDir = @"\\10.0.0.4\SMB\Robot_Files\RawFile\Robot101\";
            //string srcRawFilePath = @"/home/axesoft_nginx/smb/Robot_Files/RawFile/Robot101";
            //Random random = new Random((int)time.Ticks);

            //var conn = new SqlConnection("Data Source=x8play-uat.database.windows.net;Initial Catalog=CasinoRawCenter;Persist Security Info=True;User ID=axesoft;Password=zvmafjqrup/.,1029;MultipleActiveResultSets=True");
            //conn.Open();

            //while (fileCount-- > 0)
            //{
            //    time = DateTime.Now.AddHours(-12);
            //    destRawFilePath = Path.Combine("TY", $@"{time:yyyyMM}", $@"{time:dd}", $@"{time:HH}");
            //    destRawFilePath2 = $@"TY\{time:yyyyMM}\{time:dd}\{time:HH}\";
            //    destRawFileName = $@"TyAll-{time.Ticks}.raw";
            //    rawData = new TyBetRecordDetail
            //    {
            //        KindID = 119,
            //        CurScore = 186.05m,
            //        AllBet = 0,
            //        UserCount = 0,
            //        LineCode = "gj",
            //        GameEndTime = time.AddSeconds(10),
            //        CardValue = "13a340614132081c110319316242b212842a0a3c0c2752c3912361d",
            //        ServerID = 11901,
            //        ChairID = 17,
            //        Profit = 0,
            //        Revenue = 0,
            //        GameID = $"10445-{random.Next(0, 999999):D6}-1591933834-120-17",
            //        CellScore = 0,
            //        TableID = random.Next(0, 999999),
            //        Accounts = "test92017",
            //        ChannelID = 10045,
            //        GameStartTime = time
            //    };
            //    Directory.CreateDirectory(Path.Combine(srcRawFilePath, destRawFilePath));
            //    File.WriteAllText(Path.Combine(srcRawFilePath, destRawFilePath, destRawFileName), JsonConvert.SerializeObject(new List<TyBetRecordDetail> { rawData }));

            //    SqlCommand command = new SqlCommand("insert [CasinoRawCenter].[dbo].[ResponseRaw101]" +
            //        "(CreateTime,SwitchTime,InProgress,RobotDataType,Content,IsFinished,Counts)" +
            //        "values(@CreateTime,@SwitchTime,@InProgress,@RobotDataType,@Content,@IsFinished,@Counts)", conn);

            //    command.Parameters.Add("@CreateTime", SqlDbType.DateTime).Value = time;
            //    command.Parameters.Add("@SwitchTime", SqlDbType.DateTime).Value = time;
            //    command.Parameters.Add("@InProgress", SqlDbType.DateTime).Value = time;
            //    command.Parameters.Add("@RobotDataType", SqlDbType.NVarChar, 20).Value = "TyAll";
            //    command.Parameters.Add("@Content", SqlDbType.NVarChar, 508).Value = destRawFileDir + destRawFilePath2 + destRawFileName;
            //    command.Parameters.Add("@IsFinished", SqlDbType.Bit).Value = 0;
            //    command.Parameters.Add("@Counts", SqlDbType.Int).Value = 1;

            //    command.ExecuteNonQuery();
            //}

            //conn.Close();
            //conn.Dispose();
        }
    }
}
