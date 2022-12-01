// unity script for scene controller
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// import File
using System.IO;

public static class SceneController
{
    public static bool forcedDeath = false;
    public static string dialog;
    public static string input = "1";
    public static bool secretCredits = false;

    public static void loadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public static void loadDialogScene(string dialogName){
        dialog = dialogName;
        loadScene("Dialog");
    }

    public static void loadScene(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }

}