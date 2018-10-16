using Managers;
using Pacient;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class DatosDePaciente : MonoBehaviour
    {
        [SerializeField] private InputField nombreText = null;
        [SerializeField] private InputField apellidoText = null;
        [SerializeField] private InputField DNIText = null;
        [SerializeField] private InputField fechaNacimientoText = null;
        [SerializeField] private Toggle FToggle = null;

        [SerializeField] private UnityEvent OnInvalidDNI;
        [SerializeField] private UnityEvent OnValidDNI;

        private bool validDNI = true;
        private PacientData currentlyEditing = null;

        private void Start()
        {
            if (null != DataManager.Instance.CurrentPacient)
                InjectData(DataManager.Instance.CurrentPacient);
        }

        private void InjectData(PacientData data)
        {
            currentlyEditing = data;

            nombreText.text = data.firstName;
            apellidoText.text = data.lastName;
            DNIText.text = data.DNI;
            fechaNacimientoText.text = data.birthDate;
            FToggle.isOn = data.gender == 'F';
        }

        public void DeletePacient()
        {
            DataManager.Instance.RemovePacient(currentlyEditing.ID);
        }

        public void ExtractData()
        {
            if (nombreText.text != string.Empty && apellidoText.text != string.Empty &&
                DNIText.text != string.Empty &&
                fechaNacimientoText.text != string.Empty)
            {
                if (validDNI)
                {
                    PacientData pacient = new PacientData(
                        null != currentlyEditing ? currentlyEditing.ID : DataManager.Instance.PacientNumber,
                        nombreText.text,
                        apellidoText.text, DNIText.text, fechaNacimientoText.text, FToggle.isOn ? 'F' : 'M');


                    DataManager.Instance.AddOrUpdatePacient(pacient);
                }
            }
        }

        public void ValidateDNI(string DNI)
        {
            if (DNI.Length != 8)
                return;

            if (null != DataManager.Instance.PacientsData)
            {
                foreach (KeyValuePair<ulong, PacientData> pacient in DataManager.Instance.PacientsData)
                {
                    if (null == DataManager.Instance.CurrentPacient || DataManager.Instance.CurrentPacient.DNI != DNI)
                    {
                        if (pacient.Value.DNI == DNI)
                        {
                            validDNI = false;
                            if (null != OnInvalidDNI)
                                OnInvalidDNI.Invoke();
                            return;
                        }
                    }
                }
            }

            validDNI = true;
            if (null != OnValidDNI)
                OnValidDNI.Invoke();
        }
    }
}