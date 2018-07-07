using System.Collections.Generic;
using Managers;
using Pacient;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HistoriaClinicaInjector : MonoBehaviour
    {
        [SerializeField] private PacientRow pacientRowPrefab = null;

        private List<PacientRow> rows = null;

        private void Start()
        {
            rows = new List<PacientRow>();
            AddPacients();
        }

        private void AddPacients()
        {
            Dictionary<ulong, PacientData> pacientsData = DataManager.Instance.GetPacientsData();

            foreach (KeyValuePair<ulong, PacientData> pacientData in pacientsData)
            {
                PacientRow newPacient = Instantiate(pacientRowPrefab, transform);
                newPacient.SetPacientData(pacientData.Value);

                rows.Add(newPacient);
            }

            GameObject.Find("Scrollbar Vertical").GetComponent<Scrollbar>().value = 1;
        }

        public void SortBy(string sortString)
        {
            foreach (PacientRow element in rows)
            {
                if (sortString == "" || element.pacientData.text.ToLower().Contains(sortString.ToLower()))
                    element.gameObject.SetActive(true);
                else
                    element.gameObject.SetActive(false);
            }
        }
    }
}