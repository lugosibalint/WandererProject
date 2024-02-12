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
    public class MainMenu
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private Game1 _game;

        public MainMenu(SpriteBatch spriteBatch, SpriteFont font, Game1 game)
        {
            _spriteBatch = spriteBatch;
            _font = font;
            _game = game;
        }

        public void Update(GameTime gameTime)
        {
            // Ellenőrizzük a billentyűzet állapotát, és kezeljük a választásokat
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                // Játék indítása
                StartGame();
            }
            else if (keyboardState.IsKeyDown(Keys.Escape))
            {
                // Kilépés a játékból
                ExitGame();
            }
        }

        public void Draw()
        {
            _spriteBatch.Begin();

            // Rajzold ki a főmenü szövegeket
            string startText = "Nyomd meg az Enter-t a játék indításához";
            string exitText = "Nyomd meg az Esc-et a kilépéshez";

            Vector2 startTextPosition = new Vector2(100, 100);
            Vector2 exitTextPosition = new Vector2(100, 150);

            _spriteBatch.DrawString(_font, startText, startTextPosition, Color.White);
            _spriteBatch.DrawString(_font, exitText, exitTextPosition, Color.White);

            _spriteBatch.End();
        }

        private void StartGame()
        {
            // Itt indíthatod el a játékot vagy átnavigálhatsz egy új játékállapotba
            _game.SetGameState(GameState.Playing); // Hívjuk meg a játék állapotát beállító metódust
        }

        private void ExitGame()
        {
            // Itt kezelheted a kilépést a játékból
            // Példa: game.Exit();
        }
    }
}
