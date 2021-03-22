using AdventOfCode.Exams;
using AdventOfCode.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Factory
    {
        private const string BasePath = "InputFiles/";
        public enum ExamNames
        {
            Day1_TheTyrannyoftheRocketEquation
        }
        public static IProccessor GetProccessor(ExamNames exam)
        {
            var path = $"{BasePath}{exam}.txt";
            return exam switch
            {
                ExamNames.Day1_TheTyrannyoftheRocketEquation => new Day1_TheTyrannyoftheRocketEquation(path),
                _ => null,
            };
        }
    }
}
