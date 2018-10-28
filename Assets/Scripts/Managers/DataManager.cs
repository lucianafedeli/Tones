using Design_Patterns;
using Newtonsoft.Json;
using Pacient;
using System.Collections.Generic;
using System.IO;
using Tones.Sessions;
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
        private Button createNewPatientButton = null;
        private Button showGraphs = null;

        [SerializeField]
        private PacientData currentPatient = null;

        public PacientData CurrentPacient
        {
            get { return currentPatient; }
            set
            {
                currentPatient = value;

                if (null == editCurrentPacientButton)
                {
                    editCurrentPacientButton = GameObject.Find("ButtonEditUser").GetComponent<Button>();
                }

                if (null == startStudyForCurrentPacientButton)
                {
                    startStudyForCurrentPacientButton = GameObject.Find("ButtonNewTest").GetComponent<Button>();
                }

                if (null == createNewPatientButton)
                {
                    createNewPatientButton = GameObject.Find("ButtonNewUser").GetComponent<Button>();
                }

                if (null == showGraphs)
                {
                    showGraphs = GameObject.Find("ButtonGraphs").GetComponent<Button>();
                }

                startStudyForCurrentPacientButton.interactable = editCurrentPacientButton.interactable = null != currentPatient;
                createNewPatientButton.interactable = null == currentPatient;

                showGraphs.interactable = (null != currentPatient && null != currentPatient.lastSessions && currentPatient.lastSessions.Count > 0);
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

                PacientData[] dataArray = JsonConvert.DeserializeObject<PacientData[]>(dataAsJson);

                if (null == dataArray)
                {
                    //PacientData singlePacient = JsonUtility.FromJson<PacientData>(dataAsJson);
                    //PacientsData = new Dictionary<ulong, PacientData> { { singlePacient.ID, singlePacient } };
                }
                else
                {
                    PacientsData = new Dictionary<ulong, PacientData>(dataArray.Length);


                    foreach (PacientData t in dataArray)
                    {
                        if (pacientNumber < t.ID)
                        {
                            pacientNumber = t.ID;
                        }

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
            {
                LoadPacientsData();
            }

            return PacientsData;
        }

        public void AddOrUpdatePacient(PacientData data)
        {
            if (null == PacientsData)
            {
                PacientsData = new Dictionary<ulong, PacientData>();
            }

            if (null != currentPatient && data.ID == currentPatient.ID)
            {
                PacientsData[data.ID] = data;
            }
            else if (!PacientsData.ContainsKey(data.ID))
            {
                PacientsData.Add(pacientNumber, data);
            }
            else
            {
                Debug.LogError("ID already exists. This should never happen!");
            }

            currentPatient = null;

            SavePacientData();
        }

        public string GetLatestTest(ulong ID)
        {
            return "24/01/1991"; // TODO
        }

        public void RemovePacient(ulong ID)
        {
            PacientsData[ID].enabled = false;
            SavePacientData();
        }

        private void SavePacientData()
        {
            PacientData[] toArray = new PacientData[PacientsData.Count];
            PacientsData.Values.CopyTo(toArray, 0);

            File.WriteAllText(filePath, JsonConvert.SerializeObject(toArray));
        }

        public void SaveSuccessfulManualSession(Manual newSession)
        {
            if (null == PacientsData[CurrentPacient.ID].lastSessions)
            {
                PacientsData[CurrentPacient.ID].lastSessions = new List<Manual>();
            }

            bool alreadyExists = false;
            int existsIndex = -1;

            for (int i = 0; i < PacientsData[CurrentPacient.ID].lastSessions.Count && !alreadyExists; i++)
            {
                if (PacientsData[CurrentPacient.ID].lastSessions[i].Tone.FrequencyIndex == newSession.Tone.FrequencyIndex)
                {
                    alreadyExists = true;
                    existsIndex = i;
                }
            }

            if (alreadyExists)
            {
                PacientsData[CurrentPacient.ID].lastSessions[existsIndex] = newSession;
            }
            else
            {
                PacientsData[CurrentPacient.ID].lastSessions.Add(newSession);
            }

            SavePacientData();
        }

        public void SaveCarhartt(Carhartt newSession)
        {
            if (null == PacientsData[CurrentPacient.ID].carhartts)
            {
                PacientsData[CurrentPacient.ID].carhartts = new List<Carhartt>();
            }
            bool found = false;
            for (int i = 0; i < PacientsData[CurrentPacient.ID].carhartts.Count; i++)
            {
                if (PacientsData[CurrentPacient.ID].carhartts[i].Tone.FrequencyIndex == newSession.Tone.FrequencyIndex)
                {
                    found = true;
                    PacientsData[CurrentPacient.ID].carhartts[i] = newSession;
                    break;
                }
            }

            if (!found)
            {
                PacientsData[CurrentPacient.ID].carhartts.Add(newSession);
            }

            SavePacientData();
        }
    }
}