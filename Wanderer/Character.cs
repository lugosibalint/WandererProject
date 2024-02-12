using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Wanderer
{
    public class Character
    {
        private SpriteBatch _spriteBatch;

        public int Level { get; set; }
        public int MaxHP { get; set; }
        public int HP { get; set; }
        public int DP { get; set; }
        public int SP { get; set; }

        public Character(int level)
        {
            Level = level;
            InitializeStats();
        }

        private void InitializeStats()
        {
            Random random = new Random();
            MaxHP = 20 + 3 * random.Next(1, 7);
            DP = 2 * random.Next(1, 7);
            SP = 5 + random.Next(1, 7);
            HP = MaxHP;
        }
        
        public void LevelUp()
        {
            Level++;
            MaxHP += new Random().Next(1, 7);
            DP += new Random().Next(1, 7);
            SP += new Random().Next(1, 7);
            HP = MaxHP;
        }
    }

}
