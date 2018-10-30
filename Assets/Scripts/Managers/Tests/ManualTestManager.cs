using Managers;
using Tones.Sessions;
using Tones.Tools;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Tones.Managers
{
    public class ManualTestManager : TestManager
    {
        [SerializeField]
        private PushButton pacientButton = null;
        [SerializeField]
        private PushButton manualSessionButton = null;

        [SerializeField]
        private Button showGraphsButton = null;
        [SerializeField]
        private Sprite[] graphsSprites;

        [SerializeField]
        private Button[] interactableDuringSession = null;
        private bool[] previousState = null;

        [SerializeField]
        private Text pacientName = null;

        [SerializeField]
        private Animator ledLight = null;


        protected override void Start()
        {
            base.Start();

            pacientName.text = DataManager.Instance.CurrentPacient.ToString();

            manualSessionButton.onButtonDown.AddListener(ManualButtonDown);
            manualSessionButton.onButtonUp.AddListener(ManualButtonUp);

            previousState = new bool[interactableDuringSession.Length];
        }

        private void ManualButtonDown()
        {
            //manualSessionButton.onButtonDown.RemoveListener(ManualButtonDown);
            if (!OngoingTest)
            {
                StartTest();
            }
            (currentSession as Manual).StartTone();
        }

        private void ManualButtonUp()
        {
            if (OngoingTest)
            {
                (currentSession as Manual).EndSession();
            }
        }

        public override void StartTest()
        {
            base.StartTest();
            Debug.Log("Manual Classic test Started.");
            currentSession = new Manual(toneManager.freqIndex, toneManager.currentDB, this, ear);

            for (int i = 0; i < interactableDuringSession.Length; i++)
            {
                previousState[i] = interactableDuringSession[i].interactable;
                interactableDuringSession[i].interactable = false;
            }

            pacientButton.onButtonDown.AddListener(currentSession.PacientButtonDown);
            pacientButton.onButtonUp.AddListener(currentSession.PacientButtonUp);
            pacientButton.onButtonDown.AddListener(LedOn);
            pacientButton.onButtonUp.AddListener(LedOff);

            showGraphsButton.image.sprite = graphsSprites[0];
        }

        public void ShowGraphics()
        {
            FindObjectOfType<SceneManagerFinder>().LoadScene("Graphics");
        }

        public override void SessionEnd(bool sessionSucceeded)
        {
            OngoingTest = false;

            if (sessionSucceeded)
            {
                DataManager.Instance.SaveSuccessfulManualSession(currentSession as Manual);
            }
            else
            {
                showGraphsButton.image.sprite = graphsSprites[1];
            }

            for (int i = 0; i < interactableDuringSession.Length; i++)
            {
                interactableDuringSession[i].interactable = previousState[i];
            }

            pacientButton.onButtonDown.RemoveListener(currentSession.PacientButtonDown);
            pacientButton.onButtonUp.RemoveListener(currentSession.PacientButtonUp);
            pacientButton.onButtonDown.RemoveListener(LedOn);
            pacientButton.onButtonUp.RemoveListener(LedOff);

            currentSession = null;
        }

        private void LedOn()
        {
            ledLight.SetTrigger("On");
        }

        private void LedOff()
        {
            ledLight.SetTrigger("Off");
        }

    }
}
