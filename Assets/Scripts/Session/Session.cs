using Managers;
using System;
using Tone;
using Tools;
using UnityEngine;

namespace Session
{
    /// <summary>
    /// Esta clase se encarga de definir la duracion total de las partes de una prueba 
    /// dada una frecuencia y un volumen.
    /// Es la base de las sesiones clasicas y experimentales.
    /// </summary>
    [Serializable]
    public abstract class Session : MonoBehaviour
    {
        protected static TestManager testManager;

        protected Tone.Tone tone = null;

        protected byte frequencyIndex = 3;

        [SerializeField]
        private bool succeded = false;
        private bool isPacientPushOngoing = false;

        protected TimedPairEvents tonePlayEvents = new TimedPairEvents();
        public TimedPairEvents TonePlayEvents
        {
            get
            {
                if (succeded)
                    return tonePlayEvents;
                else
                    return null;
            }
        }

        protected TimedPairEvents pacientPushEvents = new TimedPairEvents();
        public TimedPairEvents PacientPushEvents
        {
            get
            {
                if (succeded)
                    return pacientPushEvents;
                else
                    return null;
            }
        }

        public Session(int frequency, float volume)
        {
            tone = new Tone.Tone(frequency, volume);
        }

        public void StartSession()
        {
            tonePlayEvents.EventStarted();
        }

        public void PacientButtonDown()
        {
            isPacientPushOngoing = true;
            pacientPushEvents.EventStarted();
            if (TonePlayer.Instance.CurrentlyPlaying)
                succeded = true;
        }

        public void PacientButtonUp()
        {
            isPacientPushOngoing = false;
            pacientPushEvents.EventEnded();
        }

        public bool IsPacientButtonEventOngoing()
        {
            return isPacientPushOngoing;
        }

        public void EndSession()
        {
            TonePlayer.Instance.StopTone();
            if (null == testManager)
                testManager = FindObjectOfType<TestManager>();
            testManager.SessionEnd(succeded);
        }

    }
}

