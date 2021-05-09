using UnityEngine;
public abstract class FireWeapon : Weapon
{
    protected AmmoStat _ammo = new AmmoStat(); /* nr de bullets + modifiers para saber qts reloads ? */
    protected int _initialAmmo;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;


    public void reload(){
        // Verify if has bullets to reload and wepon is not full
        if(_ammo.getReloadValue()>0 && _initialAmmo > _ammo.getClipValue()){
            int ammountToReload = _initialAmmo - _ammo.getClipValue();
            if (_ammo.getReloadValue() >= ammountToReload){
                _ammo.setClipValue(_ammo.getClipValue() + ammountToReload);
                _ammo.setReloadValue(_ammo.getReloadValue() - ammountToReload);
            }else{
                _ammo.setClipValue(_ammo.getClipValue() + _ammo.getReloadValue());
                _ammo.setReloadValue(0);
            }
        }

        Debug.Log("After reload clip: " + _ammo.getClipValue());
    }

    public void decreaseAmmo(){
        Debug.Log("Current clip: " + _ammo.getClipValue());
        _ammo.setClipValue(_ammo.getClipValue() - 1);
        Debug.Log("After shoot clip: " + _ammo.getClipValue());

    }

    public void setAmmoValue(int value){
        _ammo.setReloadValue(value);
    }
    public void setClipValue(int value){
        _ammo.setClipValue(value);
    }

    public int getClipValue(){
        return _ammo.getClipValue();
    }

    public override void shoot(Shooter controller)
    {
        if (Time.time >= _timeToFire)
        {
            if(controller.name == "Player"){
                muzzleFlash.Play();
                //  This should be outside the if statement. But since we only have 1 weapon enemy is decreasing the ammo aswell;
                decreaseAmmo();
            }

            setTimeToFire(Time.time + 1f / _fireRate);

            RaycastHit hit;
            if (Physics.Raycast(controller.getPoV().position, controller.getPoV().forward, out hit, _range))
            {
                if (hit.transform.name == "Player") /* controller = enemy */
                {
                    //Debug.Log(controller.gameObject.name + " attacked player");
                    Player player = hit.transform.gameObject.GetComponent<Player>();
                    player.TakeDamage(_damage);
                }
                else if (hit.transform.name == "Enemy") /* controller = player || enemy */
                {
                    Debug.Log(controller.gameObject.name + " attacked enemy");
                    Enemy enemy = hit.transform.gameObject.GetComponent<Enemy>();
                    enemy.TakeDamage(_damage);
                }

                GameObject temObj = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(temObj, 2f);
            }
        }
    }

}