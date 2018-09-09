using Tones.Session;
using Tones.Tools;
using UnityEngine;

namespace Tones.Managers
{
    public class ManualTestManager : TestManager
    {
        #region Manual
        [SerializeField]
        private PushButton pacientButton = null;
        [SerializeField]
        private PushButton manualSessionButton = null;

        [SerializeField]
        private Animator ledLight = null;
        #endregion

        private void Start()
        {
            manualSessionButton.onButtonDown.AddListener(ManualButtonDown);
            manualSessionButton.onButtonUp.AddListener(ManualButtonUp);

        }

        private void ManualButtonDown()
        {
            //manualSessionButton.onButtonDown.RemoveListener(ManualButtonDown);
            if (!OngoingTest)
                StartTest();

            (currentSession as Manual).StartTone();
        }

        private void ManualButtonUp()
        {
            (currentSession as Manual).StopTone();
        }

        public override void StartTest()
        {
            base.StartTest();
            Debug.Log("Manual Classic test Started.");
            currentSession = new Manual(CurrentFrequency, currentVolume, this);

            pacientButton.onButtonDown.AddListener(currentSession.PacientButtonDown);
            pacientButton.onButtonUp.AddListener(currentSession.PacientButtonUp);
        }

        public override void SessionEnd(bool sessionSucceded)
        {
            OngoingTest = false;

            if (sessionSucceded)
            {
                GraphManager.Instance.AddSession(currentSession as Manual);
                currentSession = null;
            }
        }
    }
}
