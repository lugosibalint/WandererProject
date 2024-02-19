using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wanderer
{
    public class Grid : Game
    {
        public int Size { get; set; }
        public Vector2[,] Cells { get; set; }
        public Random Rnd { get; set; }
        public List<Vector2> Walls { get; set; }

        public Grid(int gridSize)
        {
            Content.RootDirectory = "Content";
            this.Size = gridSize;
        }
        public void CreateGrid()
        {
            Cells = new Vector2[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Vector2 coords = new Vector2(); // Minden cellához új objektum
                    coords.X = 440 + j * 72;
                    coords.Y = 125 + i * 72;
                    Cells[i, j] = coords;

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
                        Vector2 wallPosition = Cells[i, j];
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
        public Vector2 GenerateRandomPosition()
        {
            Vector2 randomPosition = new Vector2();
            do
            {
                randomPosition = Cells[Rnd.Next(0, Size - 1), Rnd.Next(0, Size - 1)];
            } while (IsWall(randomPosition));
            return randomPosition;
        }
        public CharacterTextures LoadHeroTextures(ContentManager Content)
        {
            Content.RootDirectory = "Content/characters/hero";
            CharacterTextures heroTextures = new CharacterTextures();
            heroTextures.down = Content.Load<Texture2D>("hero-down");
            heroTextures.up = Content.Load<Texture2D>("hero-up");
            heroTextures.left = Content.Load<Texture2D>("hero-left");
            heroTextures.right = Content.Load<Texture2D>("hero-right");
            return heroTextures;
        }
        public CharacterTextures LoadBossTextures(ContentManager Content)
        {
            Content.RootDirectory = "Content/characters/monsters/boss";
            CharacterTextures bossTextures = new CharacterTextures();
            bossTextures.down = Content.Load<Texture2D>("boss-down");
            bossTextures.up = Content.Load<Texture2D>("boss-up");
            bossTextures.left = Content.Load<Texture2D>("boss-left");
            bossTextures.right = Content.Load<Texture2D>("boss-right");
            return bossTextures;
        }
        public CharacterTextures LoadMonsterTextures(ContentManager Content)
        {
            string[] txt = File.ReadAllLines("texturelist.txt");
            string pickedLine = txt[Rnd.Next(1, txt.Length)];
            Content.RootDirectory = $"Content/characters/monsters/{pickedLine}";
            CharacterTextures monsterTextures = new CharacterTextures();
            monsterTextures.down = Content.Load<Texture2D>(pickedLine + "-down");
            monsterTextures.up = Content.Load<Texture2D>(pickedLine + "-up");
            monsterTextures.left = Content.Load<Texture2D>(pickedLine + "-left");
            monsterTextures.right = Content.Load<Texture2D>(pickedLine + "-right");
            return monsterTextures;
        }
        public CharacterTextures LoadFruitTextures(ContentManager Content)
        {
            string[] txt = File.ReadAllLines("fruitlist.txt");
            string pickedLine = txt[Rnd.Next(1, txt.Length)];
            Content.RootDirectory = $"Content/gameitems/fruits/{pickedLine}";
            CharacterTextures fruitTextures = new CharacterTextures();
            fruitTextures.down = Content.Load<Texture2D>(pickedLine + "-down");
            fruitTextures.up = Content.Load<Texture2D>(pickedLine + "-up");
            fruitTextures.left = Content.Load<Texture2D>(pickedLine + "-left");
            fruitTextures.right = Content.Load<Texture2D>(pickedLine + "-right");
            return fruitTextures;
        }
    }
}
