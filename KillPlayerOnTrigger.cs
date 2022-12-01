using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerOnTrigger : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().life = 0;
            other.gameObject.GetComponent<Player>().Die();
        }
    }
}
