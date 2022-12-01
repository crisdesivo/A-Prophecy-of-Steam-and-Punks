using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireInferno : AttackMove
{
    public float expandSpeed = 0.1f;
    public float expandDistance = 20.0f;
    public override float damage { 
        get { return 1f; }
        set {}
    }
    public override float knockbakPower {
        get { return 0.5f; }
        set {}
    }

    public override int pierce {
        get { return 1000; }
        set {}
    }

    public override HitType hitType {
        get { return HitType.Periodic; }
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
        SetLayer(player);
        transform.position = position;
        transform.rotation = rotation;
    }

    override public IEnumerator Attack(Entity user, Entity target, Vector3 direction)
    {
        Debug.Log("FireInferno Attack");
        // this.player = player.gameObject;
        // expand
        while (transform.localScale.x < expandDistance)
        {
            transform.localScale += new Vector3(expandSpeed*60*Time.deltaTime, expandSpeed*60*Time.deltaTime, 0);
            yield return null;   
        }

        // destroy parent
        Destroy(gameObject);
    }
}