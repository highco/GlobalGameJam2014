using UnityEngine;
using System.Collections;

public class CharacterShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform turret;
    public Transform bulletSpawnPoint;
    public float reloadTime = 1f;
    public float bulletSpeed;
    public Color color;

    private float _reloadingTimer;
        
    public void Shoot(float horizontal, float vertical, CharacterType type, float dt)
    {
        _reloadingTimer += dt;

        float xDirection = horizontal;
        float yDirection = vertical;
        if (Mathf.Abs(xDirection) > 0.01 || Mathf.Abs(yDirection) > 0.01)
            turret.transform.rotation = Quaternion.Euler(0f, Mathf.Atan2(yDirection, -xDirection) / Mathf.PI * 180f, 0f); 

        if (_reloadingTimer > reloadTime &&
            (Mathf.Abs(xDirection) > 0.01 || Mathf.Abs(yDirection) > 0.01))
        {
            Shoot(type);
        }
    }

    void Shoot(CharacterType type)
    {
        _reloadingTimer = 0f;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity) as GameObject;
        bullet.rigidbody.AddForce(turret.transform.forward * bulletSpeed);
        bullet.transform.parent = GameObject.FindWithTag("DynamicObjects").transform;
        bullet.GetComponent<Bullet>().SetTypeAndOwner(type, GetComponent<Character>());
        Destroy(bullet, 1f);
    }
}
