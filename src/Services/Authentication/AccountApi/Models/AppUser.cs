using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AccountApi.Models
{
    public class AppUser : IdentityUser
    {
        [Required , StringLength(50)]
        public string FirstName { get; set; }
        [Required, StringLength(50)]
        public string LastName { get; set; }
    }
}
