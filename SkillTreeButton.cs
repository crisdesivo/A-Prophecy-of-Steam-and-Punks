using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillTreeButton : MonoBehaviour
{
    public string skillName;
    public GameObject costText;
    public GameObject levelText;
    // Start is called before the first frame update
    void Start()
    {
        // find the current skill level on Data
        int level = Data.unlockedSkills.GetValueOrDefault(skillName, 0);
        int cost = (int)(10*Mathf.Pow(1.5f, level));
        // set the cost text
        costText.GetComponent<TextMeshProUGUI>().text = cost.ToString();
        // set the level text
        levelText.GetComponent<TextMeshProUGUI>().text = level.ToString();
    }

    public void Buy(){
        // find the current skill level on Data
        int level = Data.unlockedSkills.GetValueOrDefault(skillName, 0);
        int cost = (int)(10*Mathf.Pow(1.5f, level));
        // if player has enough gold
        if (Data.gold >= cost){
            // subtract the cost from the gold
            Data.gold -= cost;
            // increase the skill level
            Data.unlockedSkills[skillName] = level + 1;
            // save the data
            Data.saveData();
            // update the cost text
            costText.GetComponent<TextMeshProUGUI>().text = ((int)(10*Mathf.Pow(1.5f, level+1))).ToString();
            // update the level text
            levelText.GetComponent<TextMeshProUGUI>().text = (level+1).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
