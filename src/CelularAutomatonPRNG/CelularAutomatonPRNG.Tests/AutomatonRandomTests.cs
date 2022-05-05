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

        [Fact]
        public void GetBytesTest()
        {
            var random = new CellularAutomatonRandom(30, 123);
            for (int i = 0; i < 128; ++i)
            {
                var bytes = random.GetBytes(i);
                Assert.Equal(i, bytes.Length);
            }
        }

        [Fact]
        public void ResultPersistenceTest()
        {
            var random = new CellularAutomatonRandom(30, 123);
            var firstRandInt = random.Next();
            var firstRandDouble = random.NextDouble();
            var firstRandByte = random.GetBytes(1)[0];

            random = new CellularAutomatonRandom(30, 123);
            var secondRandInt = random.Next();
            var secondRandDouble = random.NextDouble();
            var secondRandByte = random.GetBytes(1)[0];

            Assert.Equal(firstRandInt, secondRandInt);
            Assert.Equal(firstRandDouble, secondRandDouble);
            Assert.Equal(firstRandByte, secondRandByte);
        }
    }
}
