using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the mouse position
        Vector3 mousePos = Input.mousePosition;
        // Convert the mouse position to world coordinates
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        // Set the x, y position of the object to the world coordinates
        transform.position = new Vector3(worldPos.x, worldPos.y, 0);
        // transform.position = worldPos;
    }
}
