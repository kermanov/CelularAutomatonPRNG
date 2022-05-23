using System;
using System.Collections.Generic;
using System.Text;

namespace CelularAutomatonPRNG
{
    class CellularAutomatonRandom30 : CellularAutomatonRandom
    {
        private const byte _rule = 30;

        public CellularAutomatonRandom30(int seed) : base(_rule, seed)
        {
        }

        public CellularAutomatonRandom30() : base(_rule)
        {
        }
    }
}
