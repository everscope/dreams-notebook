using Microsoft.EntityFrameworkCore;

namespace DreamWeb.Models
{
    public interface IDreamPublicationService
    {
        public class InputModel { }
        public void AddDream();
        public void RemoveDream();
        public void EditDream();
    }


    public class DreamPublicationService: IDreamPublicationService
    {

        private DreamsContext _dbContext;

        public DreamPublicationService (DreamsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public class InputModel
        {
            public string Name { get; set; } 
            public string Topics { get; set; }
            public string Hours { get; set; }
            public string [] Content { get; set; }
        }

        public void AddDream()
        {
            throw new NotImplementedException();
        }

        public void RemoveDream()
        {
            throw new NotImplementedException();
        }

        public void EditDream()
        {
            throw new NotImplementedException();
        }
    }
}
