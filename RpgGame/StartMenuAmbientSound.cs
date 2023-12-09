using Stride.Audio;
using Stride.Engine;

namespace RpgGame.StartMenu
{
    public class StartMenuAmbientSound : StartupScript
    {
        private SoundInstance soundInstance;
        public override void Start()
        {
            var backgroundMusic = Content.Load<Sound>("Sounds/SpookyAmbientMusic");
            soundInstance = backgroundMusic.CreateInstance();
            soundInstance.IsLooping = true;
            soundInstance.Volume = 0.1f;
            soundInstance.Play();
        }

        public override void Cancel()
        {
            soundInstance.Stop();
        }
    }
}
