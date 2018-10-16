using Tones.Managers;

namespace Tones.Sessions
{
    public class Assisted : Session
    {
        private static float toneDuration = 2.5f;

        public Assisted(int frequency, float volume, float prePlayDelay, TestManager manager, Tone.EarSide isLeftEar) : base(frequency, volume, manager, isLeftEar)
        {
        }
    }
}
