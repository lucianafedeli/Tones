using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (previousScene == "Intro" && scene == "Instructions" || PlayerPrefs.GetInt("DontShowInstructions", 0) == 1)
            scene = "HistoriaClinica";

        previousScene = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(scene);
    }
}
