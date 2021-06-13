using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincible : PowerUp
{       
    private float _powerUpDuration = 5;  
    Player playerScript;

    private IEnumerator coroutine;
    public GameObject isInvencibleCanvas;
    void Start(){
        playerScript = player.GetComponent<Player>();
    }

    // Initialize the powerup
    protected override void powerupPayload(){
        isInvencibleCanvas.SetActive(true);
        hidePowerup();
        playerScript.setPlayerInvincible(true);
        coroutine = PowerUpTimer(_powerUpDuration);
        StartCoroutine(coroutine);
    }

    // Remove the powerup  payload
    protected override void powerupExpire(){
        isInvencibleCanvas.SetActive(false);
        playerScript.setPlayerInvincible(false);
        destroyPowerup();
    }

}
