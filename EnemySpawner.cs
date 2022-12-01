using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int level;
    public int floor;
    public GameObject player;
    public GameObject door;
    public GameObject soundtrack;
    public GameObject boss1;
    public GameObject boss2;
    public GameObject boss3;

    public GameObject InstantiateEnemy(string enemyName)
    {
        GameObject enemy = Instantiate(Resources.Load("Enemies/" + enemyName)) as GameObject;
        enemy.GetComponent<Enemy>().player = player;
        return enemy;
    }

    public void SpawnDifficulty(float difficulty, Dictionary<string, (float, GameObject)> enemyDifficulty){
        // get lowest enemy difficulty
        float lowestDifficulty = Mathf.Infinity;
        foreach (KeyValuePair<string, (float, GameObject)> enemy in enemyDifficulty)
        {
            if (enemy.Value.Item1 < lowestDifficulty)
            {
                lowestDifficulty = enemy.Value.Item1;
            }
        }
        float currentDifficulty = 0;
        while (currentDifficulty + lowestDifficulty < difficulty){
            // get random enemy with difficulty less than difficulty - currentDifficulty
            List<string> possibleEnemies = new List<string>();
            foreach (KeyValuePair<string, (float, GameObject)> enemy in enemyDifficulty)
            {
                if (enemy.Value.Item1 <= difficulty - currentDifficulty)
                {
                    possibleEnemies.Add(enemy.Key);
                }
            }
            // select random enemy from possible enemies, weighted by difficulty
            float totalDifficulty = 0;
            foreach (string enemy in possibleEnemies)
            {
                totalDifficulty += enemyDifficulty[enemy].Item1;
            }
            float random = Random.Range(0, totalDifficulty);
            float currentTotal = 0;
            string selectedEnemy = "";
            foreach (string enemy in possibleEnemies)
            {
                currentTotal += enemyDifficulty[enemy].Item1;
                if (currentTotal >= random)
                {
                    selectedEnemy = enemy;
                    break;
                }
            }
            // string enemyName = possibleEnemies[Random.Range(0, possibleEnemies.Count)];
            // spawn enemy
            GameObject enemyObject = Instantiate(enemyDifficulty[selectedEnemy].Item2, new Vector3(Random.Range(-6, 6), 5, 0), Quaternion.identity);
            enemyObject.GetComponent<Enemy>().player = player;
            currentDifficulty += enemyDifficulty[selectedEnemy].Item1;
        }
    }
    
    public void SpawnFloor(){
        Debug.Log("Spawning floor " + floor);
        if (floor >= 1){
            if (level == 1){
                if (floor <= 10){
                    GameObject batPrefab = Resources.Load("Enemies/bat") as GameObject;
                    GameObject spiderPrefab = Resources.Load("Enemies/spider") as GameObject;
                    GameObject enemyBallPrefab = Resources.Load("Enemies/enemy_ball") as GameObject;
                    GameObject turtlePrefab = Resources.Load("Enemies/turtle") as GameObject;

                    float batDifficulty = 1f;
                    float spiderDifficulty = 0.8f;
                    float enemyBallDifficulty = 2.9f;
                    float turtleDifficulty = 3.5f;

                    Dictionary<string, (float, GameObject)> enemies = new Dictionary<string, (float, GameObject)>();
                    enemies.Add("bat", (batDifficulty, batPrefab));
                    enemies.Add("spider", (spiderDifficulty, spiderPrefab));
                    enemies.Add("enemy_ball", (enemyBallDifficulty, enemyBallPrefab));
                    enemies.Add("turtle", (turtleDifficulty, turtlePrefab));

                    float flootDifficulty = 1.8f + floor;
                    SpawnDifficulty(flootDifficulty, enemies);
                    // for (int i = 0; i < floor*level; i++){
                    //     // spawn enemies at the top of the screen
                    //     GameObject enemy = Instantiate(enemyPrefab, new Vector3(Random.Range(-6, 6), 5, 0), Quaternion.identity);
                    //     enemy.GetComponent<Enemy>().player = player;
                    // }
                }
                else{
                    // spawn boss
                    boss1.SetActive(true);

                    soundtrack.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/boss_track_1_test_master_2");
                    soundtrack.GetComponent<AudioSource>().Play();
                }
            }
            else if (level == 2){
                if (floor <= 10){
                    GameObject soulClockPrefab = Resources.Load("Enemies/soul_clock") as GameObject;
                    GameObject pinkOrbPrefab = Resources.Load("Enemies/pink_orb") as GameObject;
                    GameObject enemyBall2Prefab = Resources.Load("Enemies/enemy_ball2") as GameObject;
                    GameObject soulShooterPrefab = Resources.Load("Enemies/soul_shooter") as GameObject;
                    float soulClockDifficulty = 2.1f;
                    float pinkOrbDifficulty = 2.1f;
                    float enemyBall2Difficulty = 2.1f;
                    float soulShooterDifficulty = 2.1f;

                    Dictionary<string, (float, GameObject)> enemies = new Dictionary<string, (float, GameObject)>();
                    enemies.Add("soul_clock", (soulClockDifficulty, soulClockPrefab));
                    enemies.Add("pink_orb", (pinkOrbDifficulty, pinkOrbPrefab));
                    enemies.Add("enemy_ball2", (enemyBall2Difficulty, enemyBall2Prefab));
                    enemies.Add("soul_shooter", (soulShooterDifficulty, soulShooterPrefab));

                    float flootDifficulty = 4.8f + 1.4f*floor;
                    SpawnDifficulty(flootDifficulty, enemies);
                    // for (int i = 0; i < floor*level; i++){
                    //     // spawn enemies at the top of the screen
                    //     GameObject enemy = Instantiate(enemyPrefab, new Vector3(Random.Range(-6, 6), 5, 0), Quaternion.identity);
                    //     enemy.GetComponent<Enemy>().player = player;
                    // }
                }
                else{
                    // spawn boss
                    boss2.SetActive(true);

                    soundtrack.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/boss_track_2_ogg");
                    soundtrack.GetComponent<AudioSource>().Play();
                }
            }
            else if (level == 3){

                if (floor <= 10){
                    GameObject batPrefab = Resources.Load("Enemies/bat2") as GameObject;
                    GameObject spiderPrefab = Resources.Load("Enemies/spider2") as GameObject;
                    GameObject enemyBallPrefab = Resources.Load("Enemies/enemy_ball2") as GameObject;
                    GameObject turtlePrefab = Resources.Load("Enemies/turtle2") as GameObject;

                    float batDifficulty = 1f;
                    float spiderDifficulty = 0.8f;
                    float enemyBallDifficulty = 2.9f;
                    float turtleDifficulty = 3.5f;

                    Dictionary<string, (float, GameObject)> enemies = new Dictionary<string, (float, GameObject)>();
                    enemies.Add("bat", (batDifficulty, batPrefab));
                    enemies.Add("spider", (spiderDifficulty, spiderPrefab));
                    enemies.Add("enemy_ball", (enemyBallDifficulty, enemyBallPrefab));
                    enemies.Add("turtle", (turtleDifficulty, turtlePrefab));

                    float flootDifficulty = 1.8f + floor*1.4f;
                    SpawnDifficulty(flootDifficulty, enemies);
                    // for (int i = 0; i < floor*level; i++){
                    //     // spawn enemies at the top of the screen
                    //     GameObject enemy = Instantiate(enemyPrefab, new Vector3(Random.Range(-6, 6), 5, 0), Quaternion.identity);
                    //     enemy.GetComponent<Enemy>().player = player;
                    // }
                }
                else{
                    // spawn boss
                    boss3.SetActive(true);

                    soundtrack.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/boss_track_3_ogg");
                    soundtrack.GetComponent<AudioSource>().Play();
                }
            }
        }
    }

    public IEnumerator waitUntilEnemiesDead(){
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);
    }

    public IEnumerator spawnDoor(){
        yield return null;
        // spawn door at the top offscreen so that it falls to the top of the screen
        GameObject door = Instantiate(this.door, new Vector3(0, 10, 0), Quaternion.identity);
        // drop the door to the top of the screen
        while (door.transform.position.y > 6 && door != null){
            door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y - 0.1f, door.transform.position.z);
            yield return null;
        }
    }

    public IEnumerator RunFloor(){
        SpawnFloor();
        yield return waitUntilEnemiesDead();
        Debug.Log("Enemies dead");
        // filter low frequencies out from the soundtrack
        soundtrack.GetComponent<AudioHighPassFilter>().enabled = true;

        yield return spawnDoor();
    }

    public void NextFloor(){

        soundtrack.GetComponent<AudioHighPassFilter>().enabled = false;
        floor++;
        if (floor <= 11){
            // set player position to the bottom of the screen
            player.transform.position = new Vector3(0, -4, 0);
            // delete all attacks
            GameObject[] attacks = GameObject.FindGameObjectsWithTag("Attack");
            foreach (GameObject attack in attacks){
                Destroy(attack);
            }
            // move all experience orbs to the center of the screen
            GameObject[] experienceOrbs = GameObject.FindGameObjectsWithTag("Exp");
            foreach (GameObject experienceOrb in experienceOrbs){
                experienceOrb.transform.position = new Vector3(Random.Range(-0.6f, 0.6f), Random.Range(-0.6f, 0.6f), 0);
            }
            StartCoroutine(RunFloor());
        }
        else{
            if (level == 1){
                if (!Data.beatenLevel1) {
                    Data.beatenLevel1 = true;
                    Data.saveData();
                    SceneController.loadDialogScene("Interlude");
                }
                else{
                    SceneController.loadScene("StageSelection");
                    Data.saveData();
                }
            }
            else if (level == 2){
                if (!Data.beatenLevel2) {
                    Data.beatenLevel2 = true;
                    Data.saveData();
                    SceneController.loadDialogScene("Interlude2");
                }
                else{
                    SceneController.loadScene("StageSelection");
                    Data.saveData();
                }
            }
            else if (level == 3){
                if (!Data.beatenLevel3) {
                    Data.beatenLevel3 = true;
                    Data.saveData();
                    SceneController.loadDialogScene("FinalDialog");
                }
                else{
                    SceneController.loadScene("StageSelection");
                    Data.saveData();
                }
            }
        }
    }

    public void Begin(){
        floor = 0;
        StartCoroutine(RunFloor());
    }

    public virtual void OnStart()
    {
        // level = 1;
        // floor = 0;
        // StartCoroutine(RunFloor());
        // Debug.Log("started");
    }

    void Start()
    {
        OnStart();
    }
}
