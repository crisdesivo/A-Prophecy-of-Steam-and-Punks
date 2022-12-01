using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBullet: Bullet {
    public override float damage {
        get { return 1.0f; }
        set { }
    }

    public override float knockbakPower {
        get { return 2.0f; }
        set { }
    }

    public override int pierce {
        get { return 1; }
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

    private float duration = 10.0f;
    private float time = 0.0f;


    public override void Initialize(Vector3 position, Quaternion rotation, GameObject player)
    {
        this.user = player;
        transform.position = position;
        transform.rotation = rotation;
        speed = 0.3f;
    }

    public override IEnumerator Attack(Entity user, Entity target, Vector3 direction)
    {
        time = 0.0f;
        // while in the screen move in the direction
        while (transform.position.x < 10 && transform.position.x > -10 && transform.position.y < 10 && transform.position.y > -10)
        {
            // rotate direction towards the target by 45 degrees per second
            Vector2 targetDirection = (target.transform.position - transform.position).normalized;
            // find if direction should be rotated clockwise or counterclockwise
            float cross = Vector3.Cross(direction, targetDirection).z;
            if (cross > 0)
            {
                direction = Quaternion.Euler(0, 0, 45*Time.deltaTime) * direction;
            }
            else
            {
                direction = Quaternion.Euler(0, 0, -45*Time.deltaTime) * direction;
            }
            // direction = Vector2.MoveTowards(direction, (target.transform.position - transform.position).normalized, 0.03f).normalized;
            transform.position += (Vector3)direction * speed * Time.deltaTime * 15;
            time += Time.deltaTime;
            // rotate the bullet to face the direction
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            // // apply transparency according to remaining lifetime
            // GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - 0.8f*time/duration);


            yield return null;
            if (time > duration)
            {
                break;
            }
        }
        // destroy the bullet
        Destroy(gameObject);
    }

    public override void BeginAttack(Entity target, Vector2 direction)
    {
        StartCoroutine(Attack(user.GetComponent<Entity>(), target, direction));
    }

    public override void OnUpdate()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
}