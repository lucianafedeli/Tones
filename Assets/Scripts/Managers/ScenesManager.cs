using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.SceneManagement;public class ScenesManager : Singleton<ScenesManager>{    private string previousScene = string.Empty;    public void LoadPrevious()    {        if(previousScene != string.Empty)            SceneManager.LoadScene(previousScene);    }    public void LoadScene (string scene)    {        previousScene = SceneManager.GetActiveScene().name;        SceneManager.LoadScene(scene);    }}