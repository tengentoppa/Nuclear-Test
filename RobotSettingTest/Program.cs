using GPK.Lib.Enum;
using Newtonsoft.Json;
using RobotSettingTest.DTO;
using RobotSettingTest.Handler;
using RobotSettingTest.Model;
using RobotSettingTest.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RobotSettingTest
{
    public class Program
    {
        readonly static Logger Logger = Logger.Instance;
        readonly static MessageRequireAnswer MRA = MessageRequireAnswer.GetInstance();

        const string SettingPath = @"C:\gjsoft\AllNetGame\Other\RobotSetting\Src";
        public static IEnumerable<string> GetGameCategoryEnums => Enum.GetNames(typeof(GameCategoryType)).ToList();
        public static IEnumerable<string> GetRobotNums => Enum.GetNames(typeof(RobotNo)).ToList();
        public static IEnumerable<FileInfo> AllSettingFiles => GetAllSettingFile(SettingPath);
        public static IEnumerable<FileInfo> RobotControlSettingFiles { get => AllSettingFiles.Where(t => t.Name.Contains("Control")); }
        static void Main(string[] args)
        {
            Logger.Log(Path.AltDirectorySeparatorChar);
            Logger.Log(Path.DirectorySeparatorChar);

            //Logger.Log(RobotControlSettingFiles.Select(t => t.FullName).AggregateByNewLine());

            var robotControlSettings = GetControlSettings(RobotControlSettingFiles);

            //Logger.Log(robotControlSettings.Select(w => w.ToString()).AggregateByNewLine());

            var test = robotControlSettings.Where(t => t.RobotDataType == RobotDataType.RyAll);
            //Logger.Log(test.Select(t => t.ToString()).AggregateByNewLine());
            var tmp = test.Where(t => t.IsPlus == false && t.RobotNo == RobotNo.Robot102).First();
            tmp.Task = 3;
            //Logger.Log(test.Select(t => t.ToString()).AggregateByNewLine());

            var RobotDataTypeVision = robotControlSettings.GroupBy(t => t.RobotDataType).OrderBy(t => t.Key);


            var foo = RobotDataTypeVision.SelectMany(a => a.Select(b => b)); //解 Group

            Logger.Log(Equals(robotControlSettings, foo));
            Logger.Log(robotControlSettings.Where(t => !foo.Contains(t)).Count());
            Logger.Log(foo.Where(t => !robotControlSettings.Contains(t)).Count());
            //WriteControlSetting(foo);

            Console.ReadLine();
        }
        public static void WriteControlSetting(IEnumerable<ControlSetting> controlSettings)
        {
            var controlSettingsWithPath = controlSettings.ToLookup(o => new ControlSettingPathInfo(o.Site, o.Robots, o.RobotNo, o.IsPlus), o => o);
            Logger.Log(controlSettingsWithPath
                .Select(a => $"{a.Key}:\n\t{a.Select(b => $"{b.RobotDataType}, {b.Task}").AggregateByNewLine()}")
                .AggregateByNewLine());
        }
        public static IEnumerable<ControlSetting> GetControlSettings(IEnumerable<FileInfo> controlFiles)
        {
            if (controlFiles is null)
            {
                throw new ArgumentNullException(nameof(controlFiles));
            }

            var result = new List<ControlSetting>();
            var robotSetting = default(RobotControlSetting);
            foreach (var controlFile in controlFiles)
            {
                try
                {
                    var settingPath = controlFile.FullName;
                    var infoFromPath = settingPath.Split(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar).ToList();
                    infoFromPath.RemoveRange(0, infoFromPath.FindIndex(t => t.Equals("Src", StringComparison.Ordinal)) + 1);
                    robotSetting = ParseDetailSetting(File.ReadAllText(settingPath));
                    robotSetting.Site = infoFromPath[1];
                    robotSetting.Robots = infoFromPath[3];
                    robotSetting.RobotNo = FileNameToRobotNo(controlFile.Name);
                    robotSetting.IsPlus = controlFile.Name.IndexOf("Plus", StringComparison.OrdinalIgnoreCase) >= 0;
                    var foo = robotSetting.TaskNumbers
                        .Select(t =>
                        new ControlSetting(robotSetting.Site, robotSetting.Robots, robotSetting.RobotNo, robotSetting.IsPlus, t.Key, t.Value));
                    result.AddRange(foo);
                }
                catch (Exception e)
                {
                    Logger.Log($"誰偷塞壞東西進來?, Path: {controlFile.FullName}", e);
                    continue;
                }
            }
            return result;
        }
        /// <summary>
        /// Check and return root path
        /// </summary>
        /// <param name="path">Start path</param>
        /// <param name="searchDeep">DO NOT FILL THIS PARAM</param>
        /// <returns>Setting File Root Path</returns>
        public static (bool IsFound, string RootPath) CheckRootPath(string path, int searchDeep = 0)
        {
            searchDeep++;

            if (searchDeep > 8)
            {
                //if (MessageBox.Show($"已搜尋超過 {searchDeep} 層，是否繼續？", "可能搜尋過深！", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                if (!MRA.MsgYN($"已搜尋超過 {searchDeep} 層，可能搜尋過深！是否繼續？"))
                    return (false, null);
            }

            const string appdata = "App_Data";

            var root = new DirectoryInfo(path);
            foreach (var dir in root.EnumerateDirectories())
            {
                if (dir.Name.StartsWith(".")) { continue; }
                if (dir.Name.Equals(appdata)) { return GetTargetRoot(dir.FullName); }

                var subDInfo = new DirectoryInfo(dir.FullName);

                try
                {
                    foreach (var subDir in subDInfo.EnumerateDirectories())
                    {
                        if (dir.Name.StartsWith(".")) continue;
                        if (subDir.Name.Equals(appdata))
                            return GetTargetRoot(subDir.FullName);

                        var checkResult = CheckRootPath(subDir.FullName, searchDeep);
                        if (checkResult.IsFound)
                            return checkResult;
                    }
                }
                catch (UnauthorizedAccessException uaaEx)
                {
                    Logger.Log("喔喔，沒權限", uaaEx);
                    continue;
                }
                catch (Exception ex)
                {
                    Logger.Log("出錯了！", ex);
                    continue;
                }
            }

            return (false, null);

            // 找出上五層的 Dir
            (bool, string) GetTargetRoot(string target)
            {
                var layer = 5;
                var parentNLayer = GetParent(target, layer);

                Logger.Log($"找到 [{target}]");
                Logger.Log($"設定根目錄為上{layer}層的 [{parentNLayer}]");

                return (true, parentNLayer);

                string GetParent(string parent, int localLayer)
                {
                    var i = 0;
                    var current = parent;
                    do
                    {
                        var tmpDi = new DirectoryInfo(current);
                        current = Directory.GetParent(tmpDi.FullName).FullName;
                        i++;

                    } while (i < localLayer);

                    return current;
                }
            }
        }
        public static IEnumerable<FileInfo> GetAllSettingFile(string parentDir)
        {
            var rootPath = CheckRootPath(parentDir);
            if (!rootPath.IsFound) { Logger.Log("噗噗，找不到App_Data資料夾咧"); return new List<FileInfo>(); }

            return new DirectoryInfo(rootPath.RootPath).EnumerateDirectories()
                .Traverse(d => d.EnumerateDirectories())
                .SelectMany(d => d.EnumerateFiles());
        }
        public static RobotControlSetting ParseDetailSetting(string text)
        {
            return JsonConvert.DeserializeObject<RobotControlSetting>(text);
        }
        public static RobotNo FileNameToRobotNo(string fileName)
        {
            foreach (var robotNo in GetRobotNums)
            {
                if (fileName.Contains(robotNo)) return (RobotNo)Enum.Parse(typeof(RobotNo), robotNo);
            }
            return RobotNo.NA;
        }
        public static IEnumerable<string> GetSites(string path)
        {
            return new DirectoryInfo(path).GetDirectories()[0].GetDirectories().Select(t => t.Name);
        }
    }
}
