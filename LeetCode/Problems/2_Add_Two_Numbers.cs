using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeetCode.Component;

namespace LeetCode.Problems
{
    public class Solution2
    {
        public static void Run()
        {
            var l1 = new ListNode(5) { next = new ListNode(3) };
            var l2 = new ListNode(6) { next = new ListNode(6) { next = new ListNode(6) { next = new ListNode(6) { next = new ListNode(6) } } } };
            var result = AddTwoNumbers(l1, l2);
            var ans = "";
            while (result != null)
            {
                ans += $"{result.val} -> ";
                result = result.next;
            }
            ans += "null";
            Console.WriteLine(ans);
        }
        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            if (l1 == null && l2 == null) { return null; }
            var val = (l1?.val ?? 0) + (l2?.val ?? 0);
            if (val >= 10)
            {
                val -= 10;
                l1 = l1 ?? new ListNode(0) { next = new ListNode(0) };
                l1.next = l1.next ?? new ListNode(0);
                l1.next.val += 1;
            }
            return new ListNode(val) { next = AddTwoNumbers(l1?.next, l2?.next) };
        }
    }
}
