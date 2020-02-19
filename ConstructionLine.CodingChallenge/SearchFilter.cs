using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public interface ISearchFilter
    {
        IEnumerable<Shirt> Apply(SearchOptions options);
    }

    public class SearchFilter : ISearchFilter
    {
        private readonly IEnumerable<Shirt> _shirts;

        public SearchFilter(IEnumerable<Shirt> shirts)
        {
            _shirts = shirts;
        }

        public IEnumerable<Shirt> Apply(SearchOptions options)
        {
            if (_shirts == null || options == null) return null;

            var sizeOptions = options.Sizes?.Select(s => s.Id).ToList();
            var colorOptions = options.Colors?.Select(c => c.Id).ToList();

            if (sizeOptions == null || colorOptions == null) return null;

            return _shirts.Where(shirt => (sizeOptions.Contains(shirt.Size.Id) || sizeOptions.Count == 0) &&
                                          (colorOptions.Contains(shirt.Color.Id) || colorOptions.Count == 0)).
                ToList();
        }
    }
}
