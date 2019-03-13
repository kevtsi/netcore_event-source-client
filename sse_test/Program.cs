using System;
using System.IO;
using System.Net;

namespace sse_test
{
    class Program
    {
        static WebClient wc;
        static Uri uri = new Uri("http://localhost:3000/events");

        static void Main(string[] args)
        {
            ListenForServerEvents();

            Console.ReadKey();
        }

        private static void ServerSentEvent(object sender, OpenReadCompletedEventArgs args)
        {
            using (var sr = new StreamReader(args.Result))
            {
                Console.WriteLine(sr.ReadLine());
            }

            ListenForServerEvents();
        }

        private static void ListenForServerEvents()
        {
            wc = new WebClient();
            wc.OpenReadAsync(uri);
            wc.OpenReadCompleted += ServerSentEvent;
        }
    }
}
