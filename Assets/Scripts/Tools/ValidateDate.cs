﻿using UnityEngine;
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
            //if (dateInput.Length == 2)
            //{
            //    if (int.TryParse(dateInput, out stringConverted))
            //    {
            //        if (stringConverted > 0 && stringConverted <= 31)
            //        {
            //            inputField.text += '/';
            //            inputField.caretPosition++;
            //        }
            //        else
            //            dateInput = "";
            //    }
            //    else
            //        dateInput = "";
            //}
            //else if (dateInput.Length == 5)
            //{
            //    if (int.TryParse(dateInput.Substring(3, 2), out stringConverted))
            //    {
            //        if (stringConverted > 0 && stringConverted <= 12)
            //        {
            //            inputField.text += '/';
            //            inputField.caretPosition++;
            //        }    
            //        else
            //            inputField.text = dateInput.Substring(0, 3);
            //    }
            //    else
            //        inputField.text = dateInput.Substring(0, 3);
            //}
            //else if (dateInput.Length == 10)
            //{
            //    if (int.TryParse(dateInput.Substring(6, 4), out stringConverted))
            //    {
            //        if (stringConverted < 1930 || stringConverted > DateTime.Now.Year)
            //            inputField.text = dateInput.Substring(0, 6);
            //    }
            //    else
            //        inputField.text = dateInput.Substring(0, 6);
            //}
        }
    }
}


