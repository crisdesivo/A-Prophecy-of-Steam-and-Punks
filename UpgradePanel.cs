using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// use textmesh pro
using TMPro;

public class UpgradePanel : MonoBehaviour
{
    public GameObject upgradeName;
    public GameObject upgradeDescription;
    public GameObject upgradeImage;
    public GameObject upgradeType;
    public Upgrade upgrade;

    // Start is called before the first frame update
    void Start()
    {
        // find the gameobjects from the grandchildren
        foreach (Transform child in transform)
        {
            foreach (Transform grandchild in child)
            {
                if (grandchild.name == "Upgrade Name")
                {
                    upgradeName = grandchild.gameObject;
                }
                else if (grandchild.name == "Upgrade Description")
                {
                    upgradeDescription = grandchild.gameObject;
                }
                else if (grandchild.name == "Image")
                {
                    upgradeImage = grandchild.gameObject;
                }
                else if (grandchild.name == "Upgrade Type")
                {
                    upgradeType = grandchild.gameObject;
                }
            }
        }
        
    }

    public void Initialize(Upgrade upgrade)
    {
        this.upgrade = upgrade;
        upgradeName.GetComponent<TextMeshProUGUI>().text = upgrade.name;
        upgradeDescription.GetComponent<TextMeshProUGUI>().text = upgrade.description;
        // set Image component source image
        upgradeImage.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(upgrade.id+"_icon");
        upgradeType.GetComponent<TextMeshProUGUI>().text = upgrade.type;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // on pressed
    public void OnPressed()
    {
        // // upgrade the player
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.GetUpgrade(upgrade);
        // player.Upgrade(upgrade);
        // hide the upgrade menu
        GameObject.Find("Upgrade Menu").SetActive(false);
        Debug.Log("Upgrade " + upgrade.name + " selected");
        // find the Controller and unpause the game
        GameObject.Find("Controller").GetComponent<BattleController>().Unpause();
    }
}
