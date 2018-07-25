using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class InstructionsManager : MonoBehaviour
    {
        [SerializeField]
        Toggle dontShowAgainToggle = null;

        [SerializeField]
        float timeBetweenInstructions = 5;
        [SerializeField]
        Animator instructionsAnim = null;

        float currentTime = 0;

        private void Start()
        {
            dontShowAgainToggle.isOn = PlayerPrefs.GetInt("DontShowInstructions", 0) == 0;
        }

        public void DontShowInstructions(bool shouldHide)
        {
            PlayerPrefs.SetInt("DontShowInstructions", shouldHide ? 0 : 1);
        }

        private void Update()
        {
            if (currentTime >= timeBetweenInstructions)
                Next();

            currentTime += Time.deltaTime;
        }

        public void OnButtonClicked()
        {
            Next();
        }

        void Next()
        {
            currentTime = 0;
            instructionsAnim.SetTrigger("Next");
        }

    }
}
