using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackMove: MonoBehaviour
{
    public bool paused = false;
    public enum HitType
    {
        OnCollision,
        Periodic
    }
    public string tagHit;
    public abstract float damage { get; set; }
    public abstract float knockbakPower { get; set; }
    public abstract int pierce { get; set; }
    public abstract float refreshRate { get; set; }
    public float lastRefresh = 0.0f;
    public abstract HitType hitType { get; set; }
    public int currentPierce = 0;
    public HashSet<Collider2D> onRange = new HashSet<Collider2D>();
    public GameObject user;

    // Start is called before the first frame update
    void Start()
    {
    }

    public abstract IEnumerator Attack(Entity user, Entity target, Vector3 direction);
    public abstract void OnUpdate();
    public void SetLayer(GameObject user){
        // find the target of the user
        string target = user.GetComponent<Entity>().targetTag;
        // if target is Player set layer to the one named Enemy Attack
        if (target == "Player")
        {
            gameObject.layer = LayerMask.NameToLayer("Enemy Attack");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Player Attack");
        }
        // set tag to Attack
        gameObject.tag = "Attack";
    }

    public abstract void Initialize(Vector3 position, Quaternion rotation, GameObject user);

    // public virtual void BeginAttack()
    // {
    //     // Debug.Log("Attack original");
    //     StartCoroutine(Attack(user.GetComponent<Entity>(), null, new Vector3(1, 0, 0)));
    // }

    public virtual void BeginAttack(Entity target, Vector2 direction)
    {
        // Debug.Log("Attack target");
        StartCoroutine(Attack(user.GetComponent<Entity>(), target, direction));
    }
    public void DamageOnRange()
    {
        foreach (Collider2D collision in onRange)
        {
            if (collision.gameObject.tag == tagHit)
            {
                collision.gameObject.GetComponent<Entity>().TakeHit(damage, knockbakPower);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!paused){
            OnUpdate();
            if (hitType == HitType.Periodic)
            {
                if (lastRefresh > refreshRate)
                {
                    DamageOnRange();
                    lastRefresh = 0f;
                }
                lastRefresh += Time.deltaTime;
            }
        }
    }

    // Add trigger to sword hitting target
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == tagHit)
        {
            Debug.Log("Hit");
            if (hitType == HitType.OnCollision)
            {
                // Destroy(collision.gameObject);
                collision.gameObject.GetComponent<Entity>().TakeHit(damage, knockbakPower);
            }
            else if (hitType == HitType.Periodic)
            {
                onRange.Add(collision);
                collision.gameObject.GetComponent<Entity>().TakeHit(damage, knockbakPower);
            }
            currentPierce++;
            if (currentPierce >= pierce)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == tagHit)
        {
            if (hitType == HitType.Periodic)
            {
                onRange.Remove(collision);
            }
        }
    }
}
