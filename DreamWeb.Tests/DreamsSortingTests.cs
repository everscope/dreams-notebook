using DreamWeb.DAL.Entities;
using DreamWeb.DreamPublicationSorting;
using FluentAssertions;
using Xunit;

namespace DreamWeb.Tests
{
    public class DreamsSortingTests
    {
        [Theory]
        [MemberData(nameof(SortByCreationDateData))]
        public void SortByOrder_SortsByDescendingCreationDate(List<Dream> dreamToSort)
        {
            DreamsSorting sorting = new DreamsSorting();

            List<Dream> sorted = sorting.SortByOrder(dreamToSort, 1);

            sorted.Should().ContainInOrder(dreamToSort.OrderByDescending(p => p.CreationDate));
        }

        [Theory]
        [MemberData(nameof(SortByCreationDateData))]
        public void SortByOrder_SortsByCreationDate(List<Dream> dreamToSort)
        {
            DreamsSorting sorting = new DreamsSorting();

            List<Dream> sorted = sorting.SortByOrder(dreamToSort, 2);

            sorted.Should().ContainInOrder(dreamToSort.OrderBy(p => p.CreationDate));
        }

        [Theory]
        [MemberData(nameof(SortByContentLengthDateData))]
        public void SortByOrder_SortsByContentLength(List<Dream> dreamToSort)
        {
            DreamsSorting sorting = new DreamsSorting();

            List<Dream> sorted = sorting.SortByOrder(dreamToSort, 3);

            sorted.Should().ContainInOrder(dreamToSort.OrderBy(p => p.Content.Length));
        }

        [Theory]
        [MemberData(nameof(SortByContentLengthDateData))]
        public void SortByOrder_SortsByDescendingContentLength(List<Dream> dreamToSort)
        {
            DreamsSorting sorting = new DreamsSorting();

            List<Dream> sorted = sorting.SortByOrder(dreamToSort, 4);

            sorted.Should().ContainInOrder(dreamToSort.OrderByDescending(p => p.Content.Length));
        }

        [Theory]
        [MemberData(nameof(SortByCreationDateData))]
        public void SortByDate_ReturnsDreamsWithDate(List<Dream> dreamToSort)
        {
            DreamsSorting sorting = new DreamsSorting();

            List<Dream> sorted = sorting.SortByDate(dreamToSort, dreamToSort[0].CreationDate);

            sorted.Should().BeEquivalentTo(dreamToSort
                .Where(p => p.CreationDate == dreamToSort[0].CreationDate));
        }

        [Theory]
        [MemberData(nameof(SortByKeyWords))]
        public void SortByKeyWord(List<Dream> dreamToSort, List<string> keyWords, List<Dream> dreamsSorted)
        {
            DreamsSorting sorting = new DreamsSorting();
            List<Dream> sorted = sorting.SortByKeyWords(dreamToSort, keyWords.ToArray());

            sorted.Should().BeEquivalentTo(dreamsSorted); 
        }

        private static IEnumerable<object[]> SortByCreationDateData()
        {
            return new List<object[]>
            {
                new object[]
                {
                    new List<Dream>()
                    {
                        new Dream() {CreationDate = new DateTime(2002, 4, 5)},
                        new Dream() {CreationDate = new DateTime(2022, 6, 14)},
                        new Dream() {CreationDate = new DateTime(2004, 12, 15)}
                    }
                },
                new object[]
                {
                    new List<Dream>()
                    {
                        new Dream() {CreationDate = new DateTime(2022, 6, 14)},
                    }
                }
            };
        }

        private static IEnumerable<object[]> SortByContentLengthDateData()
        {
            return new List<object[]>
            {
                new object[]
                {
                    new List<Dream>()
                    {
                        new Dream() {Content = "string string string"},
                        new Dream() {Content = "string"},
                        new Dream() {Content = "string string"}
                    }
                },
                new object[]
                {
                    new List<Dream>()
                    {
                        new Dream() {Content = "string string"}
                    }
                }
            };
        }

        private static IEnumerable<object[]> SortByKeyWords()
        {
            return new List<object[]>
            {
                new object[]
                {
                    new List<Dream>()
                    {
                        new Dream() {Content = "first second first"},
                        new Dream() {Content = "string"},
                        new Dream() {Content = "second string"}
                    }, 
                    new List<string>()
                    {
                        "string"
                    },
                    new List<Dream>()
                    {
                        new Dream() {Content = "string"},
                        new Dream() {Content = "second string"}
                    },
                },
                new object[]
                {
                    new List<Dream>()
                    {
                        new Dream() {Content = "string"}
                    },
                    new List<string>()
                    {
                        "string"
                    },
                    new List<Dream>()
                    {
                        new Dream() {Content = "string"}
                    },
                }
            };
        }
    }
}
