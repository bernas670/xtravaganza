using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : Character
{
    public HealthBar healthBar;
    private int _lavaLayer;
    private bool _isPlayerInvincible = false;

    private int totalScientists;
    private int pointsToEvil;


    void Awake()
    {
        _healthStat = new HealthStat(100);
        _lavaLayer = LayerMask.NameToLayer("Lava");
        totalScientists = GameObject.FindGameObjectsWithTag("Scientist").Length;
        pointsToEvil = totalScientists/2; // 50%
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

    public HealthStat getHealthStat(){
        return _healthStat;
    }
    public void setPlayerInvincible(bool isPlayerInvincible){
        _isPlayerInvincible = isPlayerInvincible;
    }

    public override void TakeDamage(int damage){
        if(!_isPlayerInvincible){
            _healthStat.TakeDamage(damage);
        }

        if(_healthStat.isDead()){
            this.Die();
        }
    }

    public void becomeEvil(){
        pointsToEvil--;

        if(pointsToEvil == 0) {
            //muda de cor
        }
        Debug.Log("points to evil:" + pointsToEvil);
    }
}