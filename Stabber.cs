using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stabber : MonoBehaviour
{
    public GameObject player;
    float radius = 1.5f;
    bool rotating = true;
    float delay = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public IEnumerator AccelerateOutwards(){
        delay = 100f;
        // paint the sprite red and wait for 0.2 seconds
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = Color.white;
        rotating = false;
        float acceleration = 150;
        float speed = 0f;

        while (radius < 3f){
            speed += acceleration * Time.deltaTime;
            radius += speed * Time.deltaTime;
            yield return null;
        }

        // then decelerate until back to 1.5f
        float deceleration = 150;
        while (radius > 1.5f){
            speed -= deceleration * Time.deltaTime;
            radius += speed * Time.deltaTime;
            yield return null;
        }
        radius = 1.5f;
        delay = 1f;
        rotating = true;
    }

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;
        // rotate around parent in the direction closest to the player
        Vector3 direction = player.transform.position - transform.parent.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float currentAngle = transform.rotation.eulerAngles.z;
        float deltaAngle = Mathf.DeltaAngle(currentAngle, angle);
        if (rotating){
            if (deltaAngle > 0){
                transform.Rotate(0, 0, 80 * Time.deltaTime);
            }
            else{
                transform.Rotate(0, 0, -80 * Time.deltaTime);
            }
            // transform.Rotate(0, 0, deltaAngle);
        }
        // flip sprite in y axis if needed
        if (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270){
            transform.localScale = new Vector3(1, -1, 1);
        }
        else{
            transform.localScale = new Vector3(1, 1, 1);
        }

        // set the position a radius away from the parent in the direction of rotation
        Vector3 localPosition = new Vector3(Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)*radius, Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)*radius, transform.localPosition.z);
        // set localtransform to localPosition
        transform.localPosition = localPosition;
        // transform.localPosition = new Vector3(radius*Mathf.Cos(angle * Mathf.Deg2Rad), radius*Mathf.Sin(angle * Mathf.Deg2Rad), transform.localPosition.z);
        if (delay < 0 && rotating){
            // check if aiming at player
            if (Mathf.Abs(deltaAngle) < 5){
                // stab player
                StartCoroutine(AccelerateOutwards());
            }
        }
    }
}
