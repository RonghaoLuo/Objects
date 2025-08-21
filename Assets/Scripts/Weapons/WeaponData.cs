using UnityEngine;

[CreateAssetMenu(menuName = "New Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public string targetType;
    public int damage;
    public float bulletSpeed;
    public AudioClip fireAudio;
    public float fireAudioVolume;
    public float fireAudioPitch;

    [SerializeField] private Bullet _bulletReference;

    //[SerializeField] private Transform shootOrigin;

    public void ShootWeapon(Transform weaponTip)
    {
        Bullet clonedBullet = Instantiate(_bulletReference, weaponTip.position, weaponTip.rotation);
        clonedBullet.speed = bulletSpeed;
        clonedBullet.damage = damage;
        clonedBullet.targetType = targetType;
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
