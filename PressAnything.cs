using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnything : MonoBehaviour
{
    public GameObject pressAnything;
    public GameObject loadingText;
    bool loaded = false;
    void Start()
    {
        // Data.loadData();
        loaded = true;
        // set the loading text to inactive
        loadingText.SetActive(false);
        // set the press anything to active
        pressAnything.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        // if the player presses any key or clicks the mouse
        if (loaded && (Input.anyKeyDown || Input.GetMouseButtonDown(0)))
        {
            // load the next scene
            SceneController.loadScene("MainMenu");
        }
        
    }
}
