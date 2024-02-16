using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wanderer
{
    internal class Hero : Character
    {
        public bool CanMove { get; set; }
        public bool CanFight { get; set; }
        public Hero(int level) : base(level)
        {
            MaxHP = 20 + 5 * DiceRoll(6);
            HP = MaxHP;
            StatPosition = new Vector2(1500, 10);
            this.CanMove = true;
            this.CanFight = true;
            InitializeStats();
        }

    }
}
