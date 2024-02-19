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
        public void Eat(Character opponent)
        {
            opponent.HP = 0;
            opponent.IsDead = true;
            int newHp = this.HP + 5 * DiceRoll(6);
            if (newHp > this.MaxHP)
            {
                this.HP = this.MaxHP;
            }
            else
            {
                this.HP = newHp;
            }
        }
    }
}
