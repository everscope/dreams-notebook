namespace DreamWeb.Models
{
    public class DreamInputModel
    {
        public string Name { get; set; }
        public string Topics { get; set; }
        public string Hours { get; set; }
        public string[] Content { get; set; }
        public bool IsPublic { get; set; }
        public string AuthorId { get; set; }
    }
}
