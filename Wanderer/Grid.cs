using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wanderer
{
    public class Grid
    {
        public int Size { get; set; }
        public Vector2[,] Content { get; set; }
        public Random Rnd { get; set; }
        public List<Vector2> Walls { get; set; }

        public Grid(int gridSize)
        {
            this.Size = gridSize;
        }
        public void CreateGrid()
        {
            Content = new Vector2[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Vector2 coords = new Vector2(); // Minden cellához új objektum
                    coords.X = 440 + j * 72;
                    coords.Y = 125 + i * 72;
                    Content[i, j] = coords;

                }
            }
        }
        public void GenerateWalls()
        {
            Rnd = new Random();
            Walls = new List<Vector2>();


            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    int rndnum = Rnd.Next(1, 10);
                    if (rndnum > 7)
                    {
                        Vector2 wallPosition = Content[i, j];
                        Walls.Add(wallPosition);
                    }
                }
            }
        }

        public bool IsWall(Vector2 vector)
        {
            if (Walls.Contains(vector))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /*
        public Texture2D GenerateRandomDirection()
        {
            return randomTexture;
        }
        */
        public Vector2 GenerateRandomPosition()
        {
            Vector2 randomPosition = new Vector2();
            do
            {
                randomPosition = Content[Rnd.Next(0, Size - 1), Rnd.Next(0, Size - 1)];
            } while (IsWall(randomPosition));
            return randomPosition;
        }
    }
}
