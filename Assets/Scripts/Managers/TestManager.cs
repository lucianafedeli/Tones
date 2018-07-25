using Session;
using System;
using System.Collections;
using Tools;
using UnityEngine;

namespace Managers
{
    public class TestManager : MonoBehaviour
    {
        [NonSerialized]
        public bool OngoingTest = false;

        #region Buttons
        [SerializeField]
        private PushButton pacientButton = null;
        [SerializeField]
        private PushButton manualSessionButton = null;
        #endregion

        #region Volume
        private readonly byte startVolume = 10;
        private readonly byte maxDb = 80;
        private readonly byte onSessionFailedIncrement = 10;
        private readonly byte onSessionSuccessDecrement = 5;

        [SerializeField]
        private byte currentVolume;
        #endregion

        #region Frequency
        public readonly int[] frequencies = { 125, 250, 500, 1000, 2000, 4000, 8000 };


        private readonly byte startFrequencyIndex = 3;

        private byte currentFrequencyIndex;
        private int CurrentFrequency
        {
            get { return frequencies[currentFrequencyIndex]; }
        }
        #endregion

        #region Sessions
        private Session.Session currentSession = null;

        private Session.Session preLimitFailedSession = null;
        private Session.Session onLimitSucceedSession = null;
        private Session.Session postLimitSucceedSession = null;

        private Vector2 timeBetweenSessionsExperimental;
        private float timeBetweenSessionsAssisted = 1;

        public enum SessionType
        {
            Classic_Manual, Classic_Assisted, Experimental
        }

        [SerializeField]
        SessionType sessionType = SessionType.Classic_Manual;
        #endregion

        private void Start()
        {
            if (sessionType == SessionType.Classic_Manual)
            {
                StartTest();
            }
        }

        void Init()
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

        public void StartTest()
        {
            Init();

            switch (sessionType)
            {
                case SessionType.Classic_Manual:
                    Debug.Log("Manual Classic test Started.");
                    Manual manualSession = new Manual(CurrentFrequency, currentVolume);
                    currentSession = manualSession;

                    manualSessionButton.onButtonUp.AddListener(manualSession.StopTone);
                    manualSessionButton.onButtonDown.AddListener(manualSession.StartTone);
                    break;
                case SessionType.Classic_Assisted:
                    Debug.Log("Assisted Classic test Started.");
                    currentSession = new Assisted(CurrentFrequency, currentVolume, timeBetweenSessionsAssisted);
                    break;
                case SessionType.Experimental:
                    Debug.Log("Experimental test Started.");
                    currentSession = new Experimental(CurrentFrequency, currentVolume);
                    break;
            }
        }

        public void SessionEnd(bool sessionSucceded)
        {
            OngoingTest = false;

            if (sessionSucceded)
            {
                StartCoroutine(WaitForPacient());
            }
            else if (sessionType != SessionType.Classic_Manual)
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

        IEnumerator WaitForPacient()
        {
            yield return new WaitUntil(() => !currentSession.IsPacientButtonEventOngoing());

            if (sessionType != SessionType.Classic_Manual)
            {
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
            }

            GraphManager.Instance.AddSession(currentSession);
        }
    }
}
