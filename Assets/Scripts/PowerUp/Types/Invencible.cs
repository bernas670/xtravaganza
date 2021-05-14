using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invencible : PowerUp
{       
    private float _powerUpDuration = 5;  
    Player playerScript;

    private IEnumerator coroutine;
    void Start(){
        playerScript = player.GetComponent<Player>();
    }

    // Initialize the powerup
    protected override void powerupPayload(){
        hidePowerup();
        playerScript.setPlayerInvencible(true);
        coroutine = PowerUpTimer(_powerUpDuration);
        StartCoroutine(coroutine);
    }

    // Remove the powerup  payload
    protected override void powerupExpire(){
        playerScript.setPlayerInvencible(false);
        destroyPowerup();
    }

}
