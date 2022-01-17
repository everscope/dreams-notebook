using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamWeb.Models
{
    public class UserAccount : IdentityUser
    { 

        [Column("external_id")]
        public string? ExternalId { get; set; }

        [Column("creation_time")]
        public DateTime CreationTime { get; set; }

    }
}
