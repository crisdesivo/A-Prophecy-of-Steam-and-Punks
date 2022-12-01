using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulClock : Enemy
{
    public override void OnStart(){

        loadStatsFromJson("soul_clock_config");

    }
    public override void ChooseAttack()
    {
        Coroutine attackCoroutine = StartCoroutine(waitAndAttack(shakeTime));
        // resume movement after attack
        StartCoroutine(waitAndResumeMovement(attackCoroutine));
    }
}
