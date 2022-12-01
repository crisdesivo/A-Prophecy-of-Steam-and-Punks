using UnityEngine;

public class AttackFactory : Factory
{

    override public MonoBehaviour GetNewInstance()
    {
        return Instantiate(prefab);
    }
    

}