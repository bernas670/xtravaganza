using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* System.Serializable to make it visible in inspector */
[System.Serializable]
public class Stat
{
    [SerializeField] private int _value;

    [SerializeField] private List<int> _modifiers = new List<int>();

    public int getValue(){
        return _value;
    }

    public int getValueWithModifiers(){
        int finalValue = _value;
        foreach(int v in _modifiers)
            finalValue += v;

        return finalValue;
    }

    public void setValue(int value){
        _value = value;
    }

    public void addModifier(int modifier){
        if(modifier != 0)
            _modifiers.Add(modifier);
    }

    public void removeModifier(int modifier){
        if(modifier != 0)
            _modifiers.Remove(modifier);
    }
    
}
