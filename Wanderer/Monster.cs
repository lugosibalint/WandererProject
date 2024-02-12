using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wanderer
{
    internal class Monster : Character
    {
        public Monster(int level) : base(level)
        {
            Level = level;
            InitializeStats();
        }
    }
}
