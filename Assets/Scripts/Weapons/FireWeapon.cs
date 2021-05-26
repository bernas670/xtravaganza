using UnityEngine;

public abstract class FireWeapon : Weapon
{
    protected AmmoStat _ammo = new AmmoStat(); /* nr de bullets + modifiers para saber qts reloads ? */
    protected int _initialAmmo;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public bool inUse;
    public bool isEquipped;
    public void reload()
    {
        // Verify if has bullets to reload and wepon is not full
        if (_ammo.getReloadValue() > 0 && _initialAmmo > _ammo.getClipValue())
        {
            int ammountToReload = _initialAmmo - _ammo.getClipValue();
            if (_ammo.getReloadValue() >= ammountToReload)
            {
                _ammo.setClipValue(_ammo.getClipValue() + ammountToReload);
                _ammo.setReloadValue(_ammo.getReloadValue() - ammountToReload);
            }
            else
            {
                _ammo.setClipValue(_ammo.getClipValue() + _ammo.getReloadValue());
                _ammo.setReloadValue(0);
            }
        }
    }

    public void decreaseAmmo()
    {
        _ammo.setClipValue(_ammo.getClipValue() - 1);
    }

    public void setAmmoValue(int value)
    {
        _ammo.setReloadValue(value);
    }

    public void setClipValue(int value)
    {
        _ammo.setClipValue(value);
    }

    public int getClipValue()
    {
        return _ammo.getClipValue();
    }

    public int getReloadValue()
    {
        return _ammo.getReloadValue();
    }

    public override void shoot(Shooter controller)
    {
        if (Time.time >= _timeToFire)
        {
            //if (controller.name == "Player")
            //{
                muzzleFlash.Play();
            //}

            //  This should be outside the if statement. But since we only have 1 weapon enemy is decreasing the ammo aswell;
            decreaseAmmo();
            setTimeToFire(Time.time + 1f / _fireRate);

            RaycastHit hit;
            if (Physics.Raycast(controller.getPoV().position, controller.getPoV().forward, out hit, _range))
            {
                if (hit.transform.name == "Player") /* controller = enemy */
                {
                    Debug.Log(controller.gameObject.name + " attacked player");
                    Player player = hit.transform.gameObject.GetComponent<Player>();
                    player.TakeDamage(_damage);
                }
                else if (hit.transform.name == "Enemy") /* controller = player || enemy */
                {
                    Debug.Log(controller.gameObject.name + " attacked enemy");
                    Enemy enemy = hit.transform.gameObject.GetComponent<Enemy>();
                    enemy.TakeDamage(_damage);

                    GameObject temObj = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    temObj.transform.parent = enemy.transform;
                    Destroy(temObj, 2f);
                }
                else
                {
                    GameObject temObj = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(temObj, 2f);
                }
            }
        }
    }

    public bool isInUse(){
        return inUse;
    }
    public void setInUse(bool value)
    {
        inUse = value;
    }

    public void SetIsEquipped(bool value){
        isEquipped = value;
    }
}