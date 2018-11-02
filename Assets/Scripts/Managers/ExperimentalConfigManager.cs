using Tones.Managers;
using UnityEngine;
using UnityEngine.UI;

public class ExperimentalConfigManager : MonoBehaviour
{
    #region AgudasGraves
    private enum RF
    {
        Agudas, Graves
    }

    [SerializeField]
    private Text TextRF = null;
    [SerializeField]
    private Button DownRF, UpRF;
    private RF currentRF = RF.Agudas;
    #endregion

    #region VolumenInicial
    [SerializeField]
    private Text TextVI = null;
    [SerializeField]
    private Button DownVI = null, UpVI = null;
    private readonly int dbDelta = 5;
    private readonly int dbMin = 5, dbMax = 60;
    private int currentVI = 10;
    #endregion

    //#region NumeroDeTonos
    //[SerializeField]
    //private Text TextNT = null;
    //[SerializeField]
    //private Button DownNT = null, UpNT = null;
    //private readonly int NTmin = 1, NTmax = 3;
    //private readonly int NTdelta = 1;
    //private int currentNT = 3;
    //#endregion

    #region TiempoEntreTonos
    [SerializeField]
    private Text TextTET = null;
    [SerializeField]
    private Button DownTET = null, UpTET = null;
    private readonly float TETmin = 1, TETmax = 2.5f;
    private readonly float TETdelta = .5f;
    private float currentTET = 1.5f;
    #endregion

    #region TonosCortos
    [SerializeField]
    private Text TextTC = null;
    [SerializeField]
    private Button DownTC = null, UpTC = null;
    private readonly float TCmin = .5f, TCmax = 2;
    private readonly float TCdelta = .5f;
    private float currentTC = 1;
    #endregion

    #region TonosLargos
    [SerializeField]
    private Text TextTL = null;
    [SerializeField]
    private Button DownTL = null, UpTL = null;
    private readonly float TLmin = 2.5f, TLmax = 4;
    private readonly float TLdelta = .5f;
    private float currentTL = 3;
    #endregion


    private void Start()
    {
        currentRF = PlayerPrefs.GetInt(ExperimentalTestManager.LowHighFreqKey, 0) == 0 ? RF.Agudas : RF.Graves;
        TextRF.text = currentRF.ToString();

        currentTET = PlayerPrefs.GetFloat(ExperimentalTestManager.DeadTimeDurationKey, currentTET);

        currentTC = PlayerPrefs.GetFloat(ExperimentalTestManager.ST_DurationKey, currentTC);

        currentTL = PlayerPrefs.GetFloat(ExperimentalTestManager.LT_DurationKey, currentTL);

        currentVI = PlayerPrefs.GetInt(ExperimentalTestManager.StartDBKey, currentVI);

        UpdateDBUI();
        UpdateTCUI();
        UpdateTETUI();
        UpdateTLUI();
    }

    private void IncreaseIntValue(ref int current, int delta, int max, Button up, Button down)
    {
        if (current < max)
        {
            current += delta;

            if (!down.interactable)
            {
                down.interactable = true;
            }
        }
        if (current >= max)
        {
            up.interactable = false;
        }
    }

    private void DecreaseIntValue(ref int current, int delta, int min, Button up, Button down)
    {
        if (current > min)
        {
            current -= delta;

            if (!up.interactable)
            {
                up.interactable = true;
            }
        }
        if (current <= min)
        {
            down.interactable = false;
        }
    }

    private void IncreaseFloatValue(ref float current, float delta, float max, Button up, Button down)
    {
        if (current < max)
        {
            current += delta;

            if (!down.interactable)
            {
                down.interactable = true;
            }
        }
        if (current >= max)
        {
            up.interactable = false;
        }
    }

    private void DecreaseFloatValue(ref float current, float delta, float min, Button up, Button down)
    {
        if (current > min)
        {
            current -= delta;
            if (!up.interactable)
            {
                up.interactable = true;
            }
        }
        if (current <= min)
        {
            down.interactable = false;
        }
    }

    //Control del rango frecuencial - RF
    public void ToggleRF()
    {
        if (currentRF == RF.Agudas)
        {
            currentRF = RF.Graves;
        }
        else
        {
            currentRF = RF.Agudas;
        }
        PlayerPrefs.SetInt(ExperimentalTestManager.LowHighFreqKey, currentRF == RF.Agudas ? 0 : 1);

        TextRF.text = currentRF.ToString();
    }

    //Control del volumen inicial - VI
    public void IncreaseVolume()
    {
        IncreaseIntValue(ref currentVI, dbDelta, dbMax, UpVI, DownVI);
        PlayerPrefs.SetInt(ExperimentalTestManager.StartDBKey, currentVI);
        UpdateDBUI();
    }

    public void DecreaseVolume()
    {
        DecreaseIntValue(ref currentVI, dbDelta, dbMin, UpVI, DownVI);
        PlayerPrefs.SetInt(ExperimentalTestManager.StartDBKey, currentVI);
        UpdateDBUI();
    }

    private void UpdateDBUI()
    {
        TextVI.text = currentVI + "dB";
    }

    ////Control del numero de tonos - NT
    //public void IncreaseNumberOfTones()
    //{
    //    IncreaseIntValue(ref currentNT, NTdelta, NTmax, UpNT, DownNT);
    //    UpdateNTUI();
    //}

    //public void DecreaseNumberOfTones()
    //{
    //    DecreaseIntValue(ref currentNT, NTdelta, NTmin, UpNT, DownNT);
    //    UpdateNTUI();
    //}

    //private void UpdateNTUI()
    //{
    //    TextNT.text = currentNT.ToString();
    //}

    //Control del tiempo entre tonos - TET
    public void IncreaseTimeBetweenTones()
    {
        IncreaseFloatValue(ref currentTET, TETdelta, TETmax, UpTET, DownTET);
        PlayerPrefs.SetFloat(ExperimentalTestManager.DeadTimeDurationKey, currentTET);
        UpdateTETUI();
    }

    public void DecreaseTimeBetweenTones()
    {
        DecreaseFloatValue(ref currentTET, TETdelta, TETmin, UpTET, DownTET);
        PlayerPrefs.SetFloat(ExperimentalTestManager.DeadTimeDurationKey, currentTET);
        UpdateTETUI();
    }

    private void UpdateTETUI()
    {
        TextTET.text = currentTET + "s";
    }

    //Control de la duracion de los tonos cortos - TC
    public void IncreaseShortTones()
    {
        IncreaseFloatValue(ref currentTC, TCdelta, TCmax, UpTC, DownTC);
        PlayerPrefs.SetFloat(ExperimentalTestManager.ST_DurationKey, currentTC);
        UpdateTCUI();
    }

    public void DecreaseShortTones()
    {
        DecreaseFloatValue(ref currentTC, TCdelta, TCmin, UpTC, DownTC);
        PlayerPrefs.SetFloat(ExperimentalTestManager.ST_DurationKey, currentTC);
        UpdateTCUI();
    }

    private void UpdateTCUI()
    {
        TextTC.text = currentTC + "s";
    }

    //Control de la duracion de los tonos largos - TL
    public void IncreaseLongTones()
    {
        IncreaseFloatValue(ref currentTL, TLdelta, TLmax, UpTL, DownTL);
        PlayerPrefs.SetFloat(ExperimentalTestManager.LT_DurationKey, currentTL);
        UpdateTLUI();
    }

    public void DecreaseLongTones()
    {
        DecreaseFloatValue(ref currentTL, TLdelta, TLmin, UpTL, DownTL);
        PlayerPrefs.SetFloat(ExperimentalTestManager.LT_DurationKey, currentTL);
        UpdateTLUI();
    }

    private void UpdateTLUI()
    {
        TextTL.text = currentTL + "s";
    }
}