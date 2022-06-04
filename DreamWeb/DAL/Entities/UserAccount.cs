using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamWeb.DAL.Entities
{
    public class UserAccount : IdentityUser
    {
        public DateTime CreationTime { get; set; }
        public ICollection<Dream> Dreams = new List<Dream>();
        
        public int Id;
    }
}
