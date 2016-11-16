using Epam.Sdesk.Model;
using System.Data.Entity;

namespace SDesc.DataAccess.EF
{
    public class SDeskContext : DbContext
    {
        public SDeskContext() : base("SDeskConnection")
        {
            Database.SetInitializer(new DbInitializer());
        }

        public DbSet<Mail> Mails { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
    }
}
