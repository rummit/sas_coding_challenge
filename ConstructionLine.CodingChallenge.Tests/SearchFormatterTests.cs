using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Assert = Xunit.Assert;

namespace ConstructionLine.CodingChallenge.Tests
{
    public class SearchFormatterTests
    {
        [Fact]
        public void When_Initialized_Results_Are_Empty()
        {
            var formatter = new SearchFormatter();
            var result = formatter.Combine();

            Assert.Empty(result.Shirts);
            Assert.Empty(result.SizeCounts);
            Assert.Empty(result.ColorCounts);
        }

        [Fact]
        public void When_BuildShirts_Called_With_Empty_Results_Are_Empty()
        {
            var formatter = new SearchFormatter();
            formatter.BuildShirts(new List<Shirt>());
            var result = formatter.Combine();

            Assert.Empty(result.Shirts);
            Assert.Empty(result.SizeCounts);
            Assert.Empty(result.ColorCounts);
        }

        [Fact]
        public void When_BuildShirts_Called_With_Null_Results_Are_Empty()
        {
            var formatter = new SearchFormatter();
            formatter.BuildShirts(null);
            var result = formatter.Combine();

            Assert.Empty(result.Shirts);
            Assert.Empty(result.SizeCounts);
            Assert.Empty(result.ColorCounts);
        }

        [Fact]
        public void When_BuildShirts_Called_With_ValidData_Results_Are_AsExpected()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
            };

            var formatter = new SearchFormatter();
            formatter.BuildShirts(shirts);
            var result = formatter.Combine();

            Assert.Equal(shirts, result.Shirts);
        }
        

        [Fact]
        public void When_BuildSizeCounts_Called_With_Empty_Results_Are_Empty()
        {
            var formatter = new SearchFormatter();
            formatter.BuildSizeCounts(new List<Shirt>());
            var result = formatter.Combine();

            Assert.Empty(result.Shirts);
            Assert.NotEmpty(result.SizeCounts);
            Assert.Empty(result.ColorCounts);
        }

        [Fact]
        public void When_BuildSizeCounts_Called_With_Null_Results_Are_Empty()
        {
            var formatter = new SearchFormatter();
            formatter.BuildSizeCounts(null);
            var result = formatter.Combine();

            Assert.Empty(result.Shirts);
            Assert.Empty(result.SizeCounts);
            Assert.Empty(result.ColorCounts);
        }

        [Fact]
        public void When_BuildSizeCounts_Called_With_ValidData_Results_Are_AsExpected()
        {
            var checkedSize = Size.Small;
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", checkedSize, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Small", checkedSize, Color.Black),
            };

            var formatter = new SearchFormatter();
            formatter.BuildSizeCounts(shirts);
            var result = formatter.Combine();

            foreach (var size in Size.All)
            {
                if (size.Id != checkedSize.Id)
                {
                    //All other sizes should be returning 0
                    Assert.Equal(0, result.SizeCounts.First(i => i.Size == size).Count);
                }
                else
                {
                    //Only CheckedSize(Size.Small) should be having a valid count of 2
                    Assert.Equal(2, result.SizeCounts.First(i => i.Size == checkedSize).Count);
                }
            }
        }

        [Fact]
        public void When_BuildColorCounts_Called_With_Empty_Results_Are_Empty()
        {
            var formatter = new SearchFormatter();
            formatter.BuildColorCounts(new List<Shirt>());
            var result = formatter.Combine();

            Assert.Empty(result.Shirts);
            Assert.Empty(result.SizeCounts);
            Assert.NotEmpty(result.ColorCounts);
        }

        [Fact]
        public void When_BuildColorCounts_Called_With_Null_Results_Are_Empty()
        {
            var formatter = new SearchFormatter();
            formatter.BuildColorCounts(null);
            var result = formatter.Combine();

            Assert.Empty(result.Shirts);
            Assert.Empty(result.SizeCounts);
            Assert.Empty(result.ColorCounts);
        }

        [Fact]
        public void When_BuildColorCounts_Called_With_ValidData_Results_Are_AsExpected()
        {
            var checkedColor = Color.Red;
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, checkedColor),
                new Shirt(Guid.NewGuid(), "Red - Large", Size.Large, checkedColor),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, checkedColor),
            };

            var formatter = new SearchFormatter();
            formatter.BuildColorCounts(shirts);
            var result = formatter.Combine();

            foreach (var color in Color.All)
            {
                if (color.Id != checkedColor.Id)
                {
                    //All other Colors should be returning 0
                    Assert.Equal(0, result.ColorCounts.First(i => i.Color == color).Count);
                }
                else
                {
                    //Only CheckedColor(Color.Red) should be having a valid count of 3
                    Assert.Equal(3, result.ColorCounts.First(i => i.Color == checkedColor).Count);
                }
            }
        }

    }
}
