using System;
using System.Collections;

namespace CelularAutomatonPRNG
{
    public class ElementaryCellularAutomaton
    {
        public byte Rule { get; }

        public ElementaryCellularAutomaton(byte rule)
        {
            Rule = rule;
        }

        public ElementaryCellularAutomaton() : this(0)
        {
        }

        public BitArray GetNextState(BitArray currentState)
        {
            var nextState = new BitArray(currentState.Length, false);
            for (int i = 0; i < currentState.Length; ++i)
            {
                var pattenIndex = (currentState[(i - 1 + currentState.Length) % currentState.Length] ? 1 : 0)
                    + (currentState[i] ? 2 : 0)
                    + (currentState[(i + 1) % currentState.Length] ? 4 : 0);

                nextState[i] = ((1 << pattenIndex) & Rule) > 0;
            }
            return nextState;
        }

        public byte[] GetNextState(byte[] currentState)
        {
            var currentStateBitArray = new BitArray(currentState);
            var nextStateBitArray = GetNextState(currentStateBitArray);
            return BitArrayToBytes(nextStateBitArray);
        }

        private byte[] BitArrayToBytes(BitArray bitArray)
        {
            var bytes = new byte[bitArray.Length / 8];
            for (int i = 0; i < bitArray.Length / 8; ++i)
            {
                bytes[i] = BitArrayToByte(bitArray, i * 8);
            }
            return bytes;
        }

        private byte BitArrayToByte(BitArray bitArray, int startIndex)
        {
            byte resultByte = 0;
            for (int i = 0; i < 8; ++i)
            {
                if (bitArray[startIndex + i])
                {
                    resultByte |= (byte)(1 << i);
                }
            }
            return resultByte;
        }
    }
}
