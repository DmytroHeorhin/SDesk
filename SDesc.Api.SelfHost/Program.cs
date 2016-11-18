using System;
using Microsoft.Owin.Hosting;

namespace SDesc.Api.SelfHost
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:9000/"))
            {
                Console.WriteLine("Web server is running.");
                Console.ReadLine();
            }          
        }
    }
}
