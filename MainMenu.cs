using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// use File
using System.IO;

public class MainMenu : MonoBehaviour
{
    public GameObject continueButton;
    public void StartGame()
    {
        SceneController.loadScene("Story Introduction");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        // if Data.tutorialComplete is true set continue button to active
        if (File.Exists(Application.persistentDataPath + "/data.txt"))
        {
            continueButton.SetActive(true);
        }
        
    }

    public void ContinueGame()
    {
        Data.DebugLogData();
        Debug.Log("Continue Game");
        Data.loadData();
        Data.DebugLogData();
        if (Data.beatenTutorial)
        {
            SceneController.loadScene("StageSelection");
        }
        else
        {
            SceneController.loadScene("Story Introduction");
        }
    }

    public void OpenCredits()
    {
        SceneController.loadScene("Credits");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
