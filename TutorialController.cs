using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : BattleController
{
    public GameObject tutorialDialog;
    public GameObject tutorialSpawner;
    bool resurrected = false;

    public IEnumerator MovePlayer(Vector3 position)
    {
        // move player to position
        // while distance to position > 0.1
        //     move player towards position
        while (Vector3.Distance(player.transform.position, position) > 0.1)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, position, 0.2f);
            yield return null;
        }
    }
    public IEnumerator StartTutorial()
    {
        Pause();
        // show first dialog
        yield return tutorialDialog.GetComponent<Dialog>().DialogCoroutine("Now then, show me your resolve.", "Nyx", "Nyx_tutorial_1");
        
        Unpause();
        GameObject enemy = tutorialSpawner.GetComponent<EnemySpawner>().InstantiateEnemy("tutorial_enemy");
        enemy.transform.position = new Vector3(-4, 0, 0);

        // wait for player to kill enemy
        while (enemy != null && enemy.GetComponent<Enemy>().life > 0)
        {
            yield return null;
        }

        player.GetComponent<Player>().life = 0.01f;
        int iterations = 0;
        while (true){
            iterations++;
            // destroy all objects tagged as Attack
            GameObject[] attacks = GameObject.FindGameObjectsWithTag("Attack");
            foreach (GameObject attack in attacks)
            {
                Destroy(attack);
            }
            // destroy Enemy tagged objects
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemi in enemies)
            {
                Destroy(enemi);
            }
            // move player to the right side of the screen
            yield return MovePlayer(new Vector3(4, 0, 0));
            // spawn 3 enemies
            enemies = new GameObject[3];
            enemy = tutorialSpawner.GetComponent<EnemySpawner>().InstantiateEnemy("tutorial_enemy");
            enemy.GetComponent<Enemy>().exp = 0;
            enemy.transform.position = new Vector3(-4, 0, 0);
            enemies[0] = enemy;
            enemy = tutorialSpawner.GetComponent<EnemySpawner>().InstantiateEnemy("tutorial_enemy");
            enemy.GetComponent<Enemy>().exp = 0;
            enemy.transform.position = new Vector3(-4, 1, 0);
            enemies[1] = enemy;
            enemy = tutorialSpawner.GetComponent<EnemySpawner>().InstantiateEnemy("tutorial_enemy");
            enemy.GetComponent<Enemy>().exp = 0;
            enemy.transform.position = new Vector3(-4, -1, 0);
            enemies[2] = enemy;

            // if not resurrected increase enemy speed
            if (!resurrected)
            {
                foreach (GameObject enemi in enemies)
                {
                    enemi.GetComponent<Enemy>().speed = iterations*0.2f;
                }
            }

            // wait until all enemies are null or all enemy's hp is less than 0 or player hp is less than 0

            // count how many enemies are alive
            while(true){
                int alive = 0;
                foreach (GameObject enemi in enemies)
                {
                    if (enemi != null && enemi.GetComponent<Enemy>().life > 0)
                    {
                        alive++;
                    }
                }
                if (alive == 0 || player.GetComponent<Player>().life <= 0)
                {
                    break;
                }
                yield return new WaitForSeconds(0.5f);
            }
            // yield return new WaitUntil(() => enemy == null || enemy.GetComponent<Enemy>().life <= 0 || player.GetComponent<Player>().life <= 0);

            Pause();
            // if player hp is less than 0
            if (player.GetComponent<Player>().life <= 0)
            {
                // increate strength by 1
                player.GetComponent<Player>().baseStrength++;
                // wait for 1 second (unscaled)
                yield return new WaitForSecondsRealtime(1);
                // remove all enemies and attacks
                attacks = GameObject.FindGameObjectsWithTag("Attack");
                foreach (GameObject attack in attacks)
                {
                    Destroy(attack);
                }
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemi in enemies)
                {
                    Destroy(enemi);
                }
                // resurrect player
                player.GetComponent<Player>().Resurrect();
                // move player to the right side of the screen
                yield return MovePlayer(new Vector3(4, 0, 0));
                if (!resurrected){
                    // show game over dialog
                    yield return tutorialDialog.GetComponent<Dialog>().DialogCoroutine("Thought you were dead? Not quite yet.", "Nyx", "Nyx_tutorial_3");
                    yield return tutorialDialog.GetComponent<Dialog>().DialogCoroutine("I have bound your consciousness to this point in space and time. Should you ever die, you will find yourself back to this very moment.", "Nyx", "Nyx_tutorial_4");
                    yield return tutorialDialog.GetComponent<Dialog>().DialogCoroutine("However, please exercise caution. We don’t know the full extent of this power yet, so do try your best to live. Now, try again.", "Nyx", "Nyx_tutorial_5");
                    resurrected = true;
                }
            }
            else{
                // show second dialog
                yield return tutorialDialog.GetComponent<Dialog>().DialogCoroutine("Impressive.", "Nyx", "Nyx_tutorial_2");
                if (resurrected){
                    Data.beatenTutorial = true;
                    Data.saveData();
                    break;
                }
            }
            Unpause();
        }
        // collect all exp
        GameObject[] exps = GameObject.FindGameObjectsWithTag("Exp");
        foreach (GameObject exp in exps)
        {
            exp.GetComponent<ExpOrb>().Collect(player);
        }
        // wait for a second
        yield return new WaitForSecondsRealtime(1);
        // load dialog scene Intro2
        SceneController.loadDialogScene("Intro2");
        // SceneController.loadScene("StageSelection");
        
    }
    public override void OnStart()
    {
        // find player
        player = GameObject.Find("Player");
        StartCoroutine(StartTutorial());
    }

}
