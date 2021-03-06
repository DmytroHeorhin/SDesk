﻿using System.Linq;
using System.Net;
using System.Web.Http;
using SDesc.DataAccess;
using SDesk.DataAccess;
using SDesk.Model;
using SDesk.Api.RouteAttributes;

namespace SDesk.Api.Controllers.V1
{   
    public class AttachmentsController : ApiController
    {
        private readonly IRepository<Attachment> _attachmentRepository;

        public AttachmentsController()
        {
            _attachmentRepository = new Repository<Attachment>();
        }

        //GET api/mails/{id}/attachments
        //GET api/mails/{id}/attachments/?extension={ext}
        //GET api/mails/{id}/attachments/?extension={ext}?status={status}
        [HttpGet]
        [VersionedRoute("api/mails/{id}/attachments", 1)]
        public IHttpActionResult AttachmentsOfMail(int id, string extension = null, int status = 0)
        {
            var attachments = _attachmentRepository.SearchFor(a => a.MailId == id).ToArray();
            if (!string.IsNullOrEmpty(extension))
                attachments = attachments.Where(a => a.FileExtention == extension).ToArray();
            if (status != 0)
                attachments = attachments.Where(a => a.StatusId == status).ToArray();
            return Ok(attachments);
        }

        //GET api/mails/{id}/attachments/{attId}
        [HttpGet]
        [VersionedRoute("api/mails/{id}/attachments/{attId:int:min(1)}", 1)]
        public IHttpActionResult AttachmentOfMail(int id, int attId)
        {
            var attachment = _attachmentRepository.SearchFor(a => a.MailId == id).FirstOrDefault(a => a.Id == attId);
            return Ok(attachment);
        }

        //PUT api/mails/{id}/attachments/{attId}
        [HttpPut]
        [VersionedRoute("api/mails/{id}/attachments/{attId:int:min(1)}", 1)]
        public IHttpActionResult UpdateAttachment(int id, int attId, Attachment model)
        {
            if (model == null)
                return BadRequest();
            model.Id = attId;
            _attachmentRepository.Update(model);
            _attachmentRepository.SaveChanges();
            return Ok(model);
        }

        //POST api/mails/{id}/attachments
        [HttpPost]
        [VersionedRoute("api/mails/{id}/attachments", 1)]
        public IHttpActionResult CreateAttachment(int id, Attachment model)
        {
            model.MailId = id;
            var result = _attachmentRepository.Add(model);
            _attachmentRepository.SaveChanges();
            return Created("/api/mails/" + id + "/attachments/" + result.Id, result);
        }

        //DELETE api/mails/{id}/attachments/{attId}
        [HttpDelete]
        [VersionedRoute("api/mails/{id}/attachments/{attId:int:min(1)}", 1)]
        public IHttpActionResult DeleteAttachment(int id, int attId)
        {
            _attachmentRepository.Delete(id);
            _attachmentRepository.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
