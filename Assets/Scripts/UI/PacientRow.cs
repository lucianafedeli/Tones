using UnityEngine;
using UnityEngine.UI;

public class PacientRow : MonoBehaviour
{
    [SerializeField]
    public Text pacientData;
    [SerializeField]
    public Text lastTestDate;

    public void LoadData(string pacientData, string lastTestDate)
    {
        this.pacientData.text = pacientData;
        this.lastTestDate.text = lastTestDate;
    }
}
