using System.Web.Http;
using SDesc.DataAccess;
using SDesk.DataAccess;
using SDesk.Model;

namespace SDesk.Api.Controllers
{
    public class JiraItemsController : ApiController
    {
        private readonly IRepository<JiraItem> _jiraItemRepository;

        public JiraItemsController()
        {
            _jiraItemRepository = new Repository<JiraItem>();
        }

        //GET api/jiraitems/{id}   
        //GET api/jiraitems must return the same as for GET api/jiraitems/1
        public IHttpActionResult GetJiraItem(int id = 1)
        {
            var mail = _jiraItemRepository.Get(id);
            if (mail == null)
                return NotFound();
            return Ok(mail);
        }
    }
}
