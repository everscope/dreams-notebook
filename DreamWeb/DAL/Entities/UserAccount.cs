using Microsoft.AspNetCore.Identity;

namespace DreamWeb.DAL.Entities
{
    public class UserAccount : IdentityUser
    {
        public DateTime CreationTime { get; set; }
        public ICollection<Dream> Dreams = new List<Dream>();
        
        public int Id;
    }
}
