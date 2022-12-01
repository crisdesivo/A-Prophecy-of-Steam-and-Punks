using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Subtitles : MonoBehaviour
{
    public static Dictionary<string, string> coloredSpeakerText = new Dictionary<string, string> {
            // narrator green
            {"Narrator","<color=#00ff00> Narrator</color>: "},
            // player blue
            {"Taylor","<color=#0000ff> Taylor</color>: "},
            // John light blue
            {"John","<color=#00ffff> John</color>: "},
            // Nyx purple
            {"Nyx","<color=#ff00ff> Nyx</color>: "}
    };
    // List of string, float pairs
    public List<(string, float, float)> subtitles = new List<(string, float, float)>();
    public GameObject narration;
    public GameObject panel;
    public void ShowSubtitle(string text, string speaker, float duration)
    {
        // set panel transparency to 0.5
        panel.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
        // get the text component
        TextMeshProUGUI textComponent = GetComponent<TextMeshProUGUI>();
        // show speaker name in green and the text in white
        textComponent.text = coloredSpeakerText[speaker] + text;
        // set the alpha to 1
        textComponent.alpha = 1;
        // // start the coroutine
        // StartCoroutine(FadeOut(duration));
    }

    IEnumerator FadeOut(float duration)
    {
        // get the text component
        TextMeshProUGUI textComponent = GetComponent<TextMeshProUGUI>();
        // wait for the duration
        yield return new WaitForSeconds(duration*0.9f);
        // fade out the text
        for (float t = 0.0f; t < duration*0.1f; t += Time.deltaTime)
        {
            textComponent.alpha = Mathf.Lerp(1, 0, t / (duration*0.1f));
            yield return null;
        }
    }

    public void LoadSubtitles(string filename)
    {
        // read file from Resources folder
        TextAsset textAsset = Resources.Load<TextAsset>(filename);
        Debug.Log(textAsset.text);
        // split the file into lines
        string[] lines = textAsset.text.Split(new string[] {"\n" }, System.StringSplitOptions.None);
        float currentTime = 0;
        // loop through the lines
        for (int i = 0; i < lines.Length-1; i++)
        {
            Debug.Log(i);
            // skip the index
            i++;
            // get the time
            string[] time = lines[i].Split(new string[] { " --> " }, System.StringSplitOptions.None);
            // get the start time
            // Debug.Log(time[0]);
            // Debug.Log(time[1]);
            float startTime = float.Parse(time[0].Split(',')[0].Split(':')[0]) * 3600 +
                float.Parse(time[0].Split(',')[0].Split(':')[1]) * 60 +
                float.Parse(time[0].Split(',')[0].Split(':')[2]) + 
                float.Parse(time[0].Split(',')[1]) / 1000;
            // get the end time
            float endTime = float.Parse(time[1].Split(',')[0].Split(':')[0]) * 3600 +
                float.Parse(time[1].Split(',')[0].Split(':')[1]) * 60 +
                float.Parse(time[1].Split(',')[0].Split(':')[2]) + 
                float.Parse(time[1].Split(',')[1]) / 1000;
            // // if the start time is after the current time, add an empty subtitle
            // if (startTime > currentTime)
            // {
            //     subtitles.Add(("", startTime - currentTime));
            // }
            // get the duration
            float duration = endTime - startTime;
            // skip the time
            i++;
            // // get the speaker
            // string speaker = lines[i].Split(':')[0];
            // // skip the speaker
            // i++;
            // get the text
            string text = lines[i];
            // add the text and duration to the list
            subtitles.Add((text, startTime, endTime));
            // skip the empty line
            i++;
            // set the current time
            currentTime = endTime;
        }
        // string[] lines = System.IO.File.ReadAllLines(filename);
        // loop through the lines
        // foreach (string line in lines)
        // {
        //     // split the line into parts
        //     string[] parts = line.Split('|');
        //     // add the subtitle to the list
        //     subtitles.Add((parts[0], float.Parse(parts[1])));
        // }
    }

    public IEnumerator PlaySubtitles()
    {
        // set panel transparency to 0
        panel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        // wait until narration started playing
        yield return new WaitUntil(() => narration.GetComponent<AudioSource>().isPlaying);
        var currentSubtitleIndex = 0;
        //while narration is playing based on the time of the narration, show the subtitle if any
        while (narration.GetComponent<AudioSource>().isPlaying)
        {
            // get the current time
            float currentTime = narration.GetComponent<AudioSource>().time;
            Debug.Log(currentTime);
            // loop through the images assuming they are sorted by start time and there are no overlapping images
            
            while (currentSubtitleIndex < subtitles.Count && subtitles[currentSubtitleIndex].Item2 < currentTime)
            {
                // if the image is not null, show it
                if (subtitles[currentSubtitleIndex].Item1 != null)
                {
                    ShowSubtitle(subtitles[currentSubtitleIndex].Item1, "Narrator", subtitles[currentSubtitleIndex].Item3 - subtitles[currentSubtitleIndex].Item2);
                }
                // increment the index
                currentSubtitleIndex++;
            }
            // wait until the next subtitle
            if (currentSubtitleIndex < subtitles.Count)
            {
                yield return new WaitUntil(() => subtitles[currentSubtitleIndex].Item2 < narration.GetComponent<AudioSource>().time);
            }
            else
            {
                yield return null;
            }
            // yield return new WaitUntil(() => subtitles[currentSubtitleIndex].Item2 < narration.GetComponent<AudioSource>().time);
            // yield return WaitForSeconds(subtitles[currentSubtitleIndex].Item2 - currentTime);
        }
    }
    public void Start()
    {
        LoadSubtitles("intro_1_subtitles");
        StartCoroutine(PlaySubtitles());
    }
}
