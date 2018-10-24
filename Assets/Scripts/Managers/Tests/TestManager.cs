using Managers;
using System;
using Tones.Sessions;
using UnityEngine;

namespace Tones.Managers
{
    public abstract class TestManager : MonoBehaviour
    {
        [NonSerialized]
        public bool OngoingTest = false;

        protected Tone.EarSide ear = Tone.EarSide.Left;

        #region Volume
        protected readonly byte startVolume = 10;
        protected readonly byte maxDb = 80;

        [SerializeField]
        protected byte currentVolume;
        #endregion

        #region Frequency
        public static readonly int[] frequencies = { 125, 250, 500, 1000, 2000, 4000, 8000 };

        protected readonly byte startFrequencyIndex = 3;

        protected byte currentFrequencyIndex;
        protected int CurrentFrequency
        {
            get { return frequencies[currentFrequencyIndex]; }
        }
        #endregion

        protected Sessions.Session currentSession = null;

        private void Start()
        {
            currentFrequencyIndex = startFrequencyIndex;
        }

        private void Init()
        {
            currentVolume = startVolume;

            OngoingTest = true;
        }

        public void ToggleEar()
        {
            if (ear == Tone.EarSide.Left)
            {
                ear = Tone.EarSide.Right;
            }
            else
            {
                ear = Tone.EarSide.Left;
            }
        }

        public void IncreaseFrequency()
        {
            if (currentFrequencyIndex < frequencies.Length)
            {
                currentFrequencyIndex++;
            }
        }

        public void DecreaseFrequency()
        {
            if (currentFrequencyIndex > 0)
            {
                currentFrequencyIndex--;
            }
        }

        public void OnPacientButtonDown()
        {
            currentSession.PacientButtonDown();
        }

        public void OnPacientButtonUp()
        {
            currentSession.PacientButtonUp();
        }

        public virtual void StartTest()
        {
            Init();
        }

        public virtual void SessionEnd(bool sessionSucceded)
        {
            OngoingTest = false;
            if (sessionSucceded)
            {
                DataManager.Instance.SaveSuccessfulSession(currentFrequencyIndex, currentSession);
            }
        }

        //protected virtual IEnumerator WaitForPacient()
        //{
        //    yield return new WaitUntil(() => !currentSession.IsPacientButtonEventOngoing());

        //    if (null == postLimitSucceedSession)
        //    {
        //        postLimitSucceedSession = currentSession;
        //    }
        //    else
        //    {
        //        onLimitSucceedSession = currentSession;
        //    }
        //    if (currentVolume > 0)
        //    {
        //        currentVolume -= onSessionSuccessDecrement;
        //        Debug.Log("Vol: " + currentVolume + "dB (-" + onSessionSuccessDecrement + ')');
        //    }

        //    GraphManager.Instance.AddSession(currentSession);
        //}
    }
}
