using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ErrorController : BaseAPIController
    {
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            throw new Exception("Exception from error controller");
            return BadRequest();
        }
    }
}