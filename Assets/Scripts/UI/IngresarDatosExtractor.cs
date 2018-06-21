using UnityEngine;
using UnityEngine.UI;

public class IngresarDatosExtractor : MonoBehaviour
{
    [SerializeField]
    Text nombreText = null;
    [SerializeField]
    Text apellidoText = null;
    [SerializeField]
    Text DNIText = null;
    [SerializeField]
    Text fechaNacimientoText = null;
    [SerializeField]
    Toggle FToggle = null;

    public void ExtractData()
    {
        PacientData newPacient = new PacientData(DataManager.Instance.pacientNumber, nombreText.text, apellidoText.text, DNIText.text, fechaNacimientoText.text, FToggle.isOn ? 'F' : 'M');
        DataManager.Instance.AddPacient(newPacient);
    }
}
