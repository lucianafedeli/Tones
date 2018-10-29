using UnityEngine;
using UnityEngine.UI;

public class showScreen : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Text>().text = "W: " + Screen.width + " H: " + Screen.height;
    }


}
