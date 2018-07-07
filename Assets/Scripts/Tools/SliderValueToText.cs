using UnityEngine;
using UnityEngine.UI;

namespace Tools
{
    [RequireComponent(typeof(Text))]
    public class SliderValueToText : MonoBehaviour
    {

        Text theText = null;

        public void ShowNumberAsText(float number)
        {
            if (null == theText)
                theText = GetComponent<Text>();
            theText.text = string.Empty + number.ToString("0");
        }
    }
}
