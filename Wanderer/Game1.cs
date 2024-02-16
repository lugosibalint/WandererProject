using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
        private Texture2D keyTexture;//kulcs
        //Karakterek
        List<Monster> monsters = new List<Monster>();
        private Hero hero = new Hero(1);
        private Boss boss = new Boss(2);
        //Játékmenet
        Grid grid = new Grid(10);
        private Vector2 statposition = new Vector2(10, 10);

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

            //generate characters
            GenerateCharacters(grid.Rnd.Next(1,4));

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            _spriteBatch.Begin();
            MovementKeys();
            FightKeys();
            //Fights


            foreach (var monster in monsters)
            {
                monster.MoveRandomly(grid);
            }
            boss.MoveRandomly(grid);

            if (hero.IsDead)
            {
                SetGameState(GameState.MainMenu);
            }

            _spriteBatch.End();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);                   //Háttérszín: Szürke
            if (true)
            {
                // Kezdd el a rajzolást itt
                _spriteBatch.Begin();                               

                DrawBackground();                                           //Kirajzoljuk a gridet

                //Kirajzoljuk a Herot és a statját
                DrawTexture(hero);
                //Kirajzoljuk a Bosst és a statját
                DrawTexture(boss);
                //Kirajzoljuk a Szörnyeket és a statukat

                ShowKey();

                if (monsters.Count != 0)
                {
                    for (int i = 0; i < monsters.Count; i++)
                    {
                        if (!monsters[i].IsDead)
                        {
                            DrawTexture(monsters[i]);
                        }
                    }
                }
                // Végződj a rajzolással itt
                _spriteBatch.End();                                 
            }
            base.Draw(gameTime);
        }

        public void NextLevel()
        {

            // Következő pálya inicializálása
            InitializeNextLevel();

            // Gyógyulás esélyeinek ellenőrzése és végrehajtása
            Random random = new Random();
            int chance = random.Next(1, 10);

            if (chance <= 1)
            {
                // 10% esély az összes HP visszatöltésére
                hero.HP = hero.MaxHP;
            }
            else if (chance <= 4)
            {
                // 40% esély a HP harmadának a visszatöltésére
                hero.HP += hero.MaxHP / 3;
                if (hero.HP > hero.MaxHP)
                    hero.HP = hero.MaxHP;
            }
            else
            {
                // 50% esély a HP 10%-nak a visszatöltésére
                hero.HP += hero.MaxHP / 10;
                if (hero.HP > hero.MaxHP)
                    hero.HP = hero.MaxHP;
            }
        }

        public void InitializeNextLevel()
        {
            hero.LevelUp();
            boss.LevelUp();
            monsters.Clear();
            statposition = new Vector2(10, 10);
            grid.GenerateWalls();
            GenerateCharacters(grid.Rnd.Next(hero.Level, hero.Level + 2));
        }
        public void ShowKey()
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].IsDead)
                {
                    monsters.Remove(monsters[i]);
                    if (monsters[i].hasKey && boss.IsDead)
                    {
                        _spriteBatch.Draw(keyTexture, new Vector2(1500, 110), Color.White);
                        NextLevel();
                    }
                    else
                    {
                        hero.LevelUp();
                    }
                }
            }
        }
        public void FightKeys()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            foreach (var monster in monsters)
            {
                if (hero.Position == monster.Position)
                {
                    if (keyboardState.IsKeyDown(Keys.Space) && hero.CanFight)
                    {
                        hero.Fight(monster);
                        HeroTimer(0.2f);
                        monster.Steps++;
                    }
                    /*
                    if (monster.CanFight)
                    {
                        monster.Fight(hero);
                        Timer(1.2f, monster);
                    }
                    */
                }
                
            }
            if (hero.Position == boss.Position)
            {
                if (keyboardState.IsKeyDown(Keys.Space)&&hero.CanFight)
                {
                    hero.Fight(boss);
                    HeroTimer(0.2f);
                    boss.Steps++;
                }
            }
        }
        public void MovementKeys()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.D) && hero.CanMove)
            {
                hero.Move("right", grid);
                ChangeTexture(hero.Textures.right, hero);
                HeroTimer(0.2f);
                foreach (var item in monsters)
                {
                    item.Steps++;
                }
            }
            if (keyboardState.IsKeyDown(Keys.A) && hero.CanMove)
            {
                hero.Move("left", grid);
                ChangeTexture(hero.Textures.left, hero);
                HeroTimer(0.2f);
                foreach (var item in monsters)
                {
                    item.Steps++;
                }
            }
            if (keyboardState.IsKeyDown(Keys.W) && hero.CanMove)
            {
                hero.Move("up", grid);
                ChangeTexture(hero.Textures.up, hero);
                HeroTimer(0.2f);
                foreach (var item in monsters)
                {
                    item.Steps++;
                }
            }
            if (keyboardState.IsKeyDown(Keys.S) && hero.CanMove)
            {
                hero.Move("down", grid);
                ChangeTexture(hero.Textures.down, hero);
                HeroTimer(0.2f);
                foreach (var item in monsters)
                {
                    item.Steps++;
                }
            }
            //
            if (keyboardState.IsKeyDown(Keys.K) && hero.CanMove)
            {
                NextLevel();
                HeroTimer(0.2f);
            }
            //
        }
        public void HeroTimer(float moveDelay)
        {
            if (hero.CanMove)
            {
                hero.CanMove = false;
                // Indítsd el az időzítőt, ami után újra mozgás lesz engedélyezve
                System.Timers.Timer timer = new System.Timers.Timer(moveDelay * 1000); // Átváltás másodpercről milliszekundumra
                timer.Elapsed += (sender, e) =>
                {
                    hero.CanMove = true;
                };
                timer.AutoReset = false; // Csak egyszer fut le
                timer.Start();
            }
            if (hero.CanFight)
            {
                hero.CanFight = false;
                // Indítsd el az időzítőt, ami után újra mozgás lesz engedélyezve
                System.Timers.Timer timer = new System.Timers.Timer(moveDelay * 1000); // Átváltás másodpercről milliszekundumra
                timer.Elapsed += (sender, e) =>
                {
                    hero.CanFight = true;
                };
                timer.AutoReset = false; // Csak egyszer fut le
                timer.Start();
            }

        }
        public void DrawTexture(Character character)
        {

            if (character.Texture != null)
            {
                _spriteBatch.Draw(character.Texture, character.Position, Color.White);
                DrawCharacterStats(character);
            }
            else
            {
                LoadContent();
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
            /*
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
            */

            grid.LoadHeroTextures(Content);
            grid.LoadMonsterTextures(Content);
            grid.LoadBossTextures(Content);

            Content.RootDirectory = "Content/gameitems";
            textTexture = Content.Load<SpriteFont>("font");                     //Font
            wallTexture = Content.Load<Texture2D>("wall");                      //Wall
            floorTexture = Content.Load<Texture2D>("floor");                    //Floor
            keyTexture = Content.Load<Texture2D>("key");

        }
        public void SetGameState(GameState newState)
        {
            _currentGameState = newState;
        }
        public void DrawCharacterStats(Character character)
        {
            string characterStats = $" Level: {character.Level} \n HP: {character.HP} / {character.MaxHP} \n DP: {character.DP} \n SP: {character.SP}";
            _spriteBatch.DrawString(textTexture, characterStats, character.StatPosition, Color.Black);
        }
        public void IncreaseStatposition(Vector2 StatPosition)
        {
            if (StatPosition.Y == 810)
            {
                statposition.Y = 10;
                statposition.X += 150;
            }
            else
            {
                statposition.Y += 100;
            }

        }
        public void GenerateCharacters(int count)
        {
            //hero
            hero.Position = grid.GenerateRandomPosition();
            hero.Textures = grid.LoadHeroTextures(Content);
            hero.Texture = hero.Textures.down;
            //boss
            boss.Position = grid.GenerateRandomPosition();
            boss.Textures = grid.LoadBossTextures(Content);
            boss.Texture = boss.Textures.down;
            //monsters
            int keyIndex = grid.Rnd.Next(0, count-1);
            for (int i = 0; i < count; i++)
            {
                Monster monster = new Monster(1); // Adj megfelelő paramétereket
                monster.Position = grid.GenerateRandomPosition();
                monster.Textures = grid.LoadMonsterTextures(Content);
                monster.Texture = monster.Textures.down;
                monster.StatPosition = statposition;
                IncreaseStatposition(monster.StatPosition);
                monster.hasKey = (i == keyIndex);
                monsters.Add(monster);
            }
        }
        public void WindowSize(GraphicsDeviceManager _graphics)
        {
            // Ablak méretének beállítása
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;
        }
    }
}
