using UnityEngine;

public class PickDropController : MonoBehaviour
{
    [HideInInspector]
    public FireWeapon fireWeapon;
    public Rigidbody rb;
    public BoxCollider coll;

    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    private FireWeapon[] fireWeaponsArray;


    void Awake()
    {
        fireWeapon = gameObject.GetComponent<FireWeapon>();
        GameObject[] fWeapons = GameObject.FindGameObjectsWithTag("FireWeapon");
        fireWeaponsArray = new FireWeapon[fWeapons.Length];

        int i =0;
        foreach(GameObject fw in fWeapons){
            fireWeaponsArray[i] = fw.GetComponent<FireWeapon>();
            i++;
        }

    }
    
    private void Start()
    {
        //Setup
        if (!equipped)
        {
            fireWeapon.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
            return;
        }

        fireWeapon.enabled = true;
        rb.isKinematic = true;
        coll.isTrigger = true;
        slotFull = true;
    }

    public void PickUp(Transform container)
    {
        fireWeapon.setInUse(true);

        equipped = true;
        slotFull = true;

        //Make weapon a child of the camera and move it to default position
        transform.SetParent(container);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //Make Rigidbody kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

        //Enable script
        fireWeapon.enabled = true;
        coll.enabled = false;
    }

    public void Drop(Vector3 velocity, Transform fpsCam)
    {
        fireWeapon.setInUse(false);

        equipped = false;
        slotFull = false;
        //Set parent to null
        transform.SetParent(null);

        //Make Rigidbody not kinematic and BoxCollider normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        //Gun carries momentum of player
        rb.velocity = velocity;

        //AddForce
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        //Disable script
        fireWeapon.enabled = false;
        coll.enabled = true;
    }
}

/* from: https://www.youtube.com/watch?v=8kKLUsn7tcg */