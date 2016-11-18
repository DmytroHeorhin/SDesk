using SDesc.DataAccess;
using SDesk.Api.RouteAttributes;
using SDesk.DataAccess;
using SDesk.Model;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace SDesk.Api.Controllers.V2
{
    
    public class Mails2Controller : ApiController
    {
        private readonly IRepository<Mail> _mailRepository;
        private readonly IRepository<Attachment> _attachmentRepository;

        public Mails2Controller()
        {
            _mailRepository = new Repository<Mail>();
            _attachmentRepository = new Repository<Attachment>();
        }

        //GET /api/mails
        [VersionedRoute("api/mails", 2)]
        public IHttpActionResult GetAllMails()
        {
            var mails = _mailRepository.GetAll();           
            if (mails == null)
                return NotFound();
            mails.Add(new Mail { Subject = "Test mail from v2" });
            return Ok(mails);
        }

        //GET /api/mails/{id}
        [VersionedRoute("api/mails/{id}", 2)]
        public IHttpActionResult GetMail(int id)
        {
            var mail = _mailRepository.Get(id);
            if (mail == null)
                return NotFound();
            mail.Subject = "Test mail from v2";
            return Ok(mail);
        }

        //PUT /api/mails/{id}
        [VersionedRoute("api/mails/{id}", 2)]
        public IHttpActionResult PutMail(int id, Mail model)
        {
            if (model == null)
                return BadRequest();
            model.Id = id;
            _mailRepository.Update(model);
            _mailRepository.SaveChanges();
            model.Subject = "Test mail from v2";
            return Ok(model);
        }

        //POST /api/mails
        [VersionedRoute("api/mails/", 2)]
        public IHttpActionResult PostMail(Mail model)
        {
            if (model == null)
                return BadRequest();
            var result = _mailRepository.Add(model);
            _mailRepository.SaveChanges();
            result.Subject = "Test mail from v2";
            return Created("/api/mails/" + result.Id, result);
        }

        //DELETE /api/mails/{id}
        [VersionedRoute("api/mails/{id}", 2)]
        public IHttpActionResult DeleteMail(int id)
        {
            _mailRepository.Delete(id);
            _mailRepository.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        //GET api/mails/{id}/attachments
        //GET api/mails/{id}/attachments/?extension={ext}
        //GET api/mails/{id}/attachments/?extension={ext}?status={status}
        [HttpGet]
        [VersionedRoute("api/mails/{id}/attachments", 2)]
        public IHttpActionResult AttachmentsOfMail(int id, string extension = null, int status = 0)
        {
            var attachments = _attachmentRepository.SearchFor(a => a.MailId == id).ToList();
            if (!string.IsNullOrEmpty(extension))
                attachments = attachments.Where(a => a.FileExtention == extension).ToList();
            if (status != 0)
                attachments = attachments.Where(a => a.StatusId == status).ToList();
            attachments.Add(new Attachment {StatusId = 99999999}); //test
            return Ok(attachments);
        }

        //GET api/mails/{id}/attachments/{attId}
        [HttpGet]
        [VersionedRoute("api/mails/{id}/attachments/{attId}", 2)]
        public IHttpActionResult AttachmentOfMail(int id, int attId)
        {
            var attachment = _attachmentRepository.SearchFor(a => a.MailId == id).FirstOrDefault(a => a.Id == attId);
            if (attachment == null)
                return NotFound();
            attachment.StatusId = 999999999; //test
            return Ok(attachment);
        }

        //PUT api/mails/{id}/attachments/{attId}
        [HttpPut]
        [VersionedRoute("api/mails/{id}/attachments/{attId}", 2)]
        public IHttpActionResult UpdateAttachment(int id, int attId, Attachment model)
        {
            if (model == null)
                return BadRequest();
            model.Id = attId;
            _attachmentRepository.Update(model);
            _attachmentRepository.SaveChanges();
            model.StatusId = 999999999; //test
            return Ok(model);
        }

        //POST api/mails/{id}/attachments
        [HttpPost]
        [VersionedRoute("api/mails/{id}/attachments", 2)]
        public IHttpActionResult CreateAttachment(int id, Attachment model)
        {
            model.MailId = id;
            var result = _attachmentRepository.Add(model);
            _attachmentRepository.SaveChanges();
            return Created("/api/mails/" + id + "/attachments/" + result.Id, result);
        }

        //DELETE api/mails/{id}/attachments/{attId}
        [HttpDelete]
        [VersionedRoute("api/mails/{id}/attachments/{attId}", 2)]
        public IHttpActionResult DeleteAttachment(int id, int attId)
        {
            _attachmentRepository.Delete(attId);
            _attachmentRepository.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}
