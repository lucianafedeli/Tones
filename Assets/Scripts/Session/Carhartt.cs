using System;
using Tones.Managers;

namespace Tones.Sessions
{
    [Serializable]
    public class Carhartt : Session
    {
        public Carhartt(byte frequencyIndex, int dB, TestManager manager, Tone.EarSide isLeftEar) : base(frequencyIndex, dB, manager, isLeftEar)
        {

        }

        public override void StartSession()
        {
            tonePlayEvents.EventStarted();
            TonePlayer.Instance.PlayTone(tone);
        }
    }
}