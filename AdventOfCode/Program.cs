using System;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var proccessor = Factory.GetProccessor(Factory.ExamNames.Day1_TheTyrannyoftheRocketEquation);
            Console.WriteLine(proccessor.RunPart1());
            Console.WriteLine(proccessor.RunPart2());
        }
    }
}
