using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tools
{
    public class SceneManagerFinder : MonoBehaviour
    {
        public void LoadScene(string scene)
        {
            ScenesManager.Instance.LoadScene(scene);
        }

        private void Start()
        {
            PlayerPrefs.DeleteAll();
        }
        public void LoadPrevious()
        {
            if (SceneManager.GetActiveScene().name == "Instructions")
                ScenesManager.Instance.LoadScene("HistoriaClinica");
            else
                ScenesManager.Instance.LoadPrevious();
        }

    }
}
