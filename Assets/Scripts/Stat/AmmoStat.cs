using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AmmoStat
{   

    // Extra bullets that can be used for reload;
    [SerializeField] private int _reloadValue;
    // Bullets in the firearm ready to shoot;

    [SerializeField] private int _clipValue;


    [SerializeField] private List<int> _modifiers = new List<int>();

    public int getReloadValue(){
        return _reloadValue;
    }

    public int getClipValue(){
        return _clipValue ;
    }
    public void setReloadValue(int value){
        _reloadValue = value;
    }

    public void setClipValue(int value){
        _clipValue = value;
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
