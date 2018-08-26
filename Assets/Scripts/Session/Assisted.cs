using Tones.Managers;

namespace Tones.Session
{
    public class Assisted : Session
    {
        private static float toneDuration = 2.5f;

        public Assisted(int frequency, float volume, float prePlayDelay, TestManager manager) : base(frequency, volume, manager)
        {
        }
    }
}
