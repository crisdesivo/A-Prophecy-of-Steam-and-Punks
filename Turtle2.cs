using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle2 : Enemy
{
    public override void OnStart(){
        loadStatsFromJson("turtle2_config");
        rotateOnDirection = true;
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
        new BoomerangSummoner().SummonAttack(transform.position, transform.rotation, gameObject, direction);
        // new BlueGun(3, 70f).SummonAttack(transform.position, transform.rotation, gameObject, direction);
    }

}
