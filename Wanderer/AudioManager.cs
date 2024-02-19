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
        private SoundEffect soundEffect;

        public AudioManager(ContentManager content, string soundPath)
        {
            soundEffect = content.Load<SoundEffect>(soundPath);
        }

        public void PlaySoundEffect()
        {
            SoundEffectInstance soundInstance = soundEffect.CreateInstance();
            soundInstance.Play();
        }
    }
}
