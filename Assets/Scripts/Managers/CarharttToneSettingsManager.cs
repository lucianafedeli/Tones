using Tones.Managers;
using UnityEngine;
using UnityEngine.UI;

public class CarharttToneSettingsManager : MonoBehaviour
{
    [SerializeField]
    private Text freqText = null;
    [SerializeField]
    private Button freqDown = null, freqUp = null;

    [SerializeField]
    private Text dBText = null;
    [SerializeField]
    private Button dBDown = null, dBUp = null;
    private const int dbMin = 5, dbMax = 60;
    private const int dbDelta = 5;
    public byte freqIndex = 0;
    public int currentDB = 5;

    private void Start()
    {
        currentDB = 10;
        freqIndex = 2; // Para 500

        UpdateDBUI();
    }

    public void IncreaseFrequency()
    {
        freqIndex++;

        if (5 == freqIndex)
        {
            freqUp.interactable = false;
        }

        if (!freqDown.interactable)
        {
            freqDown.interactable = true;
        }

        UpdateFrequencyUI();
    }

    public void DecreaseFrequency()
    {
        freqIndex--;

        if (2 == freqIndex)
        {
            freqDown.interactable = false;
        }

        if (!freqUp.interactable)
        {
            freqUp.interactable = true;
        }

        UpdateFrequencyUI();
    }

    private void UpdateFrequencyUI()
    {
        freqText.text = TestManager.frequencies[freqIndex] + "\nHz";
    }

    public void IncreaseVolume()
    {
        currentDB += dbDelta;

        if (currentDB == dbMax)
        {
            dBUp.interactable = false;
        }

        if (!dBDown.interactable)
        {
            dBDown.interactable = true;
        }

        UpdateDBUI();
    }

    public void DecreaseVolume()
    {
        currentDB -= dbDelta;

        if (currentDB == dbMin)
        {
            dBDown.interactable = false;
        }

        if (!dBUp.interactable)
        {
            dBUp.interactable = true;
        }

        UpdateDBUI();
    }

    private void UpdateDBUI()
    {
        dBText.text = currentDB + "\ndB";
    }
}
