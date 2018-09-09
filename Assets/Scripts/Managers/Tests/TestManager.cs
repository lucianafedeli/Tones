using System;
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
        #endregion

        private void Init()
        {
            currentVolume = startVolume;
            currentFrequencyIndex = startFrequencyIndex;

            OngoingTest = true;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnPacientButtonDown();
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                OnPacientButtonUp();
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
