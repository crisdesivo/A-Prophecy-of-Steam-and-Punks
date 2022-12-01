// unity static class that summons an attack from given prefab
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackSummoner
{
    public string name;
    public float waitTime = 0f;
    public abstract float delay { get; set; }
    public abstract GameObject prefab { get; set; }
    public virtual void MultiAttack(GameObject user, Vector3 position, Quaternion rotation, Entity target, Vector3 direction)
    {
        GameObject attack = GameObject.Instantiate(prefab, position, rotation);
        attack.GetComponent<AttackMove>().tagHit = user.GetComponent<Entity>().targetTag;
        // if tagHit is Player set layer of attack to Enemy Attack
        if (user.GetComponent<Entity>().targetTag == "Player")
        {
            // find layer index of Enemy Attack
            attack.layer = LayerMask.NameToLayer("Enemy Attack");
        }
        else
        {
            attack.layer = LayerMask.NameToLayer("Player Attack");
        }
        attack.GetComponent<AttackMove>().Initialize(position, rotation, user);
        attack.GetComponent<AttackMove>().BeginAttack(target, direction);
    }
    public void Attack(Vector3 position, Quaternion rotation, GameObject user, Vector3 direction)
    {
        Entity target = null;
        if (user.GetComponent<Entity>().targetTag == "Player")
        {
            target = GameObject.Find("Player").GetComponent<Entity>();

        }
        else
        {
            // find random object tagged as Enemy
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            // if there are no enemies, return
            if (enemies.Length == 0)
            {
                // return;
            }
            else{
                target = enemies[Random.Range(0, enemies.Length)].GetComponent<Entity>();
            }
        }
        MultiAttack(user, position, rotation, target, direction);
        waitTime = delay;
    }
    public void SummonAttack(Vector3 position, Quaternion rotation, GameObject user, Vector3 direction)
    {
        if (waitTime <= 0)
        {
            Attack(position, rotation, user, direction);
        }
    }

    public void OnUpdate(Entity user)
    {
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime*user.dexterity/10f;
        }
    }
}

public class ShockwaveSummoner : AttackSummoner
{
    public override float delay { get {return 1f;} set {} }
    public override GameObject prefab { get; set; }
    public ShockwaveSummoner()
    {
        name = "shockwave";
        prefab = Resources.Load("Prefabs/shockwave") as GameObject;
        Debug.Log(prefab);
    }
}

public class SwordSummoner : AttackSummoner
{
    public override float delay { get {return 0.5f;} set {} }
    public override GameObject prefab { get; set; }
    public SwordSummoner()
    {
        name = "sword";
        prefab = Resources.Load("Prefabs/sword_1") as GameObject;
        Debug.Log(prefab);
    }
}

public class BoomerangSummoner : AttackSummoner
{
    public override float delay { get {return 1f;} set {} }
    public override GameObject prefab { get; set; }
    public BoomerangSummoner()
    {
        name = "boomerang";
        prefab = Resources.Load("Prefabs/boomerang") as GameObject;
        Debug.Log(prefab);
    }
}

public class FireInfernoSummoner : AttackSummoner
{
    public override float delay { get {return 3f;} set {} }
    public override GameObject prefab { get; set; }
    public FireInfernoSummoner()
    {
        name = "fire_inferno";
        prefab = Resources.Load("Prefabs/fire_inferno") as GameObject;
        Debug.Log(prefab);
    }
}

public class BulletSummoner : AttackSummoner
{
    public override float delay { get {return 0.5f;} set {} }
    public override GameObject prefab { get; set; }
    public BulletSummoner()
    {
        name = "blue_gun";
        prefab = Resources.Load("Prefabs/bullet") as GameObject;
        Debug.Log(prefab);
    }
}

public class BlueGun : AttackSummoner
{
    public float damage = 1f;
    public int numBullets = 3;
    public float spread = 45f;
    public override float delay { get {return 0.5f;} set {} }
    public override GameObject prefab { get; set; }
    public BlueGun()
    {
        name = "blue_gun";
        prefab = Resources.Load("Prefabs/bullet") as GameObject;
        Debug.Log(prefab);
    }
    public BlueGun(int numBullets, float spread)
    {
        name = "blue_gun";
        this.numBullets = numBullets;
        this.spread = spread;
        prefab = Resources.Load("Prefabs/bullet") as GameObject;
        Debug.Log(prefab);
    }
    public BlueGun(int numBullets, float spread, float damage){
        name = "blue_gun";
        this.numBullets = numBullets;
        this.spread = spread;
        this.damage = damage;
        prefab = Resources.Load("Prefabs/bullet") as GameObject;
        prefab.GetComponent<Bullet>().damage = damage;
        Debug.Log(prefab);
    }

    public override void MultiAttack(GameObject user, Vector3 position, Quaternion rotation, Entity target, Vector3 direction)
    {
        if (numBullets > 1){
            // summon multiple bullets making sure one is aimed at the target and the rest are spread out
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float angleSpread = spread / (numBullets - 1);
            for (int i = 0; i < numBullets; i++)
            {
                float angleOffset = angleSpread * (i - (numBullets - 1) / 2f);
                Vector3 directionOffset = Quaternion.AngleAxis(angleOffset, Vector3.forward) * direction;
                GameObject attack = GameObject.Instantiate(prefab, position, rotation);
                // rotate the bullet to face the direction (directionOffset) (by default it faces up)
                attack.transform.rotation = Quaternion.AngleAxis(-90 + Mathf.Atan2(directionOffset.y, directionOffset.x) * Mathf.Rad2Deg, Vector3.forward);
                // attack.transform.rotation = Quaternion.AngleAxis(angle + angleOffset, Vector3.forward);

                attack.GetComponent<AttackMove>().tagHit = user.GetComponent<Entity>().targetTag;
                attack.GetComponent<AttackMove>().Initialize(position, attack.transform.rotation, user);
                attack.GetComponent<AttackMove>().BeginAttack(target, directionOffset);
            }
        }
        else{
            GameObject attack = GameObject.Instantiate(prefab, position, rotation);
            attack.transform.rotation = Quaternion.AngleAxis(-90 + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);
            attack.GetComponent<AttackMove>().tagHit = user.GetComponent<Entity>().targetTag;
            attack.GetComponent<AttackMove>().Initialize(position, attack.transform.rotation, user);
            attack.GetComponent<AttackMove>().BeginAttack(target, direction);
        }
    }
}

public class BrassGun: BlueGun
{
    public BrassGun()
    {
        name = "brass_gun";
        // numBullets = 5;
        // spread = 45f;
        prefab = Resources.Load("Prefabs/golden_bullet") as GameObject;
        Debug.Log(prefab);
    }

    public BrassGun(int numBullets, float spread)
    {
        name = "brass_gun";
        this.numBullets = numBullets;
        this.spread = spread;
        prefab = Resources.Load("Prefabs/golden_bullet") as GameObject;
        Debug.Log(prefab);
    }
}

public class PillSummoner : AttackSummoner
{
    public override float delay { get {return 1f;} set {} }
    public override GameObject prefab { get; set; }
    public PillSummoner()
    {
        name = "capsule";
        prefab = Resources.Load("Prefabs/pill") as GameObject;
        Debug.Log(prefab);
    }
}

public class SoulSummoner : AttackSummoner
{
    public override float delay { get {return 1f;} set {} }
    public override GameObject prefab { get; set; }
    public SoulSummoner()
    {
        name = "Soul_Summoner";
        prefab = Resources.Load("Prefabs/soul_bullet") as GameObject;
        Debug.Log(prefab);
    }
}