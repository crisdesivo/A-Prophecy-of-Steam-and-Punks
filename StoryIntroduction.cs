using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryIntroduction : MonoBehaviour
{
    public GameObject narration;

    public IEnumerator Controller()
    {
        // wait until narration starts
        yield return new WaitUntil(() => narration.GetComponent<AudioSource>().isPlaying);
        Debug.Log("narration started");
        // wait until narration ends
        yield return new WaitUntil(() => !narration.GetComponent<AudioSource>().isPlaying);
        Debug.Log("narration ended");
        // load the next scene
        LoadNextScene();
    }

    void LoadNextScene()
    {
        SceneController.loadDialogScene("Intro1");
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Controller());
    }

    // Update is called once per frame
    void Update()
    {
        // if player presses enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // load the next scene
            LoadNextScene();
        }
    }
}
