using System;
using Managers;
using Pacient;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PacientRow : MonoBehaviour
    {

        [SerializeField] public Toggle toggle;
        [SerializeField] public Image rowBG;
        [SerializeField] public Color OnSelectedColor;
        [SerializeField] public Text   pacientData;
        [SerializeField] public Text   lastTestDate;

        [NonSerialized] public PacientData pacient;

        private void Start()
        {
            toggle.group = GetComponentInParent<ToggleGroup>();
            toggle.onValueChanged.AddListener(delegate { ToggleValueChanged(); });
        }

        void ToggleValueChanged()
        {
            if (toggle.isOn)
            {
                DataManager.Instance.CurrentPacient = pacient;
                rowBG.color = OnSelectedColor;
            }
            else if (DataManager.Instance.CurrentPacient == pacient)
            {
                DataManager.Instance.CurrentPacient = null;
                rowBG.color = Color.clear;
            }
        }

        public void SetPacientData(PacientData data)
        {
            pacient           = data;
            pacientData.text  = pacient.ToString() + " - " + pacient.DNI;
            lastTestDate.text = DataManager.Instance.GetLatestTest(pacient.ID);
        }
    }
}