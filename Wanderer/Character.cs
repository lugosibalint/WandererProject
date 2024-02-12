using Microsoft.Xna.Framework;
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
        public int Level { get; set; }
        public int MaxHP { get; set; }
        public int HP { get; set; }
        public int DP { get; set; }
        public int SP { get; set; }
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public CharacterTextures Textures { get; set; }

        public Character(int level)
        {
            this.Level = level;
            this.Position = new Vector2();
            this.Texture = Textures.down;
            
            InitializeStats();
        }

        public void InitializeStats()
        {
            MaxHP = 20 + 3 * DiceRoll(6);
            DP = 2 * DiceRoll(6);
            SP = 5 + DiceRoll(6);
            HP = MaxHP;

        }
        public void Fight(Character opponent)
        {
            // Támadásnál a támadó értéke (SV) az SP és a d6 kétszeresének összege
            int attackValue = 2 * DiceRoll(6) + this.SP;

            // A támadás sikeres, ha az attackValue nagyobb, mint az ellenfél DP-je
            if (attackValue > opponent.DP)
            {
                // Sikeres támadás, csökken az ellenfél HP-ja
                int damage = attackValue - opponent.DP;
                opponent.HP -= damage;

                // Ellenfél HP-ja nem lehet negatív
                if (opponent.HP < 0)
                {
                    opponent.HP = 0;
                }
            }
        }
        private bool NextLevel()
        {
            // Ellenőrizzük, hogy a kulcsot hordozó szörny és a boss már meg van-e ölve
            bool keyMonsterDead = /* implementáció: ellenőrizd a kulcsot hordozó szörnyt */false;
            bool bossDead = /* implementáció: ellenőrizd a bosst */false;

            if (keyMonsterDead && bossDead)
            {
                // Következő pálya inicializálása
                InitializeNextLevel();

                // Gyógyulás esélyeinek ellenőrzése és végrehajtása
                Random random = new Random();
                int chance = random.Next(1, 101);

                if (chance <= 10)
                {
                    // 10% esély az összes HP visszatöltésére
                    this.HP = this.MaxHP;
                }
                else if (chance <= 50)
                {
                    // 40% esély a HP harmadának a visszatöltésére
                    this.HP += this.MaxHP / 3;
                    if (this.HP > this.MaxHP)
                        this.HP = this.MaxHP;
                }
                else
                {
                    // 50% esély a HP 10%-nak a visszatöltésére
                    this.HP += this.MaxHP / 10;
                    if (this.HP > this.MaxHP)
                        this.HP = this.MaxHP;
                }

                // Szintlépés
                this.LevelUp();

                return true; // Következő pályára lépés sikeres
            }

            return false; // Még nem ölték meg mindkét fontos szörnyet
        }
        private void InitializeNextLevel()
        {
            // Implementáld a következő pálya inicializálását
            // Frissítsd a szörnyeket, a pályát, stb.
        }
        public void LevelUp()
        {
            Level++;
            MaxHP += DiceRoll(6);
            DP += DiceRoll(6);
            SP += DiceRoll(6);
            HP = MaxHP;
        }
        public bool IsDead()
        {
            return HP <= 0;
        }
        public void Move(string direction, Grid grid)
        {
            int colIndex = (int)Math.Floor((this.Position.X - 440) / 72);
            int rowIndex = (int)Math.Floor((this.Position.Y - 125) / 72);

            Vector2 newPosition = this.Position;

            if (direction == "right" && colIndex < grid.Content.GetLength(1) - 1)
            {
                this.Texture = this.Textures.right;
                newPosition.X += 72;
            }
            else if (direction == "left" && colIndex > 0)
            {
                this.Texture = this.Textures.left;
                newPosition.X -= 72;
            }
            else if (direction == "down" && rowIndex < grid.Content.GetLength(0) - 1)
            {
                this.Texture = this.Textures.down;
                newPosition.Y += 72;
            }
            else if (direction == "up" && rowIndex > 0)
            {
                this.Texture = this.Textures.up;
                newPosition.Y -= 72;
            }

            // Ellenőrizze, hogy az új pozíció fal-e
            if (!grid.IsWall(newPosition))
            {
                this.Position = newPosition;
            }
        }
        public int DiceRoll(int Size)
        {
            Random rnd = new Random();
            return rnd.Next(1, Size);
        }
    }

}
