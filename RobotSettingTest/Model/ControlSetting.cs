using GPK.Lib.Enum;
using iText.IO.Util;
using RobotSettingTest.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSettingTest.Model
{
    public class ControlSettingPathInfo
    {
        /// <summary>
        /// 站台名稱
        /// </summary>
        public string Site { get; set; }
        /// <summary>
        /// 機器人們 (常駐、備援)
        /// </summary>
        public string Robots { get; set; }
        /// <summary>
        /// 機器人編號 (Ex: 101, 102)
        /// </summary>
        public RobotNo RobotNo { get; set; }
        /// <summary>
        /// 是否為補單機器人
        /// </summary>
        public bool IsPlus { get; set; }
        public ControlSettingPathInfo(string site, string robots, RobotNo robotNo, bool isPlus)
        {
            Site = site;
            Robots = robots;
            RobotNo = robotNo;
            IsPlus = isPlus;
        }
        /// <summary>
        /// Get File Path
        /// </summary>
        /// <returns>File Path</returns>
        public string GetPath()
        {
            var robotPos = Site.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "RobotCenter" : "Robot";
            var posfix = GetPosFix(RobotNo, IsPlus);
            var robotFileName = $"{RobotNo}Control{posfix}.setting";
            return $"{Site}\\{robotPos}\\{Robots}\\App_Data\\{robotFileName}";

            string GetPosFix(RobotNo robotNo, bool isPlus)
            {
                switch (robotNo)
                {
                    case RobotNo.Robot102:
                    case RobotNo.Robot103:
                        if (isPlus) { return "Plus"; }
                        else { return "New"; }
                    case RobotNo.Robot104:
                        if (isPlus) { return ""; }
                        else { return "New"; }
                    default:
                        throw new ArgumentOutOfRangeException(robotNo.ToString());
                }
            }
        }
        public bool Equals(ControlSettingPathInfo other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (GetType() != other.GetType())
            {
                return false;
            }
            return (Site == other.Site) && (Robots == other.Robots) && (RobotNo == other.RobotNo) && (IsPlus == other.IsPlus);
        }
        public override bool Equals(object obj)
        {
            if (obj is null) { return false; }
            return Equals(obj as ControlSettingPathInfo);
        }
        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Append(Site.GetHashCode());
            hash.Append(Robots.GetHashCode());
            hash.Append(RobotNo.GetHashCode());
            hash.Append(IsPlus.GetHashCode());
            return hash.GetHashCode();
        }
    }
    public class RobotControlSetting : ControlSettingPathInfo
    {
        public Dictionary<RobotDataType, int> TaskNumbers { get; set; }
        public RobotControlSetting(string site, string robots, RobotNo robotNo, bool isPlus) : base(site, robots, robotNo, isPlus) { }
    }
    public class ControlSetting : ControlSettingPathInfo
    {
        /// <summary>
        /// 機器人所屬類型
        /// </summary>
        public RobotDataType RobotDataType { get; set; }
        /// <summary>
        /// 執行數量
        /// </summary>
        public int Task { get; set; }
        public ControlSetting(string site, string robots, RobotNo robotNo, bool isPlus, RobotDataType robotDataType, int task)
            : base(site, robots, robotNo, isPlus)
        {
            RobotDataType = robotDataType;
            Task = task;
        }
        /// <summary>
        /// Get All Content
        /// </summary>
        /// <returns>All Content</returns>
        public override string ToString()
        {
            return
                $"{nameof(Site)}: {Site}, " +
                $"{nameof(Robots)}: {Robots}, " +
                $"{nameof(RobotNo)}: {RobotNo}, " +
                $"{nameof(IsPlus)}: {IsPlus}, " +
                $"{nameof(RobotDataType)}: {RobotDataType}, " +
                $"{nameof(Task)}: {Task}";
        }
    }
}
