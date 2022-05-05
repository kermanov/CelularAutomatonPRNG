using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CelularAutomatonPRNG
{
    public class CellularAutomatonRandom
    {
        private readonly ElementaryCellularAutomaton _automaton;
        private byte[] _currentState;

        public CellularAutomatonRandom(byte rule, int seed)
        {
            _automaton = new ElementaryCellularAutomaton(rule);
            _currentState = BitConverter.GetBytes(((long)seed << 32) | (long)seed);

            for (int i = 0; i < 32; ++i)
            {
                GetNextState();
            }
        }

        public int Next(int minValue, int maxValue)
        {
            return minValue + (int)((maxValue - minValue) * NextDouble());
        }

        public int Next(int maxValue)
        {
            return Next(0, maxValue);
        }

        public int Next()
        {
            return Next(0, int.MaxValue);
        }

        public double NextDouble()
        {
            var randUInt64 = BitConverter.ToUInt64(GetNextState());
            var uInt64InDoubleForm = (randUInt64 | 0x3FF0000000000000UL) & 0x3FFFFFFFFFFFFFFFUL;
            return BitConverter.Int64BitsToDouble((long)uInt64InDoubleForm) - 1d;
        }

        public byte[] GetBytes(int count)
        {
            var bytes = new byte[count];
            for (int i = 0; i < count; i += 8)
            {
                Array.Copy(GetNextState(), 0, bytes, i, i + 8 < count ? 8 : count - i);
            }
            return bytes;
        }

        private byte[] GetNextState()
        {
            _currentState = _automaton.GetNextState(_currentState);
            return _currentState;
        }
    }
}
