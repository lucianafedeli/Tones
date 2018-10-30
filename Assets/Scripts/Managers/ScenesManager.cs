using Design_Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class ScenesManager : Singleton<ScenesManager>
    {
        private string previousScene = "Intro";

        public void LoadPrevious()
        {
            if (previousScene != string.Empty)
            {
                LoadTonesScene(previousScene);
            }
        }

        public void LoadTonesScene(string scene)
        {
            if (previousScene == "Intro" && scene == "Instructions")
            {
                bool showInstructions = PlayerPrefs.GetInt(InstructionsManager.DontShowKey, 0) == 0;
                if (!showInstructions)
                {
                    scene = "HistoriaClinica";
                }
            }

            previousScene = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene(scene);
        }
    }
}
