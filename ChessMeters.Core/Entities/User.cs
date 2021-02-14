using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ChessMeters.Core.Entities
{
    public class User : IdentityUser
    {
        [MaxLength(20)]
        public string LichessUsername { get; set; }

        [MaxLength(100)]
        public string ChessComUsername { get; set; }
    }
}
