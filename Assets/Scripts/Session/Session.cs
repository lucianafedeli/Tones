using System;
using Tones.Managers;
using Tools;

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

        public Tone tone;
        private bool succeded = false;
        private bool isPacientPushOngoing = false;

        public TimedPairEvents tonePlayEvents;
        public TimedPairEvents pacientPushEvents;

        public Session(byte frequencyIndex, float volume, TestManager manager, Tone.EarSide ear)
        {
            tone = new Tone(frequencyIndex, volume, ear);
            testManager = manager;
            tonePlayEvents = new TimedPairEvents();
            pacientPushEvents = new TimedPairEvents();
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
            {
                succeded = true;
            }
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
            tonePlayEvents.EventEnded();
            testManager.SessionEnd(succeded);
        }

    }
}

