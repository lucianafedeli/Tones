using Managers;
using System.Collections;
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
        private Sprite[] graphsSprites = null;

        [SerializeField]
        private Button[] interactableDuringSession = null;
        private bool[] previousState = null;

        [SerializeField]
        private Text pacientName = null;

        [SerializeField]
        private Animator ledLight = null, buttonAnim = null;

        private bool classicMode = true, heardFreq = false;

        [SerializeField]
        private Image fillButton = null, fillBG = null;

        [SerializeField]
        private Slider durationSlider = null;

        protected override void Start()
        {
            base.Start();

            pacientName.text = DataManager.Instance.CurrentPacient.ToString();

            manualSessionButton.onButtonDown.AddListener(ManualButtonDown);
            manualSessionButton.onButtonUp.AddListener(ManualButtonUp);

            previousState = new bool[interactableDuringSession.Length];
        }

        public void ToggleMode()
        {
            classicMode = !classicMode;
            if (!classicMode)
            {
                toneManager.freqIndex = 3;
                toneManager.currentDB = 10;

                toneManager.UpdateDBUI();
                toneManager.UpdateFrequencyUI();
            }
        }

        private void ManualButtonDown()
        {
            if (!OngoingTest)
            {
                StartTest();

                (currentSession as Classic).StartTone();

                if (!classicMode)
                {
                    StartCoroutine(EndToneAssistedRoutine());
                }
            }
        }

        private IEnumerator EndToneAssistedRoutine()
        {
            float t = 0;

            buttonAnim.enabled = false;
            fillBG.color = new Color(1, 1, 1, .7f);
            do
            {
                yield return null;
                t += Time.deltaTime;
                fillButton.fillAmount = t / durationSlider.value;
            } while (t < durationSlider.value);
            fillButton.fillAmount = 1;
            currentSession.EndSession();
            fillBG.color = Color.white;
            buttonAnim.enabled = true;
        }

        private void ManualButtonUp()
        {
            if (classicMode && OngoingTest)
            {
                currentSession.EndSession();
            }
        }

        public override void StartTest()
        {
            base.StartTest();

            currentSession = new Classic(toneManager.freqIndex, toneManager.currentDB, this, ear);

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
                DataManager.Instance.SaveSuccsefulClassicSession(currentSession as Classic);
                if (!classicMode)
                {
                    if (toneManager.currentDB > ToneSettingsManager.dbMin)
                    {
                        toneManager.DecreaseDB();
                        heardFreq = true;
                    }
                    else
                    {
                        NextFrequency();
                    }
                }
            }
            else
            {
                if (classicMode)
                {
                    showGraphsButton.image.sprite = graphsSprites[1];
                }
                else
                {
                    if (!heardFreq || toneManager.currentDB == ToneSettingsManager.dbMax)
                    {
                        toneManager.IncreaseVolume();
                        toneManager.IncreaseVolume();
                    }
                    else
                    {
                        NextFrequency();
                    }
                }
            }

            for (int i = 0; i < interactableDuringSession.Length; i++)
            {
                interactableDuringSession[i].interactable = previousState[i];
            }

            //ledLight.SetTrigger("Off");

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

        private void NextFrequency()
        {
            heardFreq = false;

            toneManager.currentDB = 10;
            toneManager.UpdateDBUI();

            if (toneManager.freqIndex < 6)
            {
                toneManager.IncreaseFrequency();
            }
            else
            {
                toneManager.freqIndex = 0;
                toneManager.UpdateFrequencyUI();
            }
        }
    }
}
