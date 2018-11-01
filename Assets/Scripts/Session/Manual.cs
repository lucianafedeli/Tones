using System;
using Tones.Managers;

namespace Tones.Sessions
{
    [Serializable]
    public class Classic : Session
    {
        public Classic(byte frequencyIndex, int dB, TestManager manager, Tone.EarSide isLeftEar) : base(frequencyIndex, dB, manager, isLeftEar)
        {
        }

        public override void StartSession()
        {
        }

        public void StartTone()
        {
            tonePlayEvents.EventStarted();
            TonePlayer.Instance.PlayTone(tone);
        }

        public void StopTone()
        {
            tonePlayEvents.EventEnded();
            TonePlayer.Instance.StopTone();
        }
    }
}