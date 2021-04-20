using UnityEngine;
public abstract class FireWeapon : Weapon
{
    protected AmmoStat _ammo = new AmmoStat(); /* nr de bullets + modifiers para saber qts reloads ? */
    protected int _initialAmmo;

    public void reload(){
        // TODO 
        Debug.Log("Started reloading");

        Debug.Log("Current clip: " + _ammo.getClipValue());
        Debug.Log("Current reload: " + _ammo.getReloadValue());

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
        Debug.Log("After reload reload: " + _ammo.getReloadValue());
    }

    public void decresaseAmmo(){
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
        if (Time.time >= controller.getFireWeapon().getTimeToFire())
        {
            controller.getFireWeapon().setTimeToFire(Time.time + 1f / controller.getFireWeapon().getFireRate());
            RaycastHit hit;
            if (Physics.Raycast(controller.getPoV().position, controller.getPoV().forward, out hit, controller.getFireWeapon().getRange()))
            {
                if (hit.transform.name == "Player") /* controller = enemy */
                {
                    Debug.Log(controller.gameObject.name + " attacked player");
                    Player player = hit.transform.gameObject.GetComponent<Player>();
                    player.TakeDamage(controller.getFireWeapon().getDamage());
                }
                else if (hit.transform.name == "Enemy") /* controller = player || enemy */
                {
                    Debug.Log(controller.gameObject.name + " attacked enemy");
                    Enemy enemy = hit.transform.gameObject.GetComponent<Enemy>();
                    enemy.TakeDamage(controller.getFireWeapon().getDamage());
                }
            }
        }
    }

}