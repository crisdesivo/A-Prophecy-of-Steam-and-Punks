using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RotateAndWait());
    }

    // Update is called once per frame
    void Update()
    {
        float radius = 2.5f;
        // move tangentially around 0, 0 (local coordinates)
        Vector3 localPosition = new Vector3(Mathf.Sin(Time.time)*radius, Mathf.Cos(Time.time)*radius, transform.localPosition.z);
        // set localtransform to localPosition
        transform.localPosition = localPosition;

        // transform.position = new Vector3(Mathf.Cos(Time.time) * 5, Mathf.Sin(Time.time) * 5, 0);
        
    }

    public IEnumerator AcceleratedRotation(){
        // does a 360 degrees rotation by accelerating and decelearting
        float rotatedAngle = 0.0f;
        float rotationSpeed = 0.0f;
        float rotationAcceleration = 3000.0f;
        float rotationDeceleration = 600;

        while (rotatedAngle < 90.0f){
            // accelerate
            rotationSpeed += rotationAcceleration * Time.deltaTime;
            // rotate
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            // update rotatedAngle
            rotatedAngle += rotationSpeed * Time.deltaTime;
            yield return null;
        }
        while (rotatedAngle < 360f && rotationSpeed > 0.0f){
            // decelerate
            rotationSpeed -= rotationDeceleration * Time.deltaTime;
            // rotate
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            // update rotatedAngle
            rotatedAngle += rotationSpeed * Time.deltaTime;
            yield return null;
        }

        // set rotation to 0
        transform.rotation = Quaternion.identity;

    }

    public IEnumerator RotateAndWait(){
        while (true){
            Coroutine rotate = StartCoroutine(AcceleratedRotation());
            // wait until rotation is done
            yield return rotate;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
