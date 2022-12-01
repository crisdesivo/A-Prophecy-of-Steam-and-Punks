using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : AttackMove
{
    public float _damage = 1f;
    public override float damage {
        get { return _damage; }
        set { _damage = value; }
    }

    public override float knockbakPower {
        get { return 2.0f; }
        set { }
    }

    public override int pierce {
        get { return 2; }
        set { }
    }

    public override float refreshRate {
        get { return 1.0f; }
        set { }
    }

    public override HitType hitType {
        get { return HitType.OnCollision; }
        set { }
    }

    public Vector2 direction = new Vector2(1, 0);
    public float speed = 0.7f;

    public override void Initialize(Vector3 position, Quaternion rotation, GameObject player)
    {
        // set layer
        SetLayer(player);
        this.user = player;
        transform.position = position;
        transform.rotation = rotation;
    }

    public override IEnumerator Attack(Entity user, Entity target, Vector3 direction)
    {
        // direction = user.aimDirection;
        // while in the screen move in the direction
        while (transform.position.x < 10 && transform.position.x > -10 && transform.position.y < 10 && transform.position.y > -10)
        {
            transform.position += direction.normalized * speed * Time.deltaTime * 15;
            yield return null;
        }
        // destroy the bullet
        Destroy(gameObject);
    }

    public override void OnUpdate()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
