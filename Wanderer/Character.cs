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
        public Vector2 position { get; set; }

        public Character(int level)
        {
            this.Level = level;
            this.position = new Vector2();
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
            /*
            int colIndex = (int)Math.Floor((this.position.X - 440) / 72); //X
            int rowIndex = (int)Math.Floor((this.position.Y - 125) / 72); //Y
            
            if (direction=="right")
            {
                if (rowIndex != grid.Size-1)
                {
                    Vector2 newPosition = grid.Content[colIndex+1, rowIndex];
                    if (!grid.IsWall(newPosition))
                    {
                        colIndex += 1;
                        position = grid.Content[colIndex, rowIndex];
                    }
                }
            }
            if (direction == "left")
            {
                if (rowIndex != 0)
                {
                    Vector2 newPosition = grid.Content[colIndex, rowIndex + 1];
                    if (!grid.IsWall(newPosition))
                    {
                        position = newPosition;
                    }
                }
            }
            if (direction == "up")
            {
                if (rowIndex != grid.Size - 1)
                {
                    Vector2 newPosition = grid.Content[colIndex, rowIndex + 1];
                    if (!grid.IsWall(newPosition))
                    {
                        position = newPosition;
                    }
                }
            }
            if (direction == "down")
            {
                if (rowIndex != grid.Size - 1)
                {
                    Vector2 newPosition = grid.Content[colIndex, rowIndex + 1];
                    if (!grid.IsWall(newPosition))
                    {
                        position = newPosition;
                    }
                }
            }

            //if (!grid.IsWall(newpos) || colIndex != 0 || colIndex != grid.Size - 1 || rowIndex != 0 || colIndex != grid.Size - 1)

            */
            int colIndex = (int)Math.Floor((this.position.X - 440) / 72);
            int rowIndex = (int)Math.Floor((this.position.Y - 125) / 72);

            Vector2 newPosition = this.position;

            if (direction == "right" && colIndex < grid.Content.GetLength(1) - 1)
            {
                newPosition.X += 72;
            }
            else if (direction == "left" && colIndex > 0)
            {
                newPosition.X -= 72;
            }
            else if (direction == "down" && rowIndex < grid.Content.GetLength(0) - 1)
            {
                newPosition.Y += 72;
            }
            else if (direction == "up" && rowIndex > 0)
            {
                newPosition.Y -= 72;
            }

            // Ellenőrizze, hogy az új pozíció fal-e
            if (!grid.IsWall(newPosition))
            {
                this.position = newPosition;
            }
        }
    }

}
