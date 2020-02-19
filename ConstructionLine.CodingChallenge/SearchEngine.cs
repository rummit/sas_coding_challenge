using System.Collections.Generic;
using Autofac;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly ISearchEngineInternal _searchInternal;

        public SearchEngine(List<Shirt> shirts)
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(new SearchFilter(shirts)).As<ISearchFilter>();
            builder.RegisterType<SearchBuilder>().As<ISearchBuilder>();
            builder.RegisterType<SearchFormatter>().As<ISearchFormatter>();

            var container = builder.Build();

            _searchInternal = new SearchEngineInternal(container.Resolve<ISearchFilter>(),
                                                          container.Resolve<ISearchBuilder>(),
                                                          container.Resolve<ISearchFormatter>());
        }

        public SearchResults Search(SearchOptions options)
        {
            return _searchInternal.Search(options);
        }
    }
}