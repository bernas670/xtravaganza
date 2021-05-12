using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : Character
{
    public HealthBar healthBar;

    private int _lavaLayer;

    void Awake()
    {
        _healthStat = new HealthStat(100);
        _lavaLayer = LayerMask.NameToLayer("Lava");
    }

    private void Start() {
        healthBar.SetMaxHealth(_healthStat.getHealth());
    }

    void Update() {
        // FIXME: this does not seem to be the best way to update the GUI, 
        // since it is called every frame instead of only when the event occurs
        healthBar.SetHealth(_healthStat.getHealth());
    }    
 
    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.layer == _lavaLayer){
            Die();
        }
    }

    public override void Die(){
        Debug.Log("PLAYER DEAD");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}