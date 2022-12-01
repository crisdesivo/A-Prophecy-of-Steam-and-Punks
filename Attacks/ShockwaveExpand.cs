using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveExpand : AttackMove
{
    public float expandSpeed = 0.1f;
    public float expandDistance = 10.0f;
    public float expandDelay = 1.0f;
    public float expandReturnSpeed = 6.0f;
    public float expandReturnDelay = 0.1f;
    public override float damage { 
        get { return 1.5f; }
        set {}
    }
    public override float knockbakPower {
        get { return 2.0f; }
        set {}
    }

    public override int pierce {
        get { return 100; }
        set {}
    }

    public override HitType hitType {
        get { return HitType.OnCollision; }
        set {}
    }

    public override float refreshRate {
        get { return 0.1f; }
        set {}
    }
    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(Attack(null));
    }

    override public void OnUpdate()
    {
    }

    public override void Initialize(Vector3 position, Quaternion rotation, GameObject player)
    {
        this.user = player;
    }

    override public IEnumerator Attack(Entity user, Entity target, Vector3 direction)
    {
        Debug.Log("ShockwaveExpand Attack");
        // this.player = player.gameObject;
        // expand
        while (transform.localScale.x < expandDistance)
        {
            transform.localScale += new Vector3(expandSpeed*60*Time.deltaTime, expandSpeed*60*Time.deltaTime, 0);
            // rotate shockwave
            transform.Rotate(0, 0, expandSpeed*100*60*Time.deltaTime);
            yield return null;   
        }

        // destroy parent
        Destroy(gameObject);
    }
}