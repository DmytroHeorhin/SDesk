using System.Net;
using System.Web.Http;
using SDesc.DataAccess;
using SDesk.DataAccess;
using SDesk.Model;
using System;
using SDesk.Api.RouteAttributes;

namespace SDesk.Api.Controllers.V1
{  
    public class MailsController : ApiController
    {
        private readonly IRepository<Mail> _mailRepository;
        
        public MailsController()
        {
            _mailRepository = new Repository<Mail>();
        }

        //GET /api/mails
        [HttpGet]
        [VersionedRoute("api/mails", 1)]
        public IHttpActionResult GetAllMails()
        {
            //throw new Exception("Test exception");
            var mails = _mailRepository.GetAll();
            if (mails == null)
                return NotFound();
            return Ok(mails);
        }

        //GET /api/mails/{id}
        [HttpGet]
        [VersionedRoute("api/mails/{id}", 1)]
        public IHttpActionResult GetMail(int id)
        {
            var mail = _mailRepository.Get(id);
            if (mail == null)
                return NotFound();
            return Ok(mail);
        }

        //PUT /api/mails/{id}
        [HttpPut]
        [VersionedRoute("api/mails/{id}", 1)]
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
        [HttpPost]
        [VersionedRoute("api/mails", 1)]
        public IHttpActionResult PostMail(Mail model)
        {
            if (model == null)
                return BadRequest();
            var result = _mailRepository.Add(model);
            _mailRepository.SaveChanges();
            return Created("/api/mails/" + result.Id, result);
        }

        //DELETE /api/mails/{id}
        [HttpDelete]
        [VersionedRoute("api/mails/{id}", 1)]
        public IHttpActionResult DeleteMail(int id)
        {
            _mailRepository.Delete(id);
            _mailRepository.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
