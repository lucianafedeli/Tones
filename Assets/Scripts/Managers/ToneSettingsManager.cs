using Tones.Managers;
using UnityEngine;
using UnityEngine.UI;

public class ToneSettingsManager : MonoBehaviour
{
    [SerializeField]
    Text freqText= null;
    [SerializeField]
    Button freqDown= null, freqUp= null;

    [SerializeField]
    Text dBText= null;
    [SerializeField]
    Button dBDown= null, dBUp= null;

    const int dbMin = 0, dbMax = 100;
    const int dbDelta = 5;

    int freqIndex = 0, currentDB = 20;

    private void Start()
    {
        currentDB = 20;
        freqIndex = 3; // Para 1000
    }

    public void IncreaseFrequency()
    {
        freqIndex++;

        if (TestManager.frequencies.Length - 1 == freqIndex)
            freqUp.interactable = false;

        if (!freqDown.interactable)
            freqDown.interactable = true;

        UpdateFrequencyUI();
    }

    public void DecreaseFrequency()
    {
        freqIndex--;

        if (0 == freqIndex)
            freqDown.interactable = false;

        if (!freqUp.interactable)
            freqUp.interactable = true;

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
            dBUp.interactable = false;

        if (!dBDown.interactable)
            dBDown.interactable = true;

        UpdateDBUI();
    }

    public void DecreaseVolume()
    {
        currentDB -= dbDelta;

        if (currentDB == dbMin)
            dBDown.interactable = false;

        if (!dBUp.interactable)
            dBUp.interactable = true;

        UpdateDBUI();
    }

    private void UpdateDBUI()
    {
        dBText.text = currentDB + "\ndB";
    }
}
