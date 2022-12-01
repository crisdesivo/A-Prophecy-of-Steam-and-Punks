using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    public GameObject credits;
    public GameObject secretCredits;
    bool secret = false;
    public void Exit(){
        SceneController.loadScene("MainMenu");
    }

    public void ShowSecretCredits(){
        credits.SetActive(false);
        secretCredits.SetActive(true);
    }

    public void ShowCredits(){
        credits.SetActive(true);
        secretCredits.SetActive(false);
    }

    void Start()
    {
        if (SceneController.secretCredits){
            SceneController.secretCredits = false;
            secret = true;
            ShowSecretCredits();
        }
        else {
            ShowCredits();
            secret = false;
        }
    }

    void Update()
    {
        if (Input.anyKey || Input.GetMouseButtonDown(0)){
            if (secret){
                // check if scene time is greater than 5 seconds
                if (Time.timeSinceLevelLoad > 5){
                    ShowCredits();
                }
            }
        }
    }
}
