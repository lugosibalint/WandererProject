using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

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

        //Positions
        private Vector2 heroPos;
        private Vector2 ghostPos;

        //Játékmenet
        private Vector2[,] grid;
        private int gridSize;

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

        //Karakterek
        private Character hero = new Character(1);

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
            gridSize = 10;
            CreateGrid(gridSize);

            //Falak generálása
            GenerateWalls(gridSize);

            //Load font
            textTexture = Content.Load<SpriteFont>("font");
         
            //Load textures
            LoadTextures();

            //Starting positions
            heroPos = GenerateRandomPosition();
            ghostPos = GenerateRandomPosition();

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
                ButtonState.Pressed || 
                keyboardState.IsKeyDown(Keys.Escape))
            {

                SetGameState(GameState.MainMenu);
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
                ButtonState.Pressed ||
                keyboardState.IsKeyDown(Keys.D))
            {
                Move("right", heroPos);
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

                DrawBackground(gridSize);                           //Kirajzoljuk a gridet
                DrawTexture(heroTexture.down, heroPos);             //Kirajzoljuk a Herot
                DrawTexture(ghostTexture.down, ghostPos);           //Kirajzoljuk a Ghostot
                DrawCharacterStats(hero);                           //Kirajzoljuk a hero statját

                // Végződj a rajzolással itt
                _spriteBatch.End();                                 
            }
            base.Draw(gameTime);
        }
        public void Move(string direction, Vector2 charPos)
        {
            if (direction == "right")
            {
                
            }
        }
        public Vector2 GenerateRandomPosition()
        {
            return grid[rnd.Next(0, gridSize - 1), rnd.Next(0, gridSize - 1)];
        }
        public void DrawTexture(Texture2D charTexture, Vector2 charPos)
        {
            
            _spriteBatch.Draw(charTexture, charPos, Color.White);
        }
        public void GenerateWalls(int gridSize)
        {
            rnd = new Random();
            walls = new List<Vector2>();
            

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    int rndnum = rnd.Next(1, 10);
                    if (rndnum > 7)
                    {
                        Vector2 wallPosition = grid[i, j];
                        walls.Add(wallPosition);
                    }
                }
            }
        }
        public bool IsWall(Vector2 vector)
        {
            if (walls.Contains(vector))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void DrawBackground(int gridSize)
        {
            foreach (var item in grid)
            {
                if (IsWall(item))
                {

                    _spriteBatch.Draw(wallTexture, item, Color.White);
                }
                else
                {

                    _spriteBatch.Draw(floorTexture, item, Color.White);
                }
            }
        }
        public void CreateGrid(int gridSize)
        {
            grid = new Vector2[gridSize, gridSize];
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    Vector2 coords = new Vector2(); // Minden cellához új objektum
                    coords.X = 440 + j * 72;
                    coords.Y = 125 + i * 72;
                    grid[i, j] = coords;
                    
                    /*
                    if (walls[wallidx])
                    {
                        _spriteBatch.Draw(wallTexture, new Vector2(coords.X, coords.Y), Color.White);
                    }
                    else
                    {
                        _spriteBatch.Draw(floorTexture, new Vector2(coords.X, coords.Y), Color.White);
                    }
                    */
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
