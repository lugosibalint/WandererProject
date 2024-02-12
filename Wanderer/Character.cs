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
        private SpriteBatch _spriteBatch;

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
    }

}
