using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamWeb.DAL.Entities
{
    public class DreamPublication
    {
        public string Id { get; set; }
        public bool IsPublic { get; set; }
        //public string AuthorID { get; set; }
        public UserAccount UserAccount { get; set; }
        public DateTime CreationDate { get; set; }
        public string Topics { get; set; }
        public string Name { get; set; }
        public string Hours { get; set; }
        public string Content { get; set;}

    }
}
