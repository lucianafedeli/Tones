using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    // NOTE: Podrias guardar el dictionary en vez de la clase asi no duplicas DNI tontito.

    string filePath = string.Empty;

    Dictionary<ulong, PacientData> pacientData = null;

    bool pacientDataHasBeenLoaded = false;

    public ulong pacientNumber = 1;

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/HistoriaClinica.json";
    }
    private void OnEnable()
    {
        LoadPacientsData();
    }

    private void LoadPacientsData()
    {
        if (File.Exists(filePath) && !pacientDataHasBeenLoaded)
        {
            string dataAsJson = File.ReadAllText(filePath);

            PacientData[] dataArray = JsonHelper.FromJson<PacientData>(dataAsJson);

            if (null == dataArray)
            {
                PacientData singlePacient = JsonUtility.FromJson<PacientData>(dataAsJson);
                pacientData = new Dictionary<ulong, PacientData>();

                pacientData.Add(singlePacient.ID, singlePacient);
            }
            else
            {
                pacientData = new Dictionary<ulong, PacientData>(dataArray.Length);

                for (int i = 0; i < dataArray.Length; i++)
                {
                    if (pacientNumber < dataArray[i].ID)
                        pacientNumber = dataArray[i].ID;
                    pacientData.Add(dataArray[i].ID, dataArray[i]);
                }
            }
            pacientNumber++;
            pacientDataHasBeenLoaded = true;
        }
    }

    public Dictionary<ulong, PacientData> GetPacientsData()
    {
        if (null == pacientData)
            LoadPacientsData();
        return pacientData;
    }

    public void AddPacient(PacientData data)
    {
        if (null == pacientData)
            pacientData = new Dictionary<ulong, PacientData>();

        if (!pacientData.ContainsKey(data.ID))
        {
            pacientData.Add(pacientNumber, data);
            SavePacientData();
        }
        else
            Debug.LogError("Paciente ya creado wey.");
    }

    public string GetLatestTest(ulong id)
    {
        return "";
    }

    private void SavePacientData()
    {
        PacientData[] toArray = new PacientData[pacientData.Count];
        pacientData.Values.CopyTo(toArray, 0);

        File.WriteAllText(filePath, JsonHelper.ToJson(toArray, true));
    }

}
