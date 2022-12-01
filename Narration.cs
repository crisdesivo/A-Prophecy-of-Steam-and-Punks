using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narration : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // // wait untill all the scene is loaded (webgl)
        // StartCoroutine(WaitForSceneLoad());
        // play the audio
        GetComponent<AudioSource>().Play();
        
    }

    // public IEnumerator WaitForSceneLoad()
    // {
    //     yield return new WaitUntil(() => Application.CanStreamedLevelBeLoaded("Showcase"));
    //     // load the scene
    //     Application.LoadLevel("Showcase");
    // }
    // Update is called once per frame
    void Update()
    {
        
    }
}
