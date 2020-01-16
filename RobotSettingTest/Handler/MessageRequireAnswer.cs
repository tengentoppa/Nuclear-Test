using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSettingTest.Handler
{
    class MessageRequireAnswer
    {
        public static MessageRequireAnswer GetInstance() { return new MessageRequireAnswer(); }
        public bool MsgYN(string msg)
        {
            //if (MessageBox.Show($"已搜尋超過 {searchDeep} 層，是否繼續？", "可能搜尋過深！", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            Console.WriteLine($"{msg}, (Y/N):\n");
            return Console.ReadLine().Equals("Y", StringComparison.Ordinal);
        }
    }
}
