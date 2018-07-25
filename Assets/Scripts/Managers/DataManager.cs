using Design_Patterns;
using Pacient;
using System.Collections.Generic;
using System.IO;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class DataManager : Singleton<DataManager>
    {
        // NOTE: Podrias guardar el dictionary en vez de la clase asi no duplicas DNI tontito.

        [SerializeField]
        private bool pacientDataHasBeenLoaded = false;

        [SerializeField]
        private ulong pacientNumber = 1;

        public ulong PacientNumber
        {
            get { return pacientNumber; }
            set { pacientNumber = value; }
        }

        public Dictionary<ulong, PacientData> PacientsData { get; private set; }

        private Button editCurrentPacientButton = null;
        private Button startStudyForCurrentPacientButton = null;

        [SerializeField]
        private PacientData currentPacient = null;

        public PacientData CurrentPacient
        {
            get { return currentPacient; }
            set
            {
                currentPacient = value;
                if (null == editCurrentPacientButton)
                    editCurrentPacientButton = GameObject.Find("ButtonEditUser").GetComponent<Button>();
                if (null == startStudyForCurrentPacientButton)
                    startStudyForCurrentPacientButton = GameObject.Find("ButtonNewTest").GetComponent<Button>();

                startStudyForCurrentPacientButton.interactable = editCurrentPacientButton.interactable = null != currentPacient;
            }
        }


        private string filePath = string.Empty;

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
                    PacientsData = new Dictionary<ulong, PacientData> { { singlePacient.ID, singlePacient } };
                }
                else
                {
                    PacientsData = new Dictionary<ulong, PacientData>(dataArray.Length);


                    foreach (PacientData t in dataArray)
                    {
                        if (pacientNumber < t.ID)
                            pacientNumber = t.ID;
                        PacientsData.Add(t.ID, t);
                    }
                }

                pacientNumber++;
                pacientDataHasBeenLoaded = true;
            }
        }

        public Dictionary<ulong, PacientData> GetPacientsData()
        {
            if (null == PacientsData)
                LoadPacientsData();
            return PacientsData;
        }

        public void AddOrUpdatePacient(PacientData data)
        {
            if (null == PacientsData)
                PacientsData = new Dictionary<ulong, PacientData>();

            if (data.ID == currentPacient.ID)
                PacientsData[data.ID] = data;
            else if (!PacientsData.ContainsKey(data.ID))
                PacientsData.Add(pacientNumber, data);
            else
                Debug.LogError("ID already exists. This should never happen!");

            currentPacient = null;

            SavePacientData();
        }

        public string GetLatestTest(ulong id)
        {
            return "24/01/1991";
        }

        public void RemovePacient(ulong ID)
        {
            PacientsData.Remove(ID);
            SavePacientData();
        }

        private void SavePacientData()
        {
            PacientData[] toArray = new PacientData[PacientsData.Count];
            PacientsData.Values.CopyTo(toArray, 0);

            File.WriteAllText(filePath, JsonHelper.ToJson(toArray, true));
        }
    }
}