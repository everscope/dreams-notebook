using DreamWeb.DAL.Entities;

namespace DreamWeb.DreamPublicationSorting
{
    public class DreamsSorting
    {
        protected int _sortingCase;
        protected DateTime _dateTime;
        protected string[] _keyWords;

        public List<Dream> SortByOrder(IEnumerable<Dream> dreams, int sortingCase)
        {
            IEnumerable<Dream> sorted;

            if (sortingCase == 1)
            {
                sorted = dreams.OrderByDescending(p => p.CreationDate);
            }
            else if (sortingCase == 2)
            {
                sorted = dreams.OrderBy(p => p.CreationDate);
            }
            else if (sortingCase == 3)
            {
                sorted = dreams.OrderBy(p => p.Content.Length);
            }
            else if (sortingCase == 4)
            {
                sorted = dreams.OrderByDescending(p => p.Content.Length);
            }
            else
            {
                sorted = dreams.OrderByDescending(p => p.Content.Length);
            }

            return sorted.ToList();
        }

        public List<Dream> SortByDate(IEnumerable<Dream> dreams, DateTime dateTime)
        {
            return dreams.Where(p => p.CreationDate == dateTime).ToList();
        }

        public List<Dream> SortByKeyWords(IEnumerable<Dream> dreams, string[] keyWords)
        {
            List<Dream> sorted = new();

            //sorted.Append(dreams.Where(p => p.Content.Contains(keyWords)));

            foreach (string key in keyWords)
            {
                foreach (var dream in dreams)
                {
                    if (dream.Content.Contains(key) && !sorted.Contains(dream))
                    {
                        sorted.Add(dream);
                    }
                }
            }

            return sorted;

            //if (keyWords != null)
            //{
            //    List<DreamPublication> result = new List<DreamPublication>();



            //    var resultTemp = dreams.Where(p => p.Content.Contains(keyWords));

            //    foreach (DreamPublication dream in resultTemp)
            //    {
            //        result.Add(dream);
            //    }

            //    foreach (string key in keys)
            //    {
            //        var thisSearch = content.Where(p => p.Content.Contains(key));
            //        List<DreamPublication> tempList = new List<DreamPublication>();

            //        foreach (DreamPublication dream in thisSearch)
            //        {
            //            if (!result.Contains(dream))
            //            {
            //                tempList.Add(dream);
            //            }
            //        }

            //        result.AddRange(tempList);
            //    }
            //}
        }


    }
}
