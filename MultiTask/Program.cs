using System;
using System.Threading.Tasks;

namespace MultiTask
{
    class Program
    {
        static void Main(string[] args)
        {
            testAsync(1, 2, 3).Wait();
            testAsync1(4, 5, 6);
            Console.ReadKey();
        }

        static async Task testAsync(int i1, int i2, int i3)
        {
            var task1 = Task.Run(() => WaitSecondsAsync(i1));
            var task2 = Task.Run(() => WaitSecondsAsync(i2));
            var task3 = Task.Run(() => WaitSecondsAsync(i3));
            WriteConsole("Task1 Start");
            var result1 = await task1;
            WriteConsole("Task1 End");
            WriteConsole("Task2 Start");
            var result2 = await task2;
            WriteConsole("Task2 End");
            WriteConsole("Task3 Start");
            var result3 = await task3;
            WriteConsole("Task3 End");
            Console.WriteLine(result1);
            Console.WriteLine(result2);
            Console.WriteLine(result3);
        }
        static void testAsync1(int i1, int i2, int i3)
        {
            var task1 = Task.Run(() => WaitSecondsAsync(i1));
            var task2 = Task.Run(() => WaitSecondsAsync(i2));
            var task3 = Task.Run(() => WaitSecondsAsync(i3));
            Task.WaitAll(task1, task2, task3);
            Console.WriteLine(task1.Result);
            Console.WriteLine(task2.Result);
            Console.WriteLine(task3.Result);
        }

        static async Task<int> WaitSecondsAsync(int i)
        {
            await Task.Delay(i * 1000);
            return i;
        }

        static void WriteConsole(string input)
        {
            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss.fff")}] {input}");
        }
    }
}
