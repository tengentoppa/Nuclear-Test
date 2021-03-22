using System;

namespace StaticTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(TestClass.A);
            TestClass.A = "wahaha";

            var test1 = new TestClass() { B = "test1" };
            var test2 = new TestClass() { B = "test2" };


            //Console.WriteLine(test1.A);
            Console.WriteLine(test1.B);
            Console.WriteLine(test1.C);
            Console.WriteLine(test2.B);
            Console.WriteLine(test2.C);

            TestClass.A = "wolala";
            Console.WriteLine(test1.C);
            Console.WriteLine(test2.C);
        }
    }

    class TestClass
    {
        public static string A { get; set; } = "wow";
        public string B { get; set; }
        public string C { get { return A; } }

        // static 無法存取 non static，因為 non static 可能會有多個實體
        //public static string D { get { return B; } }
    }
}
