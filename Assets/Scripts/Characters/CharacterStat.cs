
using UnityEngine;

/* This class is responsible for keeping track of all stats of a character (health, damage, ..).
Goal is to derive from it and change methods in order to create game logic for all characters */
public class CharacterStat : MonoBehaviour
{
    [SerializeField] protected Stat damage; /* Inflicting damage */
    protected int _health;

    public virtual void Start(){}
    
    public void TakeDamage(int damage) {
        _health -= damage;

        if(_health < 0)
            Die();
    }

    public virtual void Die(){
        Debug.Log("Dieded");
    }
}
