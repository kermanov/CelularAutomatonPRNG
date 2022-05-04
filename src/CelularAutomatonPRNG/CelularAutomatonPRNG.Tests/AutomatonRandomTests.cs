using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CelularAutomatonPRNG.Tests
{
    public class AutomatonRandomTests
    {
        [Fact]
        public void NextDoubleTest()
        {
            var random = new CellularAutomatonRandom(30, 123);
            for (int i = 0; i < 10000; ++i)
            {
                var randomDouble = random.NextDouble();
                Assert.True(randomDouble >= 0d);
                Assert.True(randomDouble < 1d);
            }
        }

        [Fact]
        public void NextWithoutArgumentsTest()
        {
            var random = new CellularAutomatonRandom(30, 123);
            for (int i = 0; i < 10000; ++i)
            {
                var randomDouble = random.Next();
                Assert.True(randomDouble >= 0);
                Assert.True(randomDouble < int.MaxValue);
            }
        }

        [Fact]
        public void NextMaxValueArgumentTest()
        {
            var maxValue = 1234;
            var random = new CellularAutomatonRandom(30, 123);
            for (int i = 0; i < 10000; ++i)
            {
                var randomDouble = random.Next(maxValue);
                Assert.True(randomDouble >= 0);
                Assert.True(randomDouble < maxValue);
            }
        }

        [Fact]
        public void NextMinValueMaxValueArgumentsTest()
        {
            var minValue = 42;
            var maxValue = 1234;
            var random = new CellularAutomatonRandom(30, 123);
            for (int i = 0; i < 10000; ++i)
            {
                var randomDouble = random.Next(minValue, maxValue);
                Assert.True(randomDouble >= minValue);
                Assert.True(randomDouble < maxValue);
            }
        }
    }
}
