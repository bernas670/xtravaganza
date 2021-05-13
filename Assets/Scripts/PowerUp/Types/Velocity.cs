using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity : PowerUp
{       
    private float _velocityIncrement = 1f;
    private float _playerVelocity;
    void Awake(){
    }
    // Initialize the powerup
    protected override void powerupPayload(){
        hidePowerup();
    }

    // Remove the powerup  payload
    protected override void powerupExpire(){

    }

}
