// unity script for enemy class
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// import File
using System.IO;

public class Spider2: Enemy
{
    public override void OnStart(){

        loadStatsFromJson("spider2_config");

    }
    public override void ChooseAttack()
    {
        Coroutine attackCoroutine = StartCoroutine(ChargeAttack(shakeTime));
        // resume movement after attack
        StartCoroutine(waitAndResumeMovement(attackCoroutine));
    }
}