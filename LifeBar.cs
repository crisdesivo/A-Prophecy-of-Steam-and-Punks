using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    public GameObject baseBar;
    public GameObject topBar;
    
    public void UpdateLifeBar(float currentLife, float maxLife)
    {
        float ratio = currentLife / maxLife;
        topBar.transform.localScale = new Vector3(ratio, 1, 1);
    }
}
