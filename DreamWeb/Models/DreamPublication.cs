using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamWeb.Models
{

    public class DreamPublication
    {

        [Column("status")]
        public bool Status { get; set; }

        [ForeignKey("author_id")]
        [Column("author_id")]
        public string AuthorID { get; set; }

        [Column("creation_date")]
        public DateTime CreationDate { get; set; }

        [Column("internal_id")]
        [Key]
        public string Id { get; set; }

        [Column("topics")]
        public string Topics { get; set; }

        [Column("name")]
        public string Name { get; set; }
        
        [Column("hours")]
        public string Hours { get; set; }

        [Column("content")]
        public string Content { get; set;}

    }
}
