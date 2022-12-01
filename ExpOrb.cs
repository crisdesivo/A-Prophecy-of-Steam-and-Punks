using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpOrb : MonoBehaviour
{
    public float exp = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public IEnumerator collectAnimation(GameObject player)
    {
        // move towards player
        while (Vector3.Distance(transform.position, player.transform.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 10 * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator Collect(GameObject player)
    {
        // move towards player and wait for it to finish
        yield return StartCoroutine(collectAnimation(player));
        player.GetComponent<Player>().CollectExp(this);
        // destroy
        Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Collect(other.gameObject));
        }
    }


}
