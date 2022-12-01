// unity script for enemy class
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// import File
using System.IO;

public class TankBoss: Enemy
{
    public GameObject bullet;
    public GameObject lifeBar;
    public GameObject turret;
    public GameObject winSong;
    public override void OnStart()
    {
        loadStatsFromJson("tank_boss_config");
        bullet = Resources.Load("Prefabs/bullet") as GameObject;
    }
    
    public override void Move(){
        // move down if player is down or up if player is up
        if (player.transform.position.y > transform.position.y)
        {
            transform.position += new Vector3(0, 1, 0) * speed * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(0, -1, 0) * speed * Time.deltaTime;
        }
    }

    public IEnumerator AimAndShoot(){
        float aimingTime = 3f;
        float aimingSpeed = 1f;

        // compute angle between turret aiming and direction to player
        Vector3 direction = (player.transform.position - turret.transform.position).normalized;
        float directionAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float currentAngle = turret.transform.rotation.eulerAngles.z - 90;
        float angle = (directionAngle - currentAngle)%360;
        if (angle < 0){
            angle += 360;
        }

        // float angle = Vector3.Angle(turret.transform.position, player.transform.position);

        Debug.Log("angle: " + angle);
        float time = 0;
        while(time < aimingTime && Mathf.Abs(angle) > 2 && Mathf.Abs(360 - angle) > 2)
        {
            time += Time.deltaTime;
            // rotate turret towards player (constant rotation, not lerping or instant)
            direction = (player.transform.position - turret.transform.position).normalized;
            directionAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            currentAngle = turret.transform.rotation.eulerAngles.z - 90;
            angle = (directionAngle - currentAngle)%360;
            if (angle < 0){
                angle += 360;
            }
            // angle = Vector3.Angle(turret.transform.position, player.transform.position);
            Debug.Log("angle: " + angle);
            // if (angle>10 || angle<-10){
            bool rotateRight = false;
            if (angle > 180)
            {
                rotateRight = true;
            }
            else
            {
                rotateRight = false;
            }
            if (rotateRight)
            {
                turret.transform.Rotate(0, 0, -aimingSpeed);
            }
            else
            {
                turret.transform.Rotate(0, 0, aimingSpeed);
            }
            // }

            

            // turret.transform.rotation = Quaternion.Lerp(turret.transform.rotation, Quaternion.LookRotation(Vector3.forward, -player.transform.position + turret.transform.position), Time.deltaTime * 2);
            yield return null;
        }
        // shoot in the direction the turret is facing
        Vector3 bulletDirection = turret.transform.rotation * Vector3.down;
        // direction = (player.transform.position - transform.position).normalized;
        new BrassGun(1, 70f).SummonAttack(transform.position, transform.rotation, gameObject, bulletDirection);
        // // shoot
        // new BlueGun(1, 0).SummonAttack(turret.transform.position, turret.transform.rotation, gameObject, turret.transform.up);
        yield return new WaitForSeconds(0.2f);
        attackFinished = true;
    }

    public override void ChooseAttack()
    {
        // randomly choose between QuickAttack, AimAndShoot, or Attack
        int attack = Random.Range(0, 3);
        if (attack == 0)
        {
            Coroutine attackCoroutine = StartCoroutine(QuickAttack());
            // resume movement after attack
            StartCoroutine(waitAndResumeMovement(attackCoroutine));
        }
        else if (attack == 1)
        {
            Coroutine attackCoroutine = StartCoroutine(AimAndShoot());
            // resume movement after attack
            StartCoroutine(waitAndResumeMovement(attackCoroutine));
        }
        else
        {
            Coroutine attackCoroutine = StartCoroutine(waitAndAttack(shakeTime));
            // resume movement after attack
            StartCoroutine(waitAndResumeMovement(attackCoroutine));
        }
    }

    public override void Attack()
    {
        GameObject attack = Instantiate(attackPrefab, transform.position, transform.rotation);
        attack.GetComponent<AttackMove>().tagHit = "Player";
        attack.layer = LayerMask.NameToLayer("Enemy Attack");

        attack.GetComponent<AttackMove>().Initialize(new Vector3(3.0f*(Random.Range(0, 2)*2-1), transform.position.y, transform.position.z + 0.5f), transform.rotation, gameObject);
        Vector2 direction = new Vector2(Mathf.Cos(Mathf.PI * 2 / 6 * 0), Mathf.Sin(Mathf.PI * 2 / 6 * 0));
        attack.GetComponent<AttackMove>().BeginAttack(player.GetComponent<Entity>(), direction);
    
        attackFinished = true;
    }

    IEnumerator RotateTurret(float rotationSpeed){
        float rotated = 0;
        // random rotation direction
        int rotationDirection = Random.Range(0, 2)*2 - 1;
        while(rotated < 360f)
        {
            turret.transform.Rotate(0, 0, rotationDirection*rotationSpeed*Time.deltaTime);
            rotated += rotationSpeed*Time.deltaTime;
            yield return 1;
        }
        turret.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public IEnumerator QuickAttack()
    {
        float rotationPerSecond = 360f;
        Coroutine rotateCoroutine = StartCoroutine(RotateTurret(rotationPerSecond));

        float shootingDelay = 0.05f;
        // compute how many bullets to shoot
        int bulletsToShoot = (int)((360 / rotationPerSecond) / shootingDelay);

        for (int i = 0; i < bulletsToShoot; i++)
        {
            // shoot
            Vector3 direction = turret.transform.rotation * Vector3.down;
            float turretLength = 1f;
            new BlueGun(1, 0, 3.34f).SummonAttack(turret.transform.position + turretLength*direction, turret.transform.rotation, gameObject, direction);
            yield return new WaitForSeconds(shootingDelay);
        }
        attackFinished = true;

        // GameObject attack = Instantiate(bullet, transform.position, transform.rotation);
        // attack.GetComponent<AttackMove>().tagHit = "Player";
        // attack.layer = LayerMask.NameToLayer("Enemy Attack");

        // attack.GetComponent<AttackMove>().Initialize(new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f), transform.rotation, gameObject);
        // attack.GetComponent<AttackMove>().BeginAttack(player.GetComponent<Entity>(), new Vector2(0, 1));
    }

    public override void OnTakeDamage(float damage)
    {
        lifeBar.GetComponent<LifeBar>().UpdateLifeBar(life, maxLife);
        if (life <= 0)
        {
            // set lifebar to inactive
            lifeBar.SetActive(false);
            winSong.GetComponent<AudioSource>().Play();
        }
    }

}
