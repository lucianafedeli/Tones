using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    [Serializable]
    public class TimedEvent
    {
        public TimedEvent(float start, float end)
        {
            this.Start = start;
            this.End = end;
            if (end < start)
            {
                throw new Exception("Event ended before it started?");
            }
        }

        public float Start { get; set; }
        public float End { get; set; }


        public float Duration()
        {
            return End - Start;
        }

        public override string ToString()
        {
            return "Event Started: " + Start.ToString("0.00") + "s\t" +
                   "Event Ended: " + End.ToString("0.00") + "s\t" +
                   "Duration: " + Duration().ToString("0.00") + "s\n";
        }
    }

    [Serializable]
    public class TimedPairEvents
    {
        public TimedPairEvents()
        {

        }

        public TimedPairEvents(List<TimedEvent> pairs)
        {
            this.pairs = pairs;
        }

        private List<TimedEvent> pairs = new List<TimedEvent>();
        public List<TimedEvent> Pairs
        {
            get
            {
                return pairs;
            }
        }

        [NonSerialized]
        private bool currentEventIsOngoing = false;
        public bool CurrentEventIsOngoing
        {
            get { return currentEventIsOngoing; }
            private set { currentEventIsOngoing = value; }
        }

        private float eventStartedAt;

        public void EventStarted()
        {
            CurrentEventIsOngoing = true;
            eventStartedAt = Time.time;
        }

        public void EventEnded()
        {
            CurrentEventIsOngoing = false;
            pairs.Add(new TimedEvent(eventStartedAt, Time.time));
        }

        public void AddPair(TimedEvent newEvent)
        {
            pairs.Add(newEvent);
        }

        public TimedPairEvents Normalized(float normalizer)
        {
            TimedPairEvents normalizedTimedPairEvents = new TimedPairEvents(this.pairs);

            for (int i = 0; i < normalizedTimedPairEvents.pairs.Count; i++)
            {
                normalizedTimedPairEvents.pairs[i].Start = normalizedTimedPairEvents.pairs[i].Start - normalizer;
                normalizedTimedPairEvents.pairs[i].End = normalizedTimedPairEvents.pairs[i].End - normalizer;
            }

            return normalizedTimedPairEvents;
        }

        public float GetLongestDuration()
        {
            float max = 0;
            for (int i = 0; i < pairs.Count; i++)
            {
                float duration = pairs[i].Duration();
                if (max < duration)
                {
                    max = duration;
                }
            }
            return max;
        }

        public override string ToString()
        {
            string toStringReturn = string.Empty;
            for (int i = 0; i < pairs.Count; i++)
            {
                toStringReturn += pairs[i].ToString();
                toStringReturn += '\n';
            }
            return toStringReturn;
        }
    }
}