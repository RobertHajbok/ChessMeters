using ChessMeters.Core.Entities;
using ChessMeters.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChessMeters.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> userManager;

        public UsersController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        [Route(nameof(GetLinkedAccounts))]
        public async Task<UserLinkedAccountsViewModel> GetLinkedAccounts()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await userManager.FindByIdAsync(userId);
            return new UserLinkedAccountsViewModel
            {
                LichessUsername = user.LichessUsername,
                ChessComUsername = user.ChessComUsername
            };
        }
    }
}
