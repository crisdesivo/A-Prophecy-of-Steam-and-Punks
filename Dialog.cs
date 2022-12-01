using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    public GameObject dialogText;
    public GameObject dialogSound;
    public GameObject dialogImage;

    private Coroutine dialogCoroutine;
    private AudioClip dialogClip;

    public void HidePanel(){
        // set transparency of panel child to 0
        GameObject panel = transform.Find("Panel").gameObject;
        panel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        dialogImage.GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }

    public void ShowPanel(){
        // set transparency of panel child to 1
        GameObject panel = transform.Find("Panel").gameObject;
        panel.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
        dialogImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

    public IEnumerator DialogCoroutine(string text, string speaker, string audioFile)
    {
        // set active to true
        gameObject.SetActive(true);
        // show dialog text
        dialogText.GetComponent<TextMeshProUGUI>().text = Subtitles.coloredSpeakerText[speaker] + text;;
        // play audio
        dialogClip = Resources.Load<AudioClip>("Audio/" + audioFile);
        dialogSound.GetComponent<AudioSource>().PlayOneShot(dialogClip);
        // show image
        dialogImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/" + speaker);
        // if image is not found, hide image
        if (dialogImage.GetComponent<Image>().sprite == null)
        {
            dialogImage.SetActive(false);
            dialogImage.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        else
        {
            dialogImage.SetActive(true);
            dialogImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        while (dialogSound.GetComponent<AudioSource>().isPlaying)
        {
            yield return null;
        }
        StopDialog();
    }
    public void ShowDialog(string text, string speaker, string audioFile)
    {
        if (dialogCoroutine != null)
        {
            StopCoroutine(dialogCoroutine);
        }
        dialogCoroutine = StartCoroutine(DialogCoroutine(text, speaker, audioFile));
    }

    public void StopDialog()
    {
        if (dialogCoroutine != null)
        {
            StopCoroutine(dialogCoroutine);
        }
        dialogSound.GetComponent<AudioSource>().Stop();
        gameObject.SetActive(false);
        dialogText.GetComponent<TextMeshProUGUI>().text = "";
        dialogImage.GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopDialog();
        }
    }

    public IEnumerator ShowAndHideDialog(string text, string speaker, string audioFile)
    {
        // show dialog
        ShowDialog(text, speaker, audioFile);
        // log the length of the clip
        Debug.Log(dialogClip.length);
        // wait for the duration (unscaled) of the clip
        yield return new WaitForSecondsRealtime(dialogClip.length);
        // hide dialog
        StopDialog();
    }

    void Start()
    {
        // ShowDialog("Now then, show me your resolve.", "Nyx", "Nyx_tutorial_1");
        // dialogCoroutine = StartCoroutine(ShowAndHideDialog("Now then, show me your resolve.", "Nyx", "Nyx_tutorial_1"));
        
    }
}
