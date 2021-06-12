using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigController : MonoBehaviour
{
    private RigBuilder _rigBuilder;
    private TwoBoneIKConstraint _rightHand;
    private TwoBoneIKConstraint _leftHand;
    private Rig _aimRig;

    void Start()
    {
        Transform alien = transform.Find("Alien");
        Transform rigLayer = alien.Find("RigLayer");
        _rigBuilder = alien.GetComponent<RigBuilder>();
        _rightHand = rigLayer.Find("RightHandIK").GetComponent<TwoBoneIKConstraint>();
        _leftHand = rigLayer.Find("LeftHandIK").GetComponent<TwoBoneIKConstraint>();
        _aimRig = alien.Find("AimRig").GetComponent<Rig>();
    }

    //Update weapon references
    public void updateRigWeaponReference(Transform righHandRef, Transform leftHandRef)
    {
        _rightHand.data.target = righHandRef;
        _leftHand.data.target = leftHandRef;
        _rightHand.weight = 1;
        _leftHand.weight = 1;
        _rigBuilder.Build();
    }

    // detach the weapon from player;
    public void clearRigWeaponReference()
    {
        _rightHand.weight = 0;
        _leftHand.weight = 0;
        _rightHand.data.target = null;
        _leftHand.data.target = null;
    }

    public void setRigWeight(string rig, float weight)
    {
        if (rig == "righHand" && _rightHand.data.target != null)
        {
            _rightHand.weight = weight;
        }
        else if (rig == "leftHand" && _leftHand.data.target != null)
        {
            _leftHand.weight = weight;
        }
        else if (rig == "aimRig")
        {
            _aimRig.weight = weight;
        }
    }
}
