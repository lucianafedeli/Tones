using Managers;
using System.Collections;
using Tones.Sessions;
using Tones.Tools;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Tones.Managers
{
    public class ExperimentalTestManager : TestManager
    {
        protected Experimental preLimitFailedSession = null;
        protected Experimental onLimitSucceedSession = null;
        protected Experimental postLimitSucceedSession = null;

        [SerializeField]
        private Text[] preOnPostText = null;

        [SerializeField]
        private PushButton pacientButton = null;
        [SerializeField]
        private PushButton startExperimentalButton = null;

        [SerializeField]
        private Button showGraphsButton = null;

        [SerializeField]
        private Button[] interactableDuringSession = null;
        private bool[] previousState = null;

        [SerializeField]
        private Text pacientName = null;

        [SerializeField]
        private Animator ledLight = null;

        private bool heardFreq = false;

        public const string LowHighFreqKey = "LowHighFreqKey";
        public const string StartDBKey = "StartDBKey";
        public const string ST_DurationKey = "ST_DurationKey", LT_DurationKey = "LT_DurationKey";
        public const string DeadTimeDurationKey = "DeadTimeDurationKey";

        private float shortToneDuration, longToneDuration, deadTimeDuration;
        private bool startLowFreq = false;

        protected override void Start()
        {
            pacientName.text = DataManager.Instance.CurrentPacient.ToString();

            startExperimentalButton.onButtonUp.AddListener(StartExperimental);

            previousState = new bool[interactableDuringSession.Length];

            startLowFreq = PlayerPrefs.GetInt(LowHighFreqKey, 0) == 0;
            toneManager.freqIndex = (byte)(startLowFreq ? 6 : 0);

            toneManager.currentDB = PlayerPrefs.GetInt(StartDBKey, 10);

            toneManager.UpdateDBUI();
            toneManager.UpdateFrequencyUI();

            shortToneDuration = PlayerPrefs.GetFloat(ST_DurationKey, 1f);
            longToneDuration = PlayerPrefs.GetFloat(LT_DurationKey, 2.5f);
            deadTimeDuration = PlayerPrefs.GetFloat(DeadTimeDurationKey, 1.5f);
        }

        private void StartExperimental()
        {
            if (!OngoingTest)
            {
                for (int i = 0; i < 3; i++)
                {
                    preOnPostText[i].text = "";
                }

                StartCoroutine(ExperimentalRoutine());
            }
        }

        private IEnumerator ExperimentalRoutine()
        {
            int numberOfTones = 0;

            StartTest();
            while (!IsExperimentalComplete())
            {
                currentSession = new Experimental(toneManager.freqIndex, toneManager.currentDB, this, ear);

                pacientButton.onButtonDown.AddListener(currentSession.PacientButtonDown);
                pacientButton.onButtonUp.AddListener(currentSession.PacientButtonUp);
                pacientButton.onButtonDown.AddListener(LedOn);
                pacientButton.onButtonUp.AddListener(LedOff);

                numberOfTones = Random.Range(1, 3);

                for (int i = 0; i < numberOfTones; i++)
                {
                    (currentSession as Experimental).StartTone();
                    if (Random.value >= .5f) // Long
                    {
                        yield return new WaitForSecondsRealtime(longToneDuration);
                    }
                    else // Short
                    {
                        yield return new WaitForSecondsRealtime(shortToneDuration);
                    }
                    (currentSession as Experimental).StopTone();

                    yield return new WaitForSecondsRealtime(deadTimeDuration);
                }

                pacientButton.onButtonDown.RemoveListener(currentSession.PacientButtonDown);
                pacientButton.onButtonUp.RemoveListener(currentSession.PacientButtonUp);
                pacientButton.onButtonDown.RemoveListener(LedOn);
                pacientButton.onButtonUp.RemoveListener(LedOff);

                currentSession.EndSession();
                yield return new WaitUntil(() => null == currentSession);
            }
            StopTest();
        }

        private bool IsExperimentalComplete()
        {
            return null != preLimitFailedSession && null != onLimitSucceedSession && null != postLimitSucceedSession;
        }

        public override void StartTest()
        {
            base.StartTest();
            Debug.Log("Experimental test Started.");

            for (int i = 0; i < interactableDuringSession.Length; i++)
            {
                previousState[i] = interactableDuringSession[i].interactable;
                interactableDuringSession[i].interactable = false;
            }
        }

        private void StopTest()
        {


            for (int i = 0; i < interactableDuringSession.Length; i++)
            {
                interactableDuringSession[i].interactable = previousState[i];
            }
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
                DataManager.Instance.SaveSuccsefulExperimentalSession(currentSession as Experimental);

                if (toneManager.currentDB > ToneSettingsManager.dbMin)
                {
                    toneManager.DecreaseDB();
                    heardFreq = true;
                }
                else
                {
                    NextFrequency();
                }

                if (null == onLimitSucceedSession)
                {
                    onLimitSucceedSession = currentSession as Experimental;
                    preOnPostText[1].text = frequencies[currentSession.tone.FrequencyIndex] + " Hz\n" + currentSession.tone.dB + " dB";
                }
                else
                {
                    postLimitSucceedSession = onLimitSucceedSession;
                    onLimitSucceedSession = currentSession as Experimental;
                    preOnPostText[1].text = frequencies[currentSession.tone.FrequencyIndex] + " Hz\n" + currentSession.tone.dB + " dB";
                    preOnPostText[2].text = frequencies[postLimitSucceedSession.tone.FrequencyIndex] + " Hz\n" + postLimitSucceedSession.tone.dB + " dB";
                }

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

                preLimitFailedSession = currentSession as Experimental;
                preOnPostText[0].text = frequencies[currentSession.tone.FrequencyIndex] + " Hz\n" + currentSession.tone.dB + " dB";
            }

            //ledLight.SetTrigger("Off");

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

            toneManager.currentDB = PlayerPrefs.GetInt(StartDBKey, 10);
            toneManager.UpdateDBUI();

            if (startLowFreq)
            {
                toneManager.IncreaseFrequency();
            }
            else
            {
                toneManager.DecreaseFrequency();
            }

            toneManager.UpdateFrequencyUI();
        }
    }
}

