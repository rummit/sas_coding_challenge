using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ConstructionLine.CodingChallenge.Tests
{
    public class SearchFilterTests
    {
        [Fact]
        public void When_Apply_Called_With_Null_Shirts_Result_Is_Null()
        {
            var filter = new SearchFilter(null);
            var result = filter.Apply(new SearchOptions());
            Assert.Null(result);
        }

        [Fact]
        public void When_Apply_Called_With_Empty_Shirts_Result_Is_Empty()
        {
            var filter = new SearchFilter(new List<Shirt>());
            var result = filter.Apply(new SearchOptions());
            Assert.Empty(result);
        }

        [Fact]
        public void When_Apply_Called_With_Null_Options_Result_Is_Null()
        {
            var filter = new SearchFilter(new List<Shirt>());
            var result = filter.Apply((SearchOptions)null);
            Assert.Null(result);
        }

        [Fact]
        public void When_Apply_Called_With_Null_SizeOptions_Result_Is_Null()
        {
            var filter = new SearchFilter(new List<Shirt>());
            var options = new SearchOptions()
            {
                Colors = new List<Color>(){Color.Red},
                Sizes = null
            };
            var result = filter.Apply(options);
            Assert.Null(result);
        }

        [Fact]
        public void When_Apply_Called_With_Null_ColorOptions_Result_Is_Null()
        {
            var filter = new SearchFilter(new List<Shirt>());
            var options = new SearchOptions()
            {
                Colors = null, 
                Sizes = new List<Size>() { Size.Large}
            };

            var result = filter.Apply(options);
            Assert.Null(result);
        }

        [Fact]
        public void When_Apply_Called_With_Empty_SizeOptions_Result_Is_Empty()
        {
            var filter = new SearchFilter(new List<Shirt>());
            var options = new SearchOptions()
            {
                Colors = new List<Color>(){Color.Black},
                Sizes = new List<Size>()
            };
            var result = filter.Apply(options);
            Assert.Empty(result);
        }

        [Fact]
        public void When_Apply_Called_With_Empty_ColorOptions_Result_Is_Empty()
        {
            var filter = new SearchFilter(new List<Shirt>());
            var options = new SearchOptions()
            {
                Colors = new List<Color>(),
                Sizes = new List<Size>() { Size.Medium}
            };

            var result = filter.Apply(options);
            Assert.Empty(result);
        }

        [Theory]
        [InlineData(1,1)]
        [InlineData(2,2)]
        [InlineData(3,2)]
        public void When_Apply_Called_With_ValidData_Results_AsExpected(int setNumber, int resultsCount)
        {
            Tuple<List<Shirt>, SearchOptions> data = SetupData(setNumber);

            var filter = new SearchFilter(data.Item1);
            var result = filter.Apply(data.Item2);

            Assert.Equal(resultsCount, result.Count());
        }

        private Tuple<List<Shirt>, SearchOptions> SetupData(int setNumber)
        {
            switch (setNumber)
            {
                case 1:
                    return new Tuple<List<Shirt>, SearchOptions>(
                        new List<Shirt>
                        {
                            new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                            new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                            new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                        },
                        new SearchOptions
                        {
                            Colors = new List<Color> { Color.Red },
                            Sizes = new List<Size> { Size.Small }
                        });
                case 2:
                    return new Tuple<List<Shirt>, SearchOptions>(
                        new List<Shirt>
                        {
                            new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                            new Shirt(Guid.NewGuid(), "Red - Large", Size.Large, Color.Red),
                            new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                            new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                        },
                        new SearchOptions
                        {
                            Colors = new List<Color> { Color.Red },
                        });
                case 3:
                    return new Tuple<List<Shirt>, SearchOptions>(
                        new List<Shirt>
                        {
                            new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
                            new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                            new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                        },
                        new SearchOptions
                        {
                            Sizes= new List<Size> { Size.Medium },
                        });
                default:
                    return null;
            }
        }
    }
}
