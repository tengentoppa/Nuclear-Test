using System;
using System.Net.Http;

namespace HttpClientTest
{
    class Program
    {
        static void Main(string[] args)
        {

            using HttpClient client = new HttpClient();
            var resultString = client.GetAsync("https://google.com")
             .Result
             .Content
             .ReadAsStringAsync()
             .Result;
            Console.WriteLine("resultString: " + resultString);
        }
    }
}
