using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// use textmesh pro
using TMPro;

public abstract class Entity : MonoBehaviour
{
    public GameObject animation;
    public bool paused = false;
    public abstract string targetTag { get; set; }
    public virtual float speed {
        get {
            return 1.0f;
        }
        set { }
    }
    public virtual float dexterity {
        get {
            return 1.0f;
        }
        set { }
    }
    public virtual Vector2 aimDirection {
        get {
            return new Vector2(1, 0);
        }
        set { }
    }
    public float life = 10.0f;
    public float maxLife = 10.0f;
    public Coroutine getHitCoroutine = null;
    public AttackSummoner currentAttackSummoner;
    public ArrayList attackSummoners;

    // Start is called before the first frame update
    void Start()
    {
    }

    public virtual IEnumerator GetHit(float damage)
    {
        // check if it has component SpriteRenderer
        if (GetComponent<SpriteRenderer>() != null)
        {
            // flash red
            GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().color = Color.white;
            getHitCoroutine = null;
        }
        else{
            // flash red the animation
            animation.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.1f);
            animation.GetComponent<SpriteRenderer>().color = Color.white;
            getHitCoroutine = null;
        }
    }

    public virtual void TakeHit(float damage, float knockbakPower)
    {
        // move enemy in the opposite direction of the player
        // transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -knockbakPower*50 * Time.deltaTime / knockbakResistance);

        // take damage
        life -= damage;
        if (life <= 0)
        {
            // die
            Die();
        }
        else
        {
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
    }

    public abstract void Die();

}
