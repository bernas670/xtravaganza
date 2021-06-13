using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigController : MonoBehaviour
{
    public RigBuilder _rigBuilder;
    public TwoBoneIKConstraint rightHand;
    public TwoBoneIKConstraint leftHand;
    public Rig _aimRig;

    //Update weapon references
    public void updateRigWeaponReference(Transform righHandRef, Transform leftHandRef)
    {
        Debug.Log(rightHand);
        Debug.Log(leftHand);

        rightHand.data.target = righHandRef;
        leftHand.data.target = leftHandRef;
        rightHand.weight = 1;
        leftHand.weight = 1;
        _rigBuilder.Build();
    }

    // detach the weapon from player;
    public void clearRigWeaponReference()
    {
        rightHand.weight = 0;
        leftHand.weight = 0;
        rightHand.data.target = null;
        leftHand.data.target = null;
    }

    public void setRigWeight(string rig, float weight)
    {
        if (rig == "righHand" && rightHand.data.target != null)
        {
            rightHand.weight = weight;
        }
        else if (rig == "leftHand" && leftHand.data.target != null)
        {
            leftHand.weight = weight;
        }
        else if (rig == "aimRig")
        {
            _aimRig.weight = weight;
        }
    }
}
