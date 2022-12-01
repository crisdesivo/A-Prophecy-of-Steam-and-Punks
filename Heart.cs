using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public float percentage = 0.5f;
    public GameObject heart;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void UpdateHP(float hp, float maxHP){
        percentage = hp/maxHP;
        heart.GetComponent<Renderer>().material.SetFloat("_Percentage", Mathf.Pow(percentage, 2));
        // if hp <= 0 hide heart, else show heart
        if (hp <= 0){
            heart.SetActive(false);
        } else {
            heart.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
