using UnityEngine;
using System.Collections;

public class CharacterShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
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
    
    void Update()
    {
        _reloadingTimer += Time.deltaTime;

//        float xDirection = Input.GetAxis("SecondaryHorizontal" + _index);
//        float yDirection = Input.GetAxis("SecondaryVertical" + _index);

        float xDirection = Input.GetAxis("Horizontal" + _index);
        float yDirection = Input.GetAxis("Vertical" + _index);

        if (_reloadingTimer > reloadTime &&
            (xDirection > 0.01 || yDirection > 0.01))
        {
            transform.rotation = Quaternion.Euler(0f, Mathf.Atan2(-xDirection, -yDirection) / Mathf.PI * 180f, 0f); 
            Shoot();
        }
    }

    void Shoot()
    {
        _reloadingTimer = 0f;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity) as GameObject;
        bullet.AddComponent<Rigidbody>();
        bullet.rigidbody.AddForce(transform.forward);
        bullet.transform.parent = GameObject.FindWithTag("DynamicObjects").transform;
        Destroy(bullet, 2f);
    }
}
