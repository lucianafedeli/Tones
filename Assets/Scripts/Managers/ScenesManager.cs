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
                LoadScene(previousScene);
        }

        public void LoadScene(string scene)
        {
            if (previousScene == "Intro" && scene == "Instructions" && PlayerPrefs.GetInt("DontShowInstructions", 0) == 0)
                scene = "HistoriaClinica";

            previousScene = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene(scene);
        }
    }
}
