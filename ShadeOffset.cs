using UnityEngine;

public class ShadeOffset : MonoBehaviour
{
    // Scroll the main texture based on time

    float scrollSpeed = 0.5f;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer> ();
    }

    void Update()
    {
        // set renderer variable "Percentage" to fluctuate between 0 and 2.2 (sinusoidal)
        // rend.material.SetFloat("_Percentage", (Mathf.Sin(Time.time * scrollSpeed)+1)/2f);
        // rend.material.SetFloat("_Percentage", 0.5f);
        // rend.material.SetFloat("Percentage", 0.5f);
        // float offset = Time.time * scrollSpeed;
        // rend.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
