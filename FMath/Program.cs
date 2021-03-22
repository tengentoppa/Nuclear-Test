using System;

namespace FMath
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write(GetFMath(30));
        }

        static int GetFMath(int count)
        {
            if (count < 0) { throw new Exception(); }
            int result = 1;
            int shift = 0;
            for (int i = 1; i < count; i++)
            {
                Console.Write($"{i}, {shift}+{result} = ");
                var tmp = result;
                result += shift;
                shift = tmp;
                Console.WriteLine(result);
            }
            return result;
        }
    }
}
