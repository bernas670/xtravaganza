using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : PowerUp
{   
    Player playerScript;
    private int _healthBonus = 20;
    void Start(){
        Debug.Log("HEALTH UP AWAKE");
        playerScript = player.GetComponent<Player>();
    }
    
    // Initialize the powerup
    protected override void powerupPayload(){
        playerScript.getHealthStat().TakePowerUp(_healthBonus);
        powerupExpire();
    }   

    // Remove the powerup  payload
    protected override void powerupExpire(){
        destroyPowerup();
    }

}
