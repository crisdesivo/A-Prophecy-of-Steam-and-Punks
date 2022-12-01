using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // on trigger 2d
    // if player
    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            // load next level
            GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().NextFloor();
            Destroy(gameObject);
        }
    }
}
