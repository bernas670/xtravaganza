using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Player : Character
{
    public HealthBar healthBar;
    public Animator _animator;
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
        _healthStat = new HealthStat(100);
        _lavaLayer = LayerMask.NameToLayer("Lava");
        _totalScientists = GameObject.FindGameObjectsWithTag("Scientist").Length;
        _pointsToEvil = _totalScientists / 2; // 50%

        _mainCam = Camera.main;
        _deathCam = transform.Find("DeathCamera").GetComponent<Camera>();
        _rig = GetComponentsInChildren<RigController>()[0];
        _gunContainer = transform.Find("Main Camera").Find("GunContainer").gameObject;
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
            //muda de cor
        }
        Debug.Log("points to evil:" + _pointsToEvil);
    }
}