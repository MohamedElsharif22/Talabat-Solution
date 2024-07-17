using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public ActionResult Error(int code)
        {
            return NotFound(new ApiResponse(404, "EndPoint Not Exist!"));
        }
    }
}
