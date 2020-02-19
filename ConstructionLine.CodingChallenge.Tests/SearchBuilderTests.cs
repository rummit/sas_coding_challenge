using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;

namespace ConstructionLine.CodingChallenge.Tests
{
    public class SearchBuilderTests
    {
        [Fact]
        public void When_Construct_Called_Formatter_Is_Invoked()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
            };

            var formatter = new Mock<ISearchFormatter>();

            var builder = new SearchBuilder(formatter.Object);

            builder.Construct(shirts);

            formatter.Verify( x=> x.BuildShirts(It.Is<IEnumerable<Shirt>>( y => y.Count() == shirts.Count)), Times.Once);
            formatter.Verify(x => x.BuildSizeCounts(It.Is<IEnumerable<Shirt>>(y => y.Count() == shirts.Count)), Times.Once);
            formatter.Verify(x => x.BuildColorCounts(It.Is<IEnumerable<Shirt>>(y => y.Count() == shirts.Count)), Times.Once);
            formatter.Verify(x => x.Combine(), Times.Once);
        }
    }
}
