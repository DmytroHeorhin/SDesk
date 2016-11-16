using System;
using Microsoft.Owin.Hosting;
using SDesk.Api.Controllers;

namespace SDesc.Api.SelfHost
{
    public class Program
    {
        static void Main(string[] args)
        {
            //to make the self-host app see controllers in other assembly
            Type mailsControllerType = typeof(MailsController);
            //Type attachmentsControllerType = typeof(AttachmentsController);
            //Type jiraItemsControllerType = typeof(JiraItemsController);

            using (WebApp.Start<Startup>("http://localhost:9000/"))
            {
                Console.WriteLine("Web server is running.");
                Console.ReadLine();
            }          
        }
    }
}
