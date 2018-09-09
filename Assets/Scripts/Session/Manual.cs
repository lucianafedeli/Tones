using System;
using Tones.Managers;

namespace Tones.Session
{
    [Serializable]
    public class Manual : Session
    {
        public Manual(int frequency, float volume, TestManager manager) : base(frequency, volume, manager)
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