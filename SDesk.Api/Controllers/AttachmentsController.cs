using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SDesc.DataAccess;
using Epam.Sdesk.Model;

namespace SDesk.Api.Controllers
{
    [RoutePrefix("api/mails")]
    public class AttachmentsController : ApiController
    {
        private readonly IRepository<Attachment> _attachmentRepository;

        public AttachmentsController()
        {
            _attachmentRepository = new Repository<Attachment>();
        }

        [HttpGet]
        [Route("{id:int}/attachments", Name = "GetAttachmentsOfMail")]
        public IHttpActionResult GetAttachmentsOfMail(int id)
        {
            var attachments = _attachmentRepository.SearchFor(a => a.MailId == id).ToArray();
            return Ok(attachments);
        }
    }
}
