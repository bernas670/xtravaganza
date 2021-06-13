using UnityEngine;

public class MeleeAnimationController : MonoBehaviour
{
    public PlayerShootController controller;

    void MeleeKick()
    {
        controller.Kick();
    }

    public void MeleeEnd()
    {
        controller.EndMeleeAnimation();
    }
}
