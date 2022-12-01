using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBlink : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // animate the transparency of the text
        GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.1f + 0.9f*Mathf.PingPong(Time.time, 1));
        
    }
}
