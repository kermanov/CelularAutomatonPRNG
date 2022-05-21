using System;
using System.Collections.Generic;
using System.Text;

namespace CelularAutomatonPRNG
{
    class CellularAutomatonRandom45 : CellularAutomatonRandom
    {
        private const byte _rule = 45;

        public CellularAutomatonRandom45(int seed) : base(_rule, seed)
        {
        }

        public CellularAutomatonRandom45() : base(_rule)
        {
        }
    }
}
