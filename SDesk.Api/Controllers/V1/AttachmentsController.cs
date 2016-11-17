using System.Linq;
using System.Net;
using System.Web.Http;
using SDesc.DataAccess;
using SDesk.DataAccess;
using SDesk.Model;

namespace SDesk.Api.Controllers.V1
{
    [RoutePrefix("api/mails/{id:int:min(1)}")]
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
        [Route("attachments", Name = "AttachmentsOfMail")]
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
        [Route("attachments/{attId:int:min(1)}", Name = "AttachmentOfMail")]
        public IHttpActionResult AttachmentOfMail(int id, int attId)
        {
            var attachment = _attachmentRepository.SearchFor(a => a.MailId == id).FirstOrDefault(a => a.Id == attId);
            return Ok(attachment);
        }

        //PUT api/mails/{id}/attachments/{attId}
        [HttpPut]
        [Route("attachments/{attId:int:min(1)}", Name = "UpdateAttachment")]
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
        [Route("attachments", Name = "CreateAttachment")]
        public IHttpActionResult CreateAttachment(int id, Attachment model)
        {
            model.MailId = id;
            var result = _attachmentRepository.Add(model);
            _attachmentRepository.SaveChanges();
            return Created("/api/mails/" + id + "/attachments/" + result.Id, result);
        }

        //DELETE api/mails/{id}/attachments/{attId}
        [HttpDelete]
        [Route("attachments/{attId:int:min(1)}", Name = "DeleteAttachment")]
        public IHttpActionResult DeleteAttachment(int id)
        {
            _attachmentRepository.Delete(id);
            _attachmentRepository.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
