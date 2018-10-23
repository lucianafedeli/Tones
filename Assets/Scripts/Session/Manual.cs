using System;
using Tones.Managers;

namespace Tones.Sessions
{
    [Serializable]
    public class Manual : Session
    {
        public Manual(byte frequencyIndex, float volume, TestManager manager, Tone.EarSide isLeftEar) : base(frequencyIndex, volume, manager, isLeftEar)
        {
            StartSession();
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