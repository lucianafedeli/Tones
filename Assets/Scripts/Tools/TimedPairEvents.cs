using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    [Serializable]
    public class TimedPairEvents
    {
        public List<TimedEvent> pairs = new List<TimedEvent>();


        private bool currentEventIsOngoing = false;
        public bool CurrentEventIsOngoing
        {
            get { return currentEventIsOngoing; }
        }

        private float eventStartedAt;

        public void EventStarted()
        {
            currentEventIsOngoing = true;
            eventStartedAt = Time.realtimeSinceStartup;
        }

        public void EventEnded()
        {
            currentEventIsOngoing = false;
            pairs.Add(new TimedEvent(eventStartedAt, Time.realtimeSinceStartup));
        }

        public void AddPair(TimedEvent newEvent)
        {
            pairs.Add(newEvent);
        }

        public TimedPairEvents Normalized(float normalizer)
        {
            TimedPairEvents normalizedTimedPairEvents = new TimedPairEvents();
            normalizedTimedPairEvents.pairs = this.pairs;

            for (int i = 0; i < normalizedTimedPairEvents.pairs.Count; i++)
            {
                normalizedTimedPairEvents.pairs[i].start = normalizedTimedPairEvents.pairs[i].start - normalizer;
                normalizedTimedPairEvents.pairs[i].end = normalizedTimedPairEvents.pairs[i].end - normalizer;
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