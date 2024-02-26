using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wanderer
{
    public class AudioManager
    {
        private ContentManager content;
        public SoundEffect error;
        public AudioManager()
        {
            content.RootDirectory = "Content/sfx";
            error = content.Load<SoundEffect>("error");
        }
    }
}
