using UnityEngine;

public class Factory : MonoBehaviour
{
    // Reference to prefab.
    [SerializeField]
    protected internal MonoBehaviour prefab;

    public virtual MonoBehaviour GetNewInstance()
    {
        return Instantiate(prefab);
    }

    public MonoBehaviour GetObject()
    {
        return prefab;
    }

    public void SetObject(MonoBehaviour newPrefab)
    {
        prefab = newPrefab;
    }
}