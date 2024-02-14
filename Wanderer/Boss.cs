using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wanderer
{
    internal class Boss : Monster
    {
        public Boss(int level) : base(level)
        {
            this.Level = level;
        }
    }
}
