using System;
using System.Collections.Generic;

namespace ByteTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new List<byte>() { 0x10, 0x01 }.ToInt32();
            Console.WriteLine(data);
        }
    }

    static class NumConvert
    {
        public static int ToInt32(this List<byte> input)
        {
            if (BitConverter.IsLittleEndian)
            {
                input.Reverse();
            }
            if (input.Count > 4)
            {
                input = input.GetRange(0, 4);
            }
            else if (input.Count < 4)
            {
                var emptyArray = new byte[4 - input.Count];
                input.AddRange(emptyArray);
            }

            return BitConverter.ToInt32(input.ToArray());
        }
    }
}
