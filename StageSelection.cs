using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelection : MonoBehaviour
{
    public void GoToStage1()
    {
        SceneController.input = "1";
        SceneController.loadScene("Battle");
    }

    public void GoToStage2()
    {
        SceneController.input = "2";
        SceneController.loadScene("Battle");
    }

    public void GoToStage3()
    {
        SceneController.input = "3";
        SceneController.loadScene("Battle");
    }

    public void GoToStage4()
    {
        SceneController.input = "4";
        SceneController.loadScene("Battle");
    }
}
