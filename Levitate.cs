using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitate : MonoBehaviour
{
    public float maxMove = 0.25f;
    public float moveSpeed = 2.5f;

    // Update is called once per frame
    void Update()
    {
        // move up and down
        transform.position = new Vector3(transform.position.x, transform.position.y + maxMove * Mathf.Sin(Time.time*moveSpeed), transform.position.z);
        
    }
}
