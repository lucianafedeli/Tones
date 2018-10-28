using System;
using Tones.Managers;

namespace Tones.Sessions
{
    [Serializable]
    public class Carhartt : Session
    {
        public Carhartt(byte frequencyIndex, float volume, TestManager manager, Tone.EarSide isLeftEar) : base(frequencyIndex, volume, manager, isLeftEar)
        {
            StartSession();
            TonePlayer.Instance.PlayTone(tone);
        }


    }
}