using AdventOfCode.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Exams
{
    class Day1_TheTyrannyoftheRocketEquation : IProccessor
    {
        private IEnumerable<int> Datas { get; set; }
        public Day1_TheTyrannyoftheRocketEquation(string dataPath)
        {
            Datas = Parse(File.ReadAllText(dataPath));
        }

        public string RunPart1()
        {
            return GetFuel1().Aggregate((x, y) => x + y).ToString();
        }

        public string RunPart2()
        {
            return GetFuel2().Aggregate((x, y) => x + y).ToString();
        }

        public IEnumerable<int> GetFuel1()
        {
            return Datas.Select(d => formula(d));
        }

        public IEnumerable<int> GetFuel2()
        {
            return Datas.Select(d =>
            {
                var result = 0;
                while (true)
                {
                    d = formula(d);
                    if (d <= 0) { break; }
                    result += d;
                }
                return result;
            });
        }

        private int formula(int input)
        {
            return (int)Math.Floor(((float)input / 3) - 2);
        }

        private IEnumerable<int> Parse(string input)
        {
            var datas = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return datas.Select(d => int.Parse(d));
        }
    }
}
