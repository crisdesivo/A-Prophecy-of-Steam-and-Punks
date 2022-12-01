using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public bool paused = false;
    public GameObject upgradeMenu;
    public GameObject soundtrack;
    public GameObject player;
    public GameObject factoryStage;
    public GameObject mausoleumStage;
    public GameObject palaceStage;
    public GameObject enemySpawner;
    public void Pause(){
        paused = true;
        Time.timeScale = 0;
        // Find Player tagged objects and set paused to true
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.GetComponent<Player>().paused = true;
        }

        // Find Enemy tagged objects and set paused to true
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().paused = true;
        }

        // Find Attack tagged objects and set paused to true
        GameObject[] attacks = GameObject.FindGameObjectsWithTag("Attack");
        foreach (GameObject attack in attacks)
        {
            // if attack has component AttackMove
            if (attack.GetComponent<AttackMove>())
            {
                attack.GetComponent<AttackMove>().paused = true;
            }
        }
    }

    public void Unpause(){
        paused = false;
        Time.timeScale = 1;
        // Find Player tagged objects and set paused to false
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.GetComponent<Player>().paused = false;
        }

        // Find Enemy tagged objects and set paused to false
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().paused = false;
        }

        // Find Attack tagged objects and set paused to false
        GameObject[] attacks = GameObject.FindGameObjectsWithTag("Attack");
        foreach (GameObject attack in attacks)
        {
            // if attack has component AttackMove
            if (attack.GetComponent<AttackMove>())
            {
                attack.GetComponent<AttackMove>().paused = false;
            }
        }
    }

    public void PauseUnpause()
    {
        if (!paused)
        {
            Pause();
        }
        else
        {
            Unpause();
        }
    }

    public void UpgradeMenu()
    {
        Pause();
        upgradeMenu.SetActive(true);
        upgradeMenu.GetComponent<UpgradeMenu>().RandomSelect();
    }

    public virtual void OnStart()
    {
        // Data.beatenLevel1 = true; // TODO remove this
        // Data.beatenLevel2 = true; // TODO remove this
        // SceneController.input = "3"; // TODO remove this
        if (SceneController.input == "1"){
            factoryStage.SetActive(true);
            enemySpawner.GetComponent<EnemySpawner>().level = 1;
            soundtrack.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/FACTORY_THEME_FINAL");
            soundtrack.GetComponent<AudioSource>().Play();
        }
        else if (SceneController.input == "2"){
            mausoleumStage.SetActive(true);
            enemySpawner.GetComponent<EnemySpawner>().level = 2;
            soundtrack.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/MAUSOLEUM_THEME_FINAL");
            soundtrack.GetComponent<AudioSource>().Play();
        }
        else if (SceneController.input == "3"){
            palaceStage.SetActive(true);
            enemySpawner.GetComponent<EnemySpawner>().level = 3;
            soundtrack.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/PALACE_THEME_FINAL");
            soundtrack.GetComponent<AudioSource>().Play();
        }
        else{
            palaceStage.SetActive(true);
            enemySpawner.GetComponent<EnemySpawner>().level = 4;
            soundtrack.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/PALACE_THEME_FINAL");
            soundtrack.GetComponent<AudioSource>().Play();
        }
        enemySpawner.GetComponent<EnemySpawner>().Begin();
        // Unpause();
        // UpgradeMenu();
        Pause();
        upgradeMenu.SetActive(true);
        upgradeMenu.GetComponent<UpgradeMenu>().RandomSelectWeapons();
    }

    void Start()
    {
        OnStart();
    }
}
