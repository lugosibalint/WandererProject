using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Wanderer.Game1;

namespace Wanderer
{
    public class Menu : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private Game1 _game;

        private GameState _gameState;
        public Menu()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content/gameitems";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("font");                     //Font
            _game = new Game1();
        }
        protected override void Update(GameTime gameTime)
        {
            // Ellenőrizzük a billentyűzet állapotát, és kezeljük a választásokat

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                // Játék indítása
                StartGame();
            }
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                // Kilépés a játékból
                ExitGame();
            }

            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);                   //Háttérszín: Fekete
            _spriteBatch.Begin();
            
            // Rajzold ki a főmenü szövegeket
            string startText = "Nyomd meg az Enter-t a jatek inditasahoz";
            string exitText = "Nyomd meg az Esc-et a kilepeshez";

            Vector2 startTextPosition = new Vector2(100, 100);
            Vector2 exitTextPosition = new Vector2(100, 150);

            _spriteBatch.DrawString(_font, startText, startTextPosition, Color.White);
            _spriteBatch.DrawString(_font, exitText, exitTextPosition, Color.White);

            _spriteBatch.End();
        }
        private void ChangeState()
        {
            Exit();
            _game.Run();
            _gameState = GameState.Game1;
        }
        private void StartGame()
        {
            // Itt indíthatod el a játékot vagy átnavigálhatsz egy új játékállapotba
            ChangeState();
        }

        private void ExitGame()
        {
            // Itt kezelheted a kilépést a játékból
            // Példa: game.Exit();
            Exit();
        }
    }
}
