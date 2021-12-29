using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamWeb.Models
{
    public class UserAccount
    { 
        [Key]
        [Column("internal_id")]
        public string InternalId { get; set; }

        [Column("external_id")]
        public string? ExternalId { get; set; }

        [Column("nickname")]
        public string Nickname { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("creation_time")]
        public DateTime CreationTime { get; set; }

        //List<UserAccount> Followers { get; set; }
        //List<UserAccount> Following { get; set; }
    }
}
