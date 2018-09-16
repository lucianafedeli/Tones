using System;
using UnityEngine;
using UnityEngine.Events;

namespace Tones.Tools
{
    public class PushButton : MonoBehaviour
    {
        [SerializeField]
        private KeyCode key;

        [NonSerialized]
        public UnityEvent onButtonDown = new UnityEvent();
        [NonSerialized]
        public UnityEvent onButtonUp = new UnityEvent();


        public void OnButtonDown()
        {
            onButtonDown.Invoke();
        }
        public void OnButtonUp()
        {
            onButtonUp.Invoke();
        }

        private void Update()
        {
            if (Input.GetKeyDown(key))
            {
                OnButtonDown();
            }
            if (Input.GetKeyUp(key))
            {
                OnButtonUp();
            }
        }
    }
}