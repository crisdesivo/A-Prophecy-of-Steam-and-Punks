using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public static Upgrade[] upgrades;
    public GameObject upgradePanel1;
    public GameObject upgradePanel2;

    public static void LoadUpgrades()
    {
        TextAsset upgradesText = Resources.Load("upgrades") as TextAsset;
        // json: {Items: [{...}, {...}, {...}]}
        // load the json Items array into upgrades
        upgrades = JsonHelper.FromJson<Upgrade>(upgradesText.text);
    }
    public class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // load upgrades if not loaded
        if (upgrades == null)
        {
            Debug.Log("Loading upgrades");
            LoadUpgrades();
        }

        // RandomSelect();
        
    }

    public void RandomSelect()
    {
        // find player node and get its current upgrades
        GameObject player = GameObject.Find("Player");
        Player playerScript = player.GetComponent<Player>();
        // get the string ids of the current upgrades
        List<string> currentUpgrades = new List<string>();
        foreach (Upgrade upgrade in playerScript.upgrades)
        {
            currentUpgrades.Add(upgrade.id);
        }

        List<Upgrade> availableUpgrades = new List<Upgrade>();

        // find all upgrades that are not in the current upgrades
        foreach (Upgrade upgrade in upgrades)
        {
            if (!currentUpgrades.Contains(upgrade.id) || upgrade.repeatable)
            {
                // Debug.Log("Adding upgrade " + upgrade.id);
                // Debug.Log("Repeatable: " + upgrade.repeatable);
                availableUpgrades.Add(upgrade);
            }
        }

        // sample two random upgrades without replacement
        int[] indices = new int[2];
        indices[0] = Random.Range(0, availableUpgrades.Count);
        indices[1] = Random.Range(0, availableUpgrades.Count-1);
        if (indices[1] >= indices[0])
        {
            indices[1]++;
        }
        Upgrade upgrade1 = availableUpgrades[indices[0]];
        Upgrade upgrade2 = availableUpgrades[indices[1]];

        // initialize the upgrade panels
        upgradePanel1.GetComponent<UpgradePanel>().Initialize(upgrade1);
        upgradePanel2.GetComponent<UpgradePanel>().Initialize(upgrade2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void RandomSelectWeapons()
    {
        // load upgrades if not loaded
        if (upgrades == null)
        {
            Debug.Log("Loading upgrades");
            LoadUpgrades();
        }
        // find player node and get its current upgrades
        GameObject player = GameObject.Find("Player");
        Player playerScript = player.GetComponent<Player>();
        // get the string ids of the current upgrades
        List<string> currentUpgrades = new List<string>();
        foreach (Upgrade upgrade in playerScript.upgrades)
        {
            currentUpgrades.Add(upgrade.id);
        }

        List<Upgrade> availableUpgrades = new List<Upgrade>();

        // find all upgrades that are not in the current upgrades
        foreach (Upgrade upgrade in upgrades)
        {
            if (upgrade.type == "weapon" && (!currentUpgrades.Contains(upgrade.id) || upgrade.repeatable))
            {
                // Debug.Log("Adding upgrade " + upgrade.id);
                // Debug.Log("Repeatable: " + upgrade.repeatable);
                availableUpgrades.Add(upgrade);
            }
        }

        // sample two random upgrades without replacement
        int[] indices = new int[2];
        indices[0] = Random.Range(0, availableUpgrades.Count);
        indices[1] = Random.Range(0, availableUpgrades.Count-1);
        if (indices[1] >= indices[0])
        {
            indices[1]++;
        }
        Upgrade upgrade1 = availableUpgrades[indices[0]];
        Upgrade upgrade2 = availableUpgrades[indices[1]];

        // initialize the upgrade panels
        upgradePanel1.GetComponent<UpgradePanel>().Initialize(upgrade1);
        upgradePanel2.GetComponent<UpgradePanel>().Initialize(upgrade2);
    
    }
}
