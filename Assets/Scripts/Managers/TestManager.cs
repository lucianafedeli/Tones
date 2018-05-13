using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class TestManager : MonoBehaviour
{
    #region Buttons
    [SerializeField]
    private PushButton pacientButton = null;
    [SerializeField]
    private PushButton manualSessionButton = null;
    #endregion

    #region Volume
    private static byte startVolume = 10;
    private static byte maxDb = 80;
    private static byte onSessionFailedIncrement = 10;
    private static byte onSessionSuccessDecrement = 5;

    [ReadOnly]
    [SerializeField]
    private byte currentVolume;
    #endregion

    #region Frequency
    public static readonly int[] frequencies = { 125, 250, 500, 1000, 2000, 4000, 8000 };


    private static byte startFrequencyIndex = 3;

    private byte currentFrequencyIndex;
    [ReadOnly]
    private int CurrentFrequency
    {
        get { return frequencies[currentFrequencyIndex]; }
    }
    #endregion

    #region Sessions
    [ReadOnly]
    [ShowInInspector]
    private Session currentSession = null;

    [ReadOnly]
    [ShowInInspector]
    private Session preLimitFailedSession = null;
    [ReadOnly]
    [ShowInInspector]
    private Session onLimitSucceedSession = null;
    [ReadOnly]
    [ShowInInspector]
    private Session postLimitSucceedSession = null;

    private static Vector2 timeBetweenSessionsExperimental;
    private static float timeBetweenSessionsAssisted = 1;

    public enum SessionType
    {
        Classic_Manual, Classic_Assisted, Experimental
    }

    [SerializeField]
    SessionType currentSessionType = SessionType.Classic_Manual;
    #endregion

    private void Start()
    {
        pacientButton.onButtonDown += OnPacientButtonDown;
        pacientButton.onButtonUp += OnPacientButtonUp;
        pacientButton.SpaceBarPushEnabled = true;

        manualSessionButton.onButtonDown += StartTest;
    }

    void Init()
    {
        currentVolume = startVolume;
        currentFrequencyIndex = startFrequencyIndex;
    }

    void OnPacientButtonDown()
    {
        currentSession.PacientButtonDown();
    }

    void OnPacientButtonUp()
    {
        currentSession.PacientButtonUp();
    }

    public void StartTest()
    {
        Init();

        switch(currentSessionType)
        {
            case SessionType.Classic_Manual:
                Debug.Log("Manual Classic test Started.");
                currentSession = new Manual(CurrentFrequency, currentVolume);
                manualSessionButton.onButtonUp += ((Manual)currentSession).StopTone;
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
        if(sessionSucceded)
        {
            StartCoroutine(WaitForPacient());
        }
        else if(currentSessionType != SessionType.Classic_Manual)
        {
            preLimitFailedSession = currentSession;

            if(currentVolume < maxDb)
            {
                currentVolume += onSessionFailedIncrement;
                if(currentVolume > maxDb)
                    currentVolume = maxDb;
                Debug.Log("Vol: " + currentVolume + "dB (+" + onSessionFailedIncrement + ')');
            }
        }
    }

    IEnumerator WaitForPacient()
    {
        yield return new WaitUntil(() => !currentSession.IsPacientButtonEventOngoing());

        if(currentSessionType != SessionType.Classic_Manual)
        {
            if(null == postLimitSucceedSession)
            {
                postLimitSucceedSession = currentSession;
            }
            else
            {
                onLimitSucceedSession = currentSession;
            }
            if(currentVolume > 0)
            {
                currentVolume -= onSessionSuccessDecrement;
                Debug.Log("Vol: " + currentVolume + "dB (-" + onSessionSuccessDecrement + ')');
            }
        }

        GraphManager.Instance.AddSession(currentSession);
    }
}
