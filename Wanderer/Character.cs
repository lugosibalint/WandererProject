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
        public bool IsDead { get; set; }
        public Vector2 StatPosition { get; set; }

        public Character(int level)
        {
            this.Level = level;
            this.Position = new Vector2();
            this.Texture = Textures.down;
            this.IsDead = false;
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
            if (this.GetType() == opponent.GetType())
            {
                return;
            }
            // Támadásnál a támadó értéke (SV) az SP és a d6 kétszeresének összege
            int attackValue = this.SP + DiceRoll(6) + DiceRoll(6);

            // A támadás sikeres, ha az attackValue nagyobb, mint az ellenfél DP-je
            if (attackValue > opponent.DP)
            {
                // Sikeres támadás, csökken az ellenfél HP-ja
                int damage = attackValue - opponent.DP;
                opponent.HP -= damage;

                // Ellenfél HP-ja 0 alatt van akkor meghalt
                if (opponent.HP <= 0)
                {
                    opponent.IsDead = true;
                }
            }
        
        }
        public void LevelUp()
        {
            Level++;
            MaxHP += DiceRoll(6);
            DP += DiceRoll(6);
            SP += DiceRoll(6);
            HP = MaxHP;
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
