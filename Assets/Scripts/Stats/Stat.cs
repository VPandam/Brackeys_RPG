using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{

    [SerializeField]
    int valueBase;

    [SerializeField]
    List<int> modifiersList = new List<int>();
    public int GetValue()
    {
        int finalValue = valueBase;
        modifiersList.ForEach(x => finalValue += x);
        if (finalValue == 0)
            finalValue = 1;
        return finalValue;
    }

    public void AddModifier(int modifier)
    {

        if (modifier != 0)
        {
            modifiersList.Add(modifier);
        }
    }
    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
            modifiersList.Remove(modifier);
    }

}
