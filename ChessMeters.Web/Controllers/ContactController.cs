using ChessMeters.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChessMeters.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Send([FromBody] ContactViewModel contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: implement email sending

            return Ok();
        }
    }
}
