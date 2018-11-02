using Tones.Managers;
using UnityEngine;
using UnityEngine.UI;

public class ToneSettingsManager : MonoBehaviour
{
    [SerializeField]
    private Text freqText = null;
    [SerializeField]
    private Button freqDown = null, freqUp = null;

    [SerializeField]
    private Text dBText = null;
    [SerializeField]
    private Button dBDown = null, dBUp = null;
    public const int dbMin = 5, dbMax = 60;
    private const int dbDelta = 5;

    public byte freqIndex = 3;
    public int currentDB = 10;

    private void Start()
    {
        UpdateDBUI();
        UpdateFrequencyUI();
    }

    public void IncreaseFrequency()
    {
        if (freqIndex < TestManager.frequencies.Length - 1)
        {
            freqIndex++;
            if (null != freqDown)
            {
                if (TestManager.frequencies.Length - 1 == freqIndex)
                {
                    freqUp.interactable = false;
                }

                if (!freqDown.interactable)
                {
                    freqDown.interactable = true;
                }
            }
            UpdateFrequencyUI();
        }
    }

    public void DecreaseFrequency()
    {
        if (freqIndex > 0)
        {
            freqIndex--;

            if (null != freqUp)
            {
                if (0 == freqIndex)
                {
                    freqDown.interactable = false;
                }

                if (!freqUp.interactable)
                {
                    freqUp.interactable = true;
                }
            }
            UpdateFrequencyUI();
        }
    }

    public void UpdateFrequencyUI()
    {
        freqText.text = TestManager.frequencies[freqIndex] + "\nHz";
    }

    public void IncreaseVolume()
    {
        if (currentDB < dbMax)
        {
            currentDB += dbDelta;
            if (null != dBUp)
            {
                if (currentDB == dbMax)
                {
                    dBUp.interactable = false;
                }

                if (!dBDown.interactable)
                {
                    dBDown.interactable = true;
                }
            }
            UpdateDBUI();
        }
    }

    public void DecreaseDB()
    {
        if (currentDB > dbMin)
        {
            currentDB -= dbDelta;

            if (null != dBUp)
            {
                if (currentDB == dbMin)
                {
                    dBDown.interactable = false;
                }

                if (!dBUp.interactable)
                {
                    dBUp.interactable = true;
                }
            }
            UpdateDBUI();
        }
    }

    public void UpdateDBUI()
    {
        dBText.text = currentDB + "\ndB";
    }
}
