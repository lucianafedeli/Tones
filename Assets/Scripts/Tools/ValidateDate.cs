using UnityEngine;
using UnityEngine.UI;

namespace Tools
{
    [RequireComponent(typeof(InputField))]
    public class ValidateDate : MonoBehaviour
    {

        [SerializeField]
        private InputField inputField = null;
        private int stringConverted;

        private void OnValidate()
        {
            if (null == inputField)
            {
                inputField = GetComponent<InputField>();
            }
        }

        public void OnDateStringUpdated(string dateInput)
        {
            if (dateInput.Length == 2)
            {
                inputField.text += '/';
                inputField.caretPosition++;
            }
            else if (dateInput.Length == 5)
            {
                inputField.text += '/';
                inputField.caretPosition++;
            }
        }
    }
}


