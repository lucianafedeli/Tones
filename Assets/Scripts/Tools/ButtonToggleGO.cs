using UnityEngine;

public class ButtonToggleGO : MonoBehaviour
{

    [SerializeField]
    GameObject[] GOs = null;

    public void ChangeState()
    {
        if (null != GOs)
        {
            for (int i = 0; i < GOs.Length; i++)
            {
                GOs[i].SetActive(!GOs[i].activeSelf);
            }
        }
    }

}
