// unity script for enemy class
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// import File
using System.IO;

public class TombBoss: Enemy
{
    public GameObject bullet;
    public GameObject lifeBar;
    public GameObject winSong;
    public Vector3 attackDirection = new Vector3(1, 0, 0);
    // public GameObject turret;
    public override void OnStart()
    {
        loadStatsFromJson("tomb_boss_config");
        // bullet = Resources.Load("Prefabs/soul_bullet") as GameObject;
    }
    
    public override void Move(){
        // move down if player is down or up if player is up
        // if (player.transform.position.y > transform.position.y)
        // {
        //     transform.position += new Vector3(0, 1, 0) * speed * Time.deltaTime;
        // }
        // else
        // {
        //     transform.position += new Vector3(0, -1, 0) * speed * Time.deltaTime;
        // }
    }
    public override void ChooseAttack()
    {
        Coroutine attackCoroutine = StartCoroutine(QuickAttack());
        // resume movement after attack
        StartCoroutine(waitAndResumeMovement(attackCoroutine));
    }

    public override void Attack()
    {
        // GameObject attack = Instantiate(attackPrefab, transform.position, transform.rotation);
        // attack.GetComponent<AttackMove>().tagHit = "Player";
        // attack.layer = LayerMask.NameToLayer("Enemy Attack");

        // attack.GetComponent<AttackMove>().Initialize(new Vector3(3.0f*(Random.Range(0, 2)*2-1), transform.position.y, transform.position.z + 0.5f), transform.rotation, gameObject);
        // Vector2 direction = new Vector2(Mathf.Cos(Mathf.PI * 2 / 6 * 0), Mathf.Sin(Mathf.PI * 2 / 6 * 0));
        // attack.GetComponent<AttackMove>().BeginAttack(player.GetComponent<Entity>(), direction);
    
        attackFinished = true;
    }

    public IEnumerator QuickAttack()
    {
        // Summon an enemy
        GameObject enemyPrefab = Resources.Load("Enemies/enemy_ball") as GameObject;
        // summon at random position
        Vector3 summonPosition = new Vector3(Random.Range(-5f, 5f), 7, 0);
        GameObject enemy = Instantiate(enemyPrefab, summonPosition, transform.rotation);
        enemy.GetComponent<Enemy>().player = player;
        attackFinished = true;
        yield return null;

        // GameObject attack = Instantiate(bullet, transform.position, transform.rotation);
        // attack.GetComponent<AttackMove>().tagHit = "Player";
        // attack.layer = LayerMask.NameToLayer("Enemy Attack");

        // attack.GetComponent<AttackMove>().Initialize(new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f), transform.rotation, gameObject);
        // attack.GetComponent<AttackMove>().BeginAttack(player.GetComponent<Entity>(), new Vector2(0, 1));
    }

    public override void OnTakeDamage(float damage)
    {
        lifeBar.GetComponent<LifeBar>().UpdateLifeBar(life, maxLife);
        // summon a soul bullet
        GameObject attack = Instantiate(bullet, transform.position, transform.rotation);
        attack.GetComponent<AttackMove>().tagHit = "Player";
        attack.layer = LayerMask.NameToLayer("Enemy Attack");
        Debug.Log(attack);

        attack.GetComponent<AttackMove>().Initialize(transform.position, transform.rotation, gameObject);
        Vector2 direction = attackDirection;
        attack.GetComponent<AttackMove>().BeginAttack(player.GetComponent<Entity>(), direction);
        // rotate attack direction by 30 degrees
        attackDirection = Quaternion.Euler(0, 0, 30) * attackDirection;

        if (life <= 0)
        {
            // set lifebar to inactive
            lifeBar.SetActive(false);
            winSong.GetComponent<AudioSource>().Play();
        }
    }

}
