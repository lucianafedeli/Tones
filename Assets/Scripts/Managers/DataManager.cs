using System.Collections.Generic;
using System.IO;
using Design_Patterns;
using Pacient;
using Tools;
using UnityEngine;

namespace Managers
{
    public class DataManager : Singleton<DataManager>
    {
        // NOTE: Podrias guardar el dictionary en vez de la clase asi no duplicas DNI tontito.

        private string filePath = string.Empty;


        [SerializeField] private bool pacientDataHasBeenLoaded = false;

        [SerializeField] private ulong pacientNumber = 1;
        public ulong PacientNumber
        {
            get { return pacientNumber; }
            set { pacientNumber = value; }
        }

        public Dictionary<ulong, PacientData> PacientData { get; private set; }

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
                    PacientData = new Dictionary<ulong, PacientData> {{singlePacient.ID, singlePacient}};

                }
                else
                {
                    PacientData = new Dictionary<ulong, PacientData>(dataArray.Length);


                    foreach (PacientData t in dataArray)
                    {
                        if (pacientNumber < t.ID)
                            pacientNumber = t.ID;
                        PacientData.Add(t.ID, t);
                    }
                }

                pacientNumber++;
                pacientDataHasBeenLoaded = true;
            }
        }

        public Dictionary<ulong, PacientData> GetPacientsData()
        {
            if (null == PacientData)
                LoadPacientsData();
            return PacientData;
        }

        public void AddPacient(PacientData data)
        {
            if (null == PacientData)
                PacientData = new Dictionary<ulong, PacientData>();

            if (!PacientData.ContainsKey(data.ID))
            {
                PacientData.Add(pacientNumber, data);
                SavePacientData();
            }
        }

        public string GetLatestTest(ulong id)
        {
            return "24/01/1991";
        }

        private void SavePacientData()
        {
            PacientData[] toArray = new PacientData[PacientData.Count];
            PacientData.Values.CopyTo(toArray, 0);

            File.WriteAllText(filePath, JsonHelper.ToJson(toArray, true));
        }
    }
}