using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Wanderer
{
    internal class Hero : Character
    {
        public Hero(int level) : base(level)
        {
            MaxHP = 20 + 5 * DiceRoll(6);
            HP = MaxHP;
            InitializeStats();
        }
    }
}
