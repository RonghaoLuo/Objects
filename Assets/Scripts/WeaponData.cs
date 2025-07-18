using UnityEngine;

[CreateAssetMenu(menuName = "New Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public int damage;
    public Bullet bulletReference;
    //[SerializeField] private Transform shootOrigin;
    public float bulletSpeed;

    public void ShootWeapon(Transform weaponTip)
    {
        Bullet clonedBullet = GameObject.Instantiate(bulletReference, weaponTip.position, weaponTip.rotation);
        clonedBullet.speed = bulletSpeed;
        clonedBullet.damage = damage;
    }

    //public WeaponData(Bullet bullet, Transform weaponTip, float bulletSpeed = 20)
    //{
    //    weaponName = "Pistol";
    //    damage = 10;
    //    bulletReference = bullet;
    //    this.shootOrigin = weaponTip;
    //    this.bulletSpeed = bulletSpeed;
    //}
}
