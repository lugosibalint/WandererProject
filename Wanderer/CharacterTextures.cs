using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wanderer
{
    public struct CharacterTextures
    {
        public Texture2D up { get; set; }
        public Texture2D down { get; set; }
        public Texture2D left { get; set; }
        public Texture2D right { get; set; }
        public CharacterTextures(Texture2D up, Texture2D down, Texture2D left, Texture2D right)
        {
            this.up = up;
            this.down = down;
            this.left = left;
            this.right = right;
        }
        public void FileRead()
        {
        }
    }
}
