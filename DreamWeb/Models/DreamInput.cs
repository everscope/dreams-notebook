namespace DreamWeb.Models
{
    public class DreamInput
    {
        public string Name { get;  set; }
        public string Topics { get;  set; }
        public string Hours { get;  set; }
        public string[] Content { get;  set; }
        public string ExternalId { get;  set; }
        public bool IsPublic { get;  set; }
        public string AuthorId { get;  set; }
        public DateTime CreationDate { get;  set; }
        public string Id { get;  set; }

        private DreamsContext _dbContext;

        private char[] internalIdChars = "abcdefghigklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        private string splitContentMarker = "(%%#newpart#%%)";

        //Status = input.IsPublic;
        //    AuthorID = input.AuthorID;
        //    CreationDate = input.CreationDate;
        //    ExternalID = input.ExternalId;
        //    InternalID = input.Id;
        //    Topics = input.Topics;
        //    Hours = input.Hours;
        //    Content = input.Content;
        //public DreamInput(string name, string topics, string hour, string[] context,
        //                    string externalId, bool isPublic, string authorId, DreamsContext dbContext)
        //{
        //    Name = name;
        //    Topics = topics;
        //    Hours = hour;
        //    Content = context;
        //    ExternalId = externalId;
        //    AuthorID = authorId;

        //    CreationDate = DateTime.Now;

        //    _dbContext = dbContext;
        //}

        public DreamInput(DreamsContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task AddDreamAsync()
        {
            _dbContext.DreamPublications.Add(ToDreamPublication(this));
            await _dbContext.SaveChangesAsync();
        }

        public void EditDream()
        {

        }

        public void RemoveDream()
        {

        }

        private string CreateInternalId()
        {
            Random random = new Random();
            string id = "";
            for (int i = 0; i < 12; i++)
            {
                id += internalIdChars[random.Next(0, internalIdChars.Length)];
            }

            if (_dbContext.DreamPublications.
               Any(p => p.InternalID == id))
            {
                id = CreateInternalId();
            }

            return id;
        }

        private DreamPublication ToDreamPublication(DreamInput input)
        {
            DreamPublication publication = new DreamPublication();
            publication.InternalID = CreateInternalId();
            publication.Name = input.Name;
            publication.Topics = input.Topics;
            publication.Hours = input.Hours;
            publication.ExternalID = input.ExternalId;
            publication.AuthorID = input.AuthorId;

            publication.CreationDate = DateTime.Now;

            string content = "";
            for (int i = 0; i < input.Content.Length-1; i++ )
            {
                content += input.Content[i] + splitContentMarker;
            }
            publication.Content = content;
            return publication;
        }

    }

}
