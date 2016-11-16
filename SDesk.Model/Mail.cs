using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Epam.Sdesk.Model
{
    public class Mail
    {
        public long Id { get; set; }
        public string Subject { get; set; }
        public string Sender { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Body { get; set; }
        public int AttachmentId { get; set; } //save on disk, thus we have to have an object for that
        public Priority Priority { get; set; } //enum
        [Column(TypeName = "datetime2")]
        public DateTime? Received { get; set; } //date when we received email
        [Column(TypeName = "datetime2")]
        public DateTime? Saved { get; set; } //date when we saved mail entity

    }
}
