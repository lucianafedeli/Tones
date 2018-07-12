using Design_Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class ScenesManager : Singleton<ScenesManager>
    {
        private string previousScene = "Intro";

        //private void Start()
        //{
        //    DontDestroyOnLoad(this);
        //}

        public void LoadPrevious()
        {
            if (previousScene != string.Empty)
                LoadScene(previousScene);
        }

        public void LoadScene(string scene)
        {
            if (scene == "Intro" && PlayerPrefs.GetInt("DontShowInstructions", 0) == 1)
                scene = "HistoriaClinica";

            previousScene = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene(scene);
        }
    }
}
