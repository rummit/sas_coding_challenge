using System;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace ConstructionLine.CodingChallenge.Tests
{
    public class SearchEngineInternalTests
    {

        [Fact]
        public void When_Search_Called_With_Null_Options_Result_Is_Null()
        {
            var filterMock = new Mock<ISearchFilter>();
            var builderMock = new Mock<ISearchBuilder>();
            var formatterMock = new Mock<ISearchFormatter>();

            var searchEngineInternal = new SearchEngineInternal(filterMock.Object, builderMock.Object, formatterMock.Object);

            var actualResult = searchEngineInternal.Search(null);
            Assert.Null(actualResult);
        }

        [Fact]
        public void When_Search_Called_With_Empty_Options_Result_Is_Null()
        {
            var filterMock = new Mock<ISearchFilter>();
            var builderMock = new Mock<ISearchBuilder>();
            var formatterMock = new Mock<ISearchFormatter>();

            var searchEngineInternal = new SearchEngineInternal(filterMock.Object, builderMock.Object, formatterMock.Object);

            var actualResult = searchEngineInternal.Search(new SearchOptions());
            Assert.Null(actualResult);
        }
        
        [Fact]
        public void When_Search_Called_With_Filter_Returning_Null_Result_Is_Null()
        {
            var filterMock = new Mock<ISearchFilter>();
            var builderMock = new Mock<ISearchBuilder>();
            var formatterMock = new Mock<ISearchFormatter>();

            filterMock.Setup(x => x.Apply(It.IsAny<SearchOptions>())).Returns((IEnumerable<Shirt>)null);
            var searchEngineInternal = new SearchEngineInternal(filterMock.Object, builderMock.Object, formatterMock.Object);

            var actualResult = searchEngineInternal.Search(new SearchOptions());
            Assert.Null(actualResult);
        }

        [Fact]
        public void When_Search_Called_With_Valid_Data_Builder_Construct_Is_Called()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
            };
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
            };

            var filterMock = new Mock<ISearchFilter>();
            var builderMock = new Mock<ISearchBuilder>();
            var formatterMock = new Mock<ISearchFormatter>();

            filterMock.Setup(x => x.Apply(It.IsAny<SearchOptions>())).Returns(shirts);
            var searchEngineInternal = new SearchEngineInternal(filterMock.Object, builderMock.Object, formatterMock.Object);

            searchEngineInternal.Search(searchOptions);

            builderMock.Verify(x=>x.Construct(It.Is<IEnumerable<Shirt>>( y => y.Equals(shirts))));
        }
    }
}
