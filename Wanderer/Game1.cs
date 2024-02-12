using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private CharacterTextures bossTexture;//boss
        private bool loaded;

        //Karakterek
        Hero hero = new Hero(1);
        Monster ghost = new Monster(1);

        //Játékmenet
        Grid grid = new Grid(10);
        private bool canMove = true;

        private List<Vector2> walls;
        Random rnd;

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

            //Load font
            textTexture = Content.Load<SpriteFont>("font");
         
            //Load textures
            LoadTextures();

            //Starting positions
            hero.position = grid.GenerateRandomPosition();
            ghost.position = grid.GenerateRandomPosition();

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.D) && canMove)
            {
                hero.Move("right", grid);
                
                Timer(0.2f);
            }
            if (keyboardState.IsKeyDown(Keys.A) && canMove)
            {
                hero.Move("left", grid);
                Timer(0.2f);
            }
            if (keyboardState.IsKeyDown(Keys.W) && canMove)
            {
                hero.Move("up", grid);
                Timer(0.2f);
            }
            if (keyboardState.IsKeyDown(Keys.S) && canMove)
            {
                hero.Move("down", grid);
                Timer(0.2f);
            }


            if (keyboardState.IsKeyDown(Keys.K) && canMove)
            {
                grid.GenerateWalls();
                Timer(0.2f);
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);                   //Háttérszín: Szürke
            if (loaded)
            {
                // Kezdd el a rajzolást itt
                _spriteBatch.Begin();                               

                DrawBackground();                                         //Kirajzoljuk a gridet
                DrawTexture(heroTexture.down, hero.position);             //Kirajzoljuk a Herot
                DrawTexture(ghostTexture.down, ghost.position);           //Kirajzoljuk a Ghostot
                DrawCharacterStats(hero);                                 //Kirajzoljuk a hero statját

                // Végződj a rajzolással itt
                _spriteBatch.End();                                 
            }
            base.Draw(gameTime);
        }
        public void Timer(float moveDelay)
        {
            canMove = false;
            // Indítsd el az időzítőt, ami után újra mozgás lesz engedélyezve
            System.Timers.Timer timer = new System.Timers.Timer(moveDelay * 1000); // Átváltás másodpercről milliszekundumra
            timer.Elapsed += (sender, e) => canMove = true;
            timer.AutoReset = false; // Csak egyszer fut le
            timer.Start();

        }
        public void DrawTexture(Texture2D charTexture, Vector2 charPos)
        {
            
            _spriteBatch.Draw(charTexture, charPos, Color.White);
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

            ghostTexture = new CharacterTextures();
            ghostTexture.down = Content.Load<Texture2D>("ghost-down");
            ghostTexture.up = Content.Load<Texture2D>("ghost-up");
            ghostTexture.left = Content.Load<Texture2D>("ghost-left");
            ghostTexture.right = Content.Load<Texture2D>("ghost-right");

            bossTexture = new CharacterTextures();
            bossTexture.down = Content.Load<Texture2D>("boss-down");
            bossTexture.up = Content.Load<Texture2D>("boss-up");
            bossTexture.left = Content.Load<Texture2D>("boss-left");
            bossTexture.right = Content.Load<Texture2D>("boss-right");

            Content.RootDirectory = "Content/gameitems";
            wallTexture = Content.Load<Texture2D>("wall");                    //Wall
            floorTexture = Content.Load<Texture2D>("floor");                    //Floor
            loaded = true;

        }
        public void SetGameState(GameState newState)
        {
            _currentGameState = newState;
        }
        public void DrawCharacterStats(Character character)
        {

            string characterStats = $" Level: {character.Level} \n HP: {character.HP} / {character.MaxHP} \n DP: {character.DP} \n SP: {character.SP}";
            _spriteBatch.DrawString(textTexture, characterStats, new Vector2(10, 10), Color.White);
        }
        public void WindowSize(GraphicsDeviceManager _graphics)
        {
            // Ablak méretének beállítása
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;
        }
    }
}
