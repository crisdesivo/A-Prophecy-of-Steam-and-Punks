using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseImage : MonoBehaviour
{
    public List<(Sprite, float, float)> imagesPathStartEnd = new List<(Sprite, float, float)>();

    public GameObject narration;

    public void ShowImage(Sprite sprite, float duration)
    {
        // get the SpriteRenderer component
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        // load the image
        spriteRenderer.sprite = sprite;
        // set the alpha to 1
        spriteRenderer.color = new Color(1, 1, 1, 1);
        // expand image to fit the screen
        transform.localScale = new Vector3(1, 1, 1);
        // // start the coroutine
        // StartCoroutine(FadeOut(duration));
    }

    IEnumerator FadeOut(float duration)
    {
        // get the SpriteRenderer component
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        // wait for the duration
        yield return new WaitForSeconds(duration*0.9f);
        // fade out the image
        for (float t = 0.0f; t < duration*0.1f; t += Time.deltaTime)
        {
            spriteRenderer.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, t / (duration*0.1f)));
            yield return null;
        }
    }

    public void LoadImages(string filename)
    {
        // read file from Resources folder
        TextAsset textAsset = Resources.Load<TextAsset>(filename);
        Debug.Log(textAsset.text);
        // split the file into lines
        string[] lines = textAsset.text.Split(new string[] { "\r\n", "\n" }, System.StringSplitOptions.None);
        float currentTime = 0;
        // loop through the lines
        for (int i = 0; i < lines.Length-1; i++)
        {
            Debug.Log("line " + i + ": " + lines.Length);
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
                float.Parse(time[0].Split(',')[1]) / 1000;
            // // if start time is greater than current time, add a blank image
            // if (startTime > currentTime)
            // {
            //     imagesPathAndDuration.Add((null, startTime - currentTime));
            // }
            // get the duration
            float duration = endTime - startTime;
            // skip the time
            i++;
            // get the path
            string path = lines[i];
            // add the sprite and times to the list
            imagesPathStartEnd.Add((Resources.Load<Sprite>(path), startTime, endTime));

            // skip the empty line
            i++;
            currentTime = endTime;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadImages("intro_1_images");
        StartCoroutine(ShowImages());
        
    }

    public IEnumerator ShowImages()
    {
        // wait until narration started playing\
        yield return new WaitUntil(() => narration.GetComponent<AudioSource>().isPlaying);
        var currentImageIndex = 0;
        //while narration is playing based on the time of the narration, show the image if any
        while (narration.GetComponent<AudioSource>().isPlaying)
        {
            // get the current time
            float currentTime = narration.GetComponent<AudioSource>().time;
            // loop through the images assuming they are sorted by start time and there are no overlapping images
            
            while (currentImageIndex < imagesPathStartEnd.Count && imagesPathStartEnd[currentImageIndex].Item2 < currentTime)
            {
                // if the image is not null, show it
                if (imagesPathStartEnd[currentImageIndex].Item1 != null)
                {
                    ShowImage(imagesPathStartEnd[currentImageIndex].Item1, imagesPathStartEnd[currentImageIndex].Item3 - imagesPathStartEnd[currentImageIndex].Item2);
                }
                currentImageIndex++;
            }
            yield return null;
        }
        // foreach ((Sprite, float) image in imagesPathAndDuration)
        // {
        //     ShowImage(image.Item1, image.Item2);
        //     yield return new WaitForSeconds(image.Item2);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
