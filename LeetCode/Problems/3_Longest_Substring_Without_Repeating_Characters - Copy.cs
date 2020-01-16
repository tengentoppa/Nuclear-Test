using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Problems
{
    class Solution3
    {
        public static void Run()
        {
            var input = Console.ReadLine();
            var result = LengthOfLongestSubstring(input);
            Console.WriteLine(result);
        }
        public static int LengthOfLongestSubstring(string s)
        {
            var result = 0;
            var index = new int[128];
            for (int j = 0, i = 0; j < s.Length; j++)
            {
                i = Math.Max(index[s[j]], i);
                result = Math.Max(result, j - i + 1);
                index[s[i]] = j + 1;
            }
            return result;
        }
    }
}
