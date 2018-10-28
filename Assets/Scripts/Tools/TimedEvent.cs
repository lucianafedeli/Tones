using System;

namespace Tools
{
    [Serializable]
    public class TimedEvent
    {
        public TimedEvent(float start, float end)
        {
            this.start = start;
            this.end = end;
            if (end < start)
            {
                throw new Exception("Event ended before it started?");
            }
        }

        public float start;
        public float end;


        public float Duration()
        {
            return end - start;
        }

        public override string ToString()
        {
            return "Event Started: " + start.ToString("0.00") + "s\t" +
                   "Event Ended: " + end.ToString("0.00") + "s\t" +
                   "Duration: " + Duration().ToString("0.00") + "s\n";
        }
    }
}