using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public Material badAlien;
    public HealthBar healthBar;
    public Animator _animator;
    public GameObject crosshair;
    public GameObject gotHitScreen;
    private int _lavaLayer;
    private bool _isPlayerInvincible = false;
    private int _totalScientists;
    private int _pointsToEvil;
    private Camera _mainCam;
    private Camera _deathCam;
    private RigController _rig;
    private GameObject _gunContainer;
    
    void Awake()
    {
        _lavaLayer = LayerMask.NameToLayer("Lava");
        _totalScientists = GameObject.FindGameObjectsWithTag("Scientist").Length;
        _pointsToEvil = _totalScientists / 2; // 50%

        _mainCam = Camera.main;
        _deathCam = transform.Find("DeathCamera").GetComponent<Camera>();
        _rig = GetComponentsInChildren<RigController>()[0];
        _gunContainer = transform.Find("Main Camera").Find("GunContainer").gameObject;

        _healthStat = new HealthStat(100);

        GameObject mementoManager = GameObject.Find("MementoManager");
        if (!mementoManager)
        {
            return;
        }

        SnapshotPlayer sPlayer = mementoManager.GetComponent<MementoManager>().GetSnapshot();
        if (sPlayer != null)
        {
            _healthStat = new HealthStat(sPlayer.health);
        }

    }

    private void Start()
    {
        healthBar.SetMaxHealth(_healthStat.getHealth());
    }

    void Update()
    {
        // since it is called every frame instead of only when the event occurs
        healthBar.SetHealth(_healthStat.getHealth());
        Color color = gotHitScreen.GetComponent<RawImage>().color;
        if (color.a > 0)
        {
            color.a -= 0.05f;
            gotHitScreen.GetComponent<RawImage>().color = color;
        }
        else
        {
            gotHitScreen.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == _lavaLayer)
        {
            Die();
        }
    }

    public override void Die()
    {
        Destroy(_gunContainer);
        _animator.SetBool("isDead", true);
        _mainCam.enabled = false;
        _deathCam.enabled = true;
        crosshair.SetActive(false);
        _rig.clearRigWeaponReference();
        _rig.setRigWeight("aimRig", 0);
        GetComponent<MovementController>().enabled = false;
        GetComponent<CameraController>().enabled = false;
        GetComponent<PlayerShootController>().enabled = false;
    }

    public HealthStat getHealthStat()
    {
        return _healthStat;
    }

    public void setPlayerInvincible(bool isPlayerInvincible)
    {
        _isPlayerInvincible = isPlayerInvincible;
    }

    public override void TakeDamage(int damage)
    {
        if (_healthStat.isDead())
        {
            return;
        }

        if (!_isPlayerInvincible)
        {
            _healthStat.TakeDamage(damage);
            gotHitFeedback();
        }

        if (_healthStat.isDead())
        {
            this.Die();
        }
    }

    private void gotHitFeedback()
    {
        gotHitScreen.SetActive(true);
        var color = gotHitScreen.GetComponent<RawImage>().color;
        color.a = 0.65f;
        gotHitScreen.GetComponent<RawImage>().color = color;
    }

    public void becomeEvil()
    {
        _pointsToEvil--;
        if (_pointsToEvil <= 0)
        {
            GameObject head = GameObject.Find("Cube.001");
            GameObject body = GameObject.Find("Cube.010");

            head.GetComponent<SkinnedMeshRenderer>().material = badAlien;
            body.GetComponent<SkinnedMeshRenderer>().material = badAlien;
        }
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "ElevatorFloor")
        {
            //put player in the right position
            gameObject.transform.position = new Vector3(18, 12, 145);

            //disable ability to move
            MovementController mController = gameObject.GetComponent<MovementController>();
            mController.rb.velocity = new Vector3(0, 0, 0);
            
            mController.enabled = false;

            //stop player animation
            _animator.SetFloat("zVelocity", 0.0f);
            _animator.SetFloat("xVelocity", 0.0f);

            //start cutscene
            GameObject timeline = GameObject.Find("Timeline");
            Timeline cutscene = timeline.GetComponent<Timeline>();
            _rig.clearRigWeaponReference();
            _animator.SetBool("hasRifle", false);
            _animator.SetBool("hasPistol", false);
            cutscene.playCutScene();
        }
    }

    public GameObject getGunContainer(){
        return _gunContainer;
    }

    public RigController GetRigController(){
        return _rig;
    }

}