using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Wanderer
{
    public class Game1 : Game
    {
        //Graphics
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont textTexture;
        private Texture2D wallTexture;//fal
        private Texture2D floorTexture;//talaj
        private CharacterTextures heroTexture;//hero
        private CharacterTextures ghostTexture;//szellem
        private CharacterTextures ghost2Texture;
        private CharacterTextures skeletonTexture;
        private CharacterTextures bossTexture;//boss
        private bool loaded;

        //Karakterek
        Hero hero = new Hero(1);
        Monster ghost = new Monster(1);
        Monster ghost2 = new Monster(1);
        Monster skeleton = new Monster(1);
        Monster boss = new Monster(1);

        //Játékmenet
        Grid grid = new Grid(10);

        //Game állapota
        public GameState _currentGameState;
        public enum GameState
        {
            MainMenu,
            Playing,
            // etc...
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            WindowSize(_graphics);
        }

        protected override void Initialize()
        {
            SetGameState(GameState.Playing);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Grid méret beállítás
            grid.CreateGrid();

            //Falak generálása
            grid.GenerateWalls();

            //Load textures
            LoadTextures();

            //Starting positions
            hero.Position = grid.GenerateRandomPosition();
            ghost.Position = grid.GenerateRandomPosition();
            ghost2.Position = grid.GenerateRandomPosition();
            skeleton.Position = grid.GenerateRandomPosition();
            boss.Position = grid.GenerateRandomPosition();

            //Starting directions
            hero.Texture = heroTexture.down;
            ghost.Texture = ghostTexture.down;
            ghost2.Texture = ghost2Texture.down;
            skeleton.Texture = skeletonTexture.down;
            boss.Texture = bossTexture.down;
            //* hero.Texture = grid.GenerateRandomDirection();
            //* hero.Texture = grid.GenerateRandomDirection();        

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            _spriteBatch.Begin();
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.D) && hero.CanMove)
            {
                hero.Move("right", grid);
                ChangeTexture(heroTexture.right,hero);
                Timer(0.2f, hero);
                ghost.Steps++;
                ghost2.Steps++;
                skeleton.Steps++;
                boss.Steps++;

            }
            if (keyboardState.IsKeyDown(Keys.A) && hero.CanMove)
            {
                hero.Move("left", grid);
                ChangeTexture(heroTexture.left, hero);
                Timer(0.2f, hero);
                ghost.Steps++;
                ghost2.Steps++;
                skeleton.Steps++;
                boss.Steps++;
            }
            if (keyboardState.IsKeyDown(Keys.W) && hero.CanMove)
            {
                hero.Move("up", grid);
                ChangeTexture(heroTexture.up, hero);
                Timer(0.2f, hero);
                ghost.Steps++;
                ghost2.Steps++;
                skeleton.Steps++;
                boss.Steps++;
            }
            if (keyboardState.IsKeyDown(Keys.S) && hero.CanMove)
            {
                hero.Move("down", grid);
                ChangeTexture(heroTexture.down, hero);
                Timer(0.2f, hero);
                ghost.Steps++;
                ghost2.Steps++;
                skeleton.Steps++;
                boss.Steps++;
            }

            //Fights
            if (hero.Position == ghost.Position )
            {
                if (keyboardState.IsKeyDown(Keys.Space) && hero.CanFight)
                {
                    hero.Fight(ghost);
                    Timer(0.2f, hero);
                    ghost.Steps++;
                }
                if (ghost.CanFight)
                {
                    ghost.Fight(hero);
                    Timer(1.2f, ghost);
                }
            }



            ghost.MoveRandomly(grid);
            ghost2.MoveRandomly(grid);
            skeleton.MoveRandomly(grid);
            boss.MoveRandomly(grid);

            if (keyboardState.IsKeyDown(Keys.K) && hero.CanMove)
            {
                grid.GenerateWalls();
                Timer(0.2f, hero);
            }


            if (hero.IsDead())
            {
                SetGameState(GameState.MainMenu);
            }
            if (ghost.IsDead())
            {
                
            }





                _spriteBatch.End();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);                   //Háttérszín: Szürke
            if (loaded)
            {
                // Kezdd el a rajzolást itt
                _spriteBatch.Begin();                               

                DrawBackground();                                           //Kirajzoljuk a gridet

                DrawTexture(hero);                                          //Kirajzoljuk a Herot és a statját
                DrawTexture(ghost);                                         //Kirajzoljuk a Ghostot és a statját
                DrawTexture(ghost2);
                DrawTexture(skeleton);
                DrawTexture(boss);

                // Végződj a rajzolással itt
                _spriteBatch.End();                                 
            }
            base.Draw(gameTime);
        }
        public void Timer(float moveDelay, Character character)
        {
            if (character.CanMove)
            {
                character.CanMove = false;
                // Indítsd el az időzítőt, ami után újra mozgás lesz engedélyezve
                System.Timers.Timer timer = new System.Timers.Timer(moveDelay * 1000); // Átváltás másodpercről milliszekundumra
                timer.Elapsed += (sender, e) =>
                {
                    character.CanMove = true;
                };
                timer.AutoReset = false; // Csak egyszer fut le
                timer.Start();
            }
            if (character.CanFight)
            {
                character.CanFight = false;
                // Indítsd el az időzítőt, ami után újra mozgás lesz engedélyezve
                System.Timers.Timer timer = new System.Timers.Timer(moveDelay * 1000); // Átváltás másodpercről milliszekundumra
                timer.Elapsed += (sender, e) =>
                {
                    character.CanFight = true;
                };
                timer.AutoReset = false; // Csak egyszer fut le
                timer.Start();
            }

        }
        public void DrawTexture(Character character)
        {
            if (!character.IsDead())
            {
                if (character.Texture == null)
                {
                    LoadTextures();
                }
                else
                {
                    _spriteBatch.Draw(character.Texture, character.Position, Color.White);
                    DrawCharacterStats(character);
                }
            }
        }
        public void ChangeTexture(Texture2D charTexture, Character character)
        {

            _spriteBatch.Draw(charTexture, character.Position, Color.White);
        }
        public void DrawBackground()
        {
            foreach (var item in grid.Content)
            {
                if (grid.IsWall(item))
                {

                    _spriteBatch.Draw(wallTexture, item, Color.White);
                }
                else
                {

                    _spriteBatch.Draw(floorTexture, item, Color.White);
                }
            }
        }
        public void LoadTextures()
        {
            Content.RootDirectory = "Content/characters";
            heroTexture = new CharacterTextures();
            heroTexture.down = Content.Load<Texture2D>("hero-down");
            heroTexture.up = Content.Load<Texture2D>("hero-up");
            heroTexture.left = Content.Load<Texture2D>("hero-left");
            heroTexture.right = Content.Load<Texture2D>("hero-right");
            hero.Textures = heroTexture;

            ghostTexture = new CharacterTextures();
            ghostTexture.down = Content.Load<Texture2D>("ghost-down");
            ghostTexture.up = Content.Load<Texture2D>("ghost-up");
            ghostTexture.left = Content.Load<Texture2D>("ghost-left");
            ghostTexture.right = Content.Load<Texture2D>("ghost-right");
            ghost.Textures = ghostTexture;

            ghost2Texture = new CharacterTextures();
            ghost2Texture.down = Content.Load<Texture2D>("ghost2-down");
            ghost2Texture.up = Content.Load<Texture2D>("ghost2-up");
            ghost2Texture.left = Content.Load<Texture2D>("ghost2-left");
            ghost2Texture.right = Content.Load<Texture2D>("ghost2-right");
            ghost2.Textures = ghost2Texture;

            skeletonTexture = new CharacterTextures();
            skeletonTexture.down = Content.Load<Texture2D>("skeleton-down");
            skeletonTexture.up = Content.Load<Texture2D>("skeleton-up");
            skeletonTexture.left = Content.Load<Texture2D>("skeleton-left");
            skeletonTexture.right = Content.Load<Texture2D>("skeleton-right");
            skeleton.Textures = skeletonTexture;

            bossTexture = new CharacterTextures();
            bossTexture.down = Content.Load<Texture2D>("boss-down");
            bossTexture.up = Content.Load<Texture2D>("boss-up");
            bossTexture.left = Content.Load<Texture2D>("boss-left");
            bossTexture.right = Content.Load<Texture2D>("boss-right");

            Content.RootDirectory = "Content/gameitems";
            wallTexture = Content.Load<Texture2D>("wall");                      //Wall
            floorTexture = Content.Load<Texture2D>("floor");                    //Floor
            textTexture = Content.Load<SpriteFont>("font");                     //Font
            loaded = true;

        }
        public void SetGameState(GameState newState)
        {
            _currentGameState = newState;
        }
        /*
        List<Vector2> ls = new List<Vector2>();
        public void CharacterStatsController()
        {
            if (ls.Count!=0)
            {
                float newY = ls.Last().Y;
                ls.Add(new Vector2( 10, newY + 10));
            }
            else
            {
                ls.Add(new Vector2(10, 10));
            }
        }
        */
        public void DrawCharacterStats(Character character)
        {
            string characterStats = $" Level: {character.Level} \n HP: {character.HP} / {character.MaxHP} \n DP: {character.DP} \n SP: {character.SP}";
            if (character==hero)
            {
                _spriteBatch.DrawString(textTexture, characterStats, new Vector2(10, 10), Color.Black);
            }
            if (character==ghost)
            {
                _spriteBatch.DrawString(textTexture, characterStats, new Vector2(10, 110), Color.Black);
            }
            if (character == ghost2)
            {
                _spriteBatch.DrawString(textTexture, characterStats, new Vector2(10, 210), Color.Black);
            }
            if (character == skeleton)
            {
                _spriteBatch.DrawString(textTexture, characterStats, new Vector2(10, 310), Color.Black);
            }

            //_spriteBatch.DrawString(textTexture, characterStats, new Vector2(10,10), Color.Black);
        }
        public void WindowSize(GraphicsDeviceManager _graphics)
        {
            // Ablak méretének beállítása
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;
        }
    }
}
