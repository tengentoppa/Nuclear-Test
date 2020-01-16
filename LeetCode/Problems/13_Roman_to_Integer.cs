using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Problems
{
    class Solution13
    {
        public static void Run()
        {
            var input = Console.ReadLine();
            input = input.ToUpper();
            var result = RomanToInt(input);
            Console.WriteLine(result);
        }
        public static int RomanToInt(string s)
        {
            int cur, prv = 0;
            var result = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                cur = RomanCharToInt(s[i]);
                result += cur < prv ? -cur : cur;
                prv = cur;
            }
            return result;


            int RomanCharToInt(char c)
            {
                switch (c)
                {
                    case 'I': return 1;
                    case 'V': return 5;
                    case 'X': return 10;
                    case 'L': return 50;
                    case 'C': return 100;
                    case 'D': return 500;
                    case 'M': return 1000;
                    default: return 0;
                }
            }
        }
    }
}
