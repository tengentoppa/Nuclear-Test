using GPK.Lib.Enum;
using iText.IO.Util;
using System;
using System.Collections.Generic;

namespace RobotSettingTest.DTO
{
    public enum RobotNo
    {
        NA,
        Robot101,
        Robot102,
        Robot103,
        Robot104,
        Robot2,
    }
    public class DetailSetting
    {
        public string Site { get; set; }
        public string Robots { get; set; }
        public RobotNo RobotType { get; set; }
        public GameCategoryType Category { get; set; }
        public bool IsPlus { get; set; } = false;
        Dictionary<string, object> Settings { get; set; }
    }
}
