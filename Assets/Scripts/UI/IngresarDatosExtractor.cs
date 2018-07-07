using System.Collections.Generic;
using Managers;
using Pacient;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class IngresarDatosExtractor : MonoBehaviour
    {
        [SerializeField] private Text       nombreText          = null;
        [SerializeField] private Text       apellidoText        = null;
        [SerializeField] private Text       DNIText             = null;
        [SerializeField] private GameObject ErrorDNI            = null;
        [SerializeField] private Text       fechaNacimientoText = null;
        [SerializeField] private Toggle     FToggle             = null;

        public void ExtractData()
        {
            if (nombreText.text          != string.Empty && apellidoText.text != string.Empty &&
                DNIText.text             != string.Empty &&
                fechaNacimientoText.text != string.Empty)
            {
                PacientData newPacient = new PacientData(DataManager.Instance.PacientNumber, nombreText.text,
                    apellidoText.text, DNIText.text, fechaNacimientoText.text, FToggle.isOn ? 'F' : 'M');

                DataManager.Instance.AddPacient(newPacient);
            }
        }

        public void DisableWarning()
        {
            if (ErrorDNI.activeInHierarchy)
                ErrorDNI.SetActive(false);
        }

        public void ValidateDNI(string DNI)
        {
            foreach (KeyValuePair<ulong, PacientData> pacient in DataManager.Instance.PacientData)
            {
                if (pacient.Value.DNI == DNI)
                    ErrorDNI.SetActive(true);
            }
        }
    }
}