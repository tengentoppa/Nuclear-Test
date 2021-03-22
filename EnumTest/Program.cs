using System;

namespace EnumTest
{
    class Program
    {
        public enum DuType
        {
            DU1 = 0x305343,
            DU2 = 0x305041,
            DU3 = 0x305345,
            DU4 = 0x315251,
            DU5 = 0x305341,
            DU6 = 0x315251,
            DU7 = 0x305042,
        }
        static void Main(string[] args)
        {
            var data = new byte[] { 0x31, 0x52, 0x51 };
            var t = (data[0] << 16) + (data[1] << 8) + data[2];
            var d = Select(t);

            Console.WriteLine(d.ToString("X"));

            Console.ReadKey();
        }
        public static DuType Select(int duType, string rcid = "00000")
        {
            var du = (DuType)duType;
            if (du == DuType.DU4) { }

            return du;
        }
    }
}
