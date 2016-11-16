using System.Data.Entity;
using SDesk.Model;

namespace SDesk.DataAccess.EF
{
    public class SDeskContext : DbContext
    {
        public SDeskContext() : base("SDeskConnection")
        {
            Database.SetInitializer(new DbInitializer());
        }

        public DbSet<Mail> Mails { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<JiraItem> JiraItems { get; set; }
    }
}
