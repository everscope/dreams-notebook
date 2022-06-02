using DreamWeb.DAL.Entities;

namespace DreamWeb.DreamPublicationSorting
{
    public interface IDreamsSorting
    {
        public void Sort(ICollection<DreamPublication> dreams);
    }
}
