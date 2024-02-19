using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wanderer
{
    public class Fruit : Character
    {
        public Fruit(int level) : base(level)
        {
            StatPosition = new Vector2(0,0);
            DP = 0;
            SP = 0;
            HP = 1;
            MaxHP = 1;
        }
    }
}
