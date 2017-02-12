using System.Web.Http;

namespace AccountManagement.WebApi.Controllers
{
    public class HomeController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok("test");
        }

        [Authorize]
        public IHttpActionResult Get(string id)
        {
            return Ok("test2");
        }
    }
}
