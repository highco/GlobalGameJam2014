using UnityEngine;
using System.Collections;

public class CharacterShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform turret;
    public Transform bulletSpawnPoint;
    public float reloadTime = 1f;
    private int _index;
    private Transform _transform;
    private float _reloadingTimer;

    void Awake()
    {
        _index = GetComponent<Player>().playerIndex;
        _transform = this.transform;
    }
    
    public void Shoot(float horizontal, float vertical, PlayerType type)
    {
        _reloadingTimer += Time.deltaTime;

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

    void Shoot(PlayerType type)
    {
        _reloadingTimer = 0f;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity) as GameObject;
        bullet.rigidbody.AddForce(turret.transform.forward * 1000f);
        bullet.transform.parent = GameObject.FindWithTag("DynamicObjects").transform;
        bullet.GetComponent<Bullet>().Type = type;
        Destroy(bullet, 2f);
    }
}
