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
    public class MailsController : ApiController
    {
        private readonly IRepository<Mail> _mailRepository;

        public MailsController()
        {
            _mailRepository = new Repository<Mail>();
        }

        public IHttpActionResult GetAllMails()
        {
            var mails = _mailRepository.GetAll();
            if (mails == null)
                return NotFound();
            return Ok(mails);
        }

        public IHttpActionResult GatMail(int id)
        {
            var mail = _mailRepository.Get(id);
            if (mail == null)
                return NotFound();
            return Ok(mail);
        }

        public IHttpActionResult PutMail(int id, Mail model)
        {
            model.Id = id;
            _mailRepository.Update(model);
            _mailRepository.SaveChanges();
            return Ok(model);
        }

        public IHttpActionResult PostMail(Mail model)
        {
            var result = _mailRepository.Add(model);
            _mailRepository.SaveChanges();
            return Ok(result);
        }

        public IHttpActionResult DeleteMail(int id)
        {
            _mailRepository.Delete(id);
            _mailRepository.SaveChanges();
            return Ok(id.ToString());
        }
    }
}
