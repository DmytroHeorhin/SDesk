using System;
using System.Collections.Generic;
using System.Data.Entity;
using SDesk.Model;

namespace SDesk.DataAccess.EF
{
    public class DbInitializer : DropCreateDatabaseAlways<SDeskContext>
    {
        protected override void Seed(SDeskContext context)
        {
            var mails = new List<Mail>
            {
                new Mail
                {
                    Subject = "Hello, world!",
                    Sender = "John Doe",
                    To = "Mary Moe",
                    Cc = "July Duly",
                    Body = "Lorem Ipsum Dolor Sit Amet",
                    StatusId = 1,
                    Priority = Priority.Medium,
                    Received = DateTime.Now,
                    Saved = DateTime.Now
                },
                new Mail
                {
                    Subject = "1Hello, world!",
                    Sender = "1John Doe",
                    To = "1Mary Moe",
                    Cc = "1July Duly",
                    Body = "1Lorem Ipsum Dolor Sit Amet",
                    StatusId = 2,
                    Priority = Priority.High,
                    Received = DateTime.Now,
                    Saved = DateTime.Now
                },
                new Mail
                {
                    Subject = "2Hello, world!",
                    Sender = "2John Doe",
                    To = "2Mary Moe",
                    Cc = "2July Duly",
                    Body = "2Lorem Ipsum Dolor Sit Amet",
                    StatusId = 3,
                    Priority = Priority.Low,
                    Received = DateTime.Now,
                    Saved = DateTime.Now
                }
            };

            context.Mails.AddRange(mails);

            var attachments = new List<Attachment>
            {
                new Attachment
                {
                    MailId = 1,
                    FileName = "Photo",
                    FileExtention = "jpg",
                    Path = "C:/downloads"
                },

                new Attachment
                {
                    MailId = 1,
                    FileName = "Materials",
                    FileExtention = "rar",
                    Path = "C:/downloads"
                }
            };

            context.Attachments.AddRange(attachments);

            context.JiraItems.Add(new JiraItem {JiraSourceId = 1, JiraNumber = 42, RequestIdType = 1});

            context.SaveChanges();
        }
    }
}