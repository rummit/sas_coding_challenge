using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public interface ISearchFormatter
    {
        void BuildShirts(IEnumerable<Shirt> filteredShirts);

        void BuildSizeCounts(IEnumerable<Shirt> filteredShirts);

        void BuildColorCounts(IEnumerable<Shirt> filteredShirts);

        SearchResults Combine();
    }

    public class SearchFormatter : ISearchFormatter
    {
        private readonly SearchResults _returnValue;

        public SearchFormatter()
        {
            _returnValue = new SearchResults()
            {
                Shirts = new List<Shirt>(),
                SizeCounts = new List<SizeCount>(),
                ColorCounts = new List<ColorCount>()
            };
        }

        public void BuildShirts(IEnumerable<Shirt> filteredShirts)
        {
            filteredShirts?.ToList().ForEach(x => _returnValue.Shirts.Add(x));
        }

        public void BuildSizeCounts(IEnumerable<Shirt> filteredShirts)
        {
            if (filteredShirts == null) return;

            var enumerable = filteredShirts.ToList();

            foreach (var size in Size.All)
            {
                _returnValue.SizeCounts.Add(new SizeCount()
                {
                    Size = size,
                    Count = enumerable.Count(x => x.Size.Equals(size))
                });
            }
        }

        public void BuildColorCounts(IEnumerable<Shirt> filteredShirts)
        {
            if (filteredShirts == null) return;

            var enumerable = filteredShirts.ToList();

            foreach (var color in Color.All)
            {
                _returnValue.ColorCounts.Add(new ColorCount()
                {
                    Color = color,
                    Count = enumerable.Count(x => x.Color.Equals(color))
                });
            }
        }

        public SearchResults Combine() => _returnValue;
    }
}
