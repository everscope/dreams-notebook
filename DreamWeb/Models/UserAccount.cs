using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamWeb.Models
{
    public class UserAccount : IdentityUser
    { 

        [Column("creation_time")]
        public DateTime CreationTime { get; set; }

    }
}
