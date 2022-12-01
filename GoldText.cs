using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldText : MonoBehaviour
{
    public void UpdateGold(){
        GetComponent<TextMeshProUGUI>().text = Data.gold.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateGold();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGold();
    }
}
