﻿using Managers;
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
        private Image[] preOnPostImages = null;

        [SerializeField]
        private PushButton pacientButton = null;
        [SerializeField]
        private PushButton startExperimentalButton = null;

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
        private Animator ledLight = null;

        private bool heardFreq = false;

        public const string LowHighFreqKey = "LowHighFreqKey";
        public const string StartDBKey = "StartDBKey";
        public const string ST_DurationKey = "ST_DurationKey", LT_DurationKey = "LT_DurationKey";
        public const string DeadTimeDurationKey = "DeadTimeDurationKey";

        private float shortToneDuration, longToneDuration, deadTimeDuration;

        protected override void Start()
        {
            pacientName.text = DataManager.Instance.CurrentPacient.ToString();

            startExperimentalButton.onButtonUp.AddListener(StartExperimental);

            previousState = new bool[interactableDuringSession.Length];

            toneManager.freqIndex = (byte)(PlayerPrefs.GetInt(LowHighFreqKey, 0) == 0 ? 6 : 0);
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

                numberOfTones = Random.Range(1, 3);

                for (int i = 0; i < numberOfTones; i++)
                {
                    if (Random.value >= .5f)
                    {

                    }
                    else
                    {

                    }
                }
                (currentSession as Experimental).StartTone();

                yield return null;
            }
            currentSession.EndSession();
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

            for (int i = 0; i < interactableDuringSession.Length; i++)
            {
                interactableDuringSession[i].interactable = previousState[i];
            }

            ledLight.SetTrigger("Off");

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

