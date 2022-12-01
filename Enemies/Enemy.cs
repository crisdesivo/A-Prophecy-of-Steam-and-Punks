// unity script for enemy class
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// import File
using System.IO;

public class Enemy: Entity
{
    public float exp;
    public enum MovementType
    {
        None,
        Follow,
        Tangent,
        FollowTangent,
        RandomTarget
    }
    protected bool attackFinished = false;
    protected float baseSpeed = 1.0f;
    public override float speed {
        get {
            return baseSpeed;
        }
        set { }
    }
    public MovementType movementType = MovementType.Follow;
    public override string targetTag { get { return "Player"; } set { } }
    public GameObject attackPrefab { get; set; }
    public float damage = 1.0f;
    public float attackDelay = 5.0f;
    private float currentDelay = 0f;
    // public float dropExp = 1.0f;
    public static GameObject expPrefab;
    public float knockbakResistance = 1f;
    protected float afterAttackDelay = 0.5f;
    protected float shakeRadius = 0.1f;
    protected float shakeTime = 0.5f;
    string attackType = "soul_bullet";
    public GameObject player;
    // Coroutine getHitCoroutine = null;
    protected Coroutine moveCoroutine = null;
    Coroutine chargeCoroutine = null;
    private float chargeForce = 80f;
    public bool rotateOnDirection = true;
    public bool alive = true;

    public override Vector2 aimDirection { 
        get {
            return (player.transform.position - transform.position).normalized;
        }
        set { }
    }
    public void MoveTowardsPlayer()
    {
        // move enemy towards player using rigidbody with constant speed
        Vector2 direction = (player.transform.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
        // transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        // rotate and flip horizontally if needed
        if (rotateOnDirection)
        {
            // Vector2 direction = (player.transform.position - transform.position).normalized;
            if (direction.x > 0)
            {
                // rotate towards direction
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

                GetComponent<SpriteRenderer>().flipX = false;
                // GetComponent<SpriteRenderer>().flipY = false;
            }
            else
            {
                // rotate towards direction
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg);
                // flip sprite horizontally
                GetComponent<SpriteRenderer>().flipX = true;
                // GetComponent<SpriteRenderer>().flipY = true;
            }
        }
        // // flip sprite horizontally if enemy is moving left
        // if (transform.position.x > player.transform.position.x)
        // {
        //     GetComponent<SpriteRenderer>().flipX = true;
        // }
        // else
        // {
        //     GetComponent<SpriteRenderer>().flipX = false;
        // }
    }

    public void MoveTangentlyTowardsPlayer()
    {
        // move enemy tangent to player
        Vector3 direction = player.transform.position - transform.position;
        Vector3 tangent = new Vector3(direction.y, -direction.x, 0);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position + tangent, speed * Time.deltaTime);
        if (rotateOnDirection)
        {
            // rotate enemy towards player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            // flip sprite horizontally if moving left
            if (tangent.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public void MoveTangentToPlayer()
    {
        // move enemy tangent to player
        Vector3 direction = player.transform.position - transform.position;
        Vector3 tangent = new Vector3(direction.y, -direction.x, 0);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + tangent, speed * Time.deltaTime);
        if (rotateOnDirection)
        {
            // rotate enemy towards player
            float angle = Mathf.Atan2(tangent.y, tangent.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            // flip sprite horizontally if tangent is to the left of the enemy
            if (tangent.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    
    }

    public virtual void Move()
    {
        // switch
        switch (movementType)
        {
            case MovementType.Follow:
                MoveTowardsPlayer();
                break;
            case MovementType.Tangent:
                MoveTangentToPlayer();
                break;
            case MovementType.FollowTangent:
                MoveTangentlyTowardsPlayer();
                break;
            case MovementType.RandomTarget:
                break;
            default:
                break;
        }
    }

    public IEnumerator MoveCoroutine()
    {
        Debug.Log("MoveCoroutine");
        // TODO parametrize
        while (true)
        {
            // for 60 steps
            for (int i = 0; i < 500; i++)
            {
                while(paused)
                {
                    yield return null;
                }
                Move();
                yield return null;
            }
            // wait for 1 second
            while(paused)
            {
                yield return null;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public virtual void Attack()
    {
        // summon 6 attacks in 6 equidistant directions
        for (int i = 0; i < 6; i++)
        {
            GameObject attack = Instantiate(attackPrefab, transform.position, transform.rotation);
            attack.GetComponent<AttackMove>().tagHit = "Player";
            attack.layer = LayerMask.NameToLayer("Enemy Attack");

            attack.GetComponent<AttackMove>().Initialize(transform.position, transform.rotation, gameObject);
            Vector2 direction = new Vector2(Mathf.Cos(Mathf.PI * 2 / 6 * i), Mathf.Sin(Mathf.PI * 2 / 6 * i));
            attack.GetComponent<AttackMove>().BeginAttack(player.GetComponent<Entity>(), direction);
        }
    }

    public virtual void OnTakeDamage(float damage)
    {
    }

    public override void TakeHit(float damage, float knockbakPower)
    {
        // if is alive
        if (alive)
        {
            damage *= player.GetComponent<Player>().strenght/10f;
            // take damage
            life -= damage;
            OnTakeDamage(damage);
            if (life <= 0)
            {
                // die
                Die();
            }
            else
            {
                // move enemy in the opposite direction of the player
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -knockbakPower*5 * Time.deltaTime / knockbakResistance);
                // flash red
                if (getHitCoroutine == null)
                {
                    getHitCoroutine = StartCoroutine(GetHit(damage));
                }
                else
                {
                    StopCoroutine(getHitCoroutine);
                    getHitCoroutine = StartCoroutine(GetHit(damage));
                }
            }
            // Debug.Log("Enemy life: " + life);
        }
    }

    public override void Die()
    {
        // die
        alive = false;
        Destroy(gameObject);

        // drop exp
        GameObject exporb = Instantiate(expPrefab, transform.position, transform.rotation);
        exporb.GetComponent<ExpOrb>().exp = exp;
    }

    public void Spawn()
    {
        // spawn enemy
    }

    public IEnumerator waitAndAttack(float delay)
    {
        // wait for delay
        float time = 0;

        while (time < delay)
        {
            while(paused)
            {
                yield return null;
            }
            time += Time.deltaTime;
            // shake
            Vector2 offset = Random.insideUnitCircle * shakeRadius;

            transform.position = new Vector3(transform.position.x + offset.x, transform.position.y + offset.y, transform.position.z);
            yield return null;
            // remove shake
            transform.position = new Vector3(transform.position.x - offset.x, transform.position.y - offset.y, transform.position.z);
        }
        // attack
        Attack();

        while(paused)
        {
            yield return null;
        }
        yield return new WaitForSeconds(afterAttackDelay);

        attackFinished = true;
    }

    public IEnumerator ChargeAttack(float delay)
    {
        // wait for delay
        float time = 0;
        // Vector2 offset = Vector2.zero;

        Debug.Log("Start charge attack");
        while (time < delay)
        {
            while(paused)
            {
                yield return null;
            }
            time += Time.deltaTime;
            // get direction towards player
            Vector2 direction = (player.transform.position - transform.position).normalized;

            // get tangent to player
            Vector2 tangent = new Vector3(direction.y, -direction.x);

            Vector2 offset = -direction*shakeRadius*10*time/delay;

            Vector2 offset2 = tangent * shakeRadius * 0.1f * Random.Range(-1f, 1f);

            //  apply both offsets
            transform.position = new Vector3(transform.position.x + offset.x + offset2.x, transform.position.y + offset.y + offset2.y, transform.position.z);
            

            // rotate towards player
            if (rotateOnDirection)
            {
                if (direction.x > 0)
                {
                    // rotate towards direction
                    transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

                    GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    // rotate towards direction
                    transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg);
                    // flip sprite horizontally
                    GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            yield return null;
            // remove both offsets
            transform.position = new Vector3(transform.position.x - offset.x - offset2.x, transform.position.y - offset.y - offset2.y, transform.position.z);
        }
        // add offset
        Vector2 direction2 = (player.transform.position - transform.position).normalized;
        Vector2 offset3 = -direction2 * shakeRadius * 10;
        transform.position = new Vector3(transform.position.x + offset3.x, transform.position.y + offset3.y, transform.position.z);

        // handle pause
        while(paused)
        {
            yield return null;
        } 
        while(paused)
        {
            yield return null;
        }
        time = 0;
        Vector2 direction3 = (player.transform.position - transform.position).normalized;
        while (time < afterAttackDelay)
        {
            while(paused)
            {
                yield return null;
            }
            time += Time.deltaTime;

            // add velocity towards player
            GetComponent<Rigidbody2D>().velocity = direction3 * speed*3;
            // GetComponent<Rigidbody2D>().AddForce(direction2 * chargeForce * 100, ForceMode2D.Impulse);

            yield return null;
        }

        attackFinished = true;
        // yield return new WaitForSeconds(afterAttackDelay);
    }

    public virtual void ChooseAttack()
    {
        // 50/50 chance to attack or charge
        if (Random.Range(0, 2) < 0)
        {
            Coroutine attackCoroutine = StartCoroutine(waitAndAttack(shakeTime));
            // resume movement after attack
            StartCoroutine(waitAndResumeMovement(attackCoroutine));
        }
        else
        {
            chargeCoroutine = StartCoroutine(ChargeAttack(shakeTime));
            // resume movement after attack
            StartCoroutine(waitAndResumeMovement(chargeCoroutine));
        }
    }
    public void Update()
    {
        if (!paused){
            // update enemy
            currentDelay += Time.deltaTime;
            // if attack is ready
            if (currentDelay >= attackDelay)
            {

                currentDelay = 0;
                // pause movement
                if (moveCoroutine != null)
                {
                    StopCoroutine(moveCoroutine);
                    moveCoroutine = null;
                }
                // choose attack
                ChooseAttack();
            }
        }
    }

    public IEnumerator waitAndResumeMovement(Coroutine attackCoroutine)
    {
        Debug.Log("Waiting for attack to finish");
        // wait for attackCoroutine to finish
        while (!attackFinished)
        {
            yield return null;
        }
        attackFinished = false;
        // yield return attackCoroutine;
        Debug.Log("Attack finished");

        // resume movement
        moveCoroutine = StartCoroutine(MoveCoroutine());
    }

    public virtual void OnStart()
    {
        // load from json bat_config
        loadStatsFromJson("spider_config");
        currentDelay = 0;
    }

    public void Start()
    {
        // load exp prefab if not loaded
        if (expPrefab == null)
        {
            expPrefab = Resources.Load<GameObject>("Prefabs/Exp");
        }
        // set random initial attack delay
        currentDelay = Random.Range(0, attackDelay);
        OnStart();
        // start enemy
        moveCoroutine = StartCoroutine(MoveCoroutine());
        // set prefab as bullet
        attackPrefab = Resources.Load("Prefabs/"+attackType) as GameObject;

        // player = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void OnContact()
    {
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // damage player
            collision.gameObject.GetComponent<Player>().TakeHit(damage);
            // move back
            Vector2 direction = (transform.position - collision.transform.position).normalized;
            transform.position = new Vector3(transform.position.x + direction.x * 0.5f, transform.position.y + direction.y * 0.5f, transform.position.z);
            OnContact();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Collision");
        if (collision.gameObject.tag == "Player")
        {
            // damage player
            collision.gameObject.GetComponent<Player>().TakeHit(damage);
            // move back
            Vector2 direction = (transform.position - collision.transform.position).normalized;
            transform.position = new Vector3(transform.position.x + direction.x * 0.1f, transform.position.y + direction.y * 0.1f, transform.position.z);
            

            if (chargeCoroutine != null)
            {
                StopCoroutine(chargeCoroutine);
                chargeCoroutine = null;
                attackFinished = true;
            }
        // if colliding with wall or another Enemy stop charge coroutine
        } else if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy")
        {
            if (chargeCoroutine != null)
            {
                StopCoroutine(chargeCoroutine);
                chargeCoroutine = null;
                attackFinished = true;
            }
        }
    }

    public void loadStatsFromJson(string jsonPath)
    {
        // load json string from file (available on web version as well)
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonPath);
        // string jsonString = File.ReadAllText(jsonPath);
        EnemyConfig config = JsonUtility.FromJson<EnemyConfig>(jsonFile.text);
        damage = config.damage;
        life = config.life;
        maxLife = config.life;
        attackDelay = config.attackDelay;
        knockbakResistance = config.knockbakResistance;
        afterAttackDelay = config.afterAttackDelay;
        shakeRadius = config.shakeRadius;
        shakeTime = config.shakeTime;
        attackType = config.attackType;
        baseSpeed = config.speed;
        exp = config.exp;
        // convert movement type string to enum
        movementType = (MovementType)System.Enum.Parse(typeof(MovementType), config.movementType);
    }
}

public class EnemyConfig
{
    public float damage;
    public float life;
    public float attackDelay;
    public float knockbakResistance;
    public float afterAttackDelay;
    public float shakeRadius;
    public float shakeTime;
    public string attackType;
    public string movementType;
    public float exp;
    public float speed;
}