using System.Web.Http;
using SDesc.DataAccess;
using SDesk.DataAccess;
using SDesk.Model;

namespace SDesk.Api.Controllers.V1
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
        [ActionName("JiraItemInt")]
        public IHttpActionResult GetJiraItem(int id = 1)
        {
            var item = _jiraItemRepository.Get(id);
            if (item == null)
                return NotFound();
            item.JiraSourceId = 2; //to identify which method was invoked
            return Ok(item);
        }

        //GET api/jiraitems/<Jira-Id : “Jira-1034”>
        //[Route("api/jiraitems/{id:regex(^jira-[1-9][0-9]*$)}")]
        //[Route("api/jiraitems/{id:jiraid}")]
        [HttpGet]
        [ActionName("JiraItemString")]
        public IHttpActionResult JiraItemByJiraId(string id)
        {
            id = id.Substring(5);
            int intId;
            if (!int.TryParse(id, out intId))
                return BadRequest();
            var item = _jiraItemRepository.Get(intId);
            if (item == null)
                return NotFound();
            item.JiraSourceId = 1; //to identify which method was invoked
            return Ok(item);
        }               
    }
}
