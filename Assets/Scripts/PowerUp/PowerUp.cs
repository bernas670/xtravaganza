using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{   
    protected GameObject player;

    void Awake(){
        Debug.Log("POWER UP AWAKE");
        player = GameObject.Find("Player");
    }

    // When player collides with powerup, this must be hidden
    protected void hidePowerup(){
        gameObject.SetActive(false);
    }

    protected void destroyPowerup(){
        Destroy(gameObject, 0);
    }
    void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.tag == "Player"){
           powerupPayload();
       }
    }

    // Initialize the powerup
    protected abstract void powerupPayload();

    // Remove the powerup  payload
    protected abstract void powerupExpire();

}
