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


    
}
