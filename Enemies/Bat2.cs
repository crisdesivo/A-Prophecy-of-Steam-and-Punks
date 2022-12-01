// unity script for enemy class
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// import File
using System.IO;

public class Bat2: Enemy
{
    public override void OnStart(){

        loadStatsFromJson("bat2_config");
        rotateOnDirection = false;

    }
    public override void ChooseAttack()
    {
        Coroutine attackCoroutine = StartCoroutine(waitAndAttack(shakeTime));
        // resume movement after attack
        StartCoroutine(waitAndResumeMovement(attackCoroutine));
    }

    public override void Attack()
    {
        Vector3 direction = (GameObject.Find("Player").transform.position - transform.position).normalized;
        new BlueGun(3, 70f).SummonAttack(transform.position, transform.rotation, gameObject, direction);
    }
}