using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.SceneManagement;


public class Player : Character
{
    public HealthBar healthBar;
    private int _lavaLayer;
    private bool _isPlayerInvincible = false;

    private int totalScientists;
    private int pointsToEvil;

    public Scene level1;
    public Scene level2;


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


        //test
        if(Input.GetKeyDown(KeyCode.L)){
        
                string pathToScene = SceneUtility.GetScenePathByBuildIndex(1);
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(pathToScene);
                Debug.Log("LgCoreReloader: Reloading to scene(0): " + sceneName);

                SceneManager.LoadScene(sceneName);
                gameObject.transform.position = new Vector3(-400, 322, 79);

                //SceneManager.MoveGameObjectToScene(gameObject, sceneName);

               // SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(1).name, LoadSceneMode.Single);     
               // Debug.Log(SceneManager.GetSceneByBuildIndex(1).buildIndex);
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