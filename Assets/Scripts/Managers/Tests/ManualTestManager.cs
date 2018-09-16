using Tones.Session;
using Tones.Tools;
using UnityEngine;
using UnityEngine.UI;

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
        private Button stopSessionButton = null;
        [SerializeField]
        private Button graphButton = null;
        [SerializeField]
        private Button earToggle = null;

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
            currentSession = new Manual(CurrentFrequency, currentVolume, this, isLeftEar);

            stopSessionButton.gameObject.SetActive(true);
            stopSessionButton.interactable = true;
            graphButton.gameObject.SetActive(false);
            earToggle.interactable = false;

            pacientButton.onButtonDown.AddListener(currentSession.PacientButtonDown);
            pacientButton.onButtonUp.AddListener(currentSession.PacientButtonUp);
            pacientButton.onButtonDown.AddListener(LedOn);
            pacientButton.onButtonUp.AddListener(LedOff);
        }

        public void SessionEnd()
        {
            graphButton.gameObject.SetActive(true);
            earToggle.interactable = true;
            stopSessionButton.gameObject.SetActive(false);
            pacientButton.onButtonDown.RemoveListener(currentSession.PacientButtonDown);
            pacientButton.onButtonUp.RemoveListener(currentSession.PacientButtonUp);
            pacientButton.onButtonDown.RemoveListener(LedOn);
            pacientButton.onButtonUp.RemoveListener(LedOff);

            currentSession.EndSession();
        }

        private void LedOn()
        {
            ledLight.SetTrigger("On");
        }

        private void LedOff()
        {
            ledLight.SetTrigger("Off");
        }

        public override void SessionEnd(bool sessionSucceded)
        {
            OngoingTest = false;

            if (sessionSucceded)
            {
                GraphManager.Instance.AddSession(currentSession as Manual);
            }
            currentSession = null;
        }

        public void ToggleLeft()
        {
            isLeftEar = !isLeftEar;
        }
    }
}
