using Microsoft.Xna.Framework;
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
            StatPosition = new Vector2(1500, 810);
            Steps = 0;
            LastStep = 0;
            StepsPerMove = 1;
        }
    }
}
