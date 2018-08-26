using System.Collections;
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
        }

        private void ManualButtonUp()
        {

        }

        public override void StartTest()
        {
            base.StartTest();
            Debug.Log("Manual Classic test Started.");
            Manual manualSession = new Manual(CurrentFrequency, currentVolume, this);
            currentSession = manualSession;

            manualSessionButton.onButtonUp.AddListener(manualSession.StopTone);
            manualSessionButton.onButtonDown.AddListener(manualSession.StartTone);
        }

        public override void SessionEnd(bool sessionSucceded)
        {
            OngoingTest = false;

            if (sessionSucceded)
            {
                StartCoroutine(WaitForPacient());
            }
        }

        protected override IEnumerator WaitForPacient()
        {
            yield return new WaitUntil(() => !currentSession.IsPacientButtonEventOngoing());

            GraphManager.Instance.AddSession(currentSession);
        }
    }
}
