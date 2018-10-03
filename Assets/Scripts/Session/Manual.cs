using System;
using Tones.Managers;

namespace Tones.Sessions
{
    [Serializable]
    public class Manual : Session
    {
        public Manual(int frequency, float volume, TestManager manager, Tone.EarSide isLeftEar) : base(frequency, volume, manager, isLeftEar)
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