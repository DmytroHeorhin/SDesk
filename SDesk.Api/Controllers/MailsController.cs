using System;
using System.Net;
using System.Web;
using System.Web.Http;
using SDesc.DataAccess;
using SDesk.DataAccess;
using SDesk.Model;

namespace SDesk.Api.Controllers
{
    public class MailsController : ApiController
    {
        private readonly IRepository<Mail> _mailRepository;

        public MailsController()
        {
            _mailRepository = new Repository<Mail>();
        }

        //GET /api/mails
        public IHttpActionResult GetAllMails()
        {
            var mails = _mailRepository.GetAll();
            if (mails == null)
                return NotFound();
            return Ok(mails);
        }

        //GET /api/mails/{id}
        public IHttpActionResult GetMail(int id)
        {
            var mail = _mailRepository.Get(id);
            if (mail == null)
                return NotFound();
            return Ok(mail);
        }

        //PUT /api/mails/{id}
        public IHttpActionResult PutMail(int id, Mail model)
        {
            if (model == null)
                return BadRequest();
            model.Id = id;
            _mailRepository.Update(model);
            _mailRepository.SaveChanges();
            return Ok(model);
        }

        //POST /api/mails
        public IHttpActionResult PostMail(Mail model)
        {
            if (model == null)
                return BadRequest();
            var result = _mailRepository.Add(model);
            _mailRepository.SaveChanges();
            return Created("/api/mails/" + result.Id, result);
        }

        //DELETE /api/mails/{id}
        public IHttpActionResult DeleteMail(int id)
        {
            _mailRepository.Delete(id);
            _mailRepository.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
