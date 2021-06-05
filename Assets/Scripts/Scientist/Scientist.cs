using UnityEngine;

public class Scientist : Character
{
    void Awake()
    {
        _healthStat = new HealthStat(25);
    }

    public override void Die()
    {
        ScientistController controller = gameObject.GetComponent<ScientistController>();
        controller.communicateDeath();

        Destroy(gameObject, 3);
    }
}