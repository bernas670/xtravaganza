using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AmmoStat
{
    [SerializeField] private int _value;

    [SerializeField] private List<int> _modifiers = new List<int>();

    public int getValue(){
        return _value;
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
