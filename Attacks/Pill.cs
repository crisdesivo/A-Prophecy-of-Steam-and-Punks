using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : AttackMove
{
    public override float damage {
        get { return 4.0f; }
        set { }
    }

    public override float knockbakPower {
        get { return 2.0f; }
        set { }
    }

    public override int pierce {
        get { return 20; }
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
    public float initialForce = 15f;
    public float speed = 0.7f;

    public override void Initialize(Vector3 position, Quaternion rotation, GameObject player)
    {
        this.user = player;
        transform.position = position;
        // add random rotation
        transform.rotation = rotation * Quaternion.Euler(0, 0, Random.Range(0, 360));
    }

    public override void BeginAttack(Entity target, Vector2 direction)
    {
        // add force impulse up
        GetComponent<Rigidbody2D>().AddForce(user.GetComponent<Player>().aimDirection.normalized * initialForce, ForceMode2D.Impulse);
    }

    public override IEnumerator Attack(Entity user, Entity target, Vector3 direction)
    {
        // direction = player.aimDirection;
        // // while in the screen move in the direction
        // while (transform.position.x < 10 && transform.position.x > -10 && transform.position.y < 10 && transform.position.y > -10)
        // {
        //     transform.position += (Vector3)direction.normalized * speed * Time.deltaTime * 15;
        //     yield return null;
        // }
        yield return null;
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
        // delete if out of screen
        if (transform.position.x > 20 || transform.position.x < -20 || transform.position.y > 20 || transform.position.y < -20)
        {
            Destroy(gameObject);
        }
        
    }

    // onCollision2D
    void OnCollisionEnter2D(Collision2D collision)
    {
        // if hit enemy
        if (collision.gameObject.tag == tagHit)
        {
            collision.gameObject.GetComponent<Enemy>().TakeHit(damage, knockbakPower);
            currentPierce++;
            if (currentPierce >= pierce)
            {
                Destroy(gameObject);
            }
        }
    }
}
