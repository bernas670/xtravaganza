using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickDropController : MonoBehaviour
{
    public FireWeapon fireWeapon;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    void Awake(){
        player = GameObject.Find("Player").transform;
        fpsCam = Camera.main.transform;
        //gunContainer = gameObject.transform.parent;
        Debug.Log(gameObject.transform);
        fireWeapon = gameObject.GetComponent<FireWeapon>();
    }
    private void Start()
    {
        //Setup
        if (!equipped)
        {
            fireWeapon.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
            fireWeapon.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }

    private void Update()
    {
        if(equipped && fireWeapon.gameObject.transform.parent.name == "Enemy") return;

        //Check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();

        //Drop if equipped and "Q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
    }

    private void PickUp()
    {
        PlayerShootController player_controller = player.GetComponent<PlayerShootController>();
        player_controller.pick(fireWeapon);
        fireWeapon.setInUse();

        equipped = true;
        slotFull = true;

        //Make weapon a child of the camera and move it to default position
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //Make Rigidbody kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

        //Enable script
        fireWeapon.enabled = true;
    }

    private void Drop()
    {
        PlayerShootController player_controller = player.GetComponent<PlayerShootController>();
        player_controller.drop();
        fireWeapon.setNotInUse();

        equipped = false;

        slotFull = false;

        //Set parent to null
        transform.SetParent(null);

        //Make Rigidbody not kinematic and BoxCollider normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        //Gun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //AddForce
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        //Disable script
        fireWeapon.enabled = false;
    }
}


/* from: https://www.youtube.com/watch?v=8kKLUsn7tcg */