using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestBoss : Enemy
{
    public GameObject lifeBar;
    public GameObject winSong;
    public GameObject dialogController;
    public GameObject battleController;
    public override void OnStart()
    {
        loadStatsFromJson("priest_boss_config");
        rotateOnDirection = false;
    }


    public override void ChooseAttack()
    {
        attackFinished = true;
        moveCoroutine = StartCoroutine(MoveCoroutine());
        // Coroutine attackCoroutine = StartCoroutine(QuickAttack());
        // // resume movement after attack
        // StartCoroutine(waitAndResumeMovement(attackCoroutine));
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

    public override void OnTakeDamage(float damage)
    {
        baseSpeed += 0.3f;
        lifeBar.GetComponent<LifeBar>().UpdateLifeBar(life, maxLife);
    }

    public IEnumerator Death()
    {
        // remove all objects tagged Attack from screen
        GameObject[] attacks = GameObject.FindGameObjectsWithTag("Attack");
        foreach (GameObject attack in attacks)
        {
            Destroy(attack);
        }
        // stop music
        GameObject.Find("Soundtrack").GetComponent<AudioSource>().Stop();
        // pause
        battleController.GetComponent<BattleController>().Pause();
        yield return StartCoroutine(dialogController.GetComponent<DialogController>().PriestDialog());
        // kill player
        SceneController.forcedDeath = true;
        battleController.GetComponent<BattleController>().Unpause();
        player.GetComponent<Player>().Die();
    }


    public override void Die(){
        if (Data.beatenLevel3){
            // set lifebar to inactive
            lifeBar.SetActive(false);
            winSong.GetComponent<AudioSource>().Play();
            alive = false;
            Destroy(gameObject);
        }
        else{
            Data.beatenLevel3 = true;
            StartCoroutine(Death());
            
        }
    }

}
