using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : Character
{
    public TextMeshProUGUI healthText;

    private int _lavaLayer;
    private bool _isPlayerInvincible = false;


    void Awake()
    {
        _healthStat = new HealthStat(100);
        _lavaLayer = LayerMask.NameToLayer("Lava");
    }

    void Update() {
        healthText.text = string.Format("health: {0}", _healthStat.getHealth());
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

    public void TakeDamage(int damage){
        if(!_isPlayerInvincible){
            _healthStat.TakeDamage(damage);
        }

        if(_healthStat.isDead()){
            this.Die();
        }
    }
}