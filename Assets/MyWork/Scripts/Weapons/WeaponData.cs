using UnityEngine;

[CreateAssetMenu(menuName = "New Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public ProjectileType currentProjectileType;
    public int damage;
    public float bulletSpeed;
    public int penetration;
    public AudioClip fireAudio;
    public float fireAudioVolume;
    public float fireAudioPitch;

    [SerializeField] private Bullet _bulletReference;
    [SerializeField] private Beam beamReference;

    //[SerializeField] private Transform shootOrigin;

    public void ShootWeapon(Transform weaponTip)
    {
        if (currentProjectileType == ProjectileType.Bullet)
        {
            Bullet clonedBullet = Instantiate(_bulletReference, weaponTip.position, weaponTip.rotation);
            clonedBullet.Initialize(weaponTip, bulletSpeed, damage, penetration);
        }
        else if (currentProjectileType == ProjectileType.Beam)
        {
            Beam clonedBeam = Instantiate(beamReference, weaponTip.position, weaponTip.rotation);
            clonedBeam.Initialize(weaponTip, damage);
        }
    }

    public enum ProjectileType
    {
        Bullet,
        Beam
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
