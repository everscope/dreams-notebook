using DreamWeb.DAL.Entities;

namespace DreamWeb.DreamPublicationSorting
{
    public interface IDreamsSorting
    {
        public List<Dream> SortByOrder(IEnumerable<Dream> dreams, int sortingCase);
        public List<Dream> SortByDate(IEnumerable<Dream> dreams, DateTime dateTime);
        public List<Dream> SortByKeyWords(IEnumerable<Dream> dreams, string[] keyWords);
    }
}
