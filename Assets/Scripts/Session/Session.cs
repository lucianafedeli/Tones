using System;
using Tones.Managers;
using Tools;
using UnityEngine;

namespace Tones.Sessions
{
    /// <summary>
    /// Esta clase se encarga de definir la duracion total de las partes de una prueba 
    /// dada una frecuencia y un volumen.
    /// Es la base de las sesiones clasicas y experimentales.
    /// </summary>
    [Serializable]
    public abstract class Session
    {
        protected TestManager testManager;

        protected Tone tone = null;

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

        private bool isLeftEar;
        public bool IsLeftEar
        {
            get { return isLeftEar; }
            set { isLeftEar = value; }
        }


        public Session(int frequency, float volume, TestManager manager, Tone.EarSide ear)
        {
            tone = new Tone(frequency, volume, ear);
            testManager = manager;
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
            testManager.SessionEnd(succeded);
        }

    }
}

