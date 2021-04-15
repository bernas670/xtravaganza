using UnityEngine;

/* Acho que ate fazia sentido ter uma class Character e meter estas coisas la dentro e Enemy e Player fazerem extend
mas nao sei se nao é tar a partir demasiado sem ser preciso... */
public class Player : MonoBehaviour
{
    private HealthStat _health;
    void Awake()
    {
        _health = new HealthStat(100);
    }

    public HealthStat getHealth(){
        return _health;
    }

}