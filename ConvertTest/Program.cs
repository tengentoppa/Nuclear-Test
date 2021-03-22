using System;

namespace ConvertTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new byte[] {0x00, 0x0a, 0x02, 0x03 };
            Console.WriteLine(BitConverter.ToString(a, 1));

            Console.ReadKey();
        }
    }
}
