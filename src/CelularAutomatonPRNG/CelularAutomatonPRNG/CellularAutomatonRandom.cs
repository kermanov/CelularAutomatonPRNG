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
        private int _currentByteIndex;

        public CellularAutomatonRandom(byte rule) : this(rule, (int)DateTime.UtcNow.Ticks)
        {
        }

        public CellularAutomatonRandom(byte rule, int seed)
        {
            _automaton = new ElementaryCellularAutomaton(rule);
            _currentState = new byte[5];
            _currentByteIndex = 0;

            if (seed != 0)
            {
                Array.Copy(BitConverter.GetBytes(seed), _currentState, 4);
            }
            else
            {
                _currentState[^1] = 0b10000000;
            }

            for (int i = 0; i < 26; ++i)
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
            var bytes = new byte[8];
            GetBytes(bytes);
            var randUInt64 = BitConverter.ToUInt64(bytes);
            var uInt64InDoubleForm = (randUInt64 | 0x3FF0000000000000UL) & 0x3FFFFFFFFFFFFFFFUL;
            return BitConverter.Int64BitsToDouble((long)uInt64InDoubleForm) - 1d;
        }

        public void GetBytes(byte[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = GetNextByte();
            }
        }

        private byte GetNextByte()
        {
            byte nextByte = 0;
            for (int i = 0; i < 8; ++i)
            {
                // TODO: Find optimization.
                var number = BitConverter.ToUInt16(new[] { GetNextStateByte(), GetNextStateByte() });
                if (number >= (ushort.MaxValue >> 1) + 1)
                {
                    nextByte |= (byte)(1 << i);
                }
            }
            return nextByte;
        }

        private byte GetNextStateByte()
        {
            if (_currentByteIndex < _currentState.Length)
            {
                return _currentState[_currentByteIndex++];
            }
            _currentByteIndex = 0;
            GetNextState();
            return GetNextStateByte();
        }

        private byte[] GetNextState()
        {
            _currentState = _automaton.GetNextState(_currentState);
            return _currentState;
        }
    }
}
