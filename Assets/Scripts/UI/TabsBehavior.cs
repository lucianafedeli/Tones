using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TabsBehavior : MonoBehaviour
{
    [SerializeField]
    private TabsBehavior otherTab;

    [SerializeField]
    private Color unSelectedColor = Color.grey;

    [SerializeField]
    private Button tabButton;

    public void SelectTab()
    {

        otherTab.UnSelectTab();
        tabButton.interactable = false;
        tabButton.targetGraphic.color = Color.white;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void UnSelectTab()
    {
        tabButton.interactable = true;
        tabButton.targetGraphic.color = unSelectedColor;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

}
