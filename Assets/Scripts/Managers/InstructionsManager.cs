using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class InstructionsManager : MonoBehaviour
    {

        public static string DontShowKey = "DontShowInstructions";

        [SerializeField]
        private Toggle dontShowAgainToggle = null;

        [SerializeField]
        private float timeBetweenInstructions = 5;
        [SerializeField]
        private Animator instructionsAnim = null;
        private float currentTime = 0;

        private void Start()
        {
            dontShowAgainToggle.isOn = PlayerPrefs.GetInt(DontShowKey, 0) == 1;
        }

        public void DontShowInstructions(bool shouldHide)
        {
            PlayerPrefs.SetInt(DontShowKey, shouldHide ? 1 : 0);
        }

        private void Update()
        {
            if (currentTime >= timeBetweenInstructions)
            {
                Next();
            }

            currentTime += Time.deltaTime;
        }

        public void OnButtonClicked()
        {
            Next();
        }

        private void Next()
        {
            currentTime = 0;
            instructionsAnim.SetTrigger("Next");
        }

    }
}
