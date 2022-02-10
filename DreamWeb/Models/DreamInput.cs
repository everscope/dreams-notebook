namespace DreamWeb.Models
{
    public class DreamInput
    {
        public string Name { get; set; }
        public string Topics { get; set; }
        public string Hours { get; set; }
        public string[] Content { get; set; }
        public bool IsPublic { get; set; }
        public string AuthorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Id { get; set; }

        private DreamsContext _dbContext;

        private readonly char[] internalIdChars = "abcdefghigklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        public static string splitContentMarker { get; private set; } = "(%%#newpart#%%)";

        public DreamInput(DreamsContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task AddDreamAsync()
        {
            _dbContext.DreamPublications.Add(ToDreamPublication(this));
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveDreamAsync(string id)
        {
            _dbContext.DreamPublications.Remove(_dbContext.DreamPublications.First(p => p.Id == id));
            _dbContext.SaveChanges();
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
               Any(p => p.Id == id))
            {
                id = CreateInternalId();
            }

            return id;
        }

        private DreamPublication ToDreamPublication(DreamInput input)
        {
            DreamPublication publication = new DreamPublication();
            publication.Id = CreateInternalId();
            publication.Name = input.Name;
            publication.Topics = input.Topics;
            publication.Hours = input.Hours;
            publication.AuthorID = input.AuthorId;
            publication.Status = input.IsPublic;

            publication.CreationDate = DateTime.Now;

            string content = input.Content[0];
            for (int i = 1; i < input.Content.Length; i++ )
            {
                content += splitContentMarker + input.Content[i];
            }
            publication.Content = content;
            return publication;
        }

    }

}
