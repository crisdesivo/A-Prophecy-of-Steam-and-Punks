using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject target;
    Vector3 offset;
    bool offsetSet = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // save offset for first frame
        if (!offsetSet){
            offset = transform.position - target.transform.position;
            offsetSet = true;
            // log position and target position
            // Debug.Log("position: " + transform.position);
            // Debug.Log("target position: " + target.transform.position);
        }
        // follow target
        transform.position = target.transform.position + offset;
        // transform.position = target.transform.position;
        
    }
}
