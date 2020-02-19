using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public interface ISearchBuilder
    {
        SearchResults Construct(IEnumerable<Shirt> filteredShirts);
    }

    public class SearchBuilder : ISearchBuilder
    {
        private readonly ISearchFormatter _formatter;

        public SearchBuilder(ISearchFormatter formatter)
        {
            _formatter = formatter;
        }

        public SearchResults Construct(IEnumerable<Shirt> filteredShirts)
        {
            var shirts = filteredShirts.ToList();

            _formatter?.BuildShirts(shirts);
            _formatter?.BuildSizeCounts(shirts);
            _formatter?.BuildColorCounts(shirts);

            return _formatter?.Combine();
        }
    }
}
