using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wanderer
{
    internal class Monster : Character
    {
        public int Steps { get; set; }
        public int LastStep { get; set; }
        public int StepsPerMove { get; set; }
        public bool hasKey { get; set; }
        public Monster(int level) : base(level)
        {
            Level = level;
            SP = 2 + DiceRoll(6);
            Steps = 0;
            LastStep = 0;
            StepsPerMove = 2;
            StatPosition = new Vector2(10, 10);
            InitializeStats();
        }
        public void MoveRandomly(Grid grid)
        {
            if (Steps % StepsPerMove == 0)
            {
                while (Steps != LastStep)
                {
                    LastStep = Steps;
                    Random random = new Random();
                    int randomDirection = random.Next(4); // 0: fel, 1: le, 2: balra, 3: jobbra
                    switch (randomDirection)
                    {
                        case 0:
                            // Lépés felfelé
                            this.Move("up", grid);
                            break;

                        case 1:
                            // Lépés lefelé
                            this.Move("down", grid);
                            break;

                        case 2:
                            // Lépés balra
                            this.Move("left", grid);
                            break;

                        case 3:
                            // Lépés jobbra
                            this.Move("right", grid);
                            break;
                    }
                }
            }
        }
    }
}
