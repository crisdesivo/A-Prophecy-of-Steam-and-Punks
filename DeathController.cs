using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Data.saveData();
    }

    // Update is called once per frame
    void Update()
    {
        // if any key or click is pressed
        if (Input.anyKey || Input.GetMouseButtonDown(0))
        {
            if (SceneController.forcedDeath){
                SceneController.forcedDeath = false;
                SceneController.loadDialogScene("FinalDialog");
            }
            else{
                SceneController.loadScene("StageSelection");
            }
        }
        
    }
}
