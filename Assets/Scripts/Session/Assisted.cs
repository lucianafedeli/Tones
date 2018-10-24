using System;
using Tones.Managers;

namespace Tones.Sessions
{
    [Serializable]
    public class Assisted : Session
    {
        private static float toneDuration = 1;

        public Assisted(byte frequencyIndex, float volume, float prePlayDelay, TestManager manager, Tone.EarSide isLeftEar) : base(frequencyIndex, volume, manager, isLeftEar)
        {
        }
    }
}
