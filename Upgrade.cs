// use collections
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Upgrade
{
    public string name;
    public string id;
    public string type;
    public string description;
    public string effect;
    public string[] requirements;
    public bool repeatable;

    public AttackSummoner GetAttackSummoner()
    {
        if (type == "weapon")
        {
            if(id == "boomerang"){
                return new BoomerangSummoner();
            }
            else if(id == "blue_gun"){
                return new BlueGun();
            }
            else if(id == "bullet"){
                return new BulletSummoner();
            }
            else if(id == "fire_inferno"){
                return new FireInfernoSummoner();
            }
            else if(id == "sword"){
                return new SwordSummoner();
            }
            else if(id == "shockwave"){
                return new ShockwaveSummoner();
            }
            else if(id == "capsule"){
                return new PillSummoner();
            }
            else{
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public void ApplyStats(Player player){
        if (type == "stats")
        {
            List<string> effectParts = new List<string>(effect.Split(','));
            Debug.Log(effectParts);
            switch (effectParts[0])
            {
                case "maxHealth":
                    float lifeIncrease = player.maxLife*(float.Parse(effectParts[1]));
                    player.maxLife += lifeIncrease;
                    player.life += lifeIncrease;
                    player.heart.GetComponent<Heart>().UpdateHP(player.life, player.maxLife);
                    // player.modifiers["maxHealth"] += float.Parse(effectParts[1]);
                    break;
                case "shield":
                    player.shield += 1;
                    break;
                case "speed":
                    player.GetComponent<Player>().baseAgility *= (1+float.Parse(effectParts[1]));
                    break;
                default:
                    break;
            }
       

        }
    }
}