using System.Collections;using System.Collections.Generic;using UnityEngine;public class InstructionsManager : MonoBehaviour{
    [SerializeField]
    float timeBetweenInstructions = 5;
    [SerializeField]
    Animator instructionsAnim = null;

    float currentTime = 0;

    public void DontShowInstructions(bool shouldHide)    {        PlayerPrefs.SetInt("DontShowInstructions", shouldHide ? 0 : 1);    }    private void Update()
    {
        if (currentTime >= timeBetweenInstructions)
            Next();

        currentTime += Time.deltaTime;
    }    public void OnButtonClicked()
    {
        Next();
    }    void Next()
    {
        currentTime = 0;
        instructionsAnim.SetTrigger("Next");
    }}