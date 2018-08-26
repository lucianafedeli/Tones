using System;
using System.Collections;
using UnityEngine;

namespace Tones.Managers
{
    public abstract class TestManager : MonoBehaviour
    {
        [NonSerialized]
        public bool OngoingTest = false;

        #region Volume
        protected readonly byte startVolume = 10;
        protected readonly byte maxDb = 80;
        protected readonly byte onSessionFailedIncrement = 10;
        protected readonly byte onSessionSuccessDecrement = 5;

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

        #region Sessions
        protected Session.Session currentSession = null;

        protected Session.Session preLimitFailedSession = null;
        protected Session.Session onLimitSucceedSession = null;
        protected Session.Session postLimitSucceedSession = null;

        private Vector2 timeBetweenSessionsExperimental;
        protected float timeBetweenSessionsAssisted = 1;
        #endregion


        private void Init()
        {
            currentVolume = startVolume;
            currentFrequencyIndex = startFrequencyIndex;

            OngoingTest = true;
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
                StartCoroutine(WaitForPacient());
            }
            else
            {
                preLimitFailedSession = currentSession;

                if (currentVolume < maxDb)
                {
                    currentVolume += onSessionFailedIncrement;
                    if (currentVolume > maxDb)
                        currentVolume = maxDb;
                    Debug.Log("Vol: " + currentVolume + "dB (+" + onSessionFailedIncrement + ')');
                }
            }
        }

        protected virtual IEnumerator WaitForPacient()
        {
            yield return new WaitUntil(() => !currentSession.IsPacientButtonEventOngoing());

            if (null == postLimitSucceedSession)
            {
                postLimitSucceedSession = currentSession;
            }
            else
            {
                onLimitSucceedSession = currentSession;
            }
            if (currentVolume > 0)
            {
                currentVolume -= onSessionSuccessDecrement;
                Debug.Log("Vol: " + currentVolume + "dB (-" + onSessionSuccessDecrement + ')');
            }

            GraphManager.Instance.AddSession(currentSession);
        }
    }
}
