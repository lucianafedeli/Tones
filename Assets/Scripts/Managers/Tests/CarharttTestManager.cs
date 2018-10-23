using Managers;
using Tones.Sessions;
using Tones.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Tones.Managers
{
    public class CarharttTestManager : TestManager
    {
        [SerializeField]
        private PushButton pacientButton = null;

        [SerializeField]
        private Button[] interactableDuringSession = null;
        private bool[] previousState = null;

        [SerializeField]
        private Text pacientName = null;

        [SerializeField]
        private Animator ledLight = null;

        [SerializeField]
        private float carharttDuration = 60;

        private void Start()
        {
            previousState = new bool[interactableDuringSession.Length];

            if (null != DataManager.Instance.CurrentPacient)
            {
                pacientName.text = DataManager.Instance.CurrentPacient.ToString();
            }
        }

        public override void StartTest()
        {
            if (!OngoingTest)
            {
                base.StartTest();
                Debug.Log("Carhartt test Started.");
                currentSession = new Carhartt(currentFrequencyIndex, currentVolume, this, ear);

                for (int i = 0; i < interactableDuringSession.Length; i++)
                {
                    previousState[i] = interactableDuringSession[i].interactable;
                    interactableDuringSession[i].interactable = false;
                }

                pacientButton.onButtonDown.AddListener(currentSession.PacientButtonDown);
                pacientButton.onButtonUp.AddListener(currentSession.PacientButtonUp);
                pacientButton.onButtonDown.AddListener(LedOn);
                pacientButton.onButtonUp.AddListener(LedOff);

                Invoke("SessionEnd", carharttDuration);
            }
        }

        public void SessionEnd()
        {
            pacientButton.onButtonDown.RemoveListener(currentSession.PacientButtonDown);
            pacientButton.onButtonUp.RemoveListener(currentSession.PacientButtonUp);
            pacientButton.onButtonDown.RemoveListener(LedOn);
            pacientButton.onButtonUp.RemoveListener(LedOff);

            for (int i = 0; i < interactableDuringSession.Length; i++)
            {
                interactableDuringSession[i].interactable = previousState[i];
            }

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
            base.SessionEnd(sessionSucceded);

            currentSession = null;
        }

        public void ShowGraph()
        {
            if (null != succesfulSessions)
            {
                // TODO: Connect with user and persist data
                foreach (var session in succesfulSessions)
                {
                    GraphManager.Instance.GraphSession(session.Value);
                }
            }
        }
    }
}
