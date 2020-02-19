namespace ConstructionLine.CodingChallenge
{
    public interface ISearchEngineInternal
    {
        SearchResults Search(SearchOptions options);
    }

    public class SearchEngineInternal : ISearchEngineInternal
    {
        private readonly ISearchFilter _filter;
        private readonly ISearchBuilder _builder;

        public SearchEngineInternal(ISearchFilter filter, ISearchBuilder builder, ISearchFormatter formatter)
        {
            _filter = filter;
            _builder = builder;
        }

        public SearchResults Search(SearchOptions options)
        {
            if (options == null) return null;

            var filteredShirts = _filter.Apply(options);

            return filteredShirts == null ? null : _builder.Construct(filteredShirts);
        }
    }
}
