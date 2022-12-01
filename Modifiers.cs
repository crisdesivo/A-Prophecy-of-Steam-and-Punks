using System.Collections;
using System.Collections.Generic;

public class Modifiers
{
    private Dictionary<string, float> modifiers = new Dictionary<string, float>();
    public float this[string key]
    {
        get
        {
            if (modifiers.ContainsKey(key))
            {
                return modifiers[key];
            }
            else
            {
                return 0;
            }
        }
        set
        {
            modifiers[key] = value;
        }
    }
}