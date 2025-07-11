using UnityEngine;

public class WeaponData
{
    public string weaponName;
    public int damage;
    public Bullet bulletReference;
    [SerializeField] private Transform shootOrigin;
    public float bulletSpeed;

    public void ShootWeapon()
    {
        Bullet clonedBullet = GameObject.Instantiate(bulletReference, shootOrigin.position, shootOrigin.rotation);
        clonedBullet.speed = bulletSpeed;
        clonedBullet.damage = damage;
    }

    public WeaponData(Bullet bullet, Transform weaponTip, float bulletSpeed = 20)
    {
        weaponName = "Pistol";
        damage = 10;
        bulletReference = bullet;
        this.shootOrigin = weaponTip;
        this.bulletSpeed = bulletSpeed;
    }
}
