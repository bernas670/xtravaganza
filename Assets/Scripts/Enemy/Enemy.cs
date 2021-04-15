using UnityEngine;

/* Acho que ate fazia sentido ter uma class Character e meter estas coisas la dentro e Enemy e Player fazerem extend
mas nao sei se nao é tar a partir demasiado sem ser preciso... >> provavelmente ate vamos ter de fazer isso mas so para o enemy pq eles vao ser mts */
public class Enemy : MonoBehaviour
{
    private HealthStat _health;


    void Awake()
    {
        _health = new HealthStat(50);
    }

    public HealthStat getHealth()
    {
        return _health;
    }

}