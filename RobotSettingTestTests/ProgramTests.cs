using RobotSettingTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace RobotSettingTest.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        const string SettingPath = @"C:\gjsoft\AllNetGame\Other\RobotSetting\Src";
        [TestMethod()]
        public void GetGameCategoryEnumTest()
        {
            var result = Program.GetGameCategoryEnums;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void CheckRootPathTest()
        {
            var temp = Program.CheckRootPath(SettingPath);
            Assert.IsTrue(temp.IsFound && Directory.Exists(temp.RootPath));
        }

        [TestMethod()]
        public void GetAllSettingFileTest()
        {
            var result = Program.GetAllSettingFile(SettingPath);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void FileNameToRobotNoTest()
        {
            var result = Program.FileNameToRobotNo("Robot103ControlPlus") == DTO.RobotNo.Robot103;
            Assert.IsTrue(result);
        }
    }
}