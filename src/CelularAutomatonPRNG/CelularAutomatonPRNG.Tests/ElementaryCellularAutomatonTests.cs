using System;
using System.Collections;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace CelularAutomatonPRNG.Tests
{
    public class ElementaryCellularAutomatonTests
    {
        private readonly ITestOutputHelper _output;

        public ElementaryCellularAutomatonTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void GetNextStateBitArrayTest()
        {
            var currentState = new BitArray(new byte[] { 0b00110100 });
            var expectedNextState = new BitArray(new byte[] { 0b01111100 });

            var cellularAutomaton = new ElementaryCellularAutomaton(110);
            var nextState = cellularAutomaton.GetNextState(currentState);

            Assert.Equal(expectedNextState.Length, nextState.Length);
            for (int i = 0; i < expectedNextState.Length; ++i)
            {
                Assert.Equal(expectedNextState[i], nextState[i]);
            }
        }

        [Fact]
        public void GetNextStateByteArrayTest()
        {
            var currentState = new byte[] { 0b00110100, 0b01011110 };
            var expectedNextState = new byte[] { 0b01111100, 0b11110010 };

            var cellularAutomaton = new ElementaryCellularAutomaton(110);
            var nextState = cellularAutomaton.GetNextState(currentState);

            Assert.Equal(expectedNextState.Length, nextState.Length);
            for (int i = 0; i < expectedNextState.Length; ++i)
            {
                Assert.Equal(expectedNextState[i], nextState[i]);
            }
        }
    }
}
