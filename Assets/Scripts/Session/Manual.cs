using System;
using Tones.Managers;

namespace Tones.Session
{
    [Serializable]
    public class Manual : Session
    {
        public Manual(int frequency, float volume, TestManager manager) : base(frequency, volume, manager)
        {

        }

        public void StartTone()
        {
            StartSession();
            TonePlayer.Instance.PlayTone(tone);
        }

        public void StopTone()
        {
            tonePlayEvents.EventEnded();
            TonePlayer.Instance.StopTone();
            EndSession();
        }
    }
}