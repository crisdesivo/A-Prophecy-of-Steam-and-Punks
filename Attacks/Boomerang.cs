using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : AttackMove
{
    public override float damage {
        get {return 3f;}
        set {}
        }
    public override float knockbakPower {
        get {return 10f;} 
        set{}
        }

    public override int pierce {
        get {return 3;}
        set{}
        }

    public override HitType hitType {
        get {return HitType.OnCollision;}
        set{}
        }

    public override float refreshRate {
        get {return 0.1f;}
        set{}
        }
    public float speed = 15f;
    public float returnTime = 4f;

    override public void OnUpdate()
    {    
    }

    public override void Initialize(Vector3 position, Quaternion rotation, GameObject player)
    {
        this.user = player;
        SetLayer(player);
    }

    public override void BeginAttack(Entity target, Vector2 direction)
    {
        Debug.Log("Boomerang attack new");
        StartCoroutine(Attack(user.GetComponent<Entity>(), target, direction.normalized));
    }

    void Start()
    {
        Debug.Log("Boomerang attack start");
        // GameObject player = GameObject.Find("Player");
        // StartCoroutine(Attack(player.GetComponent<Entity>()));
    }

    override public IEnumerator Attack(Entity user, Entity target, Vector3 direction)
    {
        // Vector3 direction = user.aimDirection.normalized;
        for (float i = 0; i < returnTime; i+= Time.deltaTime)
        {
            transform.position += direction * speed * Time.deltaTime;
            // rotate
            transform.Rotate(0, 0, speed*60*Time.deltaTime);
            yield return null;   
        }
        // return
        for (float i = 0; i < returnTime; i+= Time.deltaTime)
        {
            transform.position -= direction * speed * Time.deltaTime;
            // rotate
            transform.Rotate(0, 0, speed*60*Time.deltaTime);
            yield return null;   
        }
        // destroy parent
        Destroy(gameObject);
    }
}