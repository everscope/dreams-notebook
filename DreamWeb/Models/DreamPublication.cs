using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamWeb.Models
{
    public class DreamPublication
    {
        [Column("status")]
        public int Status { get; set; }

        [ForeignKey("author_id")]
        [Column("author_id")]
        public string AuthorID { get; set; }

        [Column("creation_date")]
        public DateTime CreationDate { get; set; }

        [Column("external_id")]
        public string? ExternalID { get; set; }

        [Column("internal_id")]
        [Key]
        public string InternalID { get; set; }

        [Column("published_content")]
        public string Content { get; set;}
        
    }

}
