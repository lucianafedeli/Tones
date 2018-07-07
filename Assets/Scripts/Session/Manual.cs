using System;
using Tone;

namespace Session
{
    [Serializable]
    public class Manual : Session
    {
        public Manual(int frequency, float volume) : base(frequency, volume)
        {
            tonePlayEvents.EventStarted();
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