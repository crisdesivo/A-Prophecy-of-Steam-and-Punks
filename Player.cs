using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// use textmesh pro
using TMPro;
using System;

public class Player : Entity
{
    // TODO on unity editor set the stats to normal
    public override string targetTag { get { return "Enemy"; } set { } }
    public float baseAgility = 10.0f;
    public float baseStrength = 10.0f;
    public float baseResistance = 10.0f;
    public float baseDexterity = 10.0f;
    public float baseIntelligence = 10.0f;
    public int shield = 0;
    public float exp = 0.0f;
    public float expToNextLevel = 1.0f;
    public Modifiers modifiers = new Modifiers();
    public float agility { get { return baseAgility + Data.unlockedSkills.GetValueOrDefault("agility", 0)*0.8f; } }
    public float strenght { get { return baseStrength + Data.unlockedSkills.GetValueOrDefault("strength", 0)*1f; } }
    public float resistance { get { return baseResistance + modifiers["resistance"]; } }
    public override float dexterity { get { return baseDexterity + Data.unlockedSkills.GetValueOrDefault("dexterity", 0)*1f; } }
    public float intelligence { get { return baseIntelligence + modifiers["intelligence"]; } }
    public override float speed { get { return agility * 0.1f; } }

    public override Vector2 aimDirection {
        get { return Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; }
        set { }
    }
    public bool autoShoot = false;
    public bool autoShootAll = false;
    public GameObject statsText;
    public GameObject controller;
    public GameObject heart;
    public GameObject leftWeaponDisplay;
    public GameObject rightWeaponDisplay;
    public GameObject currentWeaponDisplay;
    public GameObject hurtSound;
    // public GameObject animation;

    public List<Upgrade> upgrades = new List<Upgrade>();


    public string Stats()
    {
        // returns a line of text for each stat
        return "Agility: " + agility + "\n" +
               "Strenght: " + strenght + "\n" +
               "Resistance: " + resistance + "\n" +
               "Dexterity: " + dexterity + "\n" +
               "Intelligence: " + intelligence + "\n" +
               "Speed: " + speed;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        // ShockwaveSummoner shockwaveSummoner = new ShockwaveSummoner();
        // SwordSummoner swordSummoner = new SwordSummoner();
        // BoomerangSummoner boomerangSummoner = new BoomerangSummoner();
        // FireInfernoSummoner fireInfernoSummoner = new FireInfernoSummoner();
        // BulletSummoner bulletSummoner = new BulletSummoner();
        // PillSummoner pillSummoner = new PillSummoner();

        attackSummoners = new ArrayList();
        // attackSummoners.Add(swordSummoner);
        // attackSummoners.Add(shockwaveSummoner);
        // attackSummoners.Add(boomerangSummoner);
        // attackSummoners.Add(fireInfernoSummoner);
        // attackSummoners.Add(new BoomerangSummoner());
        // attackSummoners.Add(pillSummoner);
        // currentAttackSummoner = attackSummoners[0] as AttackSummoner;

        Debug.Log(statsText);
        Debug.Log(statsText.GetComponent<TextMeshProUGUI>());
        maxLife = 10 + Data.unlockedSkills.GetValueOrDefault("health", 0);
        life = maxLife;
        heart.GetComponent<Heart>().UpdateHP(life, maxLife);
        statsText.GetComponent<TextMeshProUGUI>().text = Stats();
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused){
            bool running = false;
            bool attacking = false;
            // update progress bar
            if (currentAttackSummoner.waitTime <= 0)
            {
                GameObject.Find("Square").GetComponent<SpriteRenderer>().enabled = false;
                GameObject.Find("DelayProgress").GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                transform.Find("DelayProgress").localScale = new Vector3(currentAttackSummoner.waitTime/currentAttackSummoner.delay, 0.2f, 1);

                GameObject.Find("Square").GetComponent<SpriteRenderer>().enabled = true;
                GameObject.Find("DelayProgress").GetComponent<SpriteRenderer>().enabled = true;
            }
            foreach (AttackSummoner summoner in attackSummoners)
            {
                summoner.OnUpdate(this);
            }
            // Input handler

            if (autoShootAll)
            {
                foreach (AttackSummoner summoner in attackSummoners)
                {
                    summoner.SummonAttack(transform.position, transform.rotation, gameObject, aimDirection);
                    attacking = true;
                }
            }
            else
            {
                if (autoShoot || Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
                {
                    // check if current attack is ready
                    if (currentAttackSummoner.waitTime <= 0)
                    {
                        currentAttackSummoner.SummonAttack(transform.position, transform.rotation, gameObject, aimDirection);
                        attacking = true;
                        // play shooting sound
                        GetComponent<AudioSource>().Play();
                    }
                    currentAttackSummoner.SummonAttack(transform.position, transform.rotation, gameObject, aimDirection);
                    attacking = true;
                }
            }
            Vector2 velocity = new Vector2(0, 0);
            // Movement
            if (Input.GetKey(KeyCode.W))
            {
                velocity += new Vector2(0, 1);
            }

            if (Input.GetKey(KeyCode.A))
            {
                velocity += new Vector2(-1, 0);
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (Input.GetKey(KeyCode.S))
            {
                velocity += new Vector2(0, -1);
            }

            if (Input.GetKey(KeyCode.D))
            {
                velocity += new Vector2(1, 0);
                // set scale x to -1
                transform.localScale = new Vector3(-1, 1, 1);
                // aimDirection = new Vector2(1, 0);
            }

            // set velocity
            GetComponent<Rigidbody2D>().velocity = velocity.normalized * speed * 5f;
            // check if velocity is 0
            if (velocity.magnitude > 0)
            {
                running = true;
            }

            if (running){
                animation.GetComponent<Animator>().SetBool("running", true);
            }
            else{
                animation.GetComponent<Animator>().SetBool("running", false);
            }

            if (attacking){
                animation.GetComponent<Animator>().SetBool("attacking", true);
            }
            else{
                animation.GetComponent<Animator>().SetBool("attacking", false);
            }

            // use Q or E to change attack
            if (Input.GetKeyDown(KeyCode.Q))
            {
                int index = attackSummoners.IndexOf(currentAttackSummoner);
                index = (index - 1 + attackSummoners.Count) % attackSummoners.Count;
                currentAttackSummoner = (AttackSummoner)attackSummoners[index];
                UpdateWeaponDisplays();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                int index = attackSummoners.IndexOf(currentAttackSummoner);
                index = (index + 1) % attackSummoners.Count;
                currentAttackSummoner = (AttackSummoner)attackSummoners[index];
                UpdateWeaponDisplays();
            }

            // Aim towards mouse
            // Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Vector3 direction = mousePos - transform.position;
            // aimDirection = new Vector2(direction.x, direction.y);
        }
        
    }


    public void UpdateWeaponDisplays(){
        // show on left weapon the weapon that is swapped by pressing Q
        // show on right weapon the weapon that is swapped by pressing E
        // show on the equipped weapon the weapon that is currently equipped
        int index = attackSummoners.IndexOf(currentAttackSummoner);
        int leftIndex = (index - 1 + attackSummoners.Count) % attackSummoners.Count;
        int rightIndex = (index + 1) % attackSummoners.Count;
        leftWeaponDisplay.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(((AttackSummoner)attackSummoners[leftIndex]).name+"_icon");
        rightWeaponDisplay.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(((AttackSummoner)attackSummoners[rightIndex]).name+"_icon");
        currentWeaponDisplay.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(((AttackSummoner)attackSummoners[index]).name+"_icon");

        // set images transparency to 1
        leftWeaponDisplay.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
        rightWeaponDisplay.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
        currentWeaponDisplay.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
    }

    public void TakeHit(float damage)
    {
        // take damage
        life -= damage*Mathf.Pow(1-0.1f, shield);
        // update hp bar
        heart.GetComponent<Heart>().UpdateHP(life, maxLife);
        // Debug.Log("Player took " + damage + " damage");
        // Debug.Log("Player has " + life + " life left");
        if (life <= 0)
        {
            // Debug.Log("Player died");
            // Destroy(gameObject);
            Die();
        }

        else
        {
            // play hurt sound
            hurtSound.GetComponent<AudioSource>().Play();
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

    public override void TakeHit(float damage, float knockbakPower)
    {
        TakeHit(damage);
    }

    public override void Die()
    {
        // die
        Debug.Log("Player died");
        // load stage selection scene
        SceneController.loadScene("Death");
    }

    public void Resurrect()
    {
        // restore life
        life = maxLife;
        // update hp bar
        heart.GetComponent<Heart>().UpdateHP(life, maxLife);
        // resurrect
        Debug.Log("Player resurrected");
        // remove transparency from player sprite
        animation.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    internal void CollectExp(ExpOrb expOrb)
    {
        float experienceCollected = expOrb.exp;
        Data.gold += (int)experienceCollected;
        exp += experienceCollected;
        Debug.Log("Player collected " + experienceCollected + " exp");
        Debug.Log("Player has " + exp + " exp");
        Debug.Log("Player needs " + (expToNextLevel - exp) + " exp to level up");
        if (exp >= expToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        // level up
        // level += 1;
        exp = exp - expToNextLevel;
        expToNextLevel = expToNextLevel * 1.2f;
        Debug.Log("Player leveled up!");
        controller.GetComponent<BattleController>().UpgradeMenu();
    }

    public void GetUpgrade(Upgrade upgrade){
        if (upgrade.type == "weapon"){
            // add weapon to list of weapons
            attackSummoners.Add(upgrade.GetAttackSummoner());
            // if no current weapon, set current weapon to new weapon
            if (currentAttackSummoner == null){
                currentAttackSummoner = (AttackSummoner)attackSummoners[0];
            }
            UpdateWeaponDisplays();
        }
        else if (upgrade.type == "stats"){
            upgrade.ApplyStats(this);
        }
        upgrades.Add(upgrade);
    }
}
