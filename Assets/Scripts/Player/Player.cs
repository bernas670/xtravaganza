using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Player : Character
{
    public HealthBar healthBar;
    private int _lavaLayer;
    private bool _isPlayerInvincible = false;

    private int totalScientists;
    private int pointsToEvil;
    public GameObject gotHitScreen;



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
        // since it is called every frame instead of only when the event occurs
        healthBar.SetHealth(_healthStat.getHealth());
        if(gotHitScreen.GetComponent<Image>().color.a > 0){
            var color = gotHitScreen.GetComponent<Image>().color;
            color.a -=0.05f;
            gotHitScreen.GetComponent<Image>().color = color;
        }else {
            gotHitScreen.SetActive(false);
        }
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
            gotHitFeedback();
        }

        if(_healthStat.isDead()){
            this.Die();
        }
    }

    private void gotHitFeedback(){
        gotHitScreen.SetActive(true);
        var color = gotHitScreen.GetComponent<Image>().color;
        color.a = 0.3f;
        gotHitScreen.GetComponent<Image>().color = color;

    }

    public void becomeEvil(){
        pointsToEvil--;

        if(pointsToEvil == 0) {
            //muda de cor
        }
        Debug.Log("points to evil:" + pointsToEvil);
    }
}