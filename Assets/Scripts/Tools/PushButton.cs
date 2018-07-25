using UnityEngine;
using UnityEngine.Events;

namespace Tools
{
    public class PushButton : MonoBehaviour
    {
        [SerializeField]
        KeyCode key;

        [SerializeField]
        public UnityEvent onButtonDown;
        [SerializeField]
        public UnityEvent onButtonUp;


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