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

        //string dreamName, string topics, string hours,
        //string[] content, bool isPublic, string authorId

        //private readonly string splitContentMarker = "(%%#newpart#%%)";

        //map!

        //private DreamPublication ToDreamPublication(DreamInputModel input)
        //{
        //    DreamPublication publication = new DreamPublication();
        //    publication.Id = CreateInternalId();
        //    publication.Name = input.Name;
        //    publication.Topics = input.Topics;
        //    publication.Hours = input.Hours;
        //    publication.AuthorID = input.AuthorId;
        //    publication.Status = input.IsPublic;

        //    publication.CreationDate = DateTime.Now;

        //    string content = input.Content[0];
        //    for (int i = 1; i < input.Content.Length; i++ )
        //    {
        //        content += splitContentMarker + input.Content[i];
        //    }
        //    publication.Content = content;
        //    return publication;
        //}

    }

}
