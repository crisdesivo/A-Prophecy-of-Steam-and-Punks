using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// use textmesh pro
using TMPro;
using System;

public class PlayerTutorial : Player
{
    public override void Start()
    {
        expToNextLevel = 1000;
        attackSummoners = new ArrayList();
        UpgradeMenu.LoadUpgrades();
        // find blue_gun upgrade
        foreach (Upgrade upgrade in UpgradeMenu.upgrades)
        {
            if (upgrade.id == "blue_gun")
            {
                GetUpgrade(upgrade);
                break;
            }
        }
        life = 0.1f;
        heart.GetComponent<Heart>().UpdateHP(life, maxLife);
        // AttackSummoner blueGun = new BlueGun();
        // attackSummoners.Add(blueGun);
        // currentAttackSummoner = (AttackSummoner)attackSummoners[0];        
    }

    public override void Die()
    {
        // die
        Debug.Log("Player died");
        // add transparency to player sprite
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
    }

}
