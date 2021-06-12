using UnityEngine;

public class Scientist : Character
{   
    void Awake()
    {
        _healthStat = new HealthStat(25);
    }

    public override void Die()
    {   
        if(isDead()) return;
        ScientistController controller = gameObject.GetComponent<ScientistController>();
        controller.communicateDeath();

        Destroy(gameObject, 3);

        setIsDead();
    }
}